using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace rodanit_com.yonetim
{
    public partial class foto_galeri_ekle : System.Web.UI.Page
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
                var SQL = (from p in db.tbl_dil
                           select new
                           {
                               p.id,
                               p.dil,
                               p.dosya_ad
                           }).AsEnumerable();

                if (SQL.Any())
                {
                    Literal lbl1 = new Literal();
                    lbl1.Text = "<div id=\"tabs\">\r\n<ul>\r\n";
                    lit_icerik.Controls.Add(lbl1);

                    Literal lbl2 = new Literal();
                    foreach (var i in SQL)
                    {
                        lbl2.Text += "<li><a href=\"#" + i.dil + "\"><img src=\"/upload/bayrak/" + i.dosya_ad + "\" alt=\"" + i.dil + "\" /> " + i.dil + "</a></li>\r\n";
                    }
                    lit_icerik.Controls.Add(lbl2);

                    Literal lbl3 = new Literal();
                    lbl3.Text = "</ul>\r\n";
                    lit_icerik.Controls.Add(lbl3);

                    foreach (var i in SQL)
                    {
                        Literal lbl4 = new Literal();
                        lbl4.Text = "<div id=\"" + i.dil + "\">";
                        lit_icerik.Controls.Add(lbl4);

                        Literal lbl17 = new Literal();
                        lbl17.Text = "<br /><br /><strong>Başlık:</strong><br />";
                        lit_icerik.Controls.Add(lbl17);

                        TextBox baslik = new TextBox();
                        baslik.ID = "txt_baslik_" + i.id;
                        baslik.Width = Unit.Pixel(300);
                        lit_icerik.Controls.Add(baslik);

                        Literal lbl7 = new Literal();
                        lbl7.Text = "<br /><br /><strong>SEO Açıklama:</strong> (Bir sayfanın tanım (description) meta etiketi, Google ve diğer arama motorlarına sayfanın ne hakkında olduğuna dair özet bilgi sağlar)<br />";
                        lit_icerik.Controls.Add(lbl7);

                        TextBox seo_aciklama = new TextBox();
                        seo_aciklama.ID = "txt_seo_aciklama_" + i.id;
                        seo_aciklama.Width = Unit.Percentage(100);
                        lit_icerik.Controls.Add(seo_aciklama);

                        Literal lbl9 = new Literal();
                        lbl9.Text = "<br /><br /><strong>SEO Anahtar:</strong> (Lütfen sayfa içeriği hakkında virgülle ayırarak, en fazla 14 kelime girebilirsiniz)<br />";
                        lit_icerik.Controls.Add(lbl9);

                        TextBox seo_anahtar = new TextBox();
                        seo_anahtar.ID = "txt_seo_anahtar_" + i.id;
                        seo_anahtar.Width = Unit.Percentage(100);
                        lit_icerik.Controls.Add(seo_anahtar);

                        Literal lbl8 = new Literal();
                        lbl8.Text = "</div>\r\n";
                        lit_icerik.Controls.Add(lbl8);
                    }

                    Literal lbl10 = new Literal();
                    lbl10.Text = "</div>\r\n";
                    lit_icerik.Controls.Add(lbl10);
                }
                else
                {
                    Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Sistemde oluşturulmuş dil yoktur!", "default.aspx");
                }
            }
        }

        protected void btn_kayitekle_Click(object sender, EventArgs e)
        {
            try
            {
                using (BaglantiCumlesi db = new BaglantiCumlesi())
                {
                    tbl_foto_galeri tbl2 = new tbl_foto_galeri();
                    tbl2.baslik = txt_tanimlama_baslik.Text.Trim();
                    tbl2.admin_id_ek = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                    tbl2.onay = Class.Fonksiyonlar.StringToBool(Request.Form["ddl_onay"].ToString());
                    db.AddTotbl_foto_galeri(tbl2);
                    db.SaveChanges();

                    var SQL = (from p in db.tbl_dil
                               select new
                               {
                                   p.id
                               }).AsEnumerable();

                    if (SQL.Any())
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var i in SQL)
                        {
                            tbl_foto_galeri_detay tbl = new tbl_foto_galeri_detay();
                            tbl.galeri_id = tbl2.id;
                            tbl.dil_id = i.id;
                            tbl.baslik = Request.Form["txt_baslik_" + i.id.ToString()].ToString().Trim();
                            tbl.seo_aciklama = Request.Form["txt_seo_aciklama_" + i.id.ToString()].ToString().Trim();
                            tbl.seo_anahtar = Request.Form["txt_seo_anahtar_" + i.id.ToString()].ToString().Trim();
                            db.AddTotbl_foto_galeri_detay(tbl);
                        }
                    }

                    db.SaveChanges();

                    Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Başarıyla eklenmiştir, şimdi fotoğraf yüklemesi için düzenleme sayfalarına yönlendiriliyorsunuz.", "foto-galeri-duzenle.aspx?ID=" + tbl2.id);
                }
            }
            catch (Exception ex)
            {
                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir(ex.Message, "foto-galeri.aspx");
            }
        }
    }
}