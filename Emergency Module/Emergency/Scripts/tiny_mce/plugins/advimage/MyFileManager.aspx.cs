using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using File_Management;
using System.IO;

namespace Emergency.Scripts.tiny_mce.plugins.advimage
{
    public partial class MyFileManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            delFolder.Visible = false;
            if (!Page.IsPostBack)
            {

                BindData();
            }

        }
        public void BindData()
        {
            DataSet ds1, ds2 = new DataSet();
            Management obj_management = new Management();
            ds1 = obj_management.Get_folder();
            GridView1.DataSource = ds1;
            GridView1.DataBind();
            if (ds1.Tables[0].Rows.Count > 0)
            {
                Session["folderid"] = ds1.Tables[0].Rows[0][0].ToString();
                Session["folderName"] = obj_management.Get_folderName(Convert.ToInt32(Session["folderid"]));
                ds2 = obj_management.Show_Images(Convert.ToInt32(Session["folderid"]));
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    DataList1.DataSource = ds2;
                    DataList1.DataBind();
                    delFolder.Visible = false;
                }
                else
                {
                    delFolder.Visible = true;
                }
            }
            ddlFolderName.Items.Clear();
            ddlFolderName.Items.Add("Select Folder Name");
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                ddlFolderName.Items.Add(ds1.Tables[0].Rows[i]["FolderName"].ToString());
            }
            txtCreateFolder.Text = "";
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataSet ds = new DataSet();
            Int32 id;
            string name;
            Management obj_management = new Management();
            name = e.CommandName;
            Session["folderName"] = name;
            id = Convert.ToInt32(e.CommandArgument);
            ds = obj_management.Show_Images(id);
            if (ds.Tables[0].Rows.Count == 0)
            {
                Session["delForderID"] = id;
                Session["delFolderName"] = name;
                delFolder.Visible = true;
            }


            DataList1.DataSource = ds;
            DataList1.DataBind();

        }
        protected void imgbtnCreateFolder_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCreateFolder.Text == "")
            {
                lblerrors.Text = "Please Enter Folder Name";
                errors.Visible = true;

                return;
            }
            else
            {
                errors.Visible = false;
                if (Directory.Exists(MapPath("~/Scripts/tiny_mce/plugins/advimage/images-editor/" + txtCreateFolder.Text)))
                {
                    return;
                }
                else
                {
                    Directory.CreateDirectory(MapPath("~/Scripts/tiny_mce/plugins/advimage/images-editor/" + txtCreateFolder.Text));
                    Management obj_management = new Management();
                    obj_management.create_folder(txtCreateFolder.Text);
                    BindData();
                }
            }
        }
        protected void imgbtnUpload_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlFolderName.SelectedItem.Text == "Select Folder Name")
            {
                lblerrors.Text = "Please Select Folder First";
                errors.Visible = true;
                return;
            }
            else
            {
                if (flupload.HasFile)
                {
                    errors.Visible = false;
                    Management obj_management = new Management();
                    Int32 FolderID;
                    string Image;
                    FolderID = obj_management.Get_FolderID(ddlFolderName.SelectedValue);
                    Image = obj_management.Get_ImageID();
                    flupload.SaveAs(MapPath("~/Scripts/tiny_mce/plugins/advimage/images-editor/" + ddlFolderName.SelectedValue + "/" + Image));
                    obj_management.file_upload(FolderID, Image);
                    BindData();
                }
                else
                {
                    lblerrors.Text = "Please Select Jpg Image You Want To Upload";
                    errors.Visible = true;
                }
            }
        }
        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                Management obj_management = new Management();
                string ImageName = obj_management.Get_ImageName(Convert.ToInt32(e.CommandArgument));
                obj_management.Image_Delete(Convert.ToInt32(e.CommandArgument));

                try
                {
                    File.Delete(MapPath("~/Scripts/tiny_mce/plugins/advimage/images-editor/" + Session["folderName"] + "/" + ImageName));
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                    //throw new Exception("Deleted Successfully");
                }
                BindData();

            }
            else
            {
                string path;
                path = "/Scripts/tiny_mce/plugins/advimage/" + e.CommandName;
                Session["path"] = path;
                DataList1.Attributes.Add("OnClick", "javascript:window.opener.returnToOpener('" + Session["path"] + "');window.close();");
                lblerrors.Text = "Kindly Click Image Ones Again";
                errors.Visible = true;
            }

        }
        protected void imgbtnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            BindData();
        }


        protected void btnDelEmpty_Click(object sender, ImageClickEventArgs e)
        {
            string name = Session["delFolderName"].ToString();
            Int32 id = Convert.ToInt32(Session["delForderID"]);
            Session.Remove("delFolderName");
            Session.Remove("delFolderID");
            Management obj_management = new Management();
            obj_management.Delete_Folder(id);
            Directory.Delete(MapPath("~/Scripts/tiny_mce/plugins/advimage/images-editor/" + name), true);
            BindData();
        }
    }
}