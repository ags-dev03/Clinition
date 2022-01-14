using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using GS.Common;
using GS.Data;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.html.simpleparser;
using System.Collections;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using Microsoft.Reporting.WebForms;

namespace Emergency.Classes
{
    public class GS_Common
    {

        public DataTable CheckPassword(string usrname, string pwd)
        {
            DataManager dm = new DataManager();
            DataTable dt = dm.View.Get("select usr_pswd,usr_kid, usr_ename,usr_compid,usr_moduleType from h_usr where usr_code = '" + usrname + "' and usr_active = 'Y' ");
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
                if (f == true)
                {
                    dt1.Rows.Add("T", dt.Rows[0][1], dt.Rows[0][2], dt.Rows[0][3], dt.Rows[0][4]);

                }
                else
                {
                    dt1.Rows.Add("F", dt.Rows[0][1]);
                }
                //dt1.Rows.Add(f);
                return dt1;
            }
            else
            {
                return dt = new DataTable();
            }
        }
        public bool getPageVersionDetails(string UI_Version, string CS_Version)
        {
            if (UI_Version != CS_Version)
                return false;

            string path = HttpContext.Current.Request.RawUrl.ToString().Trim();
            if (HttpContext.Current.Request.Url.Query.Length > 0)
            {
                path = path.Replace(HttpContext.Current.Request.Url.Query, String.Empty);
            }
            DataManager dm = new DataManager();
            List<ViewParam> _lstview = new List<ViewParam>();
            DataSet ds = new DataSet();
            _lstview = new List<ViewParam>();
            _lstview.Add(new ViewParam() { Name = "@userid", Value = HttpContext.Current.Request.Cookies["ERuserID"].Value });
            _lstview.Add(new ViewParam() { Name = "@ModuleType", Value = HttpContext.Current.Request.Cookies["ERmoduleType"].Value.ToString() });
            _lstview.Add(new ViewParam() { Name = "@path", Value = path });
            _lstview.Add(new ViewParam() { Name = "@compid", Value = HttpContext.Current.Request.Cookies["ERcompID"].Value });
            ds = dm.View.GetDataSet("USP_PageVersionDetails_get", _lstview);
            if (ds.Tables[0].Rows.Count > 0)
            {
                bool versionDiff = false;
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (ds.Tables[0].Rows[i]["mnum_versionNo"].ToString() != CS_Version)
                    {
                        versionDiff = true;
                    }
                }

                if (versionDiff == false)
                {
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

        public bool getPageVersionDetails_popup(string UI_Version, string CS_Version)
        {
            if (UI_Version != CS_Version)
                return false;

            string path = HttpContext.Current.Request.RawUrl.ToString().Trim();
            if (HttpContext.Current.Request.Url.Query.Length > 0)
            {
                path = path.Replace(HttpContext.Current.Request.Url.Query, String.Empty);
            }
            DataManager dm = new DataManager();
            List<ViewParam> _lstview = new List<ViewParam>();
            DataSet ds = new DataSet();
            _lstview = new List<ViewParam>();
            _lstview.Add(new ViewParam() { Name = "@flag", Value = "P" });
            _lstview.Add(new ViewParam() { Name = "@path", Value = path });
            ds = dm.View.GetDataSet("USP_PageVersionDetails_get", _lstview);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["mnum_versionNo"].ToString() != "")
                {
                    if (UI_Version == ds.Tables[0].Rows[0]["mnum_versionNo"].ToString())
                        return true;
                    else
                        return false;
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

        public string VersionManage_DLL()
        {
            return "13.9.0";
        }

        public DataSet getURLPath()
        {
            DataManager dm = new DataManager();
            List<ViewParam> _lstview = new List<ViewParam>();
            DataSet ds = new DataSet();
            _lstview.Add(new ViewParam() { Name = "@flag", Value = "Q" });
            _lstview.Add(new ViewParam() { Name = "@userid", Value = HttpContext.Current.Request.Cookies["ERuserID"].Value });
            _lstview.Add(new ViewParam() { Name = "@ModuleType", Value = HttpContext.Current.Request.Cookies["ERmoduleType"].Value.ToString() });
            _lstview.Add(new ViewParam() { Name = "@compid", Value = HttpContext.Current.Request.Cookies["ERcompID"].Value });
            ds = dm.View.GetDataSet("USP_ReportsOTPSetting_get", _lstview);

            string path = HttpContext.Current.Request.RawUrl.ToString().Trim();
            if (HttpContext.Current.Request.Url.Query.Length > 0)
            {
                path = path.Replace(HttpContext.Current.Request.Url.Query, String.Empty);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (HttpContext.Current.Request.RawUrl.ToString().ToLower().Trim().IndexOf(ds.Tables[0].Rows[i]["mnum_redirectto"].ToString().ToLower()) >= 0)
                    {
                        path = ds.Tables[0].Rows[i]["mnum_redirectto"].ToString();
                    }
                }
            }

            _lstview = new List<ViewParam>();
            _lstview.Add(new ViewParam() { Name = "@userid", Value = HttpContext.Current.Request.Cookies["ERuserID"].Value });
            _lstview.Add(new ViewParam() { Name = "@ModuleType", Value = HttpContext.Current.Request.Cookies["ERmoduleType"].Value.ToString() });
            _lstview.Add(new ViewParam() { Name = "@path", Value = path });
            _lstview.Add(new ViewParam() { Name = "@compid", Value = HttpContext.Current.Request.Cookies["ERcompID"].Value });
            DataSet ds_path = dm.View.GetDataSet("USP_ReportsOTPSetting_get", _lstview);

            return ds_path;
        }

        public string getParamAlert()
        {
            return HttpContext.Current.Request.Cookies["ERGS_paramAlert"].Value.ToString();
        }

        public List<string> getHours(int maxhours)
        {
            List<string> _list = new List<string>();
            for (int i = 1; i <= maxhours; i++)
            {
                if (i.ToString().Length == 1)
                {
                    _list.Add("0" + i.ToString());
                }
                else
                {
                    _list.Add(i.ToString());
                }
            }
            return _list;
        }

        public List<string> getMinutes(int maxminutes)
        {
            List<string> _list = new List<string>();
            for (int i = 0; i < maxminutes; i++)
            {
                if (i.ToString().Length == 1)
                {
                    _list.Add("0" + i.ToString());
                }
                else
                {
                    _list.Add(i.ToString());
                }
            }
            return _list;
        }

        public List<string> getAmPm()
        {
            List<string> _list = new List<string>();
            for (int i = 1; i < 3; i++)
            {
                if (i == 1)
                {
                    _list.Add("AM");
                }
                else if (i == 2)
                {
                    _list.Add("PM");
                }
            }
            return _list;

        }

   

    }

    public static class cls_SMSActivities
    {
        public static string ExtractHtmlInnerText(string htmlText)
        {
            //Match any Html tag (opening or closing tags) 
            // followed by any successive whitespaces
            //consider the Html text as a single line

            Regex regex = new Regex("(<.*?>\\s*)+", RegexOptions.Singleline);

            // replace all html tags (and consequtive whitespaces) by spaces
            // trim the first and last space

            string resultText = regex.Replace(htmlText, " ").Trim();

            return resultText;
        }

        public static void EmailDataSend(string ActivityId, string RecipientID, string emailid, DateTime date, string time, string attachmnets, string parseMSG, string EmailSub)
        {
            DataManager dm = new DataManager();
            List<ViewParam> _lstview = new List<ViewParam>();
            DataSet ds = new DataSet();
            _lstview.Add(new ViewParam() { Name = "@EmailActivityID", Value = ActivityId });
            _lstview.Add(new ViewParam() { Name = "@EmailRecipientID", Value = RecipientID });
            _lstview.Add(new ViewParam() { Name = "@EmailData_emailId", Value = emailid, });
            _lstview.Add(new ViewParam() { Name = "@flag", Value = "S" });
            _lstview.Add(new ViewParam() { Name = "@EmailData_Msg", Value = parseMSG });
            _lstview.Add(new ViewParam() { Name = "@EmailData_Subject", Value = EmailSub });
            _lstview.Add(new ViewParam() { Name = "@EmailData_Sdate", Value = date });
            _lstview.Add(new ViewParam() { Name = "@EmailData_STime", Value = time });
            _lstview.Add(new ViewParam() { Name = "@EmailData_AttachmentPath", Value = attachmnets });
            ds = dm.View.GetDataSet("[E_EmailCommonGetMsg]", _lstview);
        }

        public static void SMSDATASend(string SMSActivityID, DateTime appdate, string appTime, string mobno, string parseMSG)
        {
            if (parseMSG != "")
            {
                DataManager dm = new DataManager();
                List<ViewParam> _lstview = new List<ViewParam>();
                DataSet ds = new DataSet();
                _lstview.Add(new ViewParam() { Name = "@SMSActivityID", Value = SMSActivityID });
                _lstview.Add(new ViewParam() { Name = "@appTime", Value = appTime });
                _lstview.Add(new ViewParam() { Name = "@appdate", Value = appdate });
                _lstview.Add(new ViewParam() { Name = "@mobno", Value = mobno });
                _lstview.Add(new ViewParam() { Name = "@parseMSG", Value = parseMSG });
                ds = dm.View.GetDataSet("[USP_SMSSendDataSave]", _lstview);
            }
        }


        public static void SMSDATASendVaccination(string SMSActivityID, DateTime appdate, string appTime, string mobno, string parseMSG)
        {
            DataManager dm = new DataManager();
            List<ViewParam> _lstview = new List<ViewParam>();
            DataSet ds = new DataSet();
            _lstview.Add(new ViewParam() { Name = "@SMSActivityID", Value = SMSActivityID });
            _lstview.Add(new ViewParam() { Name = "@appTime", Value = appTime });
            _lstview.Add(new ViewParam() { Name = "@appdate", Value = appdate });
            _lstview.Add(new ViewParam() { Name = "@mobno", Value = mobno });
            _lstview.Add(new ViewParam() { Name = "@parseMSG", Value = parseMSG });
            ds = dm.View.GetDataSet("[USP_SMSSendDataSaveForVaccination]", _lstview);
        }


        public static void SMSDATASend(string SMSActivityID, DateTime appdate, string appTime, string mobno, string parseMSG, string userid, string code, string deptid, string amckid, string insuranceid)
        {
            DataManager dm = new DataManager();
            List<ViewParam> _lstview = new List<ViewParam>();
            DataSet ds = new DataSet();
            _lstview.Add(new ViewParam() { Name = "@SMSActivityID", Value = SMSActivityID });
            _lstview.Add(new ViewParam() { Name = "@appTime", Value = appTime });
            _lstview.Add(new ViewParam() { Name = "@appdate", Value = appdate });
            _lstview.Add(new ViewParam() { Name = "@mobno", Value = mobno });
            _lstview.Add(new ViewParam() { Name = "@parseMSG", Value = parseMSG });
            _lstview.Add(new ViewParam() { Name = "@smsdata_userid", Value = userid });
            _lstview.Add(new ViewParam() { Name = "@smsdata_code", Value = code });
            _lstview.Add(new ViewParam() { Name = "@smsdata_deptid", Value = deptid });
            _lstview.Add(new ViewParam() { Name = "@amckid", Value = amckid });
            _lstview.Add(new ViewParam() { Name = "@Insurancekid", Value = insuranceid });
            ds = dm.View.GetDataSet("[A_SMSSendDataSave]", _lstview);
        }

        public static void SendSMS(string SMSActivityID, DateTime FollwUpDate, string FollowTime, string mobno, string parseMSG)
        {
            DataManager dm = new DataManager();
            List<ViewParam> _lstview = new List<ViewParam>();
            DataSet ds = new DataSet();
            _lstview.Add(new ViewParam() { Name = "@SMSActivityID", Value = SMSActivityID });
            _lstview.Add(new ViewParam() { Name = "@appTime", Value = FollowTime });
            _lstview.Add(new ViewParam() { Name = "@appdate", Value = FollwUpDate });
            _lstview.Add(new ViewParam() { Name = "@mobno", Value = mobno });
            _lstview.Add(new ViewParam() { Name = "@parseMSG", Value = parseMSG });
            ds = dm.View.GetDataSet("[USP_SMSSendDataSave]", _lstview);
        }

        public static void DocAp_SMSDATASend(string ptmastId, string NotifScheduleID, string type, string parseMSG)
        {
            DataManager dm = new DataManager();
            List<ViewParam> _lstview = new List<ViewParam>();
            DataSet ds = new DataSet();

            if (type == "C")
            {
                _lstview.Add(new ViewParam() { Name = "@type", Value = type });
            }
            else
            {
                _lstview.Add(new ViewParam() { Name = "@type", Value = type });
                _lstview.Add(new ViewParam() { Name = "@ptmastId", Value = ptmastId });
            }
            _lstview.Add(new ViewParam() { Name = "@notifscheduleId", Value = NotifScheduleID });
            _lstview.Add(new ViewParam() { Name = "@parseMSG", Value = parseMSG });
            _lstview.Add(new ViewParam() { Name = "@userId", Value = HttpContext.Current.Request.Cookies["ERuserID"].Value });
            ds = dm.View.GetDataSet("[DocAp_SMS_Save]", _lstview);
        }

        public static string ReplaceAddon(string msg, string Colname, string colvalue)
        {
            string ReplaceMsg = "";
            ReplaceMsg = msg.Replace("[" + Colname.ToString() + "]", colvalue.ToString());
            return ReplaceMsg;
        }

        public static void SMSDATASend(string SMSActivityID, DataTable dt)
        {
            {
                DataManager dm = new DataManager();
                List<ViewParam> _lstview = new List<ViewParam>();
                DataSet ds = new DataSet();
                _lstview.Add(new ViewParam() { Name = "@SMSActivityID", Value = SMSActivityID });
                _lstview.Add(new ViewParam() { Name = "@dtmsg", Value = dt });
                ds = dm.View.GetDataSet("USP_SMSSendDataSave_multiple", _lstview);
            }
        }





    }
}