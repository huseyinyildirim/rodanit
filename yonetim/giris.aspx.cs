using System;
using System.Linq;
using System.Web.UI;

namespace rodanit_com.yonetim
{
    public partial class giris : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                tbl_admin tbl = db.tbl_admin.First(k => k.id == 1);
                tbl.sifre = Class.Fonksiyonlar.Sifrele("123456");
                db.SaveChanges();
            }*/
        }

        protected void btn_GirisYap_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                KullaniciDenetle();
            }
            else
            {
                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Güvenlik kodunu kontrol ediniz.", "giris.aspx");
            }
        }

        protected void KullaniciDenetle()
        {
            string KullaniciAdi = Class.Fonksiyonlar.StringIslemleri(Class.Sabitler.StringIslemleri.StringIslemTipleri.SQLTemizle, txt_kullanici.Text);

            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from a in db.tbl_admin
                           where a.mail == KullaniciAdi
                           select new
                           {
                               a.id
                           }).AsEnumerable();

                if (SQL.Any())
                {
                    SifreDenetle();
                }
                else
                {
                    Class.Fonksiyonlar.JavaScript.MesajKutusu("Kullanıcı adı bulunamadı!");
                }
            }
        }

        protected void SifreDenetle()
        {
            string KullaniciAdi = Class.Fonksiyonlar.StringIslemleri(Class.Sabitler.StringIslemleri.StringIslemTipleri.SQLTemizle, txt_kullanici.Text);
            string Sifre = Class.Fonksiyonlar.StringIslemleri(Class.Sabitler.StringIslemleri.StringIslemTipleri.SQLTemizle, txt_sifre.Text);
            Sifre = Class.Fonksiyonlar.Sifrele(Sifre);

            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from a in db.tbl_admin
                           where a.mail == KullaniciAdi && a.sifre == Sifre
                           select new
                           {
                               a.id
                           }).AsEnumerable();

                if (SQL.Any())
                {
                    int kullanici_id = SQL.Select(p => p.id).FirstOrDefault();

                    Class.Yonetim.Fonksiyonlar.OturumIslemleri.CookieOlustur("GirisYonetim", "88888888");
                    Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Kimlik doğrulaması başarılı. Kontrol paneline yönlendiriliyorsunuz!", "Default.aspx");

                    Class.Yonetim.Fonksiyonlar.OturumIslemleri.CookieOlustur("KullaniciID", kullanici_id.ToString());

                    tbl_admin tbl = db.tbl_admin.First(k => k.id == kullanici_id);
                    tbl.son_giris = DateTime.Now;
                    db.SaveChanges();
                }
                else
                {
                    Class.Fonksiyonlar.JavaScript.MesajKutusu("Şifreniz hatalıdır!");
                }
            }
        }
    }
}