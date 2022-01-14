using TemplateParser;
using GS.Common;
using GS.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Web.UI;

namespace Emergency.Classes
{
    public class Email
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PageID">ID of the Page, Example PageID = 4 for Registeration.</param>
        /// <param name="FEPR_Type">The Type You have defined in Your Addon Stored Procedure. Example FEPR_Type = 'R' for Receipt defined in Procedure.</param>
        /// <param name="CompID">CompanyID</param>
        /// <param name="LinkID">According Stored Proc, if Default then 0 else the ID you have defined in Stored Proc.</param>
        /// <param name="ds_Data">The Dynamic Data, which you want to Bind in Email Varriables. The Proc should be the same defined for Template while creating.</param>
        /// <param name="str_EmailTo">Email addres whom you want to send Email, for multiple use comma (,) to Seprate.</param>
        /// <param name="pAttacments">String array off attachment with Full Absoulate Path, If no Attachment then Pass null.</param>
        public void GS_SendEmail(int PageID, string FEPR_Type, int CompID, int LinkID, DataTable ds_Data, string str_EmailTo, List<string> pAttacments)
        {
            DataManager dm = new DataManager();
            DataSet ds = new DataSet();
            List<ViewParam> _lstParam = new List<ViewParam>();
            _lstParam.Add(new ViewParam() { Name = "@PageID", Value = PageID });
            _lstParam.Add(new ViewParam() { Name = "@FEPR_Type", Value = FEPR_Type });
            _lstParam.Add(new ViewParam() { Name = "@FEPR_CompID", Value = CompID });
            _lstParam.Add(new ViewParam() { Name = "@FEPR_LinkID", Value = LinkID });
            ds = dm.View.GetDataSet("[addon_FormatEditor_EmailFormat_Get]", _lstParam);
            string path = HttpContext.Current.Request.MapPath("~/HMS/GS_MyData/" + HttpContext.Current.Request.Cookies["dbName"].Value.ToString() + "/GS_EmailTemplates/" + ds.Tables[0].Rows[0]["FEPR_FormatID"].ToString() + ".html");
            //InderMessagebox.WebMsgBox.Show(path);
            string configPath = HttpContext.Current.Request.MapPath("~/HMS/GS_MyData/" + HttpContext.Current.Request.Cookies["dbName"].Value.ToString() + "/GS_EmailTemplates/EmailConfiguration.xml");
            //InderMessagebox.WebMsgBox.Show(configPath);
            sendMail(path, ds_Data, configPath, str_EmailTo, "", CompID.ToString(), pAttacments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pTemplate">Associated Path of the Format (ed: ~/HMS/GS_MyData/CompName/GS_EmailTemplates/57.html)</param>
        /// <param name="ds">Dynamic values (DataSet) which has been Parsed in HTML Format Email</param>
        /// <param name="pEmailConfig">Email Configuration File</param>
        /// <param name="pEmailTo">Address : To</param>
        /// <param name="errTemplate"></param>
        /// <param name="CompID">CompanyID</param>
        /// <param name="attacments">Array of the Attached Files (The Path should be Absoulate, mean (mappath("/Path"))</param>
        private void sendMail(string pTemplate, DataTable ds, string pEmailConfig, string pEmailTo, string errTemplate, string CompID, List<string> attacments)
        {

            #region Craete the Template Varriables from Database
            XElement xeEditor = GS_GetXElement(ds);
            #endregion

            Hashtable templateVars = new Hashtable();
            foreach (XElement xe in xeEditor.Descendants("dbField"))
            {
                if (ds.Columns.Contains(xe.Value.ToString()))
                    templateVars.Add(xe.Value.ToString(), ds.Rows[0][xe.Value.ToString()].ToString());
            }
            Parser parser = new Parser(pTemplate, templateVars);
            XDocument xDocConfig = XDocument.Load(pEmailConfig);
            XElement ele = xDocConfig.Elements("Configuration").FirstOrDefault();
            MailMessage message = new MailMessage();
            message.From = new MailAddress(ele.Element("UserName").Value);
            message.CC.Add(new MailAddress(ele.Element("UserName").Value));
            message.To.Add(new MailAddress(pEmailTo));
            message.Subject = "Your Subject Goes Here!!!";
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Body = parser.Parse();
            message.Priority = System.Net.Mail.MailPriority.High;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Host = ele.Element("Host").Value;
            client.Port = Convert.ToInt16(ele.Element("Port").Value);
            client.EnableSsl = Convert.ToBoolean(ele.Element("EnableSSL").Value);
            client.Credentials = new System.Net.NetworkCredential(ele.Element("UserName").Value, ele.Element("Password").Value);
            if (attacments != null)
            {
                foreach (var item in attacments)
                {
                    Attachment attach = new Attachment(item);
                    message.Attachments.Add(attach);
                }
            }
            client.Send(message);
        }

        public static void SendMail(string Emailto, string Subject, string messageBody, string attpath)
        {
            try
            {
                XDocument xDoc = XDocument.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
                string FromID = Common.Decrypt(xDoc.Descendants("EmailID").FirstOrDefault().Value.Trim(), true);
                string Password = Common.Decrypt(xDoc.Descendants("EmailPasswd").FirstOrDefault().Value.Trim(), true);
                string HostName = "smtp.gmail.com";
                string PortNo = "587";

                string EnableSSL = "Y";

                if (Emailto != null && Emailto != "")
                {
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(FromID);
                    message.To.Add(new MailAddress(Emailto));
                    message.Subject = Subject;
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.Body = messageBody;
                    if (attpath != null && attpath != "")
                    {
                        Attachment attachment = new Attachment(HttpContext.Current.Request.MapPath(attpath));
                        message.Attachments.Add(attachment);
                    }
                    message.Priority = MailPriority.High;
                    message.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient();
                    client.Host = HostName;
                    client.Port = Convert.ToInt16(PortNo);
                    if (EnableSSL.ToString().Trim() == "Y")
                        client.EnableSsl = true;
                    else
                        client.EnableSsl = false;
                    client.Credentials = new System.Net.NetworkCredential(FromID, Password);
                    client.Send(message);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

        }

        public static XElement GS_GetXElement(DataTable ds)
        {
            DataTable dt = new DataTable();
            DataColumn Column_Name = new DataColumn("dbField", typeof(System.String));
            dt.TableName = "Table1";
            dt.Columns.Add(Column_Name);
            DataRow row = null;
            foreach (DataColumn col in ds.Columns)
            {
                row = dt.NewRow();
                row[Column_Name] = col.ColumnName;
                dt.Rows.Add(row);
            }
            XElement container = new XElement("dbField");
            using (XmlWriter w = container.CreateWriter())
            {
                dt.WriteXml(w, System.Data.XmlWriteMode.WriteSchema, true);
            }
            return container;
        }
    }
}