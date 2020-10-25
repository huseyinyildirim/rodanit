<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="rodanit_com.yonetim._default" %> 

<%@ Register TagPrefix="include" TagName="Head" Src="~/yonetim/ascx/head.ascx" %>
<%@ Register TagPrefix="include" TagName="Ust" Src="~/yonetim/ascx/ust.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <include:head runat="server" ID="head" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".menu ul #panel").addClass("secili");
            $(".menualt #panel_detay").css("display", "inline");
        });
    </script>
    <asp:Literal runat="server" ID="grafik" />
</head>
<body>
    <form id="form1" runat="server">
    <include:ust runat="server" ID="ust" />
        <div class="icerik">
            <div class="blokbaslik">Masaüstü</div>
            <div class="blokicerik">
                <table width="100%">
                    <tbody>
                        <tr>
                            <td width="48%" valign="top">
                                <table width="100%">
                                    <tr>
                                        <td align="center">
                                            <img src="img/icon-haber-ekle.png" /></td>
                                        <td align="center">
                                            <img src="img/icon-faaliyet-ekle.png" /></td>
                                        <td align="center">
                                            <img src="img/icon-galeri-ekle.png" /></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <a href="haber-ekle.aspx">Yeni Haber Ekle</a></td>
                                        <td align="center">
                                            <a href="manset-ekle.aspx">Yeni Manşet Ekle</a></td>
                                        <td align="center">
                                            <a href="galeri-ekle.aspx">Yeni Galeri Ekle</a></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            &nbsp;</td>
                                        <td align="center">
                                            &nbsp;</td>
                                        <td align="center">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <img class="style2" src="img/icon-kullanici.png" /></td>
                                        <td align="center">
                                            <img class="style2" src="img/icon-kullanici-ekle.png" /></td>
                                        <td align="center">
                                            <img src="img/icon-video-ekle.png" /></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <a href="kullanici.aspx">Kullanıcılar</a></td>
                                        <td align="center">
                                            <a href="kullanici-ekle.aspx">Yeni Kullanıcı Ekle</a></td>
                                        <td align="center">
                                            <a href="video-galeri-ekle.aspx">Yeni Video Ekle</a></td>
                                    </tr>
                                </table>
                            </td>
                            <td width="2%">&nbsp;</td>
                            <td width="48%" valign="top" align="center">Ziyaretçi İstatistikleri<br /><div id="chart_div" style="width:800px; height:300px;"></div></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
