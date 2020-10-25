using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rodanit_com
{
    public partial class page : System.Web.UI.Page
    {
        int DilID = Class.Fonksiyonlar.DilAyarlari.ID();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (RouteData.Values["page_id"] != null)
            {
                if (Class.Fonksiyonlar.NumerikKontrol(RouteData.Values["page_id"].ToString()))
                {
                    int page_id = int.Parse(RouteData.Values["page_id"].ToString());

                    using (BaglantiCumlesi db = new BaglantiCumlesi())
                    {
                        var SQL = (from p in db.tbl_menu
                                   where p.id == page_id
                                   select new
                                   {
                                       baslik = db.tbl_menu_detay.Where(k => k.menu_id == p.id && k.dil_id == DilID).Select(k => k.baslik).FirstOrDefault(),
                                       detay = db.tbl_menu_detay.Where(k => k.menu_id == p.id && k.dil_id == DilID).Select(k => k.detay).FirstOrDefault(),
                                       seo_aciklama = db.tbl_menu_detay.Where(k => k.menu_id == p.id && k.dil_id == DilID).Select(k => k.seo_aciklama).FirstOrDefault(),
                                       seo_anahtar = db.tbl_menu_detay.Where(k => k.menu_id == p.id && k.dil_id == DilID).Select(k => k.seo_anahtar).FirstOrDefault(),
                                       p.foto_galeri_id
                                   }).AsEnumerable();

                        if (SQL.Any())
                        {
                            lit_sayfa_baslik.Text = SQL.Select(p => p.baslik).FirstOrDefault();
                            lit_sayfa_detay.Text = Server.HtmlDecode(SQL.Select(p => p.detay).FirstOrDefault());

                            #region Sayfa seo ayarları
                            Class.Fonksiyonlar.HeaderText("head", "Title", SQL.Select(p => p.baslik).FirstOrDefault() + " | " + Class.Fonksiyonlar.Ayar().Select(k => k.seo_baslik).FirstOrDefault());

                            if (!string.IsNullOrEmpty(SQL.Select(p => p.seo_anahtar).FirstOrDefault()))
                            {
                                Class.Fonksiyonlar.HeaderText("head", "lit_anahtar", "<meta http-equiv=\"Keywords\" content=\"" + SQL.Select(p => p.seo_anahtar).FirstOrDefault() + "\" />");
                            }
                            else
                            {
                                Class.Fonksiyonlar.HeaderText("head", "lit_anahtar", "<meta http-equiv=\"Keywords\" content=\"" + Class.Fonksiyonlar.Ayar().Select(k => k.seo_anahtar).FirstOrDefault() + "\" />");
                            }

                            if (!string.IsNullOrEmpty(SQL.Select(p => p.seo_aciklama).FirstOrDefault()))
                            {
                                Class.Fonksiyonlar.HeaderText("head", "lit_aciklama", "<meta http-equiv=\"Description\" content=\"" + SQL.Select(p => p.seo_aciklama).FirstOrDefault() + "\" />");
                            }
                            else
                            {
                                Class.Fonksiyonlar.HeaderText("head", "lit_aciklama", "<meta http-equiv=\"Description\" content=\"" + Class.Fonksiyonlar.Ayar().Select(k => k.seo_anahtar).FirstOrDefault() + "\" />");
                            }
                            #endregion

                            #region Nerdesin?
                            Class.Fonksiyonlar.Nerdesin(Class.Fonksiyonlar.Textler(18) + "|default.aspx," + SQL.Select(p => p.baslik).FirstOrDefault() + "|?");
                            #endregion

                            if (!string.IsNullOrEmpty(SQL.Select(p => p.foto_galeri_id).FirstOrDefault().ToString()))
                            {
                                FotoGaleri(int.Parse(SQL.Select(p => p.foto_galeri_id).FirstOrDefault().ToString()));
                            }
                            
                        }
                        else
                        {
                            Response.Redirect("/default.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("/default.aspx");
                }
            }
            else
            {
                Response.Redirect("/default.aspx");
            }
        }

        void FotoGaleri(int ID)
        {
            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from p in db.tbl_resimler
                           where p.tip == 4 && p.tip_id == ID && p.onay == true
                           select new
                           {
                               p.id,
                               p.dosya_ad,
                               baslik = (db.tbl_foto_galeri_detay.Where(k => k.galeri_id == ID && k.dil_id == DilID).Select(k => k.baslik).FirstOrDefault())
                           }).AsEnumerable();

                lv_foto_veriler.DataSource = SQL;
                lv_foto_veriler.DataBind();
            }
        }
    }
}