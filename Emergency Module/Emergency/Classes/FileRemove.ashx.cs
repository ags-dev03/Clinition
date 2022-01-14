using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Emergency.Classes
{
    /// <summary>
    /// Summary description for FileRemove
    /// </summary>
    public class FileRemove : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {

                string tempPath = context.Server.MapPath(context.Request["Directory"]);
                string remove = context.Request["flag"];
                if (remove == "All")
                {
                    if (Directory.Exists(tempPath))
                        Directory.Delete(tempPath, true);

                    Directory.CreateDirectory(tempPath);
                    string copyPath = context.Server.MapPath(context.Request["Copy"]);
                    if (Directory.Exists(copyPath))
                    {
                        string[] files = Directory.GetFiles(copyPath);
                        foreach (var f in files)
                        {
                            FileInfo ff = new FileInfo(f);
                            File.Copy(f, Path.Combine(tempPath, ff.Name));
                        }
                    }
                }
                else
                {
                    if (Directory.GetFiles(tempPath, "*").Length > 0)
                    {
                        string file = context.Request["Num"];
                        if (file == "-1")
                        {
                            DirectoryInfo di = new DirectoryInfo(tempPath);
                            foreach (FileInfo ff in di.GetFiles())
                            {
                                ff.Delete();
                            }
                        }
                        else
                        {
                            //string[] files = Directory.GetFiles(tempPath);
                            File.Delete(Path.Combine(tempPath, file));
                            //bool flag = false;

                            //foreach (var f in files)
                            //{
                            //    FileInfo ff = new FileInfo(f);
                            //    string ext = ff.Extension;
                            //    string name = ff.Name;

                            //    //if (flag)
                            //    //{
                            //    //    File.Move(f, Path.Combine(tempPath, string.Concat(fileNum, ext)));
                            //    //    File.Delete(f);
                            //    //    fileNum++;
                            //    //}
                            //    //if (name.Trim() == string.Concat(fileNum, ext) && !flag)
                            //    //{
                            //    //    File.Delete(f);
                            //    //    flag = true;
                            //    //}
                            //}
                        }
                    }
                }

            }
            catch (Exception ex)
            {

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