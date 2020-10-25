<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="rodanit_com._default" %>

<%@ Register TagPrefix="include" TagName="head" Src="~/ascx/head.ascx" %>
<%@ Register TagPrefix="include" TagName="header" Src="~/ascx/header.ascx" %>
<%@ Register TagPrefix="include" TagName="footer" Src="~/ascx/footer.ascx" %>
<%@ Register TagPrefix="include" TagName="js" Src="~/ascx/js.ascx" %>
<%@ Register TagPrefix="include" TagName="manset" Src="~/ascx/manset.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <include:head runat="server" ID="head" />
</head>
<body>
    <form id="form1" runat="server">
    <include:header runat="server" ID="header" />
    <div class="navbar pah-none" style="background: #EFEDE0; border-top: 3px solid #D1D1C5; padding-top: 20px; margin-bottom:0px;">
        <include:manset runat="server" ID="manset" />
        <br />
        <div class="container">
            <div class="row">
            <div class="col-md-3">
                    <img src="/upload/diger/ne_yapariz.jpg" alt="" class="img-thumbnail">
                    <blockquote>
                        <p>
                            Ne Yaparız?</p>
                        <small>Yaşam alanlarınızı mermerin dansı ile hareketlendirecek uygulamalar yapıyoruz.</small>
                    </blockquote>
                </div>
                <div class="col-md-3">
                    <img src="/upload/diger/uygulama.jpg" alt="" class="img-thumbnail">
                    <blockquote>
                        <p>
                            Uygulamalarımız</p>
                        <small>Mekanlarına hayat verecek uygulamalar hakkında bilgi almak istermisiniz?</small>
                    </blockquote>
                </div>
                <div class="col-md-3">
                    <img src="/upload/diger/referans.jpg" alt="" class="img-thumbnail">
                    <blockquote>
                        <p>
                            Projelerimiz</p>
                        <small>Uygulamalarımızı işimizin aynasıdır, sizde neler yapabildiğimizi görmek istermisiniz?</small>
                    </blockquote>
                </div>
                <div class="col-md-3">
                    <img src="/upload/diger/iletisim.jpg" alt="" class="img-thumbnail">
                    <blockquote>
                        <p>
                            İletişim</p>
                        <small>Projeleriniz hakkında destek almak istiyorsanız bizimle iletişime geçebilirsiniz.</small>
                    </blockquote>
                </div>
            </div>
        </div>
    </div>
    <include:footer runat="server" ID="footer" />
    </form>
    <include:js runat="server" ID="js" />
</body>
</html>
