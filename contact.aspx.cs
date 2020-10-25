using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace rodanit_com
{
    public partial class contact : System.Web.UI.Page
    {
        int DilID = Class.Fonksiyonlar.DilAyarlari.ID();

        protected void Page_Load(object sender, EventArgs e)
        {
            Sabitler();

            Class.Fonksiyonlar.HeaderText("head", "Title", Class.Fonksiyonlar.Textler(2) + " | " + Class.Fonksiyonlar.Ayar().Select(k => k.seo_baslik).FirstOrDefault());
            Class.Fonksiyonlar.HeaderText("head", "lit_anahtar", "<meta http-equiv=\"Keywords\" content=\"" + Class.Fonksiyonlar.Ayar().Select(k => k.seo_anahtar).FirstOrDefault() + "\" />");
            Class.Fonksiyonlar.HeaderText("head", "lit_aciklama", "<meta http-equiv=\"Description\" content=\"" + Class.Fonksiyonlar.Ayar().Select(k => k.seo_anahtar).FirstOrDefault() + "\" />");
        }

        void Sabitler()
        {
            btn_iletisim_gonder.Text = Class.Fonksiyonlar.Textler(10);
        }

        protected void btn_iletisim_gonder_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<strong>Adı Soyadı:</strong> " + txt_iletisim_ad.Text.Trim() + " " + txt_iletisim_soyad.Text.Trim() + "<br />");
                    sb.Append("<strong>E-Posta:</strong> " + txt_iletisim_mail.Text.Trim() + "<br />");
                    sb.Append("<strong>Telefon:</strong> " + txt_iletisim_telefon.Text.Trim() + "<br />");
                    sb.Append("<strong>Mesaj:</strong> " + txt_iletisim_mesaj.Text.Trim().Replace("\r\n", "<br/>") + "<br /><br />");
                    sb.Append("<strong>IP:</strong> " + Request.ServerVariables["REMOTE_ADDR"].ToString() + "<br />");

                    Class.Fonksiyonlar.MailGonder(Class.Fonksiyonlar.Ayar().Select(k => k.smtp_mail).FirstOrDefault(), Class.Fonksiyonlar.Ayar().Select(k => k.smtp_sifre).FirstOrDefault(), Class.Fonksiyonlar.Ayar().Select(k => k.smtp).FirstOrDefault(), Class.Fonksiyonlar.Ayar().Select(k => k.smtp_port).FirstOrDefault(), Class.Fonksiyonlar.Ayar().Select(k => k.smtp_mail).FirstOrDefault(), null, null, Class.Fonksiyonlar.Ayar().Select(k => k.smtp_mail).FirstOrDefault(), txt_iletisim_ad.Text.Trim() + " " + txt_iletisim_soyad.Text.Trim(), Class.Fonksiyonlar.Ayar().Select(k => k.marka).FirstOrDefault() + " (İletişim)", Class.Fonksiyonlar.StringIslemleri(Class.Sabitler.StringIslemleri.StringIslemTipleri.SQLTemizle, sb.ToString()), true);

                    using (BaglantiCumlesi db = new BaglantiCumlesi())
                    {
                        tbl_gelen_mail tbl = new tbl_gelen_mail();
                        tbl.tur = "iletisim";
                        tbl.ad_soyad = txt_iletisim_ad.Text.Trim() + " " + txt_iletisim_soyad.Text.Trim();
                        tbl.mail = txt_iletisim_mail.Text.Trim();
                        tbl.telefon = txt_iletisim_telefon.Text.Trim();
                        tbl.mesaj = sb.ToString();
                        tbl.ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        db.AddTotbl_gelen_mail(tbl);
                        db.SaveChanges();
                    }

                    Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir(Class.Fonksiyonlar.Textler(80), Class.Fonksiyonlar.MevcutSayfa());
                }
                else
                {
                    Class.Fonksiyonlar.JavaScript.MesajKutusuVeYonlendir(Class.Fonksiyonlar.Textler(133), Class.Fonksiyonlar.MevcutSayfa());
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                Class.Fonksiyonlar.JavaScript.MesajKutusu(Class.Fonksiyonlar.Textler(20));
            }
        }
    }
}