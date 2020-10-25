﻿using System;
using System.Linq;
using System.Web;

namespace rodanit_com.yonetim
{
    public partial class ayar_smtp : System.Web.UI.Page
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
                var SQL = (from p in db.tbl_ayar
                           where p.id == 1
                           select new
                           {
                               p.smtp,
                               p.smtp_mail,
                               p.smtp_port,
                               p.smtp_sifre,
                               p.tarih_gun,
                               guncelleyen = db.tbl_admin.Where(b => b.id == p.admin_id_gun).Select(b => b.ad).FirstOrDefault() + " " + db.tbl_admin.Where(b => b.id == p.admin_id_gun).Select(b => b.soyad).FirstOrDefault()
                           }).AsEnumerable();

                if (SQL.Any())
                {
                    foreach (var i in SQL)
                    {
                        txt_smtp.Text = i.smtp;
                        txt_eposta.Text = i.smtp_mail;
                        txt_sifre.Text = i.smtp_sifre;
                        txt_port.Text = i.smtp_port.ToString();
                        lit_gucelleyen.Text = i.guncelleyen;
                        lit_guncelleme_tarih.Text = i.tarih_gun.ToString();
                    }
                }
                else
                {
                    Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("tbl_ayar tablosunda 1 nolu kayıt bulunmuyor!", "default.aspx");
                }
            }
        }

        protected void btn_kayitekle_Click(object sender, EventArgs e)
        {
            try
            {
                using (BaglantiCumlesi db = new BaglantiCumlesi())
                {
                    tbl_ayar tbl = db.tbl_ayar.Where(p => p.id == 1).First();
                    tbl.smtp = txt_smtp.Text.Trim();
                    tbl.smtp_mail = txt_eposta.Text.Trim();
                    tbl.smtp_sifre = txt_sifre.Text.Trim();
                    tbl.smtp_port = int.Parse(txt_port.Text.Trim());
                    tbl.admin_id_gun = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "KullaniciID"].Value);
                    db.SaveChanges();
                }

                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir("Başarıyla düzenlenmiştir.", "ayar-smtp.aspx");
            }
            catch (Exception ex)
            {
                Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir(ex.Message, "ayar-smtp.aspx");
            }
        }
    }
}