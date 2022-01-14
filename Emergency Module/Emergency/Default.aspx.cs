using GS.Common;
using GS.Data;
using Emergency.Classes;
using Newtonsoft.Json;
using RegHis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Emergency
{
    public partial class _Default : Page
    {


        string mac1 = "";
        string mac2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
              
               
                hdn_dt.Value = DateTime.Now.ToShortDateString();
                clearCookies();
                txtUserName.Focus();
               
                txtCompName.Text = getDefaultConnection.Gs_GetCompName();
                CompHead.InnerHtml = getDefaultConnection.Gs_GetCompHead();
            }
        }


        private void clearCookies()
        {
            string[] myCookies = Request.Cookies.AllKeys;
            foreach (string cookie in myCookies)
            {
                if(cookie!= "ucode" && cookie!= "pwda")
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            clearCookies();
            ViewState["LoginDt"] = null;
            Response.Cookies["dbName"].Value = txtCompName.Text.Trim();
            Response.Cookies["dbName"].Expires = DateTime.Now.AddHours(8);
            bool regchk = RegistrationCheck();
            if (regchk)
            {
                if (ValidateUser(txtUserName.Text.Trim(), txtUserPass.Text, false))
                {
                    LoginSucess();
                }
                else
                {
                    Response.Cookies.Clear();
                }
            }
            else
            {
                Response.Cookies.Clear();
            }

        }

        private void LoginSucess()
        {
            if (checkmenuaccess())
            {
                patientduebalsms();
                #region Update dashbord cache file at login time

                string userId = Common.Decrypt(HttpContext.Current.Request.Cookies["ERuserID"].Value.ToString(), true);
                string compId = Common.Decrypt(HttpContext.Current.Request.Cookies["ERcompID"].Value.ToString(), true);
                string fileName = "DashboardCache_" + compId + "_" + userId + ".txt";

                string filePath = HttpContext.Current.Server.MapPath("~/UserCache/" + fileName);

                if (File.Exists(filePath))
                {
                    File.WriteAllText(filePath, DateTime.Now.ToString());
                }
                #endregion
                SaveUserRegDetail();
                Response.Cookies["ERCache"].Value = "N";
                Response.Cookies["ERCache"].Expires = DateTime.Now.AddHours(8);
                Response.Redirect("~/View/Dashboard/Dashboard.aspx");

            }
            else
            {
                clearCookies();
                ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "Alert('"+ResponseMessage.NoMenuAccess+"', true)", true);
            }
        }

        public void GS_logindet()
        {
            DataManager dm = new DataManager();
            List<ViewParam> _lstview = new List<ViewParam>();
            DataSet ds = new DataSet();
            _lstview.Add(new ViewParam() { Name = "@flag", Value = "I" });
            _lstview.Add(new ViewParam() { Name = "@frmUsrID", Value = Common.Decrypt(Request.Cookies["ERuserID"].Value, true) });
            _lstview.Add(new ViewParam() { Value = CheckIPAddr(), Name = "@MachineId" });
            ds = dm.View.GetDataSet("USP_h_alert", _lstview);

        }


        //check user ip address 
        protected string CheckIPAddr()
        {
            string REMOTE_ADDR = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(REMOTE_ADDR))
            {
                string[] addresses = REMOTE_ADDR.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return Request.ServerVariables["REMOTE_ADDR"];
        }

        //usr log detail check
        public DataTable UserLogChecked(string UserID)
        {
            DataManager dm = new DataManager();
            DataSet ds = new DataSet();
            DataTable dtUser;
            List<ViewParam> _lstview = new List<ViewParam>();
            _lstview.Add(new ViewParam() { Name = "@usrid", Value = UserID.ToString() });
            _lstview.Add(new ViewParam() { Value = CheckIPAddr(), Name = "@macid" });
            ds = dm.View.GetDataSet("LoginTime", _lstview);
            dtUser = ds.Tables[1];
            if (dtUser.Rows.Count > 0)
            {
                return dtUser;
            }
            else
            {
                return dtUser = new DataTable();
            }
        }

        //log manage for static ip to login and local bio process
        protected void UserLogManage(string usrid, string SLflag)
        {
            string cn = getDefaultConnection.Gs_GetConnectionString();
            using (SqlConnection con = new SqlConnection(cn))
            {

                string ipaddress = CheckIPAddr();

                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "USP_usrlog_withaddress_save";
                cmd.Parameters.AddWithValue("@flag", "S");
                cmd.Parameters.AddWithValue("@userid", usrid);
                cmd.Parameters.AddWithValue("@indate", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@SLflag", SLflag);
                cmd.Parameters.AddWithValue("@ipaddress", ipaddress);
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(ds);
            }
        }

        protected bool CheckBackDateEntryLogin(Int32 UsrID)
        {
            XDocument xDoc = XDocument.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
            string cn = Common.Decrypt(xDoc.Descendants(getDefaultConnection.Gs_GetCompName()).FirstOrDefault().Value, true);

            using (SqlConnection con = new SqlConnection(cn))
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_chackentry";
                cmd.Parameters.AddWithValue("@usrid", UsrID);
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(ds);
                Response.Cookies["ERBackdateflg"].Value = Common.Encrypt(ds.Tables[0].Rows[0][0].ToString(), true);
                hdn_backDateFlag.Value = ds.Tables[0].Rows[0][0].ToString();
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }


        protected void CheckBackDateEntry()
        {
            XDocument xDoc = XDocument.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
            string cn = Common.Decrypt(xDoc.Descendants(getDefaultConnection.Gs_GetCompName()).FirstOrDefault().Value, true);

            using (SqlConnection con = new SqlConnection(cn))
            {

                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_chackentry";
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "Alert('" + ResponseMessage.backDate+"', true)", true);
                    }
                }
            }
        }

        private bool RegistrationCheck()
        {
            Boolean AuthCheck = false;
            string cn = getDefaultConnection.Gs_GetConnectionString();
            Class1 reghis = new Class1();
            AuthCheck = reghis.getRegChk(cn.Trim());
            mac1 = reghis.mac1;
            mac2 = reghis.mac2;
        CheckPassword:
            DataManager dm = new DataManager();
            DataTable dt = new DataTable();

            if ((mac1 != null && mac1 != "") || (mac2 != null && mac2 != ""))
            {
                if (mac1 != null)
                    dt = dm.View.Get("select clreg_sno,clreg_Rkey,clreg_Rto,clreg_Hname,clreg_station,clreg_comp,convert(nvarchar(10),clreg_vdt,103)clreg_vdt,clreg_Pwd from h_clreg where clreg_Rkey='" + mac1 + "'");

                if (dt != null && dt.Rows.Count == 0)
                {
                    if (mac2 != null)
                        dt = dm.View.Get("select clreg_sno,clreg_Rkey,clreg_Rto,clreg_Hname,clreg_station,clreg_comp,convert(nvarchar(10),clreg_vdt,103)clreg_vdt,clreg_Pwd from h_clreg where clreg_Rkey='" + mac2 + "'");

                }
            }

            DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));

            if (dt != null && dt.Rows.Count > 0)
            {
                if ((mac1 != null && mac1 != "") || (mac2 != null && mac2 != ""))
                {
                    int reghis1 = reghis.getitm_his(dt.Rows[0]["clreg_sno"].ToString(), dt.Rows[0]["clreg_Rkey"].ToString(), cn.Trim());
                    string vflg = reghis.clreg_Vflag;
                    DateTime Vdate = Convert.ToDateTime(reghis.g_dtval);
                    string vdt = Vdate.ToString("dd/MM/yyyy");
                    double DiffDate = (Vdate - currentdate).TotalDays;
                    if (currentdate > Vdate)
                    {
                        if (vflg == "R")
                        {
                            string newPassword = UpdateRegKey(dt.Rows[0]["clreg_Rkey"].ToString(), dt.Rows[0]["clreg_sno"].ToString(), dt.Rows[0]["clreg_Pwd"].ToString());
                            if (!string.IsNullOrEmpty(newPassword))
                                goto CheckPassword;
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "AlertRegExpiary('" + vdt + "');", true);
                            return false;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "Alert('" + ResponseMessage.DemoExpired+"', true)", true);
                            return false;
                        }
                    }
                    else
                    {
                        if (DiffDate < 15)
                        {
                            hdn_password.Value = txtUserPass.Text;
                            if (vflg == "R")
                            {
                                string newPassword = UpdateRegKey(dt.Rows[0]["clreg_Rkey"].ToString(), dt.Rows[0]["clreg_sno"].ToString(), dt.Rows[0]["clreg_Pwd"].ToString());
                                if (!string.IsNullOrEmpty(newPassword))
                                    goto CheckPassword;
                                ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "AlertRegExpiaryBefore('" + vdt + "');", true);
                                return false;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "Alert('" + ResponseMessage.DemoExpired+"', true)", true);
                                return false;
                            }
                        }
                        else
                            return true;
                    }
                }
                else
                    return false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "Alert('" + ResponseMessage.Regnotregister+"', true)", true);
                return false;
            }

        }

        public bool CheckVersion_DLL()
        {
            DataManager dm = new DataManager();
            DataTable dt = dm.View.Get("select isnull(version_DLL,'') as version_DLL from h_updationdetails");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["version_DLL"].ToString() != "")
                {
                    GS_Common _objClsCommon = new GS_Common();
                    string version_no = _objClsCommon.VersionManage_DLL();
                    if (version_no == dt.Rows[0]["version_DLL"].ToString())
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }


        protected void btn_loginNew_Click(object sender, EventArgs e)
        {
            Response.Cookies["dbName"].Value = txtCompName.Text.Trim();
            Response.Cookies["dbName"].Expires = DateTime.Now.AddHours(8);
            if (SetCookies())
            {
                Response.Cookies["ERBackdateflg"].Value = hdn_backDateFlag.Value;
                LoginSucess();
            }
        }



        private bool ValidateUser(string userName, string passWord, bool OTPAuthenticate)
        {
            // Check for invalid userName.
            // userName must not be null and must be between 1 and 5 characters.
            if ((null == userName) || (0 == userName.Length) || (userName.Length > 15))
            {
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of userName failed.");
                return false;
            }

            // Check for invalid passWord.
            // passWord must not be null and must be between 3 and 15 characters.
            if ((null == passWord) || (0 == passWord.Length) || (passWord.Length > 15))
            {
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of passWord failed.");
                return false;
            }

            try
            {
                //Check DLL Version to validate login user
                bool V_DLL = CheckVersion_DLL();
                if (V_DLL)
                {

                    //Check password throw OTP validate or not condition
                    DataTable dt;
                    if (OTPAuthenticate)
                        dt = CheckPassword(userName, passWord);
                    else
                        dt = CheckUserLoginRequest(userName, passWord);
                    //  LoginDt = dt;
                    ViewState["LoginDt"] = dt;
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() == "T")
                        {

                            bool BackDateLogin = CheckBackDateEntryLogin(Convert.ToInt32(dt.Rows[0][1].ToString()));
                            if (!BackDateLogin)
                            {
                                if (DateTime.Parse(hdn_dt.Value) < DateTime.Parse(hdn_ddt.Value) || DateTime.Parse(hdn_dt.Value) > DateTime.Parse(hdn_ddt.Value))
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "Alert('" + ResponseMessage.userBackDate+"', true)", true);
                                    return false;
                                }
                            }
                            DataTable dt2 = UserLogChecked(dt.Rows[0][1].ToString());
                            if (dt2.Rows[0][0].ToString() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "AlertConfirm('User Already login on Another system.','Do you want to continue ?')", true);
                                return false;
                            }
                            else
                            {
                                return SetCookies();

                            }

                        }   /* End On 16/12/2017 */
                        //else if (dt.Rows[0]["flag"].ToString() == "O")
                        //{
                        //    //1. For OTP validation popup show
                        //    //send OTP, hidden in manage usercode and password mantain, open popup, log table with ipaddress
                        //    var chars = "abcdefghijklmnopqrstuvwxyz123456789@ABCDEFGHIJKLMNOP123456789QRSTUVWXYZ123456789";
                        //    var random = new Random();
                        //    var result = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

                        //    UserLogManage(dt.Rows[0]["usrid"].ToString(), "S");
                        //    string otptime = "5";
                        //    if (!String.IsNullOrEmpty(dt.Rows[0]["OTPtime"].ToString()))
                        //    {
                        //        otptime = dt.Rows[0]["OTPtime"].ToString();
                        //    }
                        //    SmsOTPData(result + " is the OTP for login by static ip. OTP is valid for " + otptime + " minutes only.", dt.Rows[0]["mobileno"].ToString());
                        //    EmailOTPData(result + " is the OTP for login by static ip. OTP is valid for " + otptime + " minutes only.", dt.Rows[0]["emailid"].ToString());

                        //    hdn_usrcode.Value = txtUserName.Text;
                        //    hdn_password.Value = txtUserPass.Text;
                        //    ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "fn_openmodel('" + Common.Encrypt(result, true) + "', '" + otptime + "');", true);
                        //    return false;
                        //}
                        else if (dt.Rows[0]["flag"].ToString() == "N")
                        {
                            //2. For static ip to access unathorized person
                            //send sms and email to admin for unathorized access 
                            //show msg (you have no access for static IP to login) and log manage with ipaddress
                            UserLogManage(dt.Rows[0]["usrid"].ToString(), "S");
                            SmsOTPData("User " + dt.Rows[0]["usrname"].ToString() + " is trying to login by static ip without accessing", Common.Decrypt(dt.Rows[0]["mobileno"].ToString(), true));
                            EmailOTPData("User " + dt.Rows[0]["usrname"].ToString() + " is trying to login by static ip without accessing", Common.Decrypt(dt.Rows[0]["emailid"].ToString(), true));
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "Alert('" + ResponseMessage.staticAccess+"', true)", true);
                            return false;
                        }
                        else if (dt.Rows[0]["flag"].ToString() == "B")
                        {
                            //3. For Local to access unathorized person
                            //send sms and email to admin for someone tring to access computer 
                            //show msg (you are not login by biometric first login then access) and log manage
                            UserLogManage(dt.Rows[0]["usrid"].ToString(), "L");
                            SmsOTPData("User " + dt.Rows[0]["usrname"].ToString() + " is trying to accessing computer without login by biometric thumb impression", Common.Decrypt(dt.Rows[0]["mobileno"].ToString(), true));
                            EmailOTPData("User " + dt.Rows[0]["usrname"].ToString() + " is trying to accessing computer without login by biometric thumb impression", Common.Decrypt(dt.Rows[0]["emailid"].ToString(), true));
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "Alert('" + ResponseMessage.biometricThumb+"', true)", true);
                            return false;
                        }
                        else if (dt.Rows[0]["flag"].ToString() == "E")
                        {
                            //4. For User password expired
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "Alert('" + ResponseMessage.passwordExpired+"', true)", true);
                            return false;
                        }
                        else if (dt.Rows[0]["flag"].ToString() == "X")
                        {
                            //5. For IP access set 
                            //send sms and email to admin for someone tring to access computer 
                            //show msg (you have not access to login software from this computer) and log manage
                            UserLogManage(dt.Rows[0]["usrid"].ToString(), "X");
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "Alert('" + ResponseMessage.NoCompAccess+"', true)", true);
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "Alert('" + ResponseMessage.passwordExpired+"', true)", true);
                            return false;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "Alert('" + ResponseMessage.PassMatch+"', true)", true);
                            return false;
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "Alert('" + ResponseMessage.loginName+"', true)", true);
                        return false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "Alert('" + ResponseMessage.dllMismatch+"', true)", true);
                    return false;
                }

            }
            catch (Exception ex)
            {
                // Add error handling here for debugging.
                // This error message should not be sent back to the caller.
                ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "Alert('" + ResponseMessage.loginName+"', true)", true);
                return false;
            }

        }


        private string UpdateRegKey(string rkey, string srNo, string oldPassword)
        {
            string password = string.Empty;
            try
            {
                SPPHttpClient httpClient = new SPPHttpClient();
                string response = httpClient.GetHttpRequest($"{ApplicationConstant.HisRegDomain}home/getregistrationKey?Rkey={rkey}&SrNo={srNo}");
                RegApiResponse apiResponse = (RegApiResponse)JsonConvert.DeserializeObject(response, typeof(RegApiResponse));
                if (apiResponse.ResponseCode == ResponseCode.OK)
                {
                    RegInfo regInfo = (RegInfo)JsonConvert.DeserializeObject(apiResponse.ResponsePacket.ToString(), typeof(RegInfo));
                    if (regInfo != null && regInfo.clreg_pwd != oldPassword)
                    {
                        password = regInfo.clreg_pwd;
                        DataManager dm = new DataManager();
                        List<ViewParam> _lstview = new List<ViewParam>();
                        _lstview.Add(new ViewParam() { Name = "@RKey", Value = rkey });
                        _lstview.Add(new ViewParam() { Name = "@Regpassword", Value = regInfo.clreg_pwd });
                        _lstview.Add(new ViewParam() { Name = "@validdate", Value = regInfo.clreg_vdt });
                        _lstview.Add(new ViewParam() { Name = "@flag", Value = "U" });
                        DataSet ds = dm.View.GetDataSet("USP_SaveAdminRegistration", _lstview);
                    }
                    else
                        return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            return password;
        }
        private void SaveUserRegDetail()
        {
            try
            {
                DataManager dm = new DataManager();
                DataTable dt = dm.View.Get("select convert(nvarchar(10),opdpar_RegDate,103) as opdpar_RegDate from h_opdpar");
                DateTime backdate = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy"));
                if (dt != null)
                {
                    DateTime regdate = Convert.ToDateTime(dt.Rows[0]["opdpar_RegDate"].ToString());
                    if (regdate <= backdate)
                    {
                        SqlConnectionStringBuilder SSB = new SqlConnectionStringBuilder(getDefaultConnection.Gs_GetConnectionString());
                        List<ViewParam> _lstview = new List<ViewParam>();
                        DataSet ds = new DataSet();
                        List<UserInfo> userInfo = new List<UserInfo>();
                        _lstview.Add(new ViewParam() { Name = "@flag", Value = "G" });
                        ds = dm.View.GetDataSet("USP_SaveAdminRegistration", _lstview);
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            userInfo = ds.Tables[0].AsEnumerable().Select(x => new UserInfo
                            {
                                clreg_comp = x.Field<string>("clreg_comp"),
                                clreg_Hname = x.Field<string>("clreg_Hname"),
                                clreg_id1 = x.Field<int>("clreg_id1"),
                                clreg_Pwd = x.Field<string>("clreg_Pwd"),
                                clreg_Rkey = x.Field<string>("clreg_Rkey"),
                                clreg_Rto = x.Field<string>("clreg_Rto"),
                                clreg_sno = x.Field<string>("clreg_sno"),
                                clreg_station = x.Field<string>("clreg_station"),
                                clreg_vflag = x.Field<string>("clreg_vflag"),
                                DataBaseName = SSB.InitialCatalog,
                                CompDetail = x.Field<string>("CompDetail"),

                            }).ToList();
                            SPPHttpClient httpClient = new SPPHttpClient();
                            string response = httpClient.PostHttpRequest($"{ApplicationConstant.HisRegDomain}home/saveuserdetail", userInfo);
                            RegApiResponse apiResponse = (RegApiResponse)JsonConvert.DeserializeObject(response, typeof(RegApiResponse));
                            if (apiResponse.ResponseCode == ResponseCode.OK)
                            {
                                RegDateUpdate();
                                RegUpdate();
                            }
                        }
                    }
                }
                else
                {
                    RegDateUpdate();
                    RegUpdate();
                }
            }
            catch (Exception ex)
            {
            }

        }
        private void RegDateUpdate()
        {
            DataManager dm = new DataManager();
            List<ViewParam> _lstview = new List<ViewParam>();
            _lstview.Add(new ViewParam() { Name = "@flag", Value = "S" });
            DataSet ds = dm.View.GetDataSet("USP_SaveAdminRegistration", _lstview);
        }
        private void RegUpdate()
        {
            DataManager dm = new DataManager();
            Boolean AuthCheck = false;
            string cn = getDefaultConnection.Gs_GetConnectionString();
            Class1 reghis = new Class1();
            AuthCheck = reghis.getRegChk(cn.Trim());
            mac1 = reghis.mac1;
            DateTime Vdate = Convert.ToDateTime(reghis.g_dtval);
            string vdt = Vdate.ToString("dd/MM/yyyy");
            DataTable dt = dm.View.Get("select clreg_sno,clreg_Rkey,clreg_Rto,clreg_Hname,clreg_station,clreg_comp,convert(nvarchar(10),clreg_vdt,103)clreg_vdt,clreg_Pwd from h_clreg where clreg_Rkey='" + mac1 + "'");
            string newPassword = UpdateRegKey(dt.Rows[0]["clreg_Rkey"].ToString(), dt.Rows[0]["clreg_sno"].ToString(), dt.Rows[0]["clreg_Pwd"].ToString());
            ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "AlertRegExpiaryBefore('" + vdt + "');", true);
        }

        private void patientduebalsms()
        {
            DataManager dm = new DataManager();
            DataSet ds = new DataSet();
            List<ViewParam> _lst = new List<ViewParam>();
            string regex = @"[\[\]]";
            DataTable dtmsg = new DataTable();
            DataTable dt = dm.View.Get("select count(cdate_kid)   from h_Cdate where convert(date, getdate(), 105) = convert(date, isnull(Cdate_admitptcashcategorysms, getdate() - 1), 105)");
            if (dt != null && dt.Rows[0][0].ToString() == "0")
            {
                ds = dm.View.GetDataSet("Usp_SMSptbalcashcategory", _lst);
                if (ds != null && ds.Tables.Count > 1 && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0 && ds.Tables[1].Rows[0]["SMSSchedule_Msg"].ToString().Trim() != "")
                {
                    dtmsg.Columns.Add("id", typeof(int));
                    dtmsg.Columns.Add("mobno", typeof(string));
                    dtmsg.Columns.Add("msg", typeof(string));

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)   /// patient loop
                    {
                        string SMSSchedule_Msg = ds.Tables[1].Rows[0]["SMSSchedule_Msg"].ToString().Trim();
                        DataRow dr = dtmsg.NewRow();
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            SMSSchedule_Msg = cls_SMSActivities.ReplaceAddon(SMSSchedule_Msg, ds.Tables[0].Columns[j].ToString(), ds.Tables[0].Rows[i][ds.Tables[0].Columns[j].ToString()].ToString());
                        }
                        SMSSchedule_Msg = cls_SMSActivities.ExtractHtmlInnerText(Regex.Replace(SMSSchedule_Msg, regex, ""));
                        SMSSchedule_Msg = Regex.Replace(SMSSchedule_Msg, @"&nbsp;", "");
                        dr["ID"] = i + 1;
                        dr["mobno"] = ds.Tables[0].Rows[i]["mobno"].ToString();
                        dr["msg"] = SMSSchedule_Msg;
                        dtmsg.Rows.Add(dr);
                    }
                    cls_SMSActivities.SMSDATASend(ds.Tables[1].Rows[0]["SMSSchedule_SMSActivityID"].ToString(), dtmsg);
                }
            }
        }
        private bool checkmenuaccess()
        {

            DataManager dm = new DataManager();
            List<ViewParam> _lstview = new List<ViewParam>();
            _lstview.Add(new ViewParam() { Name = "@UserID", Value = Common.Decrypt(Request.Cookies["ERUserID"].Value.ToString(),true) });
            _lstview.Add(new ViewParam() { Name = "@CompID", Value = Common.Decrypt(Request.Cookies["ERCompID"].Value.ToString(),true) });
            DataSet ds = new DataSet();
            ds = dm.View.GetDataSet("h_modules_get", _lstview);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public DataTable CheckPassword(string usrname, string pwd)
        {
            DataManager dm = new DataManager();
            DataTable dt = dm.View.Get("select usr_pswd,usr_kid, usr_ename,usr_compid,usr_moduleType,usr_packageid,usr_days from h_usr where usr_code = '" + usrname + "' and usr_active = 'Y' ");
            DataTable dt2 = dm.View.Get("select top 1  modueType_colNo,moduleType_margin  from h_moduleType");
            DataTable dt3 = dm.View.Get("select isnull(opdpar_idleMinute,0) from h_opdpar");

            if (dt.Rows.Count > 0)
            {
                string paswd = dt.Rows[0][0].ToString();
                string decryptpwd = Common.Decrypt(paswd, true);
                bool f = (0 == string.Compare(pwd, decryptpwd, false));
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("flag", typeof(string));
                dt1.Columns.Add("id", typeof(int));
                dt1.Columns.Add("usrname", typeof(string));
                dt1.Columns.Add("compid", typeof(int));
                dt1.Columns.Add("moduleType", typeof(string));
                dt1.Columns.Add("usr_packageid", typeof(string));
                dt1.Columns.Add("colNo", typeof(string));
                dt1.Columns.Add("margin", typeof(string));
                dt1.Columns.Add("days", typeof(int));
                dt1.Columns.Add("idleMinute", typeof(int));
                if (f == true)
                {
                    dt1.Rows.Add("T", dt.Rows[0][1], dt.Rows[0][2], dt.Rows[0][3], dt.Rows[0][4], dt.Rows[0][5], dt2.Rows[0][0], dt2.Rows[0][1], dt.Rows[0][6], dt3.Rows[0][0]);
                }
                else
                {
                    dt1.Rows.Add("F", dt.Rows[0][1]);
                }
                return dt1;
            }
            else
            {
                return dt = new DataTable();
            }
        }


        protected void btn_OTPSave_Click(object sender, EventArgs e)
        {
            //Call the RedirectFromLoginPage method to automatically generate the forms authentication cookie and 
            //redirect the user to an appropriate page
            Response.Cookies["dbName"].Value = txtCompName.Text.Trim();
            Response.Cookies["dbName"].Expires = DateTime.Now.AddHours(8);
            if (ValidateUser(hdn_usrcode.Value.Trim(), hdn_password.Value, true))
            {
                //Let us now set the authentication cookie so that we can use that later.
                FormsAuthentication.SetAuthCookie(txtUserName.Text.Trim(), false);
                //Login successful lets put him to requested page
                //string returnUrl = Request.QueryString["ReturnUrl"] as string;
                Response.Cookies["ERCache"].Value = "N";
                Response.Cookies["ERCache"].Expires = DateTime.Now.AddHours(8);
                Response.Redirect("~/View/Dashboard/Dashboard.aspx");
            }
            else
            {
                Response.Cookies.Clear();
            }
        }


        protected void EmailOTPData(string EmailData, string email)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(EmailData);

            if (email != null && email != "")
            {
                DataManager dm = new DataManager();
                DataTable dt = dm.View.Get("select opdpar_OTPMail,opdpar_OTPMailPassWord from h_opdpar");
                string sendmail = Common.Decrypt(dt.Rows[0]["opdpar_OTPMail"].ToString(), true);
                string sendmailPwd = Common.Decrypt(dt.Rows[0]["opdpar_OTPMailPassWord"].ToString(), true);

                MailMessage message = new MailMessage();
                message.From = new MailAddress(sendmail);
                message.To.Add(new MailAddress(email.ToString()));
                message.Subject = "OTP";
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Body = sb.ToString();
                message.Priority = System.Net.Mail.MailPriority.High;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential(sendmail, sendmailPwd);
                client.Send(message);
            }
        }


        private bool SetCookies()
        {
            if (ViewState["LoginDt"] != null)
            {

                DataTable dt = (DataTable)ViewState["LoginDt"];
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0].ToString() == "T")
                {
                    DataTable dt1 = checkSavePdf();

                    if (dt1.Rows.Count > 0)
                    {
                        Response.Cookies["ERSavePdfFlg"].Value = dt1.Rows[0]["opdpar_PdfSave"].ToString();
                        Response.Cookies["ERSavePdfFlg"].Expires = DateTime.Now.AddHours(8);

                    }
                  
                    Response.Cookies["ERuserName"].Value =Common.Encrypt( dt.Rows[0][2].ToString(),true);
                    Response.Cookies["ERuserName"].Expires = DateTime.Now.AddHours(8);
                    Response.Cookies["ERuserID"].Value = Common.Encrypt(dt.Rows[0][1].ToString(),true);
                    Response.Cookies["ERuserID"].Expires = DateTime.Now.AddHours(8);
                    Response.Cookies["ERcompID"].Value = Common.Encrypt(dt.Rows[0][3].ToString(),true);
                    Response.Cookies["ERcompID"].Expires = DateTime.Now.AddHours(8);
                    Response.Cookies["ERmoduleType"].Value = Common.Encrypt(dt.Rows[0][4].ToString(),true);
                    Response.Cookies["ERmoduleType"].Expires = DateTime.Now.AddHours(8);

                    Response.Cookies["ERPackageType"].Value = Common.Encrypt(dt.Rows[0][5].ToString(), true);
                    Response.Cookies["ERPackageType"].Expires = DateTime.Now.AddHours(8);

                    Response.Cookies["ERLabRptType"].Value = Common.Encrypt("P", true);
                    Response.Cookies["ERLabRptType"].Expires = DateTime.Now.AddHours(8);
                    Response.Cookies["ERGS_paramAlert"].Value = Common.Encrypt("1",true);
                    Response.Cookies["ERGS_paramAlert"].Expires = DateTime.Now.AddHours(8);

                    Response.Cookies["ERcolumn"].Value = Common.Encrypt(dt.Rows[0][6].ToString(), true);
                    Response.Cookies["ERcolumn"].Expires = DateTime.Now.AddHours(8);
                    Response.Cookies["ERmargin"].Value = Common.Encrypt(dt.Rows[0][7].ToString() + "px", true);
                    Response.Cookies["ERmargin"].Expires = DateTime.Now.AddHours(8);

                    Response.Cookies["ERLastRequestTime"].Value = Common.Encrypt(DateTime.Now.ToString(), true);
                    Response.Cookies["ERLastRequestTime"].Expires = DateTime.Now.AddHours(8);
                    if (dt.Rows[0]["idleMinute"].ToString() != null && dt.Rows[0]["idleMinute"].ToString() != "0")
                    {
                        Response.Cookies["ERUserIdleMinutes"].Value = Common.Encrypt(dt.Rows[0]["idleMinute"].ToString(), true);
                        Response.Cookies["ERUserIdleMinutes"].Expires = DateTime.Now.AddHours(8);
                    }

                    Session["sess_Date"] = hdn_ddt.Value;

                    Response.Cookies["ERPrintDays"].Value = Common.Encrypt(dt.Rows[0]["days"].ToString(), true);
                    Response.Cookies["ERPrintDays"].Expires = DateTime.Now.AddHours(8);


                    DataManager dm = new DataManager();
                    DataSet ds = new DataSet();
                    DataTable dtLocation;
                    List<ViewParam> _lstview = new List<ViewParam>();
                    _lstview.Add(new ViewParam() { Name = "@usrid", Value = dt.Rows[0][1].ToString() });
                    ds = dm.View.GetDataSet("LoginTime", _lstview);
                    dtLocation = ds.Tables[0];
                    if (dtLocation.Rows.Count > 0)
                    {
                        string LocationID = dtLocation.Rows[0][0].ToString();
                        Response.Cookies["ERsess_LocId"].Value =Common.Encrypt( LocationID,true);
                        Response.Cookies["ERsess_LocId"].Expires = DateTime.Now.AddHours(8);
                        Response.Cookies["ERGS_LastLogon"].Value =Common.Encrypt(dtLocation.Rows[0][1].ToString(),true);
                        Response.Cookies["ERGS_LastLogon"].Expires = DateTime.Now.AddHours(8);
                        GS_logindet();
                    }
                    else
                    {
                        Request.Cookies["ERsess_LocId"].Value = "0";
                    }

                    DataManager dm1 = new DataManager();
                    DataTable dt_notif = dm1.View.Get("select opdpar_notification from h_opdpar");
                    if (dt_notif.Rows.Count > 0)
                    {
                        Response.Cookies["ERNotifPar"].Value =Common.Encrypt(dt_notif.Rows[0][0].ToString(),true);
                        Response.Cookies["ERNotifPar"].Expires = DateTime.Now.AddHours(8);
                    }

                    DataManager dm2 = new DataManager();
                    DataTable dt_doublebilling = dm2.View.Get("select OPDPar_DoubleBilling from h_opdpar");
                    if (dt_doublebilling.Rows.Count > 0)
                    {
                        Response.Cookies["ERDoubleBilling"].Value =Common.Encrypt( dt_doublebilling.Rows[0][0].ToString(),true);
                        Response.Cookies["ERDoubleBilling"].Expires = DateTime.Now.AddHours(8);
                    }
                    //End

                    dt_notif = dm1.View.Get("select top 1 opdpar_CashPayCal from h_opdpar");
                    if (dt_notif.Rows.Count > 0)
                    {
                        Response.Cookies["ERCashPart"].Value =Common.Encrypt(dt_notif.Rows[0][0].ToString(),true);
                        Response.Cookies["ERCashPart"].Expires = DateTime.Now.AddHours(8);
                    }
                    return true;
                }

                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public DataTable checkSavePdf()
        {
            DataManager dm = new DataManager();
            DataTable dt = dm.View.Get("select opdpar_PdfSave from h_opdpar ");
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return dt = new DataTable();
            }
        }

        protected DataTable CheckUserLoginRequest(string usrname, string pwd)
        {
            string host = HttpContext.Current.Request.Url.Authority;
            DataManager dm = new DataManager();
            DataTable dt_config = dm.View.Get("select opdpar_slflag, opdpar_OTPMinute,opdpar_pswdrecall,opdpar_pswdrecalldays,opdpar_idleMinute from h_opdpar");
            DataTable dt_admin = dm.View.Get("select top 1 moduleType_mobile, moduleType_Email from h_moduleType order by moduleType_Email desc");
            DataTable dt_Access = dm.View.Get("select usr_kid, usr_mobileno, usr_emailid, usr_staticflg, usr_Bioflg, usr_empid, usr_chngDate from h_usr where usr_code = '" + usrname + "' and usr_active = 'Y' ");
            DataTable dt_UserCompAccess = dm.View.Get("select IpSetting_ipaddress from h_IpUserLinking, h_ipsetting where IpUserLinking_ipsettingid = IpSetting_kid and IpSetting_status = 'A' and IpUserLinking_userid = " + dt_Access.Rows[0]["usr_kid"].ToString() + "");

            TimeSpan numdays = (Convert.ToDateTime(dt_Access.Rows[0]["usr_chngDate"].ToString()) - DateTime.Now);
            int days = numdays.Days;
        
            if (dt_config.Rows[0]["opdpar_pswdrecall"].ToString() == "Y" && days < -(Convert.ToInt32(dt_config.Rows[0]["opdpar_pswdrecalldays"].ToString())))
            {
                DataTable dt_false = new DataTable();
                dt_false.Columns.Add("flag", typeof(string));
                dt_false.Rows.Add("E");
                return dt_false;

            }    //End
            else
            {
                string ipaddress = CheckIPAddr();
                bool compaccess = false;
                bool tabledata = false;
                if (dt_UserCompAccess.Rows.Count > 0)
                {
                    tabledata = true;
                    foreach (DataRow row in dt_UserCompAccess.Rows)
                    {
                        if (ipaddress == row["IpSetting_ipaddress"].ToString())
                        {
                            compaccess = true;
                        }
                    }
                }

                if (compaccess == false && tabledata == true)
                {
                    DataTable dt_false = new DataTable();
                    dt_false.Columns.Add("flag", typeof(string));
                    dt_false.Columns.Add("mobileno", typeof(string));
                    dt_false.Columns.Add("emailid", typeof(string));
                    dt_false.Columns.Add("usrname", typeof(string));
                    dt_false.Columns.Add("usrid", typeof(string));
                    dt_false.Rows.Add("X", dt_admin.Rows[0]["moduleType_mobile"].ToString(), dt_admin.Rows[0]["moduleType_Email"].ToString(), usrname, dt_Access.Rows[0]["usr_kid"].ToString());
                    return dt_false;
                }
                else
                {
                    int index = host.IndexOf(':');
                    if (index != -1)
                    {
                        host = host.Substring(0, host.IndexOf(":"));
                    }
                    // End

                    DataTable dt_BioIPs = dm.View.Get("select ipadd_ipid, ipadd_flag from h_ipadd");
                    string Ipflag = "";
                    foreach (DataRow row in dt_BioIPs.Rows)
                    {
                        if (host == row["ipadd_ipid"].ToString())
                        {
                            Ipflag = row["ipadd_flag"].ToString();
                        }
                    }

                    //checked validation static ip configure or not
                    if (Ipflag == "S" && dt_config.Rows[0]["opdpar_slflag"].ToString() == "Y")
                    {
                        //check user allow for access or not 
                        if (dt_Access.Rows[0]["usr_staticflg"].ToString() == "Y")
                        {
                            //check usercode and password is correct or not
                            DataTable dt3 = CheckPassword(usrname, pwd);
                            if (dt3.Rows.Count > 0)
                            {
                                if (dt3.Rows[0]["flag"].ToString() == "T")
                                {
                                    //result = true;
                                    DataTable dt_false = new DataTable();
                                    dt_false.Columns.Add("flag", typeof(string));
                                    dt_false.Columns.Add("mobileno", typeof(string));
                                    dt_false.Columns.Add("emailid", typeof(string));
                                    dt_false.Columns.Add("usrname", typeof(string));
                                    dt_false.Columns.Add("usrid", typeof(string));
                                    dt_false.Columns.Add("OTPtime", typeof(string));
                                    dt_false.Rows.Add("O", dt_Access.Rows[0]["usr_mobileno"].ToString(), dt_Access.Rows[0]["usr_emailid"].ToString(), usrname, dt_Access.Rows[0]["usr_kid"].ToString(), dt_config.Rows[0]["opdpar_OTPMinute"].ToString());
                                    return dt_false;
                                }
                                else
                                {
                                    //result = false;
                                    return dt3 = new DataTable();
                                }
                            }
                            else
                            {
                                //result = false;
                                DataTable dt4 = new DataTable();
                                return dt4;
                            }
                        }
                        else
                        {
                            //result = false;
                            DataTable dt_false = new DataTable();
                            dt_false.Columns.Add("flag", typeof(string));
                            dt_false.Columns.Add("mobileno", typeof(string));
                            dt_false.Columns.Add("emailid", typeof(string));
                            dt_false.Columns.Add("usrname", typeof(string));
                            dt_false.Columns.Add("usrid", typeof(string));
                            dt_false.Rows.Add("N", dt_admin.Rows[0]["moduleType_mobile"].ToString(), dt_admin.Rows[0]["moduleType_Email"].ToString(), usrname, dt_Access.Rows[0]["usr_kid"].ToString());
                            return dt_false;
                        }
                    }
                    //check biomatric validation configure or not
                    else if (Ipflag == "L" && dt_config.Rows[0]["opdpar_slflag"].ToString() == "Y" && dt_Access.Rows[0]["usr_Bioflg"].ToString() == "Y")
                    {
                        //check user login in biometric process or not
                        DataTable dt_Bioprocess = dm.View.Get("select EmpInOut_Type from p_empinout where EmpInOut_empid = " + dt_Access.Rows[0]["usr_empid"].ToString() + " and convert(varchar(10),EmpInOut_Date, 103) = convert(varchar(10),'" + DateTime.Now.ToString() + "', 105) ");
                        if (dt_Bioprocess.Rows.Count > 0)
                        {
                            if (dt_Bioprocess.Rows[0]["EmpInOut_Type"].ToString() == "I")
                            {
                                //check usercode and password is correct or not
                                DataTable dt3 = CheckPassword(usrname, pwd);
                                return dt3;
                            }
                            else
                            {
                                //result = false;
                                DataTable dt_false = new DataTable();
                                dt_false.Columns.Add("flag", typeof(string));
                                dt_false.Columns.Add("mobileno", typeof(string));
                                dt_false.Columns.Add("emailid", typeof(string));
                                dt_false.Columns.Add("usrname", typeof(string));
                                dt_false.Columns.Add("usrid", typeof(string));
                                dt_false.Rows.Add("B", dt_admin.Rows[0]["moduleType_mobile"].ToString(), dt_admin.Rows[0]["moduleType_Email"].ToString(), usrname, dt_Access.Rows[0]["usr_kid"].ToString());
                                return dt_false;
                            }
                        }
                        else
                        {
                            //result = false;
                            DataTable dt_false = new DataTable();
                            dt_false.Columns.Add("flag", typeof(string));
                            dt_false.Columns.Add("mobileno", typeof(string));
                            dt_false.Columns.Add("emailid", typeof(string));
                            dt_false.Columns.Add("usrname", typeof(string));
                            dt_false.Columns.Add("usrid", typeof(string));
                            dt_false.Rows.Add("B", dt_admin.Rows[0]["moduleType_mobile"].ToString(), dt_admin.Rows[0]["moduleType_Email"].ToString(), usrname, dt_Access.Rows[0]["usr_kid"].ToString());
                            return dt_false;
                        }
                    }
                    else
                    {
                        //If StaticIP and Bio process not configure 
                        DataTable dt3 = CheckPassword(usrname, pwd);
                        return dt3;
                    }
                }
                /* Roshan On 30/08/2016 */
            }

        }

        //send sms to user and admin on validate request
        protected void SmsOTPData(string SMSSchedule_Msg, string mobileNO)
        {
            string cn = getDefaultConnection.Gs_GetConnectionString();
            using (SqlConnection con = new SqlConnection(cn))
            {

                string Time = DateTime.Now.ToString("HH:mm:ss");
                string date = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "USP_SMSSendDataSave";
                cmd.Parameters.AddWithValue("@SMSActivityID", null);
                cmd.Parameters.AddWithValue("@appTime", Time);
                cmd.Parameters.AddWithValue("@appdate", Convert.ToDateTime(date));
                cmd.Parameters.AddWithValue("@mobno", mobileNO);
                cmd.Parameters.AddWithValue("@parseMSG", SMSSchedule_Msg);
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(ds);
            }
        }


        [WebMethod]
        public static string GetOTP(string username)
        {
            DataManager dm = new DataManager();
            DataTable dt = new DataTable();
            dt = dm.View.Get("select Usr_name,usr_contact from M_usr where usr_code='" + username + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                string name = dt.Rows[0][0].ToString();
                var chars = "1234567890";
                var random = new Random();
                var result = new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
                string MsgEmailFormat = "Dear " + dt.Rows[0][0].ToString() + " thanks for associating with Emergency. Your OTP for password reset is " + result + " Valid for 5 minutes only.";
                Common.InsertSms(MsgEmailFormat, dt.Rows[0][1].ToString());
                return Common.Encrypt(result, true);
            }
            else
            {
                return "0";
            }

        }
        [WebMethod]
        public static string VerifyOTP(string OTP, string hdn)
        {
            if (OTP == Common.Decrypt(hdn, true))
            {
                return "1";
            }
            else
            {
                return "0";
            }

        }



        [WebMethod]
        public static string ResetPassword(string User, string Password)
        {
            DataManager DM = new DataManager();
            List<ViewParam> lst = new List<ViewParam>();
            DataSet DS = new DataSet();
            string password = Common.Encrypt(Password, true);

            if (User != "")
                lst.Add(new ViewParam() { Name = "@user", Value = User });
            if (password != "")
                lst.Add(new ViewParam() { Name = "@password", Value = password });

            DS = DM.View.GetDataSet("[M_UpdatePaswd_save]", lst);
            if (DS != null)
                return DS.Tables[0].Rows[0][0].ToString();
            else
                return "0";

        }


        [System.Web.Services.WebMethod]
        public static string LoginOTPCheck(string sysOTP, string usrOTP)
        {
            string sysDecrypt = Common.Decrypt(sysOTP, true);
            string result = "false";
            if (sysDecrypt.ToString() == usrOTP)
            {
                result = "true";
            }
            else
            {
                result = "false";
            }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable("Table");
            dt.Columns.Add("OTPresult", typeof(string));
            DataRow dr = dt.NewRow();
            dr["OTPresult"] = result;
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            return Common.ConverDatasetToJson(ds);
        }



    }



    public class RegInfo
    {
        public string clreg_pwd { get; set; }
        public string clreg_vdt { get; set; }

    }
    public class UserInfo
    {
        public string clreg_Rkey { get; set; }
        public string clreg_sno { get; set; }
        public string clreg_Rto { get; set; }
        public string clreg_Hname { get; set; }
        public string clreg_station { get; set; }
        public string clreg_Pwd { get; set; }
        public int clreg_id1 { get; set; }
        public string clreg_comp { get; set; }
        public string clreg_vflag { get; set; }
        public string DataBaseName { get; set; }
        public string CompDetail { get; set; }



    }
}