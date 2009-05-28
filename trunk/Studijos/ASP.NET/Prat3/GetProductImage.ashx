<%@ WebHandler Language="C#" Class="GetProductImage" %>

using System;
using System.Web;
using System.Linq;
using System.Data.Linq;

public class GetProductImage : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        Int32 picid;
        if (context.Request.QueryString["id"] != null)
            picid = Convert.ToInt32(context.Request.QueryString["id"]);
        else
            throw new ArgumentException("No parameter specified");
 
        context.Response.ContentType = "image/jpeg";
        System.IO.Stream strm = ShowAlbumImage(picid);
        byte[] buffer = new byte[4096];
        int byteSeq = strm.Read(buffer, 0, 4096);
 
        while (byteSeq > 0)
        {
            context.Response.OutputStream.Write(buffer, 0, byteSeq);
            byteSeq = strm.Read(buffer, 0, 4096);
        }
        //context.Response.BinaryWrite(buffer);   
    }

    public System.IO.Stream ShowAlbumImage(int picid)
    {
        MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
        var d = (from g in context.Products where g.id == picid select g.Picture).First();    
        try
        {
            return new System.IO.MemoryStream(d.ToArray());
        }
        catch
        {
            return null;
        }
        finally
        {

        }
    }    
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}