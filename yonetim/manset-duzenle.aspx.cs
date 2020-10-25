using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace rodanit_com.yonetim
{
    public partial class manset_duzenle : System.Web.UI.Page
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
                    var SQL = (from p in db.tbl_manset
                               where p.id == ID
                               select new
                               {
                                   p.id
                               }).AsEnumerable();

                    if (!SQL.Any())
                    {
                        Response.Redirect("manset.aspx");
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
                        lit_onizleme.Text = "<img style=\"border:1px dotted #CCC;\" src=\"/ashx/resim-getir.ashx?i=upload/manset/" + Class.Yonetim.Fonksiyonlar.Manset(ID).Select(p => p.dosya_ad).FirstOrDefault() + "&amp;w=300&amp;h=150&amp;k=t\" />";

                        ddl_onay.SelectedValue = Class.Fonksiyonlar.BoolToInteger(Class.Yonetim.Fonksiyonlar.Manset(ID).Select(p => p.onay).FirstOrDefault()).ToString();

                        txt_tanimlama_baslik.Text = Class.Yonetim.Fonksiyonlar.Manset(ID).Select(p => p.baslik).FirstOrDefault();
                        txt_son_gosterim.Text = Class.Yonetim.Fonksiyonlar.Manset(ID).Select(p => p.son_gosterim_tarih).FirstOrDefault().Value.ToShortDateString();
                        //txt_url.Text = Class.Yonetim.Fonksiyonlar.Manset(ID).Select(p => p.url).FirstOrDefault();

                        lit_ekleyen.Text = Class.Yonetim.Fonksiyonlar.AdminAd(int.Parse(Class.Yonetim.Fonksiyonlar.Manset(ID).Select(p => p.admin_id_ek).FirstOrDefault().ToString()));
                        lit_kayit_tarih.Text = Class.Yonetim.Fonksiyonlar.Manset(ID).Select(p => p.tarih_ek).FirstOrDefault().ToString();

                        if (!string.IsNullOrEmpty(Class.Yonetim.Fonksiyonlar.Manset(ID).Select(p => p.admin_id_gun).FirstOrDefault().ToString()))
                        {
                            lit_gucelleyen.Text = Class.Yonetim.Fonksiyonlar.AdminAd(int.Parse(Class.Yonetim.Fonksiyonlar.Manset(ID).Select(p => p.admin_id_gun).FirstOrDefault().ToString()));
                        }

                        lit_guncelleme_tarih.Text = Class.Yonetim.Fonksiyonlar.Manset(ID).Select(p => p.tarih_gun).FirstOrDefault().ToString();

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
                            baslik.Text = Class.Yonetim.Fonksiyonlar.MansetDetay(ID, i.id).Select(p => p.baslik).FirstOrDefault();
                            baslik.Width = Unit.Pixel(500);
                            lit_icerik.Controls.Add(baslik);

                            Literal lbl5 = new Literal();
                            lbl5.Text = "<br /><strong>Url:</strong><br />";
                            lit_icerik.Controls.Add(lbl5);

                            TextBox url = new TextBox();
                            url.ID = "txt_url_" + i.id;
                            url.Text = Class.Yonetim.Fonksiyonlar.MansetDetay(ID, i.id).Select(p => p.url).FirstOrDefault();
                            url.Width = Unit.Pixel(500);
                            lit_icerik.Controls.Add(url);

                            Literal lbl6 = new Literal();
                            lbl6.Text = "<br /><strong>Onay:</strong>";
                            lit_icerik.Controls.Add(lbl6);

                            CheckBox chk = new CheckBox();
                            chk.ID = "chk_onay_" + i.id;
                            chk.Width = Unit.Pixel(200);
                            if (!string.IsNullOrEmpty(Class.Yonetim.Fonksiyonlar.MansetDetay(ID, i.id).Select(p => p.onay).FirstOrDefault().ToString()))
                            {
                                chk.Checked = bool.Parse(Class.Yonetim.Fonksiyonlar.MansetDetay(ID, i.id).Select(p => p.onay).FirstOrDefault().ToString());
                            }
                            else
                            {
                                chk.Checked = false;
                            }
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
                    Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Manşet başarıyla düzenlenmiştir.", "manset-duzenle.aspx?ID=" + ID + "");
                }
                catch (Exception ex)
                {
                    Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir(ex.Message, "manset.aspx");
                }
            }
        }

        protected void KayitEkle(int manset_id, int dil_id)
        {
            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                #region detay tablo kayıt
                var SQL = (from t in db.tbl_manset_detay
                           where t.manset_id == manset_id && t.dil_id == dil_id
                           select new
                           {
                               t.id
                           }).AsEnumerable();

                if (SQL.Any())
                {
                    tbl_manset_detay tbl = db.tbl_manset_detay.Where(p => p.manset_id == manset_id && p.dil_id == dil_id).First();
                    tbl.baslik = Request.Form["txt_baslik_" + dil_id].ToString().Trim();
                    tbl.url = Request.Form["txt_url_" + dil_id].ToString().Trim();
                    tbl.onay = Class.Fonksiyonlar.ChkToBool(Request.Form["chk_onay_" + dil_id]);
                }
                else
                {
                    tbl_manset_detay tbl = new tbl_manset_detay();
                    tbl.manset_id = manset_id;
                    tbl.dil_id = dil_id;
                    tbl.baslik = Request.Form["txt_baslik_" + dil_id].ToString().Trim();
                    tbl.url = Request.Form["txt_url_" + dil_id].ToString().Trim();
                    tbl.onay = Class.Fonksiyonlar.ChkToBool(Request.Form["chk_onay_" + dil_id]);
                    db.AddTotbl_manset_detay(tbl);
                }
                #endregion

                #region ana tablo kayıt
                tbl_manset tbl2 = db.tbl_manset.Where(p => p.id == manset_id).First();
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

                tbl2.admin_id_gun = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                tbl2.onay = Class.Fonksiyonlar.StringToBool(Request.Form["ddl_onay"].ToString());
                #endregion

                db.SaveChanges();
            }
        }
    }
}