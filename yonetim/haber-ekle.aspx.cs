using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace rodanit_com.yonetim
{
    public partial class haber_ekle : System.Web.UI.Page
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
                        lbl4.Text = "<div id=\"" + i.dil + "\"><strong>Başlık:</strong><br />";
                        lit_icerik.Controls.Add(lbl4);

                        TextBox baslik = new TextBox();
                        baslik.ID = "txt_baslik_" + i.id;
                        baslik.Width = Unit.Pixel(500);
                        lit_icerik.Controls.Add(baslik);

                        Literal lbl5 = new Literal();
                        lbl5.Text = "<br /><br /><strong>Özet:</strong><br />";
                        lit_icerik.Controls.Add(lbl5);

                        CKEditor.NET.CKEditorControl ozet = new CKEditor.NET.CKEditorControl();
                        ozet.ID = "txt_ozet_" + i.id;
                        ozet.Height = Unit.Pixel(100);
                        ozet.BasePath = "~/Scripts/ckeditor";
                        ozet.ContentsCss = "~/Scripts/ckeditor/contents.css";
                        ozet.TemplatesFiles = "~/Scripts/ckeditor/plugins/templates/templates/default.js";
                        ozet.EnterMode = CKEditor.NET.EnterMode.BR;
                        ozet.ShiftEnterMode = CKEditor.NET.EnterMode.BR;
                        ozet.Toolbar = "";
                        lit_icerik.Controls.Add(ozet);

                        Literal lbl6 = new Literal();
                        lbl6.Text = "<br /><br /><strong>Detay:</strong><br />";
                        lit_icerik.Controls.Add(lbl6);

                        CKEditor.NET.CKEditorControl detay = new CKEditor.NET.CKEditorControl();
                        detay.ID = "txt_detay_" + i.id;
                        detay.Height = Unit.Pixel(200);
                        detay.BasePath = "~/Scripts/ckeditor";
                        detay.ContentsCss = "~/Scripts/ckeditor/contents.css";
                        detay.TemplatesFiles = "~/Scripts/ckeditor/plugins/templates/templates/default.js";
                        detay.EnterMode = CKEditor.NET.EnterMode.BR;
                        detay.ShiftEnterMode = CKEditor.NET.EnterMode.BR;
                        detay.FilebrowserBrowseUrl = "/Scripts/ckfinder/ckfinder.html";
                        detay.FilebrowserImageBrowseUrl = "/Scripts/ckfinder/ckfinder.html?type=Images";
                        detay.FilebrowserFlashBrowseUrl = "/Scripts/ckfinder/ckfinder.html?type=Flash";
                        detay.FilebrowserUploadUrl = "/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files";
                        detay.FilebrowserImageUploadUrl = "/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images";
                        detay.FilebrowserFlashUploadUrl = "/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash";
                        lit_icerik.Controls.Add(detay);

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
                    tbl_haber tbl2 = new tbl_haber();
                    tbl2.baslik = txt_tanimlama_baslik.Text.Trim();

                    if (Request.Files["FileUpload1"].FileName.ToString() != "")
                    {
                        HttpPostedFile postedFile = Request.Files["FileUpload1"];
                        string ResimAdi = Class.Fonksiyonlar.StringIslemleri(Class.Sabitler.StringIslemleri.StringIslemTipleri.DosyaAd, txt_tanimlama_baslik.Text) + Path.GetExtension(postedFile.FileName);
                        postedFile.SaveAs(Server.MapPath("~/upload/haber/" + ResimAdi + ""));
                        tbl2.dosya_ad = ResimAdi;
                    }

                    tbl2.admin_id_ek = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                    tbl2.onay = Class.Fonksiyonlar.StringToBool(Request.Form["ddl_onay"].ToString());
                    db.AddTotbl_haber(tbl2);
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
                            tbl_haber_detay tbl = new tbl_haber_detay();
                            tbl.haber_id = tbl2.id;
                            tbl.dil_id = i.id;
                            tbl.baslik = Request.Form["txt_baslik_" + i.id.ToString()].ToString().Trim();
                            tbl.ozet = Request.Form["txt_ozet_" + i.id.ToString()].ToString().Trim();
                            tbl.detay = Request.Form["txt_detay_" + i.id.ToString()].ToString().Trim();
                            tbl.seo_aciklama = Request.Form["txt_seo_aciklama_" + i.id.ToString()].ToString().Trim();
                            tbl.seo_anahtar = Request.Form["txt_seo_anahtar_" + i.id.ToString()].ToString().Trim();
                            db.AddTotbl_haber_detay(tbl);

                        }
                    }

                    db.SaveChanges();
                }
                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Haber başarıyla eklenmiştir.", "haber.aspx");
            }
            catch (Exception ex)
            {
                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir(ex.Message, "haber.aspx");
            }
        }
    }
}