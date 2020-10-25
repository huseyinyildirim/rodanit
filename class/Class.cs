using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace rodanit_com
{
    public class Class
    {
        public class Yonetim
        {
            public class Sabitler
            {
                public static string YoneticiListeKayitSayi = "20";
            }

            public class Fonksiyonlar
            {
                public class OturumIslemleri
                {
                    public static void CookieOlustur(string CookieAdi, string Deger)
                    {
                        HttpContext.Current.Response.SetCookie(new HttpCookie(Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + CookieAdi, Deger));
                    }

                    public static void CookieSil()
                    {
                        HttpContext.Current.Session.Clear();
                        HttpContext.Current.Session.RemoveAll();
                        HttpContext.Current.Session.Abandon();

                        string[] cookies = HttpContext.Current.Request.Cookies.AllKeys;
                        HttpCookie tmpCookie;

                        foreach (string cookieKey in cookies)
                        {
                            tmpCookie = HttpContext.Current.Response.Cookies[cookieKey];
                            tmpCookie.Expires = DateTime.Now.AddDays(-2);
                            HttpContext.Current.Response.Cookies.Add(tmpCookie);
                        }

                        //HttpContext.Current.Response.Redirect("Giris.aspx");
                    }

                    public static void CookieKontrol()
                    {
                        if (HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "GirisYonetim"] != null)
                        {
                            if (HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "GirisYonetim"].Value != "88888888")
                            {
                                HttpContext.Current.Response.Redirect("giris.aspx");
                            }
                        }
                        else
                        {
                            HttpContext.Current.Response.Redirect("giris.aspx");
                        }
                    }
                }

                public static List<tbl_haber> Haber(int id)
                {
                    List<tbl_haber> SQL = new List<tbl_haber>();
                    try
                    {
                        using (BaglantiCumlesi db = new BaglantiCumlesi())
                        {
                            SQL = (from p in db.tbl_haber
                                   where p.id == id
                                   select p).AsEnumerable().Take(1).Cast<tbl_haber>().ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }

                    return SQL;
                }

                public static List<tbl_haber_detay> HaberDetay(int haber_id, int dil_id)
                {
                    List<tbl_haber_detay> SQL = new List<tbl_haber_detay>();
                    try
                    {
                        using (BaglantiCumlesi db = new BaglantiCumlesi())
                        {
                            SQL = (from p in db.tbl_haber_detay
                                   where p.haber_id == haber_id && p.dil_id == dil_id
                                   select p).AsEnumerable().Take(1).Cast<tbl_haber_detay>().ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }

                    return SQL;
                }

                public static List<tbl_manset> Manset(int id)
                {
                    List<tbl_manset> SQL = new List<tbl_manset>();
                    try
                    {
                        using (BaglantiCumlesi db = new BaglantiCumlesi())
                        {
                            SQL = (from p in db.tbl_manset
                                   where p.id == id
                                   select p).AsEnumerable().Take(1).Cast<tbl_manset>().ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }

                    return SQL;
                }

                public static List<tbl_manset_detay> MansetDetay(int manset_id, int dil_id)
                {
                    List<tbl_manset_detay> SQL = new List<tbl_manset_detay>();
                    try
                    {
                        using (BaglantiCumlesi db = new BaglantiCumlesi())
                        {
                            SQL = (from p in db.tbl_manset_detay
                                   where p.manset_id == manset_id && p.dil_id == dil_id
                                   select p).AsEnumerable().Take(1).Cast<tbl_manset_detay>().ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }

                    return SQL;
                }

                public static List<tbl_foto_galeri> FotoGaleri(int id)
                {
                    List<tbl_foto_galeri> SQL = new List<tbl_foto_galeri>();
                    try
                    {
                        using (BaglantiCumlesi db = new BaglantiCumlesi())
                        {
                            SQL = (from p in db.tbl_foto_galeri
                                   where p.id == id
                                   select p).AsEnumerable().Take(1).Cast<tbl_foto_galeri>().ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }

                    return SQL;
                }

                public static List<tbl_foto_galeri_detay> FotoGaleriDetay(int galeri_id, int dil_id)
                {
                    List<tbl_foto_galeri_detay> SQL = new List<tbl_foto_galeri_detay>();
                    try
                    {
                        using (BaglantiCumlesi db = new BaglantiCumlesi())
                        {
                            SQL = (from p in db.tbl_foto_galeri_detay
                                   where p.galeri_id == galeri_id && p.dil_id == dil_id
                                   select p).AsEnumerable().Take(1).Cast<tbl_foto_galeri_detay>().ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }

                    return SQL;
                }

                public static List<tbl_video_galeri> VideoGaleri(int id)
                {
                    List<tbl_video_galeri> SQL = new List<tbl_video_galeri>();
                    try
                    {
                        using (BaglantiCumlesi db = new BaglantiCumlesi())
                        {
                            SQL = (from p in db.tbl_video_galeri
                                   where p.id == id
                                   select p).AsEnumerable().Take(1).Cast<tbl_video_galeri>().ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }

                    return SQL;
                }

                public static List<tbl_video_galeri_detay> VideoGaleriDetay(int galeri_id, int dil_id)
                {
                    List<tbl_video_galeri_detay> SQL = new List<tbl_video_galeri_detay>();
                    try
                    {
                        using (BaglantiCumlesi db = new BaglantiCumlesi())
                        {
                            SQL = (from p in db.tbl_video_galeri_detay
                                   where p.video_galeri_id == galeri_id && p.dil_id == dil_id
                                   select p).AsEnumerable().Take(1).Cast<tbl_video_galeri_detay>().ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }

                    return SQL;
                }

                public static List<tbl_menu> Menu(int id)
                {
                    List<tbl_menu> SQL = new List<tbl_menu>();
                    try
                    {
                        using (BaglantiCumlesi db = new BaglantiCumlesi())
                        {
                            SQL = (from p in db.tbl_menu
                                   where p.id == id
                                   select p).AsEnumerable().Take(1).Cast<tbl_menu>().ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }

                    return SQL;
                }

                public static List<tbl_menu_detay> MenuDetay(int menu_id, int dil_id)
                {
                    List<tbl_menu_detay> SQL = new List<tbl_menu_detay>();
                    try
                    {
                        using (BaglantiCumlesi db = new BaglantiCumlesi())
                        {
                            SQL = (from p in db.tbl_menu_detay
                                   where p.menu_id == menu_id && p.dil_id == dil_id
                                   select p).AsEnumerable().Take(1).Cast<tbl_menu_detay>().ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }

                    return SQL;
                }

                public static List<tbl_resimler> Resimler(int id)
                {
                    List<tbl_resimler> SQL = new List<tbl_resimler>();
                    try
                    {
                        using (BaglantiCumlesi db = new BaglantiCumlesi())
                        {
                            SQL = (from p in db.tbl_resimler
                                   where p.id == id
                                   select p).AsEnumerable().Take(1).Cast<tbl_resimler>().ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }

                    return SQL;
                }

                public static List<tbl_admin> Admin(int admin_id)
                {
                    List<tbl_admin> SQL = new List<tbl_admin>();
                    try
                    {
                        using (BaglantiCumlesi db = new BaglantiCumlesi())
                        {
                            SQL = (from p in db.tbl_admin
                                   where p.id == admin_id
                                   select p).AsEnumerable().Take(1).Cast<tbl_admin>().ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }

                    return SQL;
                }

                public static List<tbl_dil> Dil(int dil_id)
                {
                    List<tbl_dil> SQL = new List<tbl_dil>();
                    try
                    {
                        using (BaglantiCumlesi db = new BaglantiCumlesi())
                        {
                            SQL = (from p in db.tbl_dil
                                   where p.id == dil_id
                                   select p).AsEnumerable().Take(1).Cast<tbl_dil>().ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }

                    return SQL;
                }

                public static string AdminAd(int ID)
                {
                    string Yonetici = string.Empty;

                    using (BaglantiCumlesi db = new BaglantiCumlesi())
                    {
                        var SQL = (from p in db.tbl_admin
                                   where p.id == ID
                                   select new
                                   {
                                       p.ad,
                                       p.soyad
                                   }).AsEnumerable();

                        if (SQL.Any())
                        {
                            Yonetici = SQL.Select(p => p.ad).FirstOrDefault() + " " + SQL.Select(p => p.soyad).FirstOrDefault();
                        }
                    }

                    return Yonetici;
                }
            }
        }

        public class Sabitler
        {
            public enum MessageTypes
            {
                OK, ERROR, Other
            }

            public enum Ayar
            {
                ID, Domain, Marka, Logo, Css, Baslik, Anahtar, Aciklama, GoogleAnalytics, MapApi, VarsayilanDilID, Smtp, SmtpMail, SmtpSifre, SmtpPort, GuvenlikKodu, AnaMenuID, AltMenuID, OperasyonMail, MuhasebeMail, RezervasyonMail, ITMail, YonetimMail, IletisimMail, Telefon, Faks, Adres
            }

            public class StringIslemleri
            {
                public class HarfClass
                {
                    public enum Tip
                    {
                        Buyuk, Kucuk
                    }

                    public enum Islem
                    {
                        Hepsi, IlkHarf, IlkKelime, Secilenler
                    }
                }

                public enum StringIslemTipleri
                {
                    TelFormatla, IbanFormatla, SQLTemizle, DosyaAdiizle, StringTemizle, StringTemizleAdres, Etiket, TurkceKarakter, DosyaAd
                }
            }
        }

        public class Degiskenler
        {
            public class Site
            {
                public static string ResimUzantilari = "*.jpg,*.gif,*.png,*.bmp,*.JPG,*.GIF,*.PNG,*.BMP";

                public class Yollar
                {
                    public static string Ana = @HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"];

                    public static string Resim = "/img/";
                    public static string Css = "/css/";
                    public static string Upload = "/upload/";

                    public static string Bayrak = Upload + "flag/";
                    public static string Banner = Upload + "banner/";
                    public static string Tur = Upload + "excursion/";
                    public static string Manset = Upload + "header/";
                    public static string Otel = Upload + "hotel/";
                    public static string Haber = Upload + "news/";
                }
            }

            public static class Diger
            {
                public static string TamAdres = String.Empty;
            }
        }

        public class Fonksiyonlar
        {
            public class JavaScript
            {
                public static void Yonlendir(string Url)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<script type=\"text/javascript\">\n");
                        sb.Append("//<![CDATA[\n");
                        sb.Append("location.href=\"" + Url + "\"\n");
                        sb.Append("//]]>\n");
                        sb.Append("</script>\n");
                        HttpContext.Current.Response.Write(sb.ToString());
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                }

                public static void MesajKutusuVeYonlendir(string Mesaj, string Url)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<script type=\"text/javascript\">\n");
                        sb.Append("//<![CDATA[\n");
                        sb.Append("alert(\"" + Mesaj.Replace("[ln]", @"\n") + "\");\n");
                        sb.Append("location.href=\"" + Url + "\"\n");
                        sb.Append("//]]>\n");
                        sb.Append("</script>\n");
                        HttpContext.Current.Response.Write(sb.ToString());
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                }

                protected static Hashtable handlerPages = new Hashtable();

                public static void MesajKutusu(string Message)
                {
                    try
                    {
                        if (!(handlerPages.Contains(HttpContext.Current.Handler)))
                        {
                            Page currentPage = (Page)HttpContext.Current.Handler;
                            if (!((currentPage == null)))
                            {
                                Queue messageQueue = new Queue();
                                messageQueue.Enqueue(Message);
                                handlerPages.Add(HttpContext.Current.Handler, messageQueue);
                                currentPage.Unload += new EventHandler(CurrentPageUnload);
                            }
                        }
                        else
                        {
                            Queue queue = ((Queue)(handlerPages[HttpContext.Current.Handler]));
                            queue.Enqueue(Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                }

                private static void CurrentPageUnload(object sender, EventArgs e)
                {
                    try
                    {
                        Queue queue = ((Queue)(handlerPages[HttpContext.Current.Handler]));
                        if (queue != null)
                        {
                            StringBuilder builder = new StringBuilder();
                            int iMsgCount = queue.Count;
                            builder.Append("<script language='javascript'>");
                            string sMsg;
                            while ((iMsgCount > 0))
                            {
                                iMsgCount = iMsgCount - 1;
                                sMsg = System.Convert.ToString(queue.Dequeue());
                                sMsg = sMsg.Replace("\"", "'");
                                builder.Append("alert( \"" + sMsg + "\" );");
                            }
                            builder.Append("</script>");
                            handlerPages.Remove(HttpContext.Current.Handler);
                            HttpContext.Current.Response.Write(builder.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                }
            }

            public class ResimIslemleri
            {
                public static ImageCodecInfo Enkoder(ImageFormat ResimFormati)
                {
                    try
                    {
                        ImageCodecInfo[] ICI = ImageCodecInfo.GetImageDecoders();

                        foreach (ImageCodecInfo i in ICI)
                        {
                            if (i.FormatID == ResimFormati.Guid)
                            {
                                return i;
                            }
                        }

                        return null;
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                        return null;
                    }
                }

                public static void Getir(System.Drawing.Image Resim, int Width, int Height, Color Renk, bool Kirp, Int64 Kalite, int Cozunurluk)
                {
                    try
                    {
                        using (Resim)
                        {
                            using (EncoderParameters EP = new EncoderParameters(1))
                            {
                                EP.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Kalite);

                                using (Bitmap B2 = new Bitmap(Boyutlandir(Resim, Width, Height, Renk, Kirp)))
                                {
                                    if (B2.HorizontalResolution != Cozunurluk || B2.VerticalResolution != Cozunurluk)
                                    {
                                        B2.SetResolution((float)Cozunurluk, (float)Cozunurluk);
                                    }

                                    B2.Save(HttpContext.Current.Response.OutputStream, Enkoder(Format(Resim)), EP);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                }

                public static ImageFormat Format(System.Drawing.Image Resim)
                {
                    ImageFormat I = null;

                    try
                    {
                        using (Resim)
                        {
                            if (Resim.RawFormat.Equals(ImageFormat.Jpeg))
                            {
                                I = ImageFormat.Jpeg;
                            }
                            else if (Resim.RawFormat.Equals(ImageFormat.Bmp))
                            {
                                I = ImageFormat.Bmp;
                            }
                            else if (Resim.RawFormat.Equals(ImageFormat.Gif))
                            {
                                I = ImageFormat.Gif;
                            }
                            else if (Resim.RawFormat.Equals(ImageFormat.Png))
                            {
                                I = ImageFormat.Png;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                        I = ImageFormat.Jpeg;
                    }

                    return I;
                }

                public static System.Drawing.Image Boyutlandir(System.Drawing.Image Resim, int Width, int Height, Color Renk, bool Kirp)
                {
                    Bitmap B = null;

                    try
                    {
                        #region Değişkenler
                        int ResimW = Resim.Width;
                        int ResimH = Resim.Height;
                        int ResimX = 0;
                        int ResimY = 0;
                        int SonucX = 0;
                        int SonucY = 0;

                        float Yuzde = 0;
                        float YuzdeW = 0;
                        float YuzdeH = 0;

                        YuzdeW = ((float)Width / (float)ResimW);
                        YuzdeH = ((float)Height / (float)ResimH);

                        if (YuzdeH < YuzdeW)
                        {
                            Yuzde = YuzdeH;
                            SonucX = System.Convert.ToInt16((Width - (ResimW * Yuzde)) / 2);
                        }
                        else
                        {
                            Yuzde = YuzdeW;
                            SonucY = System.Convert.ToInt16((Height - (ResimH * Yuzde)) / 2);
                        }

                        int SonucW = (int)(ResimW * Yuzde);
                        int SonucH = (int)(ResimH * Yuzde);
                        #endregion

                        if (Kirp)
                        {
                            B = new Bitmap(Width, Height);
                        }
                        else
                        {
                            B = new Bitmap(SonucW, SonucH);
                        }

                        //B.SetResolution(Resim.HorizontalResolution,Resim.VerticalResolution);

                        using (Graphics G = Graphics.FromImage(B))
                        {
                            if (Kirp)
                            {
                                G.Clear(Renk);
                            }

                            #region Resim Kalitesi
                            G.CompositingQuality = CompositingQuality.HighQuality;
                            G.SmoothingMode = SmoothingMode.HighQuality;
                            G.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            #endregion

                            if (Kirp)
                            {
                                G.DrawImage(Resim, new Rectangle(SonucX, SonucY, SonucW, SonucH), new Rectangle(ResimX, ResimY, ResimW, ResimH), GraphicsUnit.Pixel);
                            }
                            else
                            {
                                G.DrawImage(Resim, 0, 0, SonucW, SonucH);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }

                    return B;
                }
            }

            public class DilAyarlari
            {
                public static void Olustur(string CookieAdi, string Deger)
                {
                    HttpContext.Current.Response.SetCookie(new HttpCookie(CookieAdi, Deger));
                }

                public static void DomainVer()
                {
                    if (UrlvePathYaz().IndexOf(Class.Fonksiyonlar.Ayar().Select(k => k.domain).FirstOrDefault()) != -1)
                    {
                        string[] cookies = HttpContext.Current.Request.Cookies.AllKeys;

                        foreach (string cookie in cookies)
                        {
                            HttpContext.Current.Response.Cookies[cookie].Domain = "." + Class.Fonksiyonlar.Ayar().Select(k => k.domain).FirstOrDefault();
                        }
                    }
                }

                public static void Kontrol()
                {
                    if (HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "Lang"] != null)
                    {
                        if (!string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "Lang"].Value))
                        {
                            int DilID = int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "Lang"].Value);

                            using (BaglantiCumlesi db = new BaglantiCumlesi())
                            {
                                var SQL = (from p in db.tbl_dil
                                           where p.id == DilID
                                           select new
                                           {
                                               p.culture
                                           });

                                if (SQL.Any())
                                {
                                    foreach (var i in SQL)
                                    {
                                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(i.culture);
                                        Thread.CurrentThread.CurrentUICulture = new CultureInfo(i.culture);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Olustur(Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "Lang", Class.Fonksiyonlar.Ayar().Select(k => k.varsayilan_dil).FirstOrDefault().ToString());
                        }
                    }
                    else
                    {
                        Olustur(Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "Lang", Class.Fonksiyonlar.Ayar().Select(k => k.varsayilan_dil).FirstOrDefault().ToString());
                    }

                    /*if (HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "Lang"].Value != Class.Fonksiyonlar.Ayar(Class.Sabitler.Ayar.VarsayilanDilID))
                    {
                        
                    }*/
                }

                public static int ID()
                {
                    Kontrol();

                    return int.Parse(HttpContext.Current.Request.Cookies[Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "Lang"].Value);
                }
            }

            public class OturumIslemleri
            {
                public static void CookieOlustur(string CookieAdi, string Deger)
                {
                    HttpContext.Current.Response.SetCookie(new HttpCookie(Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + CookieAdi, Deger));
                }

                public static void CookieSil()
                {
                    HttpContext.Current.Session.Clear();
                    HttpContext.Current.Session.RemoveAll();
                    HttpContext.Current.Session.Abandon();

                    string[] cookies = HttpContext.Current.Request.Cookies.AllKeys;
                    HttpCookie tmpCookie;

                    foreach (string cookieKey in cookies)
                    {
                        tmpCookie = HttpContext.Current.Response.Cookies[cookieKey];
                        tmpCookie.Expires = DateTime.Now.AddDays(-2);
                        HttpContext.Current.Response.Cookies.Add(tmpCookie);
                    }

                    //HttpContext.Current.Response.Redirect("Giris.aspx");
                }

                public static void CookieKontrol()
                {
                    if (HttpContext.Current.Request.Cookies["" + Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "Giris"] != null)
                    {
                        if (HttpContext.Current.Request.Cookies["" + Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "Giris"].Value != "7777777")
                        {
                            HttpContext.Current.Response.Redirect("login.aspx");
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("login.aspx");
                    }
                }
            }

            public static bool NumerikKontrol(string str)
            {
                int numeric;

                if (int.TryParse(str, out numeric))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public static void Nerdesin(string str)
            {
                try
                {
                    //str = "Ana Sayfa|default.aspx,Hakkımızda|hakkimizda.aspx,İletişim|?";
                    string[] dizi = str.Split(',');

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<ul class=\"breadcrumb\">");

                    foreach (string i in dizi)
                    {
                        string[] dizi2 = i.Split('|');

                        if (dizi2[1] == "?")
                        {
                            sb.Append("<li class=\"active\">" + dizi2[0] + "</li>");
                        }
                        else
                        {
                            sb.Append("<li><a href=\"/" + dizi2[1] + "\">" + dizi2[0] + "</a> <span class=\"divider\">»</span></li>");
                        }
                    }

                    sb.Append("</ul>");

                    HeaderText("top_header", "lit_nerdesin", sb.ToString());
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    HeaderText("top_header", "lit_nerdesin", Class.Fonksiyonlar.Textler(20));
                }
            }

            public static void MailGonder(string KullaniciAdi, string Sifre, string Host, int Port, string TO, string CC, string BCC, string GonderenMail, string GonderenIsim, string Konu, string Icerik, bool HTML)
            {
                try
                {
                    using (MailMessage MM = new MailMessage())
                    {
                        MM.To.Add(TO);

                        if (CC != null)
                        {
                            MM.CC.Add(CC);
                        }

                        if (BCC != null)
                        {
                            MM.Bcc.Add(BCC);
                        }

                        MM.From = new MailAddress(GonderenMail, GonderenIsim);
                        MM.BodyEncoding = Encoding.GetEncoding("utf-8");
                        MM.Subject = Konu;

                        AlternateView AV;

                        if (HTML)
                        {
                            MM.IsBodyHtml = true;
                            AV = AlternateView.CreateAlternateViewFromString(@Icerik, null, "text/html");
                        }
                        else
                        {
                            MM.IsBodyHtml = false;
                            AV = AlternateView.CreateAlternateViewFromString(@Icerik, null, "text/plain");
                        }

                        MM.AlternateViews.Add(AV);

                        using (SmtpClient SC = new SmtpClient())
                        {
                            SC.Host = Host;
                            SC.Port = Port;
                            SC.UseDefaultCredentials = false;

                            NetworkCredential NC = new NetworkCredential();
                            NC.UserName = KullaniciAdi;
                            NC.Password = Sifre;

                            SC.Credentials = NC;
                            SC.DeliveryMethod = SmtpDeliveryMethod.Network;
                            SC.EnableSsl = true;
                            SC.Send(MM);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }

            public static string Textler(int TextID)
            {
                try
                {
                    int DilID = Class.Fonksiyonlar.DilAyarlari.ID();

                    using (BaglantiCumlesi db = new BaglantiCumlesi())
                    {
                        var SQL = (from p in db.tbl_text_detay
                                   where p.dil_id == DilID && p.text_id == TextID
                                   select p.baslik);

                        if (SQL.Any())
                        {
                            return SQL.FirstOrDefault().ToString();
                        }
                        else
                        {
                            return "Language Error";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    return null;
                }
            }

            public static string StringIslemleri(Sabitler.StringIslemleri.StringIslemTipleri Islem, string str)
            {
                try
                {
                    string Sonuc = "";

                    if (!string.IsNullOrEmpty(str))
                    {
                        switch (Islem)
                        {
                            case Sabitler.StringIslemleri.StringIslemTipleri.SQLTemizle:
                                //Regex R4 = new Regex("['’]", RegexOptions.None);
                                //Sonuc = R4.Replace(str, "`");
                                Sonuc = Regex.Replace(str, @"'", "`");
                                Sonuc = Regex.Replace(Sonuc, @"’", "`");
                                break;

                            case Sabitler.StringIslemleri.StringIslemTipleri.DosyaAdiizle:
                                //Alfabe Dışındaki Karakterler
                                Regex R1 = new Regex("(?:[^á-ža-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                                //Boşluklar
                                Regex R2 = new Regex(@"[ ]{2,}", RegexOptions.None);
                                //Türkçe Karakterler
                                Regex R3 = new Regex("(?:[^ĞÜŞİÖÇ])", RegexOptions.None);
                                Sonuc = R2.Replace(R1.Replace(str.Trim().ToString().ToLower(), String.Empty).Trim(), @" ").Trim();
                                break;

                            case Sabitler.StringIslemleri.StringIslemTipleri.StringTemizle:
                                Sonuc = str.Replace("''", "");
                                Sonuc = Sonuc.Replace("'", "");
                                Sonuc = Sonuc.Replace("’", "");
                                Sonuc = Sonuc.Replace("~", "");
                                Sonuc = Sonuc.Replace("}", "");
                                Sonuc = Sonuc.Replace("|", "");
                                Sonuc = Sonuc.Replace("{", "");
                                Sonuc = Sonuc.Replace("`", "");
                                Sonuc = Sonuc.Replace("^", "");
                                Sonuc = Sonuc.Replace("]", "");
                                Sonuc = Sonuc.Replace("\\", "");
                                Sonuc = Sonuc.Replace("[", "");
                                Sonuc = Sonuc.Replace("@", "");
                                Sonuc = Sonuc.Replace("?", "");
                                Sonuc = Sonuc.Replace(">", "");
                                Sonuc = Sonuc.Replace("=", "");
                                Sonuc = Sonuc.Replace("<", "");
                                Sonuc = Sonuc.Replace(";", "");
                                Sonuc = Sonuc.Replace("//", "");
                                Sonuc = Sonuc.Replace("--", "");
                                Sonuc = Sonuc.Replace("+", "");
                                Sonuc = Sonuc.Replace("*", "");
                                Sonuc = Sonuc.Replace(")", "");
                                Sonuc = Sonuc.Replace("(", "");
                                Sonuc = Sonuc.Replace("&", "");
                                Sonuc = Sonuc.Replace("%", "");
                                Sonuc = Sonuc.Replace("$", "");
                                Sonuc = Sonuc.Replace("#", "");
                                Sonuc = Sonuc.Replace("!", "");
                                Sonuc = Sonuc.Replace("..", "");
                                Sonuc = Sonuc.Replace(" ", "_");

                                break;

                            case Sabitler.StringIslemleri.StringIslemTipleri.StringTemizleAdres:
                                Sonuc = str.Replace("''", "");
                                Sonuc = Sonuc.Replace("'", "");
                                Sonuc = Sonuc.Replace("’", "");
                                Sonuc = Sonuc.Replace("~", "");
                                Sonuc = Sonuc.Replace("}", "");
                                Sonuc = Sonuc.Replace("|", "");
                                Sonuc = Sonuc.Replace("{", "");
                                Sonuc = Sonuc.Replace("`", "");
                                Sonuc = Sonuc.Replace("^", "");
                                Sonuc = Sonuc.Replace("]", "");
                                Sonuc = Sonuc.Replace("\\", "");
                                Sonuc = Sonuc.Replace("[", "");
                                Sonuc = Sonuc.Replace("@", "");
                                Sonuc = Sonuc.Replace("?", "");
                                Sonuc = Sonuc.Replace(">", "");
                                Sonuc = Sonuc.Replace("=", "");
                                Sonuc = Sonuc.Replace("<", "");
                                Sonuc = Sonuc.Replace(";", "");
                                Sonuc = Sonuc.Replace("//", "");
                                Sonuc = Sonuc.Replace("--", "-");
                                Sonuc = Sonuc.Replace("+", "");
                                Sonuc = Sonuc.Replace("*", "");
                                Sonuc = Sonuc.Replace(")", "");
                                Sonuc = Sonuc.Replace("(", "");
                                Sonuc = Sonuc.Replace("&", "-");
                                Sonuc = Sonuc.Replace("%", "");
                                Sonuc = Sonuc.Replace("$", "");
                                Sonuc = Sonuc.Replace("#", "");
                                Sonuc = Sonuc.Replace("!", "");
                                Sonuc = Sonuc.Replace(".", "");
                                Sonuc = Sonuc.Replace(",", "");
                                Sonuc = Sonuc.Replace("_", "");
                                Sonuc = Sonuc.Replace(":", "");

                                break;

                            case Sabitler.StringIslemleri.StringIslemTipleri.Etiket:
                                Sonuc = str.Replace(" ", "-");
                                Sonuc = Sonuc.Replace("Ğ", "G");
                                Sonuc = Sonuc.Replace("Ü", "U");
                                Sonuc = Sonuc.Replace("Ş", "S");
                                Sonuc = Sonuc.Replace("İ", "I");
                                Sonuc = Sonuc.Replace("Ö", "O");
                                Sonuc = Sonuc.Replace("Ç", "C");
                                Sonuc = Sonuc.ToLower();
                                Sonuc = Sonuc.Replace("ğ", "g");
                                Sonuc = Sonuc.Replace("ü", "u");
                                Sonuc = Sonuc.Replace("ş", "s");
                                Sonuc = Sonuc.Replace("ı", "i");
                                Sonuc = Sonuc.Replace("ö", "o");
                                Sonuc = Sonuc.Replace("ç", "c");

                                Sonuc = Sonuc.Replace("&", "");
                                Sonuc = Sonuc.Replace("--", "-");

                                break;

                            case Sabitler.StringIslemleri.StringIslemTipleri.TurkceKarakter:
                                Sonuc = str.Replace("Ğ", "G");
                                Sonuc = Sonuc.Replace("Ü", "U");
                                Sonuc = Sonuc.Replace("Ş", "S");
                                Sonuc = Sonuc.Replace("İ", "I");
                                Sonuc = Sonuc.Replace("Ö", "O");
                                Sonuc = Sonuc.Replace("Ç", "C");
                                Sonuc = Sonuc.Replace("ğ", "g");
                                Sonuc = Sonuc.Replace("ü", "u");
                                Sonuc = Sonuc.Replace("ş", "s");
                                Sonuc = Sonuc.Replace("ı", "i");
                                Sonuc = Sonuc.Replace("ö", "o");
                                Sonuc = Sonuc.Replace("ç", "c");

                                break;

                            case Sabitler.StringIslemleri.StringIslemTipleri.DosyaAd:
                                Sonuc = str.Replace("''", "");
                                Sonuc = Sonuc.Replace("'", "");
                                Sonuc = Sonuc.Replace("’", "");
                                Sonuc = Sonuc.Replace("~", "");
                                Sonuc = Sonuc.Replace("}", "");
                                Sonuc = Sonuc.Replace("|", "");
                                Sonuc = Sonuc.Replace("{", "");
                                Sonuc = Sonuc.Replace("`", "");
                                Sonuc = Sonuc.Replace("^", "");
                                Sonuc = Sonuc.Replace("]", "");
                                Sonuc = Sonuc.Replace("\\", "");
                                Sonuc = Sonuc.Replace("[", "");
                                Sonuc = Sonuc.Replace("@", "");
                                Sonuc = Sonuc.Replace("?", "");
                                Sonuc = Sonuc.Replace(">", "");
                                Sonuc = Sonuc.Replace("=", "");
                                Sonuc = Sonuc.Replace("<", "");
                                Sonuc = Sonuc.Replace(";", "");
                                Sonuc = Sonuc.Replace("//", "");
                                Sonuc = Sonuc.Replace("+", "");
                                Sonuc = Sonuc.Replace("*", "");
                                Sonuc = Sonuc.Replace(")", "");
                                Sonuc = Sonuc.Replace("(", "");
                                Sonuc = Sonuc.Replace("&", "-");
                                Sonuc = Sonuc.Replace("%", "");
                                Sonuc = Sonuc.Replace("$", "");
                                Sonuc = Sonuc.Replace("#", "");
                                Sonuc = Sonuc.Replace("!", "");
                                Sonuc = Sonuc.Replace(".", "");
                                Sonuc = Sonuc.Replace(",", "");
                                Sonuc = Sonuc.Replace("_", "-");
                                Sonuc = Sonuc.Replace(":", "");
                                Sonuc = Sonuc.Replace("Ğ", "G");
                                Sonuc = Sonuc.Replace("Ü", "U");
                                Sonuc = Sonuc.Replace("Ş", "S");
                                Sonuc = Sonuc.Replace("İ", "I");
                                Sonuc = Sonuc.Replace("Ö", "O");
                                Sonuc = Sonuc.Replace("Ç", "C");
                                Sonuc = Sonuc.Replace("ğ", "g");
                                Sonuc = Sonuc.Replace("ü", "u");
                                Sonuc = Sonuc.Replace("ş", "s");
                                Sonuc = Sonuc.Replace("ı", "i");
                                Sonuc = Sonuc.Replace("ö", "o");
                                Sonuc = Sonuc.Replace("ç", "c");
                                Sonuc = Sonuc.Replace(" ", "-");
                                Sonuc = Sonuc.Replace("--", "-");
                                Sonuc = Sonuc.ToLower();

                                break;
                        }

                        return Sonuc.ToString();
                    }
                    else
                    {
                        return Sonuc.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    return null;
                }
            }

            public static string KarakterTemizle(string str, bool UrlEncode)
            {
                try
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        //Alfabe Dışındaki Karakterler
                        Regex R1 = new Regex("(?:[^á-ža-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

                        //Boşluklar
                        Regex R2 = new Regex(@"[ ]{2,}", RegexOptions.None);

                        //Türkçe Karakterler
                        //Regex R3 = new Regex("(?:[^ĞÜŞİÖÇ])", RegexOptions.None);

                        string sonuc = R2.Replace(R1.Replace(str.Trim().Replace("&amp;", "&").Replace("&", ""), String.Empty).Trim(), @" ").Trim();

                        if (UrlEncode)
                        {
                            return HttpContext.Current.Server.UrlEncode(StringIslemleri(Sabitler.StringIslemleri.StringIslemTipleri.TurkceKarakter, sonuc.ToLower())).Replace("+", "-");
                        }
                        else
                        {
                            return sonuc;
                        }
                    }
                    else
                    {
                        return str;
                    }
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    return str;
                }
            }

            public static string HarfBoyutlandirma(string str, Sabitler.StringIslemleri.HarfClass.Tip Tip, Sabitler.StringIslemleri.HarfClass.Islem Islem, string[] BoyutlanacakHarfler)
            {
                string ReturnString = str;

                try
                {
                    if (Tip == Sabitler.StringIslemleri.HarfClass.Tip.Buyuk)
                    {
                        switch (Islem)
                        {
                            case Sabitler.StringIslemleri.HarfClass.Islem.Hepsi:
                                ReturnString = str.ToUpper();
                                break;
                            case Sabitler.StringIslemleri.HarfClass.Islem.IlkHarf:
                                ReturnString = str.Substring(0, 1).ToUpper();
                                break;
                            case Sabitler.StringIslemleri.HarfClass.Islem.IlkKelime:
                                break;
                            case Sabitler.StringIslemleri.HarfClass.Islem.Secilenler:
                                for (int i = 0; i < BoyutlanacakHarfler.Length; i++)
                                {
                                    ReturnString = str.Replace(BoyutlanacakHarfler[i], BoyutlanacakHarfler[i].ToUpper());
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else if (Tip == Sabitler.StringIslemleri.HarfClass.Tip.Kucuk)
                    {
                        switch (Islem)
                        {
                            case Sabitler.StringIslemleri.HarfClass.Islem.Hepsi:
                                ReturnString = str.ToLower();
                                break;
                            case Sabitler.StringIslemleri.HarfClass.Islem.IlkHarf:
                                ReturnString = str.Substring(0, 1).ToLower();
                                break;
                            case Sabitler.StringIslemleri.HarfClass.Islem.IlkKelime:
                                break;
                            case Sabitler.StringIslemleri.HarfClass.Islem.Secilenler:
                                for (int i = 0; i < BoyutlanacakHarfler.Length; i++)
                                {
                                    ReturnString = str.Replace(BoyutlanacakHarfler[i], BoyutlanacakHarfler[i].ToLower());
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    ReturnString = str;
                }

                return ReturnString;
            }

            public static string SifreUret()
            {
                char[] karakter = "0123456789abcdefghijklmnopqrstuvwxyz".ToCharArray();

                string sonuc = string.Empty;
                Random r = new Random();

                for (int i = 0; i < 7; i++)
                {
                    sonuc += karakter[r.Next(0, karakter.Length - 1)].ToString();
                }

                return sonuc;
            }

            public static string Sifrele(string str)
            {
                try
                {
                    return FormsAuthentication.HashPasswordForStoringInConfigFile(FormsAuthentication.HashPasswordForStoringInConfigFile(FormsAuthentication.HashPasswordForStoringInConfigFile(str + "- Fatih ÜNAL", "sha1"), "md5"), "md5");
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    return str;
                }
            }

            public static List<tbl_ayar> Ayar()
            {
                List<tbl_ayar> SQL = new List<tbl_ayar>();
                try
                {
                    using (BaglantiCumlesi db = new BaglantiCumlesi())
                    {
                        SQL = (from p in db.tbl_ayar
                               where p.id == 1
                               select p).AsEnumerable().Cast<tbl_ayar>().ToList();
                    }
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }

                return SQL;
            }

            public static string SeoLink(string sayfa, string ID, string baslik)
            {
                return "/" + sayfa + "/" + ID + "/" + StringIslemleri(Sabitler.StringIslemleri.StringIslemTipleri.Etiket, baslik) + ".aspx";
            }

            public static void HeaderText(string UserControlAdi, string LiteralID, string Text)
            {
                try
                {
                    if (LiteralID == "Title")
                    {
                        var Page = (Page)HttpContext.Current.Handler;
                        Page.Title = Text;
                    }
                    else
                    {
                        UserControl UC = (UserControl)((Page)HttpContext.Current.Handler).FindControl(UserControlAdi);
                        System.Web.UI.WebControls.Literal Li = UC.FindControl(LiteralID) as System.Web.UI.WebControls.Literal;
                        Li.Text = Text;
                    }
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }

            public static string UrlvePathYaz()
            {
                string protocol = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
                if (String.IsNullOrEmpty(protocol) | String.Equals(protocol, "0"))
                    protocol = "http://";
                else
                    protocol = "https://";

                string currentAddress = HttpContext.Current.Request.Url.ToString();
                Regex rx = new Regex(@"([^/]*)(localhost|\blocalhost:\d+\b)([^/]*)", RegexOptions.IgnoreCase);
                Match MatchObj = rx.Match(currentAddress);
                if (!(string.IsNullOrEmpty(MatchObj.Groups[0].Value)))
                {
                    Degiskenler.Diger.TamAdres = String.Concat(protocol,
                    MatchObj.Groups[0].Value,
                    HttpContext.Current.Request.ApplicationPath);
                }
                else
                {
                    Degiskenler.Diger.TamAdres = String.Concat(protocol,
                    HttpContext.Current.Request.ServerVariables["SERVER_NAME"],
                    HttpContext.Current.Request.ApplicationPath);
                }

                if (!Degiskenler.Diger.TamAdres.EndsWith("/"))
                    Degiskenler.Diger.TamAdres += "/";

                return Degiskenler.Diger.TamAdres;
            }

            public static string MevcutSayfa()
            {
                return (HttpContext.Current.Request.Url.AbsoluteUri);
            }

            public static int BoolToInteger(bool GelenBool)
            {
                try
                {
                    if (GelenBool)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    return 0;
                }
            }

            public static bool StringToBool(string GelenString)
            {
                try
                {
                    if (GelenString == "1")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    return false;
                }
            }

            public static bool ChkToBool(string GelenString)
            {
                try
                {
                    if (GelenString == "on")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    return false;
                }
            }
        }
    }
}