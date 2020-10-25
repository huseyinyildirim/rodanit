using System;
using System.Linq;
using System.Text;

namespace rodanit_com.ascx
{
    public partial class manset : System.Web.UI.UserControl
    {
        int DilID = Class.Fonksiyonlar.DilAyarlari.ID();

        protected void Page_Load(object sender, EventArgs e)
        {
            Manset();
        }

        void Manset()
        {
            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from a in db.tbl_manset
                           from b in db.tbl_manset_detay
                           where a.onay == true && a.son_gosterim_tarih > DateTime.Today && b.manset_id == a.id && b.dil_id == DilID
                           orderby a.tarih_ek descending
                           select new
                           {
                               a.dosya_ad,
                               b.baslik,
                               a.son_gosterim_tarih,
                               b.url
                           }).OrderBy(p => p.son_gosterim_tarih).AsEnumerable();

                if (SQL.Any())
                {
                    StringBuilder sb1 = new StringBuilder();
                    StringBuilder sb2 = new StringBuilder();

                    int x = 0;

                    foreach (var i in SQL)
                    {
                        sb1.Append("<li data-target=\"#carousel-example-generic\" data-slide-to=\"0\"");

                        if (x == 0)
                        {
                            sb1.Append(" class=\"active\"");
                        }
                        sb1.Append("></li>\r\n");

                        if (x == 0)
                        {
                            sb2.Append("<div class=\"item active\">\r\n");
                            x = 1;
                        }
                        else
                        {
                            sb2.Append("<div class=\"item\">\r\n");
                        }

                        sb2.Append("<img src=\"/ashx/resim-getir.ashx?i=upload/manset/" + i.dosya_ad + "&amp;w=1140&amp;h=400&amp;k=\" alt=\"\">\r\n");
                        sb2.Append("<div class=\"carousel-caption\">\r\n");
                        sb2.Append("<h3>" + i.baslik + "</h3>\r\n");
                        //sb2.Append("<p>haber özeti</p>\r\n");
                        sb2.Append("</div>\r\n");
                        sb2.Append("</div>\r\n");
                    }

                    lit_manset_sayi.Text = sb1.ToString();
                    lit_manset_icerik.Text = sb2.ToString();
                }
            }
        }
    }
}