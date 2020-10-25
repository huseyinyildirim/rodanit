using System;
using System.Linq;
using System.Web;

namespace rodanit_com.yonetim
{
    public partial class profil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Class.Yonetim.Fonksiyonlar.OturumIslemleri.CookieKontrol();

            Kayit();
        }

        protected void Kayit()
        {
            int ID = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);

            txt_ad.Text = Class.Yonetim.Fonksiyonlar.Admin(ID).Select(p => p.ad).FirstOrDefault();
            txt_soyad.Text = Class.Yonetim.Fonksiyonlar.Admin(ID).Select(p => p.soyad).FirstOrDefault();
            txt_mail.Text = Class.Yonetim.Fonksiyonlar.Admin(ID).Select(p => p.mail).FirstOrDefault();
            lit_son_giris.Text = Class.Yonetim.Fonksiyonlar.Admin(ID).Select(p => p.son_giris).FirstOrDefault().ToString();
            ddl_onay.SelectedValue = Class.Fonksiyonlar.BoolToInteger(Class.Yonetim.Fonksiyonlar.Admin(ID).Select(p => p.onay).FirstOrDefault()).ToString();

            lit_ekleyen.Text = Class.Yonetim.Fonksiyonlar.AdminAd(int.Parse(Class.Yonetim.Fonksiyonlar.Admin(ID).Select(p => p.admin_id_ek).FirstOrDefault().ToString()));
            lit_kayit_tarih.Text = Class.Yonetim.Fonksiyonlar.Admin(ID).Select(p => p.tarih_ek).FirstOrDefault().ToString();

            if (!string.IsNullOrEmpty(Class.Yonetim.Fonksiyonlar.Admin(ID).Select(p => p.admin_id_gun).FirstOrDefault().ToString()))
            {
                lit_gucelleyen.Text = Class.Yonetim.Fonksiyonlar.AdminAd(int.Parse(Class.Yonetim.Fonksiyonlar.Admin(ID).Select(p => p.admin_id_gun).FirstOrDefault().ToString()));
            }

            lit_guncelleme_tarih.Text = Class.Yonetim.Fonksiyonlar.Admin(ID).Select(p => p.tarih_gun).FirstOrDefault().ToString();
        }

        protected void btn_kayitekle_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);

                using (BaglantiCumlesi db = new BaglantiCumlesi())
                {
                    tbl_admin tbl = db.tbl_admin.Where(p => p.id == ID).First();
                    tbl.ad = txt_ad.Text;
                    tbl.soyad = txt_soyad.Text;
                    tbl.mail = txt_mail.Text;

                    if (!string.IsNullOrEmpty(txt_sifre.Text))
                    {
                        tbl.sifre = Class.Fonksiyonlar.Sifrele(txt_sifre.Text);
                    }

                    tbl.admin_id_gun = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                    tbl.onay = Class.Fonksiyonlar.StringToBool(Request.Form["ddl_onay"].ToString());
                    db.SaveChanges();
                }

                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Başarıyla düzenlenmiştir.", "profil.aspx");
            }
            catch (Exception ex)
            {
                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir(ex.Message, "default.aspx");
            }
        }
    }
}