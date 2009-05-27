using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoggedIn_AdminZone_NewProduct : ExtendedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ProductsList.aspx?category=" + Request.Params["category"]);
    }
    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
    }
    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
       // int id = (int)e.Values["id"];
        string name = e.Values["Name"].ToString();
        string currency = e.Values["Currency"].ToString();
        decimal price = Decimal.Parse(e.Values["Price"].ToString());
        string category = e.Values["Category"].ToString();
        FileUpload upload = (FileUpload)FormView1.FindControl("FileUpload1");
        if ((upload.HasFile) && (upload.PostedFile != null))
        {

            HttpPostedFile File = upload.PostedFile;
            Byte[] imgByte = new Byte[File.ContentLength];
            File.InputStream.Read(imgByte, 0, File.ContentLength);

            MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
            Product product = context.Products.First(p => (p.Name == name) && (p.Price == price) && (p.Currency == currency) && (p.Category == category));
            //Product product = context.Products.Where(p => p.id == id).First();
            product.Picture = imgByte;            
            context.SubmitChanges();
        }
        Response.Redirect("~/ProductsList.aspx?category=" + Request.Params["category"]);
    }
}
