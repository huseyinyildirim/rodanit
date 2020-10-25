using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace rodanit_com.yonetim
{
    public partial class menu : System.Web.UI.Page
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
                var SQL = db.tbl_menu.Count();
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
                var SQL = (from a in db.tbl_menu
                           select new
                           {
                               a.id,
                               a.baslik,
                               ust_kategori = (db.tbl_menu.Where(k => k.id == a.ust_menu_id).Select(k => k.ust_menu_id).FirstOrDefault()) == 0 ? "Ana Kategori" : (db.tbl_menu.Where(k => k.id == a.ust_menu_id).Select(k => k.baslik).FirstOrDefault()),
                               a.onay,
                               a.tarih_ek,
                               a.tarih_gun,
                               ekleyen = db.tbl_admin.Where(b => b.id == a.admin_id_ek).Select(b => b.ad).FirstOrDefault() + " " + db.tbl_admin.Where(b => b.id == a.admin_id_ek).Select(b => b.soyad).FirstOrDefault(),
                               guncelleyen = db.tbl_admin.Where(b => b.id == a.admin_id_gun).Select(b => b.ad).FirstOrDefault() + " " + db.tbl_admin.Where(b => b.id == a.admin_id_gun).Select(b => b.soyad).FirstOrDefault()
                           }).OrderBy(p => p.ust_kategori).Skip(Limit).Take(int.Parse(kayitsayi.Text));

                Cache["icerik"] = SQL;

                kayitlar.DataSource = SQL;
                kayitlar.DataBind();
            }
        }

        protected void kayitlar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                switch (e.Row.Cells[4].Text)
                {
                    case "True":
                        e.Row.Cells[4].Text = "<img src=\"img/komut-aktif.png\" />";
                        break;

                    case "False":
                        e.Row.Cells[4].Text = "<img src=\"img/komut-pasif.png\" />";
                        break;
                }

                e.Row.Cells[6].Text = "<a href=\"menu-duzenle.aspx?ID=" + e.Row.Cells[1].Text + "\"><img src=\"img/komut-duzenle.png\" /></a>";
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

        protected void secilenlerisil_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(1000);

            foreach (GridViewRow satir in kayitlar.Rows)
            {
                CheckBox kutu = (CheckBox)satir.FindControl("secim");
                if (kutu.Checked)
                {
                    int ID = int.Parse(kayitlar.DataKeys[satir.RowIndex].Value.ToString());

                    KayitSil(ID);
                }
            }

            KayitYukle("");
        }

        protected void yeniekle_Click(object sender, EventArgs e)
        {
            Response.Redirect("menu-ekle.aspx");
        }

        protected void kayitlar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Threading.Thread.Sleep(500);

            int ID = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "Onay")
            {
                using (BaglantiCumlesi db = new BaglantiCumlesi())
                {
                    var SQL = from a in db.tbl_menu where a.id == ID select new { a.onay };

                    if (SQL.Any())
                    {
                        foreach (var item in SQL)
                        {
                            switch (item.onay)
                            {
                                case true:
                                    using (BaglantiCumlesi dbOnay = new BaglantiCumlesi())
                                    {
                                        tbl_menu TblOnay = dbOnay.tbl_menu.First(a => a.id == ID);
                                        TblOnay.onay = false;
                                        TblOnay.admin_id_gun = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                                        dbOnay.SaveChanges();
                                    }
                                    break;
                                case false:
                                    using (BaglantiCumlesi dbOnay = new BaglantiCumlesi())
                                    {
                                        tbl_menu TblOnay = dbOnay.tbl_menu.First(a => a.id == ID);
                                        TblOnay.onay = true;
                                        TblOnay.admin_id_gun = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                                        dbOnay.SaveChanges();
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            if (e.CommandName == "Sil")
            {
                KayitSil(ID);
            }

            KayitYukle("");
        }

        protected void KayitSil(int ID)
        {
            using (BaglantiCumlesi db = new BaglantiCumlesi())
            {
                tbl_menu tbl = db.tbl_menu.First(a => a.id == ID);
                db.DeleteObject(tbl);
                db.SaveChanges();
            }
        }
    }
}