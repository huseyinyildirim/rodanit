using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace rodanit_com.yonetim
{
    public partial class menu_duzenle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Class.Yonetim.Fonksiyonlar.OturumIslemleri.CookieKontrol();

            if (!IsPostBack)
            {
                using (BaglantiCumlesi db = new BaglantiCumlesi())
                {

                    var SQL2 = (from p in db.tbl_menu
                                where p.ust_menu_id == 0
                                select new
                                {
                                    p.id,
                                    p.baslik
                                }).AsEnumerable();

                    if (SQL2.Any())
                    {
                        ddl_ust_kategori_id.Items.Add(new ListItem("Ana Kategori", "0"));
                        foreach (var i in SQL2)
                        {
                            ddl_ust_kategori_id.Items.Add(new ListItem(i.baslik, i.id.ToString()));
                        }
                    }
                }

                using (BaglantiCumlesi db = new BaglantiCumlesi())
                {

                    var SQL2 = (from p in db.tbl_foto_galeri
                                select new
                                {
                                    p.id,
                                    p.baslik
                                }).AsEnumerable();

                    if (SQL2.Any())
                    {
                        ddl_foto_galeri.Items.Add(new ListItem("Foto Galeri Seçin", "0"));
                        foreach (var i in SQL2)
                        {
                            ddl_foto_galeri.Items.Add(new ListItem(i.baslik, i.id.ToString()));
                        }
                    }
                }

                ddl_tur.Items.Add(new ListItem("Sayfa", "1"));
                ddl_tur.Items.Add(new ListItem("Yönlendirme", "2"));
            }

            Kayit();
        }

        protected void Kayit()
        {
            if (Request.QueryString["ID"] != null && Class.Fonksiyonlar.NumerikKontrol(Request.QueryString["ID"].ToString()))
            {
                int ID = int.Parse(Request.QueryString["ID"]);

                using (BaglantiCumlesi db = new BaglantiCumlesi())
                {
                    var SQL = (from p in db.tbl_menu
                               where p.id == ID
                               select new
                               {
                                   p.id
                               }).AsEnumerable();

                    if (!SQL.Any())
                    {
                        Response.Redirect("menu.aspx");
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
                        ddl_onay.SelectedValue = Class.Fonksiyonlar.BoolToInteger(Class.Yonetim.Fonksiyonlar.Menu(ID).Select(p => p.onay).FirstOrDefault()).ToString();

                        #region Statik alanlar
                        txt_tanimlama_baslik.Text = Class.Yonetim.Fonksiyonlar.Menu(ID).Select(p => p.baslik).FirstOrDefault();
                        ddl_ust_kategori_id.SelectedValue = Class.Yonetim.Fonksiyonlar.Menu(ID).Select(p => p.ust_menu_id).FirstOrDefault().ToString();
                        ddl_foto_galeri.SelectedValue = Class.Yonetim.Fonksiyonlar.Menu(ID).Select(p => p.foto_galeri_id).FirstOrDefault().ToString();
                        ddl_tur.SelectedValue = Class.Yonetim.Fonksiyonlar.Menu(ID).Select(p => p.tur).FirstOrDefault().ToString();
                        txt_url.Text = Class.Yonetim.Fonksiyonlar.Menu(ID).Select(p => p.url).FirstOrDefault();
                        txt_sira.Text = Class.Yonetim.Fonksiyonlar.Menu(ID).Select(p => p.sira).FirstOrDefault().ToString();
                        #endregion

                        lit_ekleyen.Text = Class.Yonetim.Fonksiyonlar.AdminAd(int.Parse(Class.Yonetim.Fonksiyonlar.Menu(ID).Select(p => p.admin_id_ek).FirstOrDefault().ToString()));
                        lit_kayit_tarih.Text = Class.Yonetim.Fonksiyonlar.Menu(ID).Select(p => p.tarih_ek).FirstOrDefault().ToString();

                        if (!string.IsNullOrEmpty(Class.Yonetim.Fonksiyonlar.Menu(ID).Select(p => p.admin_id_gun).FirstOrDefault().ToString()))
                        {
                            lit_gucelleyen.Text = Class.Yonetim.Fonksiyonlar.AdminAd(int.Parse(Class.Yonetim.Fonksiyonlar.Menu(ID).Select(p => p.admin_id_gun).FirstOrDefault().ToString()));
                        }

                        lit_guncelleme_tarih.Text = Class.Yonetim.Fonksiyonlar.Menu(ID).Select(p => p.tarih_gun).FirstOrDefault().ToString();

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
                            baslik.Text = Class.Yonetim.Fonksiyonlar.MenuDetay(ID, i.id).Select(p => p.baslik).FirstOrDefault();
                            baslik.Width = Unit.Percentage(100);
                            lit_icerik.Controls.Add(baslik);

                            Literal lbl11 = new Literal();
                            lbl11.Text = "<br /><br /><strong>Detay:</strong><br />";
                            lit_icerik.Controls.Add(lbl11);

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
                            detay.Text = Class.Yonetim.Fonksiyonlar.MenuDetay(ID, i.id).Select(p => p.detay).FirstOrDefault();
                            lit_icerik.Controls.Add(detay);

                            Literal lbl7 = new Literal();
                            lbl7.Text = "<br /><br /><strong>SEO Açıklama:</strong> (Bir sayfanın tanım (description) meta etiketi, Google ve diğer arama motorlarına sayfanın ne hakkında olduğuna dair özet bilgi sağlar)<br />";
                            lit_icerik.Controls.Add(lbl7);

                            TextBox seo_aciklama = new TextBox();
                            seo_aciklama.ID = "txt_seo_aciklama_" + i.id;
                            seo_aciklama.Text = Class.Yonetim.Fonksiyonlar.MenuDetay(ID, i.id).Select(p => p.seo_aciklama).FirstOrDefault();
                            seo_aciklama.Width = Unit.Percentage(100);
                            lit_icerik.Controls.Add(seo_aciklama);

                            Literal lbl9 = new Literal();
                            lbl9.Text = "<br /><br /><strong>SEO Anahtar:</strong> (Lütfen sayfa içeriği hakkında virgülle ayırarak, en fazla 14 kelime girebilirsiniz)<br />";
                            lit_icerik.Controls.Add(lbl9);

                            TextBox seo_anahtar = new TextBox();
                            seo_anahtar.ID = "txt_seo_anahtar_" + i.id;
                            seo_anahtar.Text = Class.Yonetim.Fonksiyonlar.MenuDetay(ID, i.id).Select(p => p.seo_anahtar).FirstOrDefault();
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
                    Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Başarıyla düzenlenmiştir.", "menu-duzenle.aspx?ID=" + ID + "");
                }
                catch (Exception ex)
                {
                    Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir(ex.Message, "menu.aspx");
                }
            }
        }

        protected void KayitEkle(int menu_id, int dil_id)
        {
            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from t in db.tbl_menu_detay
                           where t.menu_id == menu_id && t.dil_id == dil_id
                           select new
                           {
                               t.id
                           }).AsEnumerable();

                if (SQL.Any())
                {
                    tbl_menu_detay tbl = db.tbl_menu_detay.Where(p => p.menu_id == menu_id && p.dil_id == dil_id).First();
                    tbl.baslik = Request.Form["txt_baslik_" + dil_id].ToString().Trim();
                    tbl.detay = Request.Form["txt_detay_" + dil_id].ToString().Trim();
                    tbl.seo_aciklama = Request.Form["txt_seo_aciklama_" + dil_id].ToString().Trim();
                    tbl.seo_anahtar = Request.Form["txt_seo_anahtar_" + dil_id].ToString().Trim();
                }
                else
                {
                    tbl_menu_detay tbl = new tbl_menu_detay();
                    tbl.menu_id = menu_id;
                    tbl.dil_id = dil_id;
                    tbl.baslik = Request.Form["txt_baslik_" + dil_id].ToString().Trim();
                    tbl.detay = Request.Form["txt_detay_" + dil_id].ToString().Trim();
                    tbl.seo_aciklama = Request.Form["txt_seo_aciklama_" + dil_id].ToString().Trim();
                    tbl.seo_anahtar = Request.Form["txt_seo_anahtar_" + dil_id].ToString().Trim();
                    db.AddTotbl_menu_detay(tbl);
                }

                tbl_menu tbl2 = db.tbl_menu.Where(p => p.id == menu_id).First();
                tbl2.baslik = txt_tanimlama_baslik.Text.Trim();
                tbl2.ust_menu_id = int.Parse(Request.Form["ddl_ust_kategori_id"].ToString());
                tbl2.tur = int.Parse(Request.Form["ddl_tur"].ToString());
                tbl2.url = txt_url.Text.Trim();

                if (Request.Form["ddl_foto_galeri"].ToString() != "0")
                {
                    tbl2.foto_galeri_id = int.Parse(Request.Form["ddl_foto_galeri"].ToString());
                }

                tbl2.sira = int.Parse(txt_sira.Text);
                tbl2.admin_id_gun = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                tbl2.onay = Class.Fonksiyonlar.StringToBool(Request.Form["ddl_onay"].ToString());

                db.SaveChanges();
            }
        }
    }
}