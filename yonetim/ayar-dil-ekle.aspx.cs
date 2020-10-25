using System;
using System.Linq;
using System.Web;

namespace rodanit_com.yonetim
{
    public partial class ayar_dil_ekle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Class.Yonetim.Fonksiyonlar.OturumIslemleri.CookieKontrol();


        }

        protected void btn_kayitekle_Click(object sender, EventArgs e)
        {
            try
            {
                using (BaglantiCumlesi db = new BaglantiCumlesi())
                {
                    tbl_dil tbl = new tbl_dil();
                    tbl.dil = txt_dil.Text.Trim();
                    tbl.dil_kodu = txt_dil_kodu.Text.Trim();
                    tbl.dosya_ad = txt_dosya_ad.Text.Trim();
                    tbl.kodlama = txt_kodlama.Text.Trim();
                    tbl.culture = txt_culture_kod.Text.Trim();

                    tbl.onay = Class.Fonksiyonlar.StringToBool(Request.Form["ddl_onay"].ToString());
                    tbl.admin_id_gun = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                    db.AddTotbl_dil(tbl);
                    db.SaveChanges();
                }
                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Başarıyla düzenlenmiştir.", "ayar-dil.aspx");
            }
            catch (Exception ex)
            {
                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir(ex.Message, "ayar-dil.aspx");
            }
        }
    }
}