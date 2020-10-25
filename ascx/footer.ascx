<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="footer.ascx.cs" Inherits="rodanit_com.ascx.footer" %>

<div class="navbar pah-none" style="background: #FFF; margin-bottom: 0px; margin-top: 0px; padding: 10px 0px;">
    <div class="container">
    <marquee behavior="scroll" onmouseover="this.stop();" onmouseout="this.start();" scrollamount="5" height="80" width="100%" direction="left" scrolldelay="0">
        <img src="/upload/diger/rodanit_madencilik.png" />&nbsp;&nbsp;&nbsp;
        <img src="/upload/diger/rodanit_mermer.png" />&nbsp;&nbsp;&nbsp;
        <img src="/upload/diger/rodanit_turizm.png" />&nbsp;&nbsp;&nbsp;
        <img src="/upload/diger/rodanit_insaat.png" />
        </marquee>
    </div>
    </div>

<div class="navbar pah-none" style="background: #333333; border-top: 3px solid #D1D1C5;
    margin-bottom: 0px; margin-top: 0px; padding: 20px 0px;">
    <div class="container" style="color:#FFF;">
        <div class="row">
            <div class="col-md-4">
                &copy; <% Response.Write(rodanit_com.Class.Fonksiyonlar.Ayar().Select(k=>k.sirket_unvan).FirstOrDefault()); %> 2014<br /><br />
                <address>
                <%if (!string.IsNullOrEmpty(rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.telefon).FirstOrDefault()))
                            {
                                Response.Write("<abbr title=\"Phone\">T:</abbr> " + rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.telefon).FirstOrDefault() + "<br />");
                            }

                  if (!string.IsNullOrEmpty(rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.faks).FirstOrDefault()))
                  {
                      Response.Write("<abbr title=\"Fax\">F:</abbr> " + rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.faks).FirstOrDefault() + "<br />");
                  }
                            
                            if (!string.IsNullOrEmpty(rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.mail).FirstOrDefault()))
                            {
                                Response.Write("<abbr title=\"Mail\">E:</abbr> " + rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.mail).FirstOrDefault() + "<br />");
                            }
                            %>
                            </address>
            </div>
            <div class="col-md-4">
            </div>
            <div class="col-md-4" style="text-align: right;">
        <a style="color:#FFF;" href="#" onclick="$('html,body').animate({scrollTop:0},750,function(){});return false;">
            <span class="glyphicon glyphicon-arrow-up"></span> Yukarı</a><br />
        <br />
            
                <!-- AddThis Button BEGIN -->
                <div class="addthis_toolbox addthis_default_style addthis_32x32_style" style="float: right;">
                    <a class="addthis_button_preferred_1"></a><a class="addthis_button_preferred_2">
                    </a><a class="addthis_button_preferred_3"></a><a class="addthis_button_preferred_4">
                    </a><a class="addthis_button_compact"></a><a class="addthis_counter addthis_bubble_style">
                    </a>
                </div>
                <script type="text/javascript">                    var addthis_config = { "data_track_addressbar": true };</script>
                <script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=aristor"></script>
                <!-- AddThis Button END -->
            </div>
        </div>
    </div>
</div>
