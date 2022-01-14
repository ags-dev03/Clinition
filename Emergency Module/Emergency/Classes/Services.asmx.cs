using GS.Common;
using GS.Data;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;

namespace Emergency.Classes
{
    /// <summary>
    /// Summary description for Services
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Services : System.Web.Services.WebService
    {
        DataManager DM = new DataManager();
        List<ViewParam> lst = new List<ViewParam>();
        DataSet DS = new DataSet();
        DataTable DT = new DataTable();
        [WebMethod]
        public string GetMenuBySearch(string item, string usrID)
        {
            lst.Add(new ViewParam() { Name = "@Name", Value = item });
            lst.Add(new ViewParam() { Name = "@usrID", Value = usrID });
            DS = DM.View.GetDataSet("M_MenuSearch_byBox", lst);
            return Common.ConverDatasetToJson(DS);
        }


        [WebMethod]
        public string GetAllDepartment()
        {
            lst.Add(new ViewParam() { Name = "@LabRptType", Value = Common.Decrypt(HttpContext.Current.Request.Cookies["ERLabRptType"].Value, true) });
            lst.Add(new ViewParam() { Name = "@compid", Value = Common.Decrypt(HttpContext.Current.Request.Cookies["ERcompID"].Value, true) });
            DS = DM.View.GetDataSet("Lab_Departement_get", lst);
            return Common.ConverDatasetToJson(DS);
        }

        [WebMethod]
        public string GetDepartmentByID(string id)
        {
            lst.Add(new ViewParam() { Name = "@labdeptid", Value = id });
            DS = DM.View.GetDataSet("Lab_Departement_get", lst);
            return Common.ConverDatasetToJson(DS);
        }


        [WebMethod]
        public string DeleteUpdateDepartment(int id, string flag, bool status = false)
        {
            lst.Add(new ViewParam() { Name = "@flag", Value = flag });
            lst.Add(new ViewParam() { Name = "@id", Value = id });
            string stat = "D";
            if (status == true)
                stat = "A";
            lst.Add(new ViewParam() { Name = "@status", Value = stat });
            lst.Add(new ViewParam() { Name = "@compid", Value = Common.Decrypt(HttpContext.Current.Request.Cookies["ERcompID"].Value, true) });
            DS = DM.View.GetDataSet("Lab_Departement_Save", lst);
          
            return Common.ConverDatasetToJson(DS);
        }

    }

}


