using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRCoder;
using System.IO;
using System.Drawing;


public partial class Default6 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        string code = txtInput.Text;
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new QRCode(qrCodeData);

        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        imgBarCode.Height = 150;
        imgBarCode.Width = 150;

        System.Web.UI.WebControls.Image Imgqrcode = new System.Web.UI.WebControls.Image();
        using (Bitmap bitMap = qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                string result = Convert.ToBase64String(byteImage, 0, byteImage.Length); ;
                CreateImage(result.ToString());
            }
            plQRCode.Controls.Add(imgBarCode);
        }

    }

    public string CreateImage(string Byt)
    {

        try
        {
            byte[] data = Convert.FromBase64String(Byt);

            var filename = Convert.ToString(System.Guid.NewGuid()).Substring(0, 5) + Convert.ToString(System.Guid.NewGuid()).Substring(0, 5) + System.DateTime.Now.ToString("FFFFFF") + System.DateTime.Now.Minute + ".png";// +System.DateTime.Now.ToString("fffffffffff") + ".png";
            var file = HttpContext.Current.Server.MapPath("~/AppImages/" + filename);
            System.IO.File.WriteAllBytes(file, data);
            string ImgName = ".../profileimages/" + filename;

            return filename;
        }
        catch (Exception e)
        {
            return "Error";

        }



    }
 
}