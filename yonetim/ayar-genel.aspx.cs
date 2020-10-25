using System;
using System.Linq;
using System.Web;

namespace rodanit_com.yonetim
{
    public partial class ayar_genel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Class.Yonetim.Fonksiyonlar.OturumIslemleri.CookieKontrol();

            Kayit();
        }

        protected void Kayit()
        {
            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from p in db.tbl_ayar
                           where p.id == 1
                           select new
                           {
                               p.domain,
                               p.marka,
                               p.sirket_unvan,
                               p.yetkili,
                               p.mail,
                               p.adres,
                               p.faks,
                               p.telefon,
                               p.tarih_gun,
                               guncelleyen = db.tbl_admin.Where(b => b.id == p.admin_id_gun).Select(b => b.ad).FirstOrDefault() + " " + db.tbl_admin.Where(b => b.id == p.admin_id_gun).Select(b => b.soyad).FirstOrDefault()
                           }).AsEnumerable();

                if (SQL.Any())
                {
                    foreach (var i in SQL)
                    {
                        txt_adres.Text = i.adres;
                        txt_faks.Text = i.faks;
                        txt_telefon.Text = i.telefon;

                        txt_marka.Text = i.marka;
                        txt_mail.Text = i.mail;
                        txt_sirket_unvan.Text = i.sirket_unvan;
                        txt_yetkili.Text = i.yetkili;
                        txt_domain.Text = i.domain;

                        lit_gucelleyen.Text = i.guncelleyen;
                        lit_guncelleme_tarih.Text = i.tarih_gun.ToString();
                    }
                }
                else
                {
                    Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("tbl_ayar tablosunda 1 nolu kayıt bulunmuyor!", "default.aspx");
                }
            }
        }

        protected void btn_kayitekle_Click(object sender, EventArgs e)
        {
            try
            {
                using (BaglantiCumlesi db = new BaglantiCumlesi())
                {
                    tbl_ayar tbl = db.tbl_ayar.Where(p => p.id == 1).First();
                    tbl.adres = txt_adres.Text.Trim();
                    tbl.faks = txt_faks.Text.Trim();
                    tbl.telefon = txt_telefon.Text.Trim();

                    tbl.domain = txt_domain.Text.Trim();
                    tbl.sirket_unvan = txt_sirket_unvan.Text.Trim();
                    tbl.marka = txt_marka.Text.Trim();
                    tbl.mail = txt_mail.Text.Trim();
                    tbl.yetkili = txt_yetkili.Text.Trim();

                    tbl.admin_id_gun = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                    db.SaveChanges();
                }

                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Başarıyla düzenlenmiştir.", "ayar-genel.aspx");
            }
            catch (Exception ex)
            {
                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir(ex.Message, "ayar-genel.aspx");
            }
        }
    }
}