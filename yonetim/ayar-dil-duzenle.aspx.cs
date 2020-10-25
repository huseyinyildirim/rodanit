using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace rodanit_com.yonetim
{
    public partial class ayar_dil_duzenle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Class.Yonetim.Fonksiyonlar.OturumIslemleri.CookieKontrol();

            if (!IsPostBack)
            {

                Kayit();
            }
        }

        protected void Kayit()
        {
            if (Request.QueryString["ID"] != null && Class.Fonksiyonlar.NumerikKontrol(Request.QueryString["ID"].ToString()))
            {
                int ID = int.Parse(Request.QueryString["ID"]);

                using (BaglantiCumlesi db = new BaglantiCumlesi())
                {
                    var SQL = (from p in db.tbl_dil
                               where p.id == ID
                               select new
                               {
                                   p.dil,
                                   p.dil_kodu,
                                   p.dosya_ad,
                                   p.kodlama,
                                   p.culture,
                                   p.onay,
                                   p.tarih_ek,
                                   p.tarih_gun,
                                   ekleyen = db.tbl_admin.Where(b => b.id == p.admin_id_ek).Select(b => b.ad).FirstOrDefault() + " " + db.tbl_admin.Where(b => b.id == p.admin_id_ek).Select(b => b.soyad).FirstOrDefault(),
                                   guncelleyen = db.tbl_admin.Where(b => b.id == p.admin_id_gun).Select(b => b.ad).FirstOrDefault() + " " + db.tbl_admin.Where(b => b.id == p.admin_id_gun).Select(b => b.soyad).FirstOrDefault()
                               }).AsEnumerable();

                    if (SQL.Any())
                    {
                        foreach (var i in SQL)
                        {
                            txt_dil.Text = i.dil;
                            txt_dil_kodu.Text = i.dil_kodu;
                            txt_dosya_ad.Text = i.dosya_ad;
                            txt_kodlama.Text = i.kodlama;
                            txt_culture_kod.Text = i.culture;

                            lit_ekleyen.Text = i.ekleyen;
                            lit_kayit_tarih.Text = i.tarih_ek.ToString();
                            lit_gucelleyen.Text = i.guncelleyen;
                            lit_guncelleme_tarih.Text = i.tarih_gun.ToString();

                            ddl_onay.SelectedValue = i.onay.ToString();
                        }
                    }
                    else
                    {
                        Response.Redirect("ayar-dil.aspx");
                    }
                }
            }
        }

        protected void btn_kayitekle_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null && Class.Fonksiyonlar.NumerikKontrol(Request.QueryString["ID"].ToString()))
            {
                int ID = int.Parse(Request.QueryString["ID"]);

                try
                {
                    using (BaglantiCumlesi db = new BaglantiCumlesi())
                    {
                        tbl_dil tbl = db.tbl_dil.Where(p => p.id == ID).First();
                        tbl.dil = txt_dil.Text.Trim();
                        tbl.dil_kodu = txt_dil_kodu.Text.Trim();
                        tbl.dosya_ad = txt_dosya_ad.Text.Trim();
                        tbl.kodlama = txt_kodlama.Text.Trim();
                        tbl.culture = txt_culture_kod.Text.Trim();

                        tbl.onay = Class.Fonksiyonlar.StringToBool(Request.Form["ddl_onay"].ToString());
                        tbl.admin_id_gun = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                        db.SaveChanges();
                    }
                    Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Başarıyla düzenlenmiştir.", "ayar-dil-duzenle.aspx?ID=" + ID + "");
                }
                catch (Exception ex)
                {
                    Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir(ex.Message, "ayar-dil.aspx");
                }
            }
        }
    }
}