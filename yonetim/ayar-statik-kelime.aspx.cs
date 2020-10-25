using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace rodanit_com.yonetim
{
    public partial class ayar_statik_kelime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Class.Yonetim.Fonksiyonlar.OturumIslemleri.CookieKontrol();

            if (!IsPostBack)
            {
                YuklenecekAlanlar();
                Sayfalama();
                KayitYukle("");
            }
        }

        protected void YuklenecekAlanlar()
        {
            kayitsayi.Items.Clear();
            kayitsayi.Items.Add(new ListItem("10", "10"));
            kayitsayi.Items.Add(new ListItem("20", "20"));
            kayitsayi.Items.Add(new ListItem("30", "30"));
            kayitsayi.Items.Add(new ListItem("50", "50"));
            kayitsayi.Items.Add(new ListItem("75", "75"));
            kayitsayi.Items.Add(new ListItem("100", "100"));
            kayitsayi.Items.Add(new ListItem("125", "125"));
            kayitsayi.Items.Add(new ListItem("150", "150"));

            kayitsayi.Text = Class.Yonetim.Sabitler.YoneticiListeKayitSayi;
        }

        protected void Sayfalama()
        {
            sayfa.Items.Clear();

            int ToplamKayitSayisi = 0;
            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = db.tbl_text.Count();
                ToplamKayitSayisi = SQL;
            }

            int ToplamSayfaSayisi = 0;

            if ((ToplamKayitSayisi % int.Parse(kayitsayi.Text)) == 0)
            {
                ToplamSayfaSayisi = ToplamKayitSayisi / int.Parse(kayitsayi.Text);
            }
            else
            {
                ToplamSayfaSayisi = (ToplamKayitSayisi / int.Parse(kayitsayi.Text)) + 1;
            }

            for (int i = 1; i <= ToplamSayfaSayisi; i++)
            {
                sayfa.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        protected void KayitYukle(string SqlCumle)
        {
            System.Threading.Thread.Sleep(1000);

            int Limit;
            if (sayfa.Text == "1")
            {
                Limit = 0;
            }
            else if (sayfa.SelectedValue == "")
            {
                Limit = 0;
            }
            else
            {
                Limit = (int.Parse(sayfa.Text) - 1) * int.Parse(kayitsayi.Text);
            }

            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from a in db.tbl_text
                           select new
                           {
                               a.id,
                               a.baslik,
                               tr = db.tbl_text_detay.Where(b => b.dil_id == 1 & b.text_id == a.id).Select(b => b.baslik).FirstOrDefault(),
                               ru = db.tbl_text_detay.Where(b => b.dil_id == 2 & b.text_id == a.id).Select(b => b.baslik).FirstOrDefault(),
                               en = db.tbl_text_detay.Where(b => b.dil_id == 3 & b.text_id == a.id).Select(b => b.baslik).FirstOrDefault(),
                               de = db.tbl_text_detay.Where(b => b.dil_id == 4 & b.text_id == a.id).Select(b => b.baslik).FirstOrDefault(),
                               fr = db.tbl_text_detay.Where(b => b.dil_id == 5 & b.text_id == a.id).Select(b => b.baslik).FirstOrDefault(),
                               a.tarih,
                               guncelleyen = db.tbl_admin.Where(b => b.id == a.admin_id).Select(b => b.ad).FirstOrDefault() + " " + db.tbl_admin.Where(b => b.id == a.admin_id).Select(b => b.soyad).FirstOrDefault()
                           }).OrderBy(p => p.baslik).Skip(Limit).Take(int.Parse(kayitsayi.Text));

                Cache["icerik"] = SQL;

                kayitlar.DataSource = SQL;
                kayitlar.DataBind();
            }
        }

        protected void kayitsayisec(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
            Sayfalama();
            KayitYukle("");
        }

        protected void sayfasec(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
            KayitYukle("");
        }

        protected void gridedit(object sender, GridViewEditEventArgs e)
        {
            kayitlar.EditIndex = e.NewEditIndex;
            KayitYukle("");
        }

        protected void gridcancel(object sender, GridViewCancelEditEventArgs e)
        {
            kayitlar.EditIndex = -1;
            KayitYukle("");
        }

        protected void gridupdate(object sender, GridViewUpdateEventArgs e)
        {
            int id = int.Parse(kayitlar.DataKeys[e.RowIndex].Value.ToString());
            //string tanimlama = ((TextBox)kayitlar.Rows[e.RowIndex].Cells[0].Controls[0]).Text;
            string tr = Server.HtmlEncode(((TextBox)kayitlar.Rows[e.RowIndex].Cells[1].Controls[0]).Text);
            string ru = Server.HtmlEncode(((TextBox)kayitlar.Rows[e.RowIndex].Cells[2].Controls[0]).Text);
            string en = Server.HtmlEncode(((TextBox)kayitlar.Rows[e.RowIndex].Cells[3].Controls[0]).Text);
            string de = Server.HtmlEncode(((TextBox)kayitlar.Rows[e.RowIndex].Cells[4].Controls[0]).Text);
            string fr = Server.HtmlEncode(((TextBox)kayitlar.Rows[e.RowIndex].Cells[5].Controls[0]).Text);

            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                var SQL = (from p in db.tbl_text_detay
                           where p.text_id == id
                           select p).AsEnumerable();
                foreach (var i in SQL)
                {
                    db.tbl_text_detay.DeleteObject(i);
                }
                db.SaveChanges();

                tbl_text_detay tbl1 = new tbl_text_detay();
                tbl1.text_id = id;
                tbl1.dil_id = 1;
                tbl1.baslik = tr;
                db.AddTotbl_text_detay(tbl1);

                tbl_text_detay tbl2 = new tbl_text_detay();
                tbl2.text_id = id;
                tbl2.dil_id = 2;
                tbl2.baslik = ru;
                db.AddTotbl_text_detay(tbl2);

                tbl_text_detay tbl3 = new tbl_text_detay();
                tbl3.text_id = id;
                tbl3.dil_id = 3;
                tbl3.baslik = en;
                db.AddTotbl_text_detay(tbl3);

                tbl_text_detay tbl4 = new tbl_text_detay();
                tbl4.text_id = id;
                tbl4.dil_id = 4;
                tbl4.baslik = de;
                db.AddTotbl_text_detay(tbl4);

                tbl_text_detay tbl5 = new tbl_text_detay();
                tbl5.text_id = id;
                tbl5.dil_id = 5;
                tbl5.baslik = fr;
                db.AddTotbl_text_detay(tbl5);

                db.SaveChanges();
            }

            kayitlar.EditIndex = -1;
            KayitYukle("");
        }
    }
}