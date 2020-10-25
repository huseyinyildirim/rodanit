using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace rodanit_com.ascx
{
    public partial class sag : System.Web.UI.UserControl
    {
        int DilID = Class.Fonksiyonlar.DilAyarlari.ID();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Sabitler();

            try
            {
                string[] dizi = Class.Fonksiyonlar.MevcutSayfa().Split('/');

                if (Class.Fonksiyonlar.NumerikKontrol(dizi[4]))
                {
                    int page_id = int.Parse(dizi[4]);

                    Menu(page_id);
                }
                else
                {
                    //DikeyMenu(true);
                }
            }
            catch
            {
                if (Request.QueryString["page_id"] != null && Class.Fonksiyonlar.NumerikKontrol(Request.QueryString["page_id"].ToString()))
                {
                    int page_id = int.Parse(Request.QueryString["page_id"].ToString());
                    Menu(page_id);
                }
                else
                {
                    //DikeyMenu(true);
                }
            }
        }

        void Menu(int page_id)
        {
            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from p in db.tbl_menu
                           where p.ust_menu_id == page_id
                           select new
                           {
                               p.id,
                               baslik = db.tbl_menu_detay.Where(k => k.menu_id == p.id && k.dil_id == DilID).Select(k => k.baslik).FirstOrDefault(),
                               p.tur,
                               p.url,
                               p.ust_menu_id,
                               p.sira
                           }).OrderBy(p => p.sira).AsEnumerable();

                if (SQL.Any())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div class=\"list-group\">");
                    sb.Append("<a href=\"/default.aspx\" class=\"list-group-item\">" + Class.Fonksiyonlar.Textler(1) + "</a>");

                    foreach (var i in SQL)
                    {
                        switch (i.tur)
                        {
                            case 1:
                                sb.Append("<a href=\"" + Class.Fonksiyonlar.SeoLink("page", i.id.ToString(), i.baslik) + "\" class=\"list-group-item\">" + i.baslik + "</a>");
                                break;
                            case 2:
                                sb.Append("<a href=\"" + i.url + "\" class=\"list-group-item\">" + i.baslik + "</a>");
                                break;
                        }
                    }

                    sb.Append("</div>");
                    lit_dikey_menu.Text = sb.ToString();
                }
                else
                {
                    int page_id2 = Class.Yonetim.Fonksiyonlar.Menu(page_id).Select(k => k.ust_menu_id).FirstOrDefault();

                    var SQL2 = (from p in db.tbl_menu
                                where p.ust_menu_id == page_id2 && p.onay == true
                                select new
                                {
                                    p.id,
                                    baslik = db.tbl_menu_detay.Where(k => k.menu_id == p.id && k.dil_id == DilID).Select(k => k.baslik).FirstOrDefault(),
                                    p.tur,
                                    p.url,
                                    p.ust_menu_id,
                                    p.sira
                                }).OrderBy(p => p.sira).AsEnumerable();

                    if (SQL2.Any())
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<div class=\"list-group\">");
                        sb.Append("<a href=\"/default.aspx\" class=\"list-group-item\">" + Class.Fonksiyonlar.Textler(1) + "</a>");

                        foreach (var i in SQL2)
                        {
                            switch (i.tur)
                            {
                                case 1:
                                    sb.Append("<a href=\"" + Class.Fonksiyonlar.SeoLink("page", i.id.ToString(), i.baslik) + "\" class=\"list-group-item\">" + i.baslik + "</a>");
                                    break;
                                case 2:
                                    sb.Append("<a href=\"" + i.url + "\" class=\"list-group-item\">" + i.baslik + "</a>");
                                    break;
                            }
                        }

                        sb.Append("</div>");
                        lit_dikey_menu.Text = sb.ToString();
                    }
                    else
                    {
                        //DikeyMenu(true);
                    }
                }
            }
        }

        /*void DikeyMenu(bool html)
        {
            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from p in db.tbl_blok
                           where p.onay == true
                           orderby p.sira ascending
                           select new
                           {
                               p.id
                           });

                if (SQL.Any())
                {
                    StringBuilder sb = new StringBuilder();
                    if (html == true)
                    {
                        sb.Append("<div class=\"list-group\">");
                        sb.Append("<a href=\"/default.aspx\" class=\"list-group-item\">" + Class.Fonksiyonlar.Textler(227) + "</a>");
                    }

                    foreach (var i in SQL)
                    {
                        sb.Append(DikeyMenuCagir(i.id));
                    }
                    if (html == true)
                    {
                        sb.Append("</div>");
                    }
                    lit_dikey_menu.Text = sb.ToString();
                }
            }
        }

        string DikeyMenuCagir(int BlokID)
        {
            string Sonuc = string.Empty;

            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from p in db.tbl_menu
                           where p.blok_id == BlokID && p.ust_menu_id == 0
                           select new
                           {
                               p.id,
                               baslik = db.tbl_menu_detay.Where(k => k.menu_id == p.id && k.dil_id == DilID).Select(k => k.baslik).FirstOrDefault(),
                               p.tur,
                               p.url,
                               p.sira
                           }).OrderBy(p => p.sira).AsEnumerable();

                if (SQL.Any())
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var i in SQL)
                    {
                        switch (i.tur)
                        {
                            case 1:
                                sb.Append("<a class=\"list-group-item\" href=\"" + Class.Fonksiyonlar.SeoLink("page", i.id.ToString(), i.baslik) + "\">" + i.baslik + "</a>");
                                break;
                            case 2:
                                sb.Append("<a href=\"" + i.url + "\" class=\"list-group-item\">" + i.baslik + "</a>");
                                break;
                        }
                    }

                    Sonuc = sb.ToString();
                }
            }

            return Sonuc;
        }*/
    }
}