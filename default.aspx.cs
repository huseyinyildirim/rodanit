using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rodanit_com
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Sayfa seo ayarları
            Class.Fonksiyonlar.HeaderText("head", "Title", Class.Fonksiyonlar.Ayar().Select(k=>k.seo_baslik).FirstOrDefault());
            Class.Fonksiyonlar.HeaderText("head", "lit_anahtar", "<meta http-equiv=\"Keywords\" content=\"" + Class.Fonksiyonlar.Ayar().Select(k => k.seo_anahtar).FirstOrDefault() + "\" />");
            Class.Fonksiyonlar.HeaderText("head", "lit_aciklama", "<meta http-equiv=\"Description\" content=\"" + Class.Fonksiyonlar.Ayar().Select(k => k.seo_aciklama).FirstOrDefault() + "\" />");
            #endregion
        }
    }
}