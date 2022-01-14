using GS.Common;
using GS.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;


namespace Emergency.View.Masters
{
    public partial class Department : System.Web.UI.Page
    {
        DataManager DM = new DataManager();
        List<ViewParam> lst = new List<ViewParam>();
        DataTable DT = new DataTable();
        DataSet DS = new DataSet();

        #region PageINIT

        //protected void Page_PreInit(Object sender, EventArgs e)
        //{
        //    string usrID = "", compID = "";
        //    if (Request.Cookies["userID"] != null)
        //        usrID = Request.Cookies["userID"].Value;
        //    if (Request.Cookies["compID"] != null)
        //        compID = Request.Cookies["compID"].Value;
        //    if (compID == "" && usrID == "")
        //    {
        //        Response.Redirect("~/default.aspx");
        //    }

        //}
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
          

        }
        protected void btn_save_Click(object sender, ImageClickEventArgs e)
        {
            if (hdn_id.Value != "")
                lst.Add(new ViewParam() { Name = "@id", Value = hdn_id.Value });
            lst.Add(new ViewParam() { Name = "@name", Value = txt_name.Value });
            lst.Add(new ViewParam() { Name = "@seqno", Value = txt_seq.Value });
            string chk = "D";
            if (chk_status.Checked)
                chk = "A";
            lst.Add(new ViewParam() { Name = "@status", Value = chk });

            lst.Add(new ViewParam() { Name = "@type", Value = Common.Decrypt(Request.Cookies["LabRptType"].Value.ToString(), true) });
            lst.Add(new ViewParam() { Name = "@compid", Value = Common.Decrypt(Request.Cookies["compID"].Value.ToString(), true) });
            DS = DM.View.GetDataSet("[Lab_Departement_Save]", lst);
            if (DS != null && DS.Tables.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "Save('" + DS.Tables[0].Rows[0][0].ToString() + "')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), Guid.NewGuid().ToString(), "Save('-1')", true);
            }
        }
    }
}