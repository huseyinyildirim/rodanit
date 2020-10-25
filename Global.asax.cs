using System;

namespace rodanit_com
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            System.Web.Routing.RouteTable.Routes.Add("page", new System.Web.Routing.Route("page/{page_id}/{page_name}", new System.Web.Routing.PageRouteHandler("~/page.aspx")));
            System.Web.Routing.RouteTable.Routes.Add("photo-gallery", new System.Web.Routing.Route("photo-gallery/{photo_gallery_id}/{photo_gallery_name}", new System.Web.Routing.PageRouteHandler("~/photo-gallery-detail.aspx")));
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            #region Sitenin varsayilan dilini kontrol ediyor
            Class.Fonksiyonlar.DilAyarlari.Kontrol();
            #endregion

            #region Online kullanıcı sayısı arttırma
            Application.Lock();
            Application["OnlineUser"] = Convert.ToInt32(Application["OnlineUser"]) + 1;
            Application.UnLock();
            #endregion
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            #region Online kullanıcı sayısı eksiltme
            Application.Lock();
            Application["OnlineUser"] = Convert.ToInt32(Application["OnlineUser"]) - 1;
            Application.UnLock();
            #endregion
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}