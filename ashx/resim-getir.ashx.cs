using System;
using System.Web;

namespace rodanit_com.ashx
{
    [Serializable]
    public class resim_getir : IHttpHandler
    {
        public void ProcessRequest(HttpContext HC)
        {
            HttpResponse HR = HC.Response;

            int w = 1;
            int h = 1;
            bool k = false;
            string r = null;
            //string y = null;

            if (HC.Request.QueryString["i"] != null)
            {
                if (!string.IsNullOrEmpty(HC.Request.QueryString["i"]))
                {
                    r = @HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"] + HC.Request.QueryString["i"].ToString();
                }
            }

            if (HC.Request.QueryString["w"] != null)
            {
                if (!string.IsNullOrEmpty(HC.Request.QueryString["w"]))
                {
                    if (Convert.ToInt16(HC.Request.QueryString["w"]) < 3000)
                    {
                        w = Convert.ToInt16(HC.Request.QueryString["w"]);
                    }
                }
            }

            if (HC.Request.QueryString["h"] != null)
            {
                if (!string.IsNullOrEmpty(HC.Request.QueryString["h"]))
                {
                    if (Convert.ToInt16(HC.Request.QueryString["h"]) < 3000)
                    {
                        h = Convert.ToInt16(HC.Request.QueryString["h"]);
                    }
                }
            }

            if (HC.Request.QueryString["k"] != null)
            {
                if (!string.IsNullOrEmpty(HC.Request.QueryString["k"]))
                {
                    k = true;
                }
            }

            if (!System.IO.File.Exists(r))
            {
                r = @HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"] + "upload/blank.jpg";
            }

            using (System.Drawing.Image I = System.Drawing.Image.FromFile(r))
            {
                if (I.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                {
                    HR.ContentType = "image/jpeg";
                }
                else if (I.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                {
                    HR.ContentType = "image/bmp";
                }
                else if (I.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                {
                    HR.ContentType = "image/gif";
                }
                else if (I.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                {
                    HR.ContentType = "image/png";
                }

                Class.Fonksiyonlar.ResimIslemleri.Getir(I, w, h, System.Drawing.Color.White, k, 90, 72);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}