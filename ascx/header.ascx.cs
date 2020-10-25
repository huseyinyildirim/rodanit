using System;
using System.Linq;
using System.Text;

namespace rodanit_com.ascx
{
    public partial class header : System.Web.UI.UserControl
    {
        int DilID = Class.Fonksiyonlar.DilAyarlari.ID();

        protected void Page_Load(object sender, EventArgs e)
        {
            Diller();
            AnaMenu();
        }

        void AnaMenu()
        {
            #region Üst ana menü
            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from p in db.tbl_menu
                           where p.ust_menu_id == 0 && p.onay == true
                           orderby p.sira
                           select new
                           {
                               p.id,
                               baslik = db.tbl_menu_detay.Where(k => k.menu_id == p.id && k.dil_id == DilID).Select(k => k.baslik).FirstOrDefault(),
                               p.tur,
                               p.url,
                               alt_menu_sayi = (db.tbl_menu.Where(k => k.ust_menu_id == p.id).Count())
                           }).AsEnumerable();

                if (SQL.Any())
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var i in SQL)
                    {
                        if (i.alt_menu_sayi == 0)
                        {
                            switch (i.tur)
                            {
                                case 1:
                                    sb.Append("<li><a href=\"" + Class.Fonksiyonlar.SeoLink("page", i.id.ToString(), i.baslik) + "\">" + i.baslik.ToUpper() + "</a></li>\r\n");
                                    break;
                                case 2:
                                    sb.Append("<li><a href=\"" + i.url + "?page_id=" + i.id + "\">" + i.baslik.ToUpper() + "</a></li>\r\n");
                                    break;
                            }
                        }
                        else
                        {
                            sb.Append("<li class=\"dropdown\"><a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">" + i.baslik.ToUpper() + "\r\n");
                            sb.Append("<b class=\"caret\"></b></a>\r\n");
                            sb.Append("<ul class=\"dropdown-menu dropdown-menu-kirmizi\">\r\n");
                            sb.Append(AltMenu(i.id));
                            sb.Append("</ul>\r\n");
                            sb.Append("</li>\r\n");
                        }
                    }

                    lit_ana_menu.Text = sb.ToString();
                }
            }
            #endregion
        }

        protected static string AltMenu(int id)
        {
            int DilID = Class.Fonksiyonlar.DilAyarlari.ID();

            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                StringBuilder sb = new StringBuilder();

                var SQL = (from a in db.tbl_menu
                           where a.ust_menu_id == id
                           select new
                           {
                               a.id,
                               a.tur,
                               baslik = (db.tbl_menu_detay.Where(k => k.menu_id == a.id && k.dil_id == DilID).Select(k => k.baslik).FirstOrDefault()),
                               a.url
                           }).AsEnumerable();

                if (SQL.Any())
                {
                    foreach (var i in SQL)
                    {
                        switch (i.tur)
                        {
                            case 1:
                                sb.Append("<li><a href=\"" + Class.Fonksiyonlar.SeoLink("page", i.id.ToString(), i.baslik) + "\">" + i.baslik.ToUpper() + "</a></li>\r\n");
                                break;
                            case 2:
                                sb.Append("<li><a href=\"/" + i.url + "?page_id=" + i.id + "\">" + i.baslik.ToUpper() + "</a></li>\r\n");
                                break;
                        }
                    }

                    return sb.ToString();
                }
                else
                {
                    return sb.ToString();
                }
            }
        }

        void Diller()
        {
            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from p in db.tbl_dil
                           where p.onay == true
                           select new
                           {
                               p.id,
                               p.dosya_ad,
                               p.dil,
                               p.dil_kodu
                           });

                if (SQL.Any())
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var i in SQL)
                    {
                        sb.Append("<a href=\"/language.aspx?id=" + i.id + "&amp;lang=" + i.dil_kodu + "&amp;referer=" + Class.Fonksiyonlar.MevcutSayfa() + "\"><img src=\"/upload/bayrak/" + i.dosya_ad + "\" alt=\"" + i.dil + "\" /> " + i.dil + "</a>&nbsp;&nbsp;");
                    }

                    lit_diller.Text = sb.ToString();
                }
            }
        }
    }
}