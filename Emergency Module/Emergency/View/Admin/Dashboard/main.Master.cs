using GS.Common;
using GS.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Caching;

namespace Emergency.View.Admin.Dashboard
{
    public partial class main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
                DataManager dm = new DataManager();
                List<ViewParam> _lstview = new List<ViewParam>();
                DataSet ds = new DataSet();
                string userId = Common.Decrypt(Request.Cookies["ERuserID"].Value, true);
                string compID = Common.Decrypt(Request.Cookies["ERcompID"].Value, true);

                string cacheName = "MenuDa_" + compID + "_" + userId;

                if (Request.Cookies["ERCache"].Value == "Y" && HttpContext.Current.Cache[cacheName] != null)
                {
                    ds = (DataSet)System.Web.HttpContext.Current.Cache[cacheName];
                }
                else
                {
                    string fileName = "DashboardCache_" + compID + "_" + userId + ".txt";

                    _lstview.Add(new ViewParam() { Name = "@CompID", Value = compID });
                    _lstview.Add(new ViewParam() { Name = "@UserID", Value = userId });
                    ds = dm.View.GetDataSet("[h_mnum_Menu_Get_Emergency]", _lstview);

                    bool exists = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("/UserCache"));
                    if (!exists)
                        System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/UserCache"));

                    string filePath = HttpContext.Current.Server.MapPath("/UserCache/" + fileName);
                    if (!System.IO.File.Exists(filePath))
                    {
                        var myFile = System.IO.File.Create(filePath);
                        myFile.Close();
                        System.IO.File.WriteAllText(filePath, DateTime.Now.ToString());
                    }
                    CacheDependency dep = new CacheDependency(filePath);
                    System.Web.HttpContext.Current.Cache.Insert(cacheName, ds, dep);

                    Response.Cookies["ERCache"].Value = "Y";
                    Response.Cookies["ERCache"].Expires = DateTime.Now.AddHours(8);
                }
                hdn_Menus.Value = Common.ConverDatasetToJson(ds);

                hdn_currenturl.Value = HttpContext.Current.Request.Url.AbsolutePath;


                string Name = Common.Decrypt(Request.Cookies["ERuserName"].Value, true);

                lbl_Name.InnerText = Name;

                //span_ShortName.InnerText = Name.Substring(0, 1);

                //div_profimg.Visible = false;
                //}
                //else
                //{
                //    List<ViewParam> _lst = new List<ViewParam>();
                //    DataTable DT = new DataTable();
                //    _lstview.Add(new ViewParam() { Name = "@userRole", Value = hdn_Type.Value });
                //    _lstview.Add(new ViewParam() { Name = "@id", Value = Common.Decrypt(Request.Cookies["ERMedID"].Value, true) });

                //    DataTable dt = dm.View.Get("select top 1 File_Path from M_File where  File_Type='D' and File_id=(select usr_id from M_usr where usr_kid='" + Common.Decrypt(Request.Cookies["ERMedID"].Value, true) + "') ");
                //    if (dt != null && dt.Rows.Count > 0)
                //    {
                //        usr_image.Src = dt.Rows[0][0].ToString();
                //    }
                //    else
                //    {
                //        usr_image.Src = "";
                //    }
            //}
            //catch (Exception ex) {
            //    Response.Redirect("~/Default.aspx");
            //}
        }
    }
}