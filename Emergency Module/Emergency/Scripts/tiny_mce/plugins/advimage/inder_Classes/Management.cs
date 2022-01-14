using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;

/// <summary>
/// Summary description for Management
/// </summary>
namespace File_Management
{
    public class Management
    {
        public Management()
        {
            //
            // TODO: Add constructor logic here
            //
        }



        public DataSet Get_folder()
        {
            DataSet ds1= new DataSet();
            ds1=DBManager.ExecuteDataset("Page_T_get_folder");
            return ds1;
        }



        public void create_folder(string folder_name)
        {
            DBManager.ExecuteNonQuery("Page_T_create_folder", folder_name); 
        }




        public Int32 Get_FolderID(string folder_name)
        {
            Int32 i = Convert.ToInt32( DBManager.ExecuteScalar("Page_T_get_folderID",folder_name));
            return i;
        }




        public string Get_ImageID()
        {
            string i = DBManager.ExecuteScalar("Page_T_get_imageID").ToString();
            i += ".jpg";
            return i;
        }




        public void file_upload(Int32 folderid, string image)
        {
            DBManager.ExecuteNonQuery("Page_T_file_upload",folderid,image);
        }




        public DataSet Show_Images(Int32 folder_ID)
        {
            DataSet  ds2 = new DataSet();
            ds2 = DBManager.ExecuteDataset("Page_T_show_image", folder_ID);
            return ds2;
        }




        public string Get_folderName(Int32 folderID)
        {
            string i = DBManager.ExecuteScalar("Page_T_get_folderName",folderID).ToString();
            return i;
        }




        public void Image_Delete(Int32 ImageID)
        {
            DBManager.ExecuteNonQuery("Page_T_image_delete", ImageID);
        }




        public string Get_ImageName(Int32 ImageID)
        {
            string s = Convert.ToString( DBManager.ExecuteScalar("Page_T_get_imageName", ImageID));
            return s;
        }

        public void Delete_Folder(Int32 id)
        {
            DBManager.ExecuteNonQuery("Page_T_delete_folder", id);
        }


    }

}

