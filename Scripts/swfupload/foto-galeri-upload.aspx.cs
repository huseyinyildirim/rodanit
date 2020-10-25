using System;
using System.IO;
using System.Linq;
using System.Web;

namespace rodanit_com.Scripts.swfupload
{
    public partial class foto_galeri_upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int ID = int.Parse(Request["resim_id"]);
                HttpPostedFile postedFile = Request.Files["Filedata"];
                string ResimAdi = Class.Fonksiyonlar.StringIslemleri(Class.Sabitler.StringIslemleri.StringIslemTipleri.DosyaAd, Class.Yonetim.Fonksiyonlar.FotoGaleri(ID).Select(p => p.baslik).FirstOrDefault()) + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(Server.MapPath("~/upload/foto/" + ResimAdi + ""));

                using (BaglantiCumlesi db = new BaglantiCumlesi())
                {
                    tbl_resimler tbl = new tbl_resimler();
                    tbl.tip = 4;
                    tbl.tip_id = ID;
                    tbl.dosya_ad = ResimAdi;
                    tbl.varsayilan = false;
                    tbl.onay = true;
                    //tbl.admin_id_ek = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar(Class.Sabitler.Ayar.GuvenlikKodu) + "KullaniciID"].Value);
                    db.AddTotbl_resimler(tbl);
                    db.SaveChanges();
                }
            }
            catch
            {
                Response.End();
            }
        }
    }
}