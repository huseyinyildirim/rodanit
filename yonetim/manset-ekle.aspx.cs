using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace rodanit_com.yonetim
{
    public partial class manset_ekle : System.Web.UI.Page
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
                        lbl5.Text = "<br /><strong>Url:</strong><br />";
                        lit_icerik.Controls.Add(lbl5);

                        TextBox url = new TextBox();
                        url.ID = "txt_url_" + i.id;
                        url.Width = Unit.Pixel(500);
                        lit_icerik.Controls.Add(url);

                        Literal lbl6 = new Literal();
                        lbl6.Text = "<br /><strong>Onay:</strong>";
                        lit_icerik.Controls.Add(lbl6);

                        CheckBox chk = new CheckBox();
                        chk.ID = "chk_onay_" + i.id;
                        chk.Width = Unit.Pixel(200);
                        lit_icerik.Controls.Add(chk);

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
                    tbl_manset tbl2 = new tbl_manset();
                    tbl2.baslik = txt_tanimlama_baslik.Text.Trim();
                    tbl2.son_gosterim_tarih = DateTime.Parse(Request.Form["txt_son_gosterim"].ToString());
                    //tbl2.url = Request.Form["txt_url"].ToString();

                    if (Request.Files["FileUpload1"].FileName.ToString() != "")
                    {
                        HttpPostedFile postedFile = Request.Files["FileUpload1"];
                        string ResimAdi = Class.Fonksiyonlar.StringIslemleri(Class.Sabitler.StringIslemleri.StringIslemTipleri.DosyaAd, txt_tanimlama_baslik.Text) + Path.GetExtension(postedFile.FileName);
                        postedFile.SaveAs(Server.MapPath("~/upload/manset/" + ResimAdi + ""));
                        tbl2.dosya_ad = ResimAdi;
                    }

                    tbl2.admin_id_ek = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                    tbl2.onay = Class.Fonksiyonlar.StringToBool(Request.Form["ddl_onay"].ToString());
                    db.AddTotbl_manset(tbl2);
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
                            tbl_manset_detay tbl = new tbl_manset_detay();
                            tbl.manset_id = tbl2.id;
                            tbl.dil_id = i.id;
                            tbl.baslik = Request.Form["txt_baslik_" + i.id.ToString()].ToString().Trim();
                            tbl.url = Request.Form["txt_url_" + i.id.ToString()].ToString().Trim();
                            tbl.onay = Class.Fonksiyonlar.ChkToBool(Request.Form["chk_onay_" + i.id.ToString()]);
                            tbl.admin_id = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                            db.AddTotbl_manset_detay(tbl);
                        }
                    }

                    db.SaveChanges();
                }
                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Manset başarıyla eklenmiştir.", "manset.aspx");
            }
            catch (Exception ex)
            {
                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir(ex.Message, "manset.aspx");
            }
        }
    }
}