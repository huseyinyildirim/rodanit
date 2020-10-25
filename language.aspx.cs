using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rodanit_com
{
    public partial class language : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.QueryString["id"] != null)
            {
                if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["id"].ToString()))
                {
                    Class.Fonksiyonlar.DilAyarlari.Olustur(Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "Lang", Class.Fonksiyonlar.Ayar().Select(k => k.varsayilan_dil).FirstOrDefault().ToString());
                }
                else
                {
                    Class.Fonksiyonlar.DilAyarlari.Olustur(Class.Fonksiyonlar.Ayar().Select(k => k.guvenlik_kodu).FirstOrDefault() + "Lang", HttpContext.Current.Request.QueryString["id"].ToString());
                }

                if (Request.QueryString["referer"] != null)
                {
                    if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["referer"].ToString()))
                    {
                        Class.Fonksiyonlar.JavaScript.Yonlendir(Request.QueryString["referer"].ToString());
                    }
                    else
                    {
                        Class.Fonksiyonlar.JavaScript.Yonlendir("default.aspx");
                    }
                }
                else
                {
                    Class.Fonksiyonlar.JavaScript.Yonlendir("default.aspx");
                }
            }
            else
            {
                Class.Fonksiyonlar.JavaScript.Yonlendir("default.aspx");
            }
        }
    }
}