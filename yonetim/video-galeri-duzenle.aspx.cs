using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace rodanit_com.yonetim
{
    public partial class video_galeri_duzenle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Class.Yonetim.Fonksiyonlar.OturumIslemleri.CookieKontrol();

            Kayit();
        }

        protected void Kayit()
        {
            if (Request.QueryString["ID"] != null && Class.Fonksiyonlar.NumerikKontrol(Request.QueryString["ID"].ToString()))
            {
                int ID = int.Parse(Request.QueryString["ID"]);

                using (BaglantiCumlesi db = new BaglantiCumlesi())
                {
                    var SQL = (from p in db.tbl_video_galeri
                               where p.id == ID
                               select new
                               {
                                   p.id
                               }).AsEnumerable();

                    if (!SQL.Any())
                    {
                        Response.Redirect("video-galeri.aspx");
                    }
                }

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
                        lit_onizleme.Text = "<img style=\"border:1px dotted #CCC;\" src=\"../ashx/resim-getir.ashx?i=upload/video-gallery/" + Class.Yonetim.Fonksiyonlar.VideoGaleri(ID).Select(p => p.dosya_ad).FirstOrDefault() + "&amp;w=300&amp;h=150&amp;k=t\" />";

                        ddl_onay.SelectedValue = Class.Fonksiyonlar.BoolToInteger(Class.Yonetim.Fonksiyonlar.VideoGaleri(ID).Select(p => p.onay).FirstOrDefault()).ToString();

                        txt_tanimlama_baslik.Text = Class.Yonetim.Fonksiyonlar.VideoGaleri(ID).Select(p => p.baslik).FirstOrDefault();
                        txt_video_url.Text = Class.Yonetim.Fonksiyonlar.VideoGaleri(ID).Select(p => p.video).FirstOrDefault();

                        lit_ekleyen.Text = Class.Yonetim.Fonksiyonlar.AdminAd(int.Parse(Class.Yonetim.Fonksiyonlar.VideoGaleri(ID).Select(p => p.admin_id_ek).FirstOrDefault().ToString()));
                        lit_kayit_tarih.Text = Class.Yonetim.Fonksiyonlar.VideoGaleri(ID).Select(p => p.tarih_ek).FirstOrDefault().ToString();

                        if (!string.IsNullOrEmpty(Class.Yonetim.Fonksiyonlar.VideoGaleri(ID).Select(p => p.admin_id_gun).FirstOrDefault().ToString()))
                        {
                            lit_gucelleyen.Text = Class.Yonetim.Fonksiyonlar.AdminAd(int.Parse(Class.Yonetim.Fonksiyonlar.VideoGaleri(ID).Select(p => p.admin_id_gun).FirstOrDefault().ToString()));
                        }

                        lit_guncelleme_tarih.Text = Class.Yonetim.Fonksiyonlar.VideoGaleri(ID).Select(p => p.tarih_gun).FirstOrDefault().ToString();

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
                            lbl4.Text = "<div id=\"" + i.dil + "\"><strong>Başlık:</strong><br />";
                            lit_icerik.Controls.Add(lbl4);

                            TextBox baslik = new TextBox();
                            baslik.ID = "txt_baslik_" + i.id;
                            baslik.Text = Class.Yonetim.Fonksiyonlar.VideoGaleriDetay(ID, i.id).Select(p => p.baslik).FirstOrDefault();
                            baslik.Width = Unit.Pixel(500);
                            lit_icerik.Controls.Add(baslik);

                            Literal lbl7 = new Literal();
                            lbl7.Text = "<br /><br /><strong>SEO Açıklama:</strong> (Bir sayfanın tanım (description) meta etiketi, Google ve diğer arama motorlarına sayfanın ne hakkında olduğuna dair özet bilgi sağlar)<br />";
                            lit_icerik.Controls.Add(lbl7);

                            TextBox seo_aciklama = new TextBox();
                            seo_aciklama.ID = "txt_seo_aciklama_" + i.id;
                            seo_aciklama.Text = Class.Yonetim.Fonksiyonlar.VideoGaleriDetay(ID, i.id).Select(p => p.seo_aciklama).FirstOrDefault();
                            seo_aciklama.Width = Unit.Percentage(100);
                            lit_icerik.Controls.Add(seo_aciklama);

                            Literal lbl9 = new Literal();
                            lbl9.Text = "<br /><br /><strong>SEO Anahtar:</strong> (Lütfen sayfa içeriği hakkında virgülle ayırarak, en fazla 14 kelime girebilirsiniz)<br />";
                            lit_icerik.Controls.Add(lbl9);

                            TextBox seo_anahtar = new TextBox();
                            seo_anahtar.ID = "txt_seo_anahtar_" + i.id;
                            seo_anahtar.Text = Class.Yonetim.Fonksiyonlar.VideoGaleriDetay(ID, i.id).Select(p => p.seo_anahtar).FirstOrDefault();
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
                        var SQL = (from p in db.tbl_dil
                                   select new
                                   {
                                       p.id
                                   }).AsEnumerable();

                        if (SQL.Any())
                        {
                            foreach (var i in SQL)
                            {
                                KayitEkle(ID, i.id);
                            }
                        }
                    }
                    Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Kayıt başarıyla düzenlenmiştir.", "video-galeri-duzenle.aspx?ID=" + ID + "");
                }
                catch (Exception ex)
                {
                    Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir(ex.Message, "video-galeri.aspx");
                }
            }
        }

        protected void KayitEkle(int haber_id, int dil_id)
        {
            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from t in db.tbl_video_galeri_detay
                           where t.video_galeri_id == haber_id && t.dil_id == dil_id
                           select new
                           {
                               t.id
                           }).AsEnumerable();

                if (SQL.Any())
                {
                    tbl_video_galeri_detay tbl = db.tbl_video_galeri_detay.Where(p => p.video_galeri_id == haber_id && p.dil_id == dil_id).First();
                    tbl.baslik = Request.Form["txt_baslik_" + dil_id].ToString().Trim();
                    tbl.seo_aciklama = Request.Form["txt_seo_aciklama_" + dil_id].ToString().Trim();
                    tbl.seo_anahtar = Request.Form["txt_seo_anahtar_" + dil_id].ToString().Trim();
                }
                else
                {
                    tbl_video_galeri_detay tbl = new tbl_video_galeri_detay();
                    tbl.video_galeri_id = haber_id;
                    tbl.dil_id = dil_id;
                    tbl.baslik = Request.Form["txt_baslik_" + dil_id].ToString().Trim();
                    tbl.seo_aciklama = Request.Form["txt_seo_aciklama_" + dil_id].ToString().Trim();
                    tbl.seo_anahtar = Request.Form["txt_seo_anahtar_" + dil_id].ToString().Trim();
                    db.AddTotbl_video_galeri_detay(tbl);
                }

                tbl_video_galeri tbl2 = db.tbl_video_galeri.Where(p => p.id == haber_id).First();
                tbl2.baslik = txt_tanimlama_baslik.Text.Trim();
                tbl2.video = Request.Form["txt_video_url"].ToString().Trim();
                if (Request.Files["FileUpload1"].FileName.ToString() != "")
                {
                    HttpPostedFile postedFile = Request.Files["FileUpload1"];
                    string ResimAdi = Class.Fonksiyonlar.StringIslemleri(Class.Sabitler.StringIslemleri.StringIslemTipleri.DosyaAd, txt_tanimlama_baslik.Text) + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Server.MapPath("/upload/video-gallery/" + ResimAdi + ""));
                    tbl2.dosya_ad = ResimAdi;
                }
                tbl2.admin_id_gun = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                tbl2.onay = Class.Fonksiyonlar.StringToBool(Request.Form["ddl_onay"].ToString());

                db.SaveChanges();
            }
        }
    }
}