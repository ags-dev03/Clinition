using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml;




namespace Emergency.Classes
{
    public class getDefaultConnection
    {
        public static string Gs_GetConnectionString()
        {
            XDocument xDoc = XDocument.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
            string dbName =HttpContext.Current.Request.Cookies["dbName"].Value;
            return Common.Decrypt(xDoc.Descendants(dbName).FirstOrDefault().Value, true);
        }

        public static string Gs_GetCompHead()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
            if (xmlDoc.GetElementsByTagName("CompHead").Count == 0)
            {
                XmlElement ele = xmlDoc.CreateElement("CompHead");
                XmlAttribute attr = xmlDoc.CreateAttribute("providername");
                attr.InnerText = "CompHead";
                ele.Attributes.Append(attr);
                ele.InnerText = "Hospital / Emergency Name";
                xmlDoc.DocumentElement.AppendChild(ele);
                xmlDoc.Save(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));

                return "Hospital / Emergency Name";
            }
            else
            {
                XDocument xDoc = XDocument.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
                return xDoc.Descendants("CompHead").FirstOrDefault().Value;
            }
        }
        public static string Gs_GetCompName()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
            if (xmlDoc.GetElementsByTagName("CompName").Count == 0)
            {

                XmlElement ele = xmlDoc.CreateElement("CompName");
                XmlAttribute attr = xmlDoc.CreateAttribute("providername");
                attr.InnerText = "CompName";
                ele.Attributes.Append(attr);
                ele.InnerText = "Company Name";
                xmlDoc.DocumentElement.AppendChild(ele);
                xmlDoc.Save(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));

                return "Company Name";
            }
            else
            {
                XDocument xDoc = XDocument.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
                return xDoc.Descendants("CompName").FirstOrDefault().Value;
            }
        }

        public static string Gs_GetFontFactory()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
            if (xmlDoc.GetElementsByTagName("FontSetting").Count == 0)
            {
                XmlElement ele = xmlDoc.CreateElement("FontSetting");
                XmlAttribute attr = xmlDoc.CreateAttribute("providername");
                attr.InnerText = "FontSetting";
                ele.Attributes.Append(attr);
                ele.InnerText = "C:\\WINDOWS\\Fonts";
                xmlDoc.DocumentElement.AppendChild(ele);
                xmlDoc.Save(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));

                return "C:\\WINDOWS\\Fonts";
            }
            else
            {
                XDocument xDoc = XDocument.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
                return xDoc.Descendants("FontSetting").FirstOrDefault().Value;
            }
        }

        public static string Gs_GetDefaultPath(Boolean ISdicomPath = true)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));

            if (ISdicomPath)
            {
                if (xmlDoc.GetElementsByTagName("apppath").Count == 0)
                {
                    XmlElement ele = xmlDoc.CreateElement("apppath");
                    XmlAttribute attr = xmlDoc.CreateAttribute("providername");
                    attr.InnerText = "apppath";
                    ele.Attributes.Append(attr);
                    ele.InnerText = HttpContext.Current.Request.MapPath("~/HMS/GS_MyData/");
                    xmlDoc.DocumentElement.AppendChild(ele);
                    xmlDoc.Save(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));

                    return HttpContext.Current.Request.MapPath("~/HMS/GS_MyData/");
                }
                else
                {
                    XDocument xDoc = XDocument.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
                    return xDoc.Descendants("apppath").FirstOrDefault().Value.Trim();
                }
            }
            else
            {
                if (xmlDoc.GetElementsByTagName("DicomPath").Count == 0)
                {
                    XmlElement ele = xmlDoc.CreateElement("DicomPath");
                    XmlAttribute attr = xmlDoc.CreateAttribute("providername");
                    attr.InnerText = "DicomPath";
                    ele.Attributes.Append(attr);
                    ele.InnerText = HttpContext.Current.Request.MapPath("~/dicomdata/");
                    xmlDoc.DocumentElement.AppendChild(ele);
                    xmlDoc.Save(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));

                    return HttpContext.Current.Request.MapPath("~/dicomdata/");
                }
                else
                {
                    XDocument xDoc = XDocument.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
                    return xDoc.Descendants("DicomPath").FirstOrDefault().Value.Trim();
                }
            }
        }

        public static string Gs_GetDefaultViewPath()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
            if (xmlDoc.GetElementsByTagName("apppathView").Count == 0)
            {
                XmlElement ele = xmlDoc.CreateElement("apppathView");
                XmlAttribute attr = xmlDoc.CreateAttribute("providername");
                attr.InnerText = "apppathView";
                ele.Attributes.Append(attr);
                ele.InnerText = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/HMS/GS_MyData/";
                xmlDoc.DocumentElement.AppendChild(ele);
                xmlDoc.Save(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));

                return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/HMS/GS_MyData/";
            }
            else
            {
                XDocument xDoc = XDocument.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
                return xDoc.Descendants("apppathView").FirstOrDefault().Value.Trim();
            }
        }

        public static string GetpathFormXml(string XML_Path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));

            if (xmlDoc.GetElementsByTagName(XML_Path).Count == 0)
            {
                XmlElement ele = xmlDoc.CreateElement(XML_Path);
                XmlAttribute attr = xmlDoc.CreateAttribute("providername");
                attr.InnerText = XML_Path;
                ele.Attributes.Append(attr);
                ele.InnerText = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/HMS/GS_MyData/";
                xmlDoc.DocumentElement.AppendChild(ele);
                xmlDoc.Save(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));

                return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/HMS/GS_MyData/";
            }
            else
            {
                XDocument xDoc = XDocument.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
                return xDoc.Descendants(XML_Path).FirstOrDefault().Value.Trim();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string Gs_GetWellConnectionString()
        {
            XDocument xDoc = XDocument.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
            return Common.Decrypt(xDoc.Descendants("GetwellVPS").FirstOrDefault().Value, true);
        }

        public static string Gs_GetDashboardType()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
            if (xmlDoc.GetElementsByTagName("dashboardtype").Count == 0)
            {
                XmlElement ele = xmlDoc.CreateElement("dashboardtype");
                XmlAttribute attr = xmlDoc.CreateAttribute("providername");
                attr.InnerText = "dashboardtype";
                ele.Attributes.Append(attr);
                ele.InnerText = "Old";
                xmlDoc.DocumentElement.AppendChild(ele);
                xmlDoc.Save(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));

                return "Old";
            }
            else
            {
                XDocument xDoc = XDocument.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
                return xDoc.Descendants("dashboardtype").FirstOrDefault().Value;
            }
        }
        public static string Gs_GetPathFromDbCredential(string whichpart)
        {
            try
            {
                XDocument xDoc = XDocument.Load(HttpContext.Current.Request.MapPath("~/dbCredential.xml"));
                return xDoc.Descendants(whichpart).FirstOrDefault().Value.Trim();
            }
            catch (Exception ex)
            {
                return "";
            }
        }


    }
}
