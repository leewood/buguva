using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoggedIn_AdminZone_EditProduct : ExtendedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
       // int id = (int)e.Values["id"];
        int id = (int)e.Keys[0];
        FileUpload upload = (FileUpload)FormView1.FindControl("FileUpload1");
        if ((upload.HasFile) && (upload.PostedFile != null))
        {

            HttpPostedFile File = upload.PostedFile;
            Byte[] imgByte = new Byte[File.ContentLength];
            File.InputStream.Read(imgByte, 0, File.ContentLength);

            MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
            Product product = context.Products.First(p => p.id == id);
            //Product product = context.Products.Where(p => p.id == id).First();
            product.Picture = imgByte;            
            context.SubmitChanges();
        }
    }

    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PublicNews.aspx?category=" + Request.Params["category"]);
    }
}
