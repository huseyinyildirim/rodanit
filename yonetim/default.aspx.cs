using System;

namespace rodanit_com.yonetim
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Class.Yonetim.Fonksiyonlar.OturumIslemleri.CookieKontrol();

            Grafik();
        }

        protected void Grafik()
        {
            /*using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from a in db.sayac
                           group a by a.Tarih into g
                           select new
                           {

                               Tarih = g.Key,
                               Tekil = g.Count(),
                               Cogul = g.Sum(p => p.Cogul),
                               SayfaGosterim = db.sayac_sayfa.Where(p => p.Tarih == g.Key).Sum(p => p.Hit)
                           }).OrderByDescending(p => p.Tarih);

                if (SQL.AsEnumerable().Count() > 0)
                {
                    string Veri = string.Empty;
                    int i = 1;
                    int KayitSayi = SQL.AsEnumerable().Count();
                    foreach (var item in SQL)
                    {
                        Veri += "" + item.Tarih.Value.ToShortDateString() + "|" + item.Tekil + "|" + item.Cogul + "";

                        if (KayitSayi == i)
                        {
                            Veri += "";
                        }
                        else
                        {
                            Veri += "/";
                            i++;
                        }
                    }

                    grafik.Text = Yonetim.JavaScript.BarPasta(Veri);
                }
            }*/
        }
    }
}