using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GS.Common;
using GS.Data;
using System.Data;
using iTextSharp.text;
using System.IO;
using System.Net;

//for new mergefilesPDF 
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Web.Services;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Emergency.PDFHelper
{
    public partial class PdfPresSave : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(Common.Decrypt(WebConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString.ToString(), true));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string PageID = "1";
                if (Request.QueryString["PageID"] != null)
                {
                    PageID = Request.QueryString["PageID"].ToString();
                }
                string Type = "R";
                if (Request.QueryString["Type"] != null)
                {
                    Type = Request.QueryString["Type"].ToString();
                }

                string LinkID = "0";
                if (Request.QueryString["LinkID"] != null)
                {
                    LinkID = Request.QueryString["LinkID"].ToString();
                }
                List<ViewParam> _lst = new List<ViewParam>();
                DataManager dm = new DataManager();
                _lst.Add(new ViewParam() { Name = "@PageID", Value = PageID });
                _lst.Add(new ViewParam() { Name = "@Type", Value = Type });
                _lst.Add(new ViewParam() { Name = "@LinkID", Value = LinkID });
                DataSet ds = dm.View.GetDataSet("M_WebService_GETFormat", _lst);

                Session["sess_printmulPDFJson"] = ds;
                Session["sess_countPDFIndex"] = 0;

                /*Edit By 13/04/2015*/
                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        BindMultipleBody();
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        BindMultipleHeader();
                    }
                    else
                    {
                        hdn_HeaderBodyMargin.Value = "0";
                        hdn_BodyFooterMargin.Value = "0";
                    }
                }
                else
                {
                    Response.Write("<h2> There is no printing format linked to this activity currently. Please contact your Supervisor or IT Manager for assistance. !! </h2>");
                    Response.Write("<h2>Activity Name : " + ds.Tables[3].Rows[0]["formateActivities_ename"].ToString() + "</h2>");
                    Response.End();
                }
            }
        }


        private void BindMultipleBody()
        {
            DataSet ds_sess = (DataSet)Session["sess_printmulPDFJson"];
            int index = Convert.ToInt16(Session["sess_countPDFIndex"]);

            if (ds_sess.Tables[0].Rows.Count >= index + 1)
            {
                DataSet ds_rptr = new DataSet();
                DataTable dt = new DataTable("Table");
                dt.Columns.Add(new DataColumn("FormatDetail_Format", typeof(string)));
                DataRow dr = dt.NewRow();
                dr["FormatDetail_Format"] = ds_sess.Tables[0].Rows[index][0].ToString();
                dt.Rows.Add(dr);
                ds_rptr.Tables.Add(dt);

                rptr_Format.DataSource = ds_rptr;
                rptr_Format.DataBind();
                hdnfld_PageOrientation.Value = ds_sess.Tables[0].Rows[index][2].ToString();
                hdnfld_PageType.Value = ds_sess.Tables[0].Rows[index][1].ToString();
                hdn_activityID.Value = ds_sess.Tables[0].Rows[index]["FormatTemplate_kid"].ToString();

                if (ds_sess.Tables[0].Rows[index]["FormatDetail_PageLeftMargin"].ToString() == "")
                {
                    hdn_leftmargin.Value = "15";
                    hdn_rightmargin.Value = "15";
                }
                else
                {
                    hdn_leftmargin.Value = ds_sess.Tables[0].Rows[index]["FormatDetail_PageLeftMargin"].ToString();
                    hdn_rightmargin.Value = ds_sess.Tables[0].Rows[index]["FormatDetail_PageRightMargin"].ToString();
                }
            }
        }

        private void BindMultipleHeader()
        {

            DataSet ds_sess = (DataSet)Session["sess_printmulPDFJson"];
            int index = Convert.ToInt16(Session["sess_countPDFIndex"]);

            if (ds_sess.Tables[2].Rows.Count >= index + 1)
            {

                ltrl_Header.Text = ds_sess.Tables[2].Rows[index][0].ToString();
                ltrl_Footer.Text = ds_sess.Tables[2].Rows[index][1].ToString();

                ltrl_OPH.Text = ds_sess.Tables[2].Rows[index][2].ToString();
                ltrl_LPF.Text = ds_sess.Tables[2].Rows[index][3].ToString();

                if (ds_sess.Tables[2].Rows[index]["FormatDetail_HeaderBodyMargin"].ToString() == "")
                {
                    hdn_HeaderBodyMargin.Value = "0";
                }
                else
                {
                    hdn_HeaderBodyMargin.Value = ds_sess.Tables[2].Rows[index]["FormatDetail_HeaderBodyMargin"].ToString();
                }
                if (ds_sess.Tables[2].Rows[0]["FormatDetail_BodyFooterMargin"].ToString() == "")
                {
                    hdn_BodyFooterMargin.Value = "0";
                }
                else
                {
                    hdn_BodyFooterMargin.Value = ds_sess.Tables[2].Rows[index]["FormatDetail_BodyFooterMargin"].ToString();
                }
            }
            else
            {
                ltrl_Header.Text = "";
                ltrl_Footer.Text = "";

                ltrl_OPH.Text = "";
                ltrl_LPF.Text = "";

                hdn_HeaderBodyMargin.Value = "0";
                hdn_BodyFooterMargin.Value = "0";

            }
        }

        private void GS_ManageFileState(string filePath)
        {
            List<string> _listfilePath = new List<string>();
            if (Session["sess_pdfState"] != null)
            {
                _listfilePath = (List<string>)Session["sess_pdfState"];
            }
            _listfilePath.Add(filePath);
            Session["sess_pdfState"] = _listfilePath;
        }

        // readStream is the stream you need to read
        // writeStream is the stream you want to write to
        private void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }

        protected void btn_createPDf_Click(object sender, EventArgs e)
        {
            //if (hdn_count.Value == "1")
            //{
            try
            {
                //string filename = Request.QueryString["regno"].ToString();
                if (!Directory.Exists(MapPath("Presc")))
                {
                    Directory.CreateDirectory(MapPath("Presc"));
                }
                string appPath = Common.Gs_GetDefaultPath();
                string DirectPath = appPath + "Presc";
                PdfHelperLib.GS_pdfHelper objHelper;
                string HTMLCode = string.Empty;
                HTMLCode = hdnfld_Page.Value;
                //string rnd = Common.GetRandomString();
                string rnd = Request.QueryString["ptmastId"].ToString() + "_" + Request.QueryString["ptOPDID"].ToString() + "_" + Request.QueryString["LinkID"].ToString();
                GS_ManageFileState(rnd + ".pdf");

                objHelper = new PdfHelperLib.GS_pdfHelper("", "", "Presc/" + rnd + ".pdf", "");


                //GS_pdfHelper objHelper = new GS_pdfHelper();
                objHelper.setHeader = hdnfld_Header.Value;
                objHelper.setFooter = hdnfld_Footer.Value;
                objHelper.OPH = hdnfld_OPH.Value;
                objHelper.LPF = hdnfld_LPF.Value;
                //objHelper.LPF = "";
                objHelper.LPF_BottmMargin = Convert.ToInt32(hdnfld_LPFHeight.Value);
                //objHelper.LPF_BottmMargin = 0;
                objHelper.OPH_TopMargin = Convert.ToInt32(hdnfld_OPHHeight.Value);
                /*Edit By 13/04/2015*/
                if (HTMLCode != "" && HTMLCode.Trim() != "")
                {
                    Rectangle rect = PageSize.GetRectangle(hdnfld_PageType.Value);
                    objHelper.ConvertHTMLToPDF(HTMLCode, Convert.ToInt32(hdn_leftmargin.Value), Convert.ToInt32(hdn_rightmargin.Value), Convert.ToInt32(hdnfld_HeaderHeight.Value) + Convert.ToInt32(hdn_HeaderBodyMargin.Value), Convert.ToInt32(hdnfld_FooterHeight.Value) + Convert.ToInt32(hdn_BodyFooterMargin.Value), rect, hdnfld_PageOrientation.Value.ToLower(), Convert.ToInt32(hdn_activityID.Value), conn);
                    //Stream ipStream= 

                    DataSet ds_sess = (DataSet)Session["sess_printmulPDFJson"];
                    int index = Convert.ToInt16(Session["sess_countPDFIndex"]);
                    int length = ds_sess.Tables["Table"].Rows.Count;
                    Session["sess_countPDFIndex"] = index + 1;
                    Session["sess_printmulPDFJson"] = ds_sess;

                    // Load all pdf files
                    List<byte[]> filesByte = new List<byte[]>();
                    List<string> _listFiles = (List<string>)Session["sess_pdfState"];
                    Session.Remove("sess_printmulPDFJson");
                    Session.Remove("sess_pdfState");
                    Session.Remove("sess_countPDFIndex");
                    foreach (string file in _listFiles)
                    {
                        filesByte.Add(File.ReadAllBytes(MapPath("Presc/") + file));
                    }
                    MergeFilesPDF(filesByte, rnd + ".pdf");
                }
                /*Edit By 13/04/2015*/
                else
                {
                    Response.Write("<h2> No Format Created !!! Please First Create a Format. </h2>");
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
                Response.End();
            }
            //}
            //else
            //    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "Teds();", true);
        }

        public void MergeFilesPDF(List<byte[]> sourceFiles, string PDfName)
        {
            string applicationpath = MapPath("Presc/");
            Document document = new Document(PageSize.A5);
            try
            {
                try
                {
                    // Initialize pdf writer
                    //PdfWriter writer = PdfWriter.GetInstance(document, HttpContext.Current.Response.OutputStream);

                    PdfWriter writer = null;
                    if (PDfName == string.Empty || PDfName == null)
                    {
                        writer = PdfWriter.GetInstance(document, HttpContext.Current.Response.OutputStream);
                        PdfAction action = new PdfAction(PdfAction.PRINTDIALOG);
                        writer.SetOpenAction(action);
                    }
                    else if (PDfName != null)
                        writer = PdfWriter.GetInstance(document, new FileStream(applicationpath + PDfName, FileMode.Create));

                    // Open document to write
                    document.Open();
                    PdfContentByte content = writer.DirectContent;

                    // Iterate through all pdf documents
                    for (int fileCounter = 0; fileCounter < sourceFiles.Count; fileCounter++)
                    {
                        // Create pdf reader
                        PdfReader reader = new PdfReader(sourceFiles[fileCounter]);
                        int numberOfPages = reader.NumberOfPages;

                        // Iterate through all pages
                        for (int currentPageIndex = 1; currentPageIndex <= numberOfPages; currentPageIndex++)
                        {
                            // Determine page size for the current page
                            document.SetPageSize(reader.GetPageSizeWithRotation(currentPageIndex));
                            // Create page
                            document.NewPage();
                            PdfImportedPage importedPage = writer.GetImportedPage(reader, currentPageIndex);
                            // Determine page orientation
                            int pageOrientation = reader.GetPageRotation(currentPageIndex);
                            if ((pageOrientation == 90) || (pageOrientation == 270))
                            {
                                content.AddTemplate(importedPage, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(currentPageIndex).Height);
                            }
                            else
                            {
                                content.AddTemplate(importedPage, 1f, 0, 0, 1f, 0, 0);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception("There has an unexpected exception occured during the pdf merging process.", exception);
                }
            }
            finally
            {
                document.Close();
                string pp = Server.MapPath("~/Prescription/Prescription/" + Request.QueryString["ptmastId"].ToString() + "/" + Request.QueryString["ptOPDID"].ToString() + "/");

                if (Request.QueryString["ptmastId"] != null && Request.QueryString["ptmastId"].ToString() != "" && Request.QueryString["ptOPDID"] != null && Request.QueryString["ptOPDID"].ToString() != "")
                {
                    if (File.Exists(applicationpath + PDfName))
                    {

                        if (!Directory.Exists(pp))
                            Directory.CreateDirectory(pp);
                        File.Copy(applicationpath + PDfName, pp + PDfName, true);
                    }
                    File.Delete(applicationpath + PDfName);

                    List<ViewParam> _lst = new List<ViewParam>();
                    DataManager dm = new DataManager();
                    _lst.Add(new ViewParam() { Name = "@path", Value = "/Prescription/Prescription/" + Request.QueryString["ptmastId"].ToString() + "/" + Request.QueryString["ptOPDID"].ToString() + "/" + PDfName });
                    _lst.Add(new ViewParam() { Name = "@usrid", Value = Common.Decrypt(Request.Cookies["MedID"].Value, true) });
                    _lst.Add(new ViewParam() { Name = "@ptmast", Value = Request.QueryString["ptmastId"].ToString() });
                    _lst.Add(new ViewParam() { Name = "@ptopdid", Value = Request.QueryString["ptOPDID"].ToString() });
                    DataSet ds = dm.View.GetDataSet("M_PrescriptionPDF_save", _lst);
                }
                string filePath = pp + PDfName;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.WriteFile(filePath);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
                hdnfld_Page.Value = "";
                //hdn_count.Value = "0";
            }
        }
    }
}