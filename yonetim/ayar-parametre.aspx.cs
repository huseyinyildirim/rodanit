using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace rodanit_com.yonetim
{
    public partial class ayar_parametre : System.Web.UI.Page
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
                               p.guvenlik_kodu,
                               p.tarih_gun,
                               guncelleyen = db.tbl_admin.Where(b => b.id == p.admin_id_gun).Select(b => b.ad).FirstOrDefault() + " " + db.tbl_admin.Where(b => b.id == p.admin_id_gun).Select(b => b.soyad).FirstOrDefault()
                           }).AsEnumerable();

                if (SQL.Any())
                {
                    foreach (var i in SQL)
                    {
                        txt_guvenlik_kodu.Text = i.guvenlik_kodu;
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
                    tbl.guvenlik_kodu = txt_guvenlik_kodu.Text.Trim();
                    tbl.admin_id_gun = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                    db.SaveChanges();
                }

                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Başarıyla düzenlenmiştir.", "ayar-parametre.aspx");
            }
            catch (Exception ex)
            {
                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir(ex.Message, "ayar-parametre.aspx");
            }
        }
    }
}