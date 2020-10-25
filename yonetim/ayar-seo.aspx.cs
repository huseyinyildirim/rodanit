using System;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace rodanit_com.yonetim
{
    public partial class ayar_seo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Class.Yonetim.Fonksiyonlar.OturumIslemleri.CookieKontrol();

            if (!Page.IsPostBack)
            {
                Kayit();
            }
        }

        protected void Kayit()
        {
            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from a in db.tbl_ayar
                           where a.id == 1
                           select new
                           {
                               a.id,
                               a.seo_baslik,
                               a.seo_anahtar,
                               a.seo_aciklama,
                               a.tarih_gun,
                               guncelleyen = db.tbl_admin.Where(b => b.id == a.admin_id_gun).Select(b => b.ad).FirstOrDefault() + " " + db.tbl_admin.Where(b => b.id == a.admin_id_gun).Select(b => b.soyad).FirstOrDefault()
                           }).AsEnumerable();

                if (SQL.Any())
                {
                    foreach (var item in SQL)
                    {
                        form_title.Text = HttpUtility.HtmlDecode(item.seo_baslik);
                        form_aciklama.Text = HttpUtility.HtmlDecode(item.seo_aciklama);
                        form_anahtar.Text = HttpUtility.HtmlDecode(item.seo_anahtar);

                        kayitbilgi_gucelleyen.Text = item.guncelleyen;
                        kayitbilgi_guncellemetarih.Text = item.tarih_gun.ToString();
                    }
                }
            }
        }

        protected void btn_kayitekle_Click(object sender, EventArgs e)
        {
            try
            {
                using (BaglantiCumlesi db = new BaglantiCumlesi())
                {
                    tbl_ayar TblEkle = db.tbl_ayar.First(a => a.id == 1);
                    TblEkle.seo_baslik = Class.Fonksiyonlar.StringIslemleri(Class.Sabitler.StringIslemleri.StringIslemTipleri.SQLTemizle, form_title.Text);
                    TblEkle.seo_anahtar = Class.Fonksiyonlar.StringIslemleri(Class.Sabitler.StringIslemleri.StringIslemTipleri.SQLTemizle, form_anahtar.Text);
                    TblEkle.seo_aciklama = Class.Fonksiyonlar.StringIslemleri(Class.Sabitler.StringIslemleri.StringIslemTipleri.SQLTemizle, form_aciklama.Text);
                    TblEkle.admin_id_gun = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                    db.SaveChanges();
                }

                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Başarıyla düzenlenmiştir.", "ayar-seo.aspx");
            }
            catch (Exception ex)
            {
                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir(ex.Message, "ayar-seo.aspx");
            }
        }
    }
}