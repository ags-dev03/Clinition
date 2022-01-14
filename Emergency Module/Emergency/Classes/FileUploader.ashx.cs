using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Emergency.Classes
{
    /// <summary>
    /// Summary description for FileUploader
    /// </summary>
    public class FileUploader : IHttpHandler, IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            HttpFileCollection hfc = context.Request.Files;


            string savepath = context.Server.MapPath(context.Request["Directory"]);
            if (!Directory.Exists(savepath))
                Directory.CreateDirectory(savepath);
            DirectoryInfo di = new DirectoryInfo(savepath);
            if (context.Request["Num"].ToString() == "profile")
            {

                foreach (FileInfo ff in di.GetFiles())
                {
                    ff.Delete();
                }
            }
            string filename = string.Empty;
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile postedFile = context.Request.Files[i];

                savepath = "";
                savepath = context.Server.MapPath(context.Request["Directory"]);

                filename = Common.GetRandomString() + Path.GetExtension(postedFile.FileName);

                postedFile.SaveAs(savepath + @"\" + filename);


            }
            if (context.Request["Num"].ToString() == "profile")
            {
                string file = context.Request["Directory"].ToString() + "/" + filename;
                string type = context.Request["Type"].ToString();
                DataTable table = new DataTable();
                table.Columns.Add("path", typeof(string));
                Common.FileSave(Convert.ToInt32(context.Request["id"].ToString()), file, type, table);
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}