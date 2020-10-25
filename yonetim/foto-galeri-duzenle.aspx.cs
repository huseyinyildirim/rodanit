using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace rodanit_com.yonetim
{
    public partial class foto_galeri_duzenle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Class.Yonetim.Fonksiyonlar.OturumIslemleri.CookieKontrol();

            Islemler();
            Kayit();
        }

        protected void Islemler()
        {
            if (Request.QueryString["ID"] != null && Class.Fonksiyonlar.NumerikKontrol(Request.QueryString["ID"].ToString()) && Request.QueryString["resim_id"] != null && Class.Fonksiyonlar.NumerikKontrol(Request.QueryString["resim_id"].ToString()) && Request.QueryString["islem"] != null && !string.IsNullOrEmpty(Request.QueryString["islem"].ToString()))
            {
                int ID = int.Parse(Request.QueryString["ID"]);
                int resim_id = int.Parse(Request.QueryString["resim_id"]);
                string islem = Request.QueryString["islem"].ToString();

                using (BaglantiCumlesi db = new BaglantiCumlesi())
                {
                    switch (islem)
                    {
                        case "sil":
                            var SQL = (from p in db.tbl_resimler
                                       where p.id == resim_id
                                       select new
                                       {
                                           p.dosya_ad
                                       }).AsEnumerable();

                            if (SQL.Any())
                            {
                                foreach (var i in SQL)
                                {
                                    if (File.Exists(Server.MapPath("~/upload/foto/" + i.dosya_ad + "")))
                                    {
                                        File.Delete(Server.MapPath("~/upload/foto/" + i.dosya_ad + ""));
                                    }
                                }
                            }

                            var SQL2 = (from p in db.tbl_resimler where p.id == resim_id select p);

                            foreach (var i in SQL2)
                            {
                                db.tbl_resimler.DeleteObject(i);
                            }

                            break;

                        case "varsayilan":
                            var SQL4 = (from p in db.tbl_resimler where p.tip_id == ID && p.tip == 4 select p);

                            foreach (var i in SQL4)
                            {
                                using (BaglantiCumlesi db2 = new BaglantiCumlesi())
                                {
                                    tbl_resimler tbl = db2.tbl_resimler.First(a => a.id == i.id);
                                    tbl.varsayilan = false;
                                    tbl.admin_id_gun = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                                    db2.SaveChanges();
                                }
                            }

                            tbl_resimler tbl2 = db.tbl_resimler.First(a => a.id == resim_id);
                            tbl2.varsayilan = true;
                            tbl2.admin_id_gun = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);

                            break;

                        case "onay":
                            var SQL3 = (from a in db.tbl_resimler where a.id == resim_id select new { a.onay });

                            if (SQL3.Any())
                            {
                                foreach (var item in SQL3)
                                {
                                    switch (item.onay)
                                    {
                                        case true:
                                            using (BaglantiCumlesi dbOnay = new BaglantiCumlesi())
                                            {
                                                tbl_resimler TblOnay = dbOnay.tbl_resimler.First(a => a.id == resim_id);
                                                TblOnay.onay = false;
                                                TblOnay.admin_id_gun = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                                                dbOnay.SaveChanges();
                                            }
                                            break;
                                        case false:
                                            using (BaglantiCumlesi dbOnay = new BaglantiCumlesi())
                                            {
                                                tbl_resimler TblOnay = dbOnay.tbl_resimler.First(a => a.id == resim_id);
                                                TblOnay.onay = true;
                                                TblOnay.admin_id_gun = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                                                dbOnay.SaveChanges();
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                    }
                    db.SaveChanges();

                    Response.Redirect("foto-galeri-duzenle.aspx?ID=" + ID);
                }
            }
        }

        protected void GaleriGetir(int ID)
        {
            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from p in db.tbl_resimler
                           where p.tip == 4 && p.tip_id == ID
                           select new
                           {
                               p.id,
                               p.dosya_ad,
                               p.varsayilan,
                               p.onay
                           }).AsEnumerable();

                lv_foto_veriler.DataSource = SQL;
                lv_foto_veriler.DataBind();
            }
        }

        protected void Kayit()
        {
            if (Request.QueryString["ID"] != null && Class.Fonksiyonlar.NumerikKontrol(Request.QueryString["ID"].ToString()))
            {
                int ID = int.Parse(Request.QueryString["ID"]);

                GaleriGetir(ID);

                using (BaglantiCumlesi db = new BaglantiCumlesi())
                {
                    var SQL = (from p in db.tbl_foto_galeri
                               where p.id == ID
                               select new
                               {
                                   p.id
                               }).AsEnumerable();

                    if (!SQL.Any())
                    {
                        Response.Redirect("foto-galeri.aspx");
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
                        ddl_onay.SelectedValue = Class.Fonksiyonlar.BoolToInteger(Class.Yonetim.Fonksiyonlar.FotoGaleri(ID).Select(p => p.onay).FirstOrDefault()).ToString();

                        #region Statik alanlar
                        txt_tanimlama_baslik.Text = Class.Yonetim.Fonksiyonlar.FotoGaleri(ID).Select(p => p.baslik).FirstOrDefault();
                        #endregion

                        lit_ekleyen.Text = Class.Yonetim.Fonksiyonlar.AdminAd(int.Parse(Class.Yonetim.Fonksiyonlar.FotoGaleri(ID).Select(p => p.admin_id_ek).FirstOrDefault().ToString()));
                        lit_kayit_tarih.Text = Class.Yonetim.Fonksiyonlar.FotoGaleri(ID).Select(p => p.tarih_ek).FirstOrDefault().ToString();

                        if (!string.IsNullOrEmpty(Class.Yonetim.Fonksiyonlar.FotoGaleri(ID).Select(p => p.admin_id_gun).FirstOrDefault().ToString()))
                        {
                            lit_gucelleyen.Text = Class.Yonetim.Fonksiyonlar.AdminAd(int.Parse(Class.Yonetim.Fonksiyonlar.FotoGaleri(ID).Select(p => p.admin_id_gun).FirstOrDefault().ToString()));
                        }

                        lit_guncelleme_tarih.Text = Class.Yonetim.Fonksiyonlar.FotoGaleri(ID).Select(p => p.tarih_gun).FirstOrDefault().ToString();

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
                            baslik.Text = Class.Yonetim.Fonksiyonlar.FotoGaleriDetay(ID, i.id).Select(p => p.baslik).FirstOrDefault();
                            baslik.Width = Unit.Pixel(300);
                            lit_icerik.Controls.Add(baslik);

                            Literal lbl7 = new Literal();
                            lbl7.Text = "<br /><br /><strong>SEO Açıklama:</strong> (Bir sayfanın tanım (description) meta etiketi, Google ve diğer arama motorlarına sayfanın ne hakkında olduğuna dair özet bilgi sağlar)<br />";
                            lit_icerik.Controls.Add(lbl7);

                            TextBox seo_aciklama = new TextBox();
                            seo_aciklama.ID = "txt_seo_aciklama_" + i.id;
                            seo_aciklama.Text = Class.Yonetim.Fonksiyonlar.FotoGaleriDetay(ID, i.id).Select(p => p.seo_aciklama).FirstOrDefault();
                            seo_aciklama.Width = Unit.Percentage(100);
                            lit_icerik.Controls.Add(seo_aciklama);

                            Literal lbl9 = new Literal();
                            lbl9.Text = "<br /><br /><strong>SEO Anahtar:</strong> (Lütfen sayfa içeriği hakkında virgülle ayırarak, en fazla 14 kelime girebilirsiniz)<br />";
                            lit_icerik.Controls.Add(lbl9);

                            TextBox seo_anahtar = new TextBox();
                            seo_anahtar.ID = "txt_seo_anahtar_" + i.id;
                            seo_anahtar.Text = Class.Yonetim.Fonksiyonlar.FotoGaleriDetay(ID, i.id).Select(p => p.seo_anahtar).FirstOrDefault();
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
                    Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Başarıyla düzenlenmiştir.", "foto-galeri-duzenle.aspx?ID=" + ID + "");
                }
                catch (Exception ex)
                {
                    Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir(ex.Message, "foto-galeri.aspx");
                }
            }
        }

        protected void KayitEkle(int galeri_id, int dil_id)
        {
            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from t in db.tbl_foto_galeri_detay
                           where t.galeri_id == galeri_id && t.dil_id == dil_id
                           select new
                           {
                               t.id
                           }).AsEnumerable();

                if (SQL.Any())
                {
                    tbl_foto_galeri_detay tbl = db.tbl_foto_galeri_detay.Where(p => p.galeri_id == galeri_id && p.dil_id == dil_id).First();
                    tbl.baslik = Request.Form["txt_baslik_" + dil_id].ToString().Trim();
                    tbl.seo_aciklama = Request.Form["txt_seo_aciklama_" + dil_id].ToString().Trim();
                    tbl.seo_anahtar = Request.Form["txt_seo_anahtar_" + dil_id].ToString().Trim();
                }
                else
                {
                    tbl_foto_galeri_detay tbl = new tbl_foto_galeri_detay();
                    tbl.galeri_id = galeri_id;
                    tbl.dil_id = dil_id;
                    tbl.baslik = Request.Form["txt_baslik_" + dil_id].ToString().Trim();
                    tbl.seo_aciklama = Request.Form["txt_seo_aciklama_" + dil_id].ToString().Trim();
                    tbl.seo_anahtar = Request.Form["txt_seo_anahtar_" + dil_id].ToString().Trim();
                    db.AddTotbl_foto_galeri_detay(tbl);
                }

                tbl_foto_galeri tbl2 = db.tbl_foto_galeri.Where(p => p.id == galeri_id).First();
                tbl2.baslik = txt_tanimlama_baslik.Text.Trim();
                tbl2.admin_id_gun = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                tbl2.onay = Class.Fonksiyonlar.StringToBool(Request.Form["ddl_onay"].ToString());

                db.SaveChanges();
            }
        }
    }
}