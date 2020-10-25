<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page.aspx.cs" Inherits="rodanit_com.page" %>

<%@ Register TagPrefix="include" TagName="head" Src="~/ascx/head.ascx" %>
<%@ Register TagPrefix="include" TagName="header" Src="~/ascx/header.ascx" %>
<%@ Register TagPrefix="include" TagName="footer" Src="~/ascx/footer.ascx" %>
<%@ Register TagPrefix="include" TagName="js" Src="~/ascx/js.ascx" %>
<%@ Register TagPrefix="include" TagName="sag" Src="~/ascx/sag.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <include:head runat="server" ID="head" />
    
</head>
<body>
    <form id="form1" runat="server">
    <include:header runat="server" ID="header" />
    <div class="navbar pah-none" style="background: #EFEDE0; border-top: 3px solid #D1D1C5;
        padding-top: 20px; padding-bottom: 20px; margin-bottom: 0px;">
        <div class="container">
            <div class="row">
                <div class="col-md-9">
                    <div class="page-header sayfa-baslik">
                        <h1>
                            <small>
                                <asp:Literal runat="server" ID="lit_sayfa_baslik" /></small></h1>
                    </div>
                    <asp:Literal runat="server" ID="lit_sayfa_detay" />
                    <asp:ListView runat="server" ID="lv_foto_veriler" GroupItemCount="5">
                        <LayoutTemplate>
                            <table width="100%" cellpadding="10">
                                <asp:PlaceHolder runat="server" ID="groupPlaceHolder" />
                            </table>
                        </LayoutTemplate>
                        <GroupTemplate>
                            <tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder" />
                            </tr>
                        </GroupTemplate>
                        <ItemTemplate>
                            <td valign="top" style="text-align: center;">
                                <a class="foto-galeri" href="/ashx/resim-getir.ashx?i=upload/foto/<%#Eval("dosya_ad")%>&amp;w=600&amp;h=400&amp;k=t"
                                    title="<%#Eval("baslik")%>">
                                    <img src="/ashx/resim-getir.ashx?i=upload/foto/<%#Eval("dosya_ad")%>&amp;w=100&amp;h=100&amp;k=t"
                                        class="img-thumbnail" alt="<%#Eval("baslik")%>" /></a>
                            </td>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <div class="col-md-3">
                    <include:sag runat="server" ID="sag" />
                </div>
            </div>
        </div>
    </div>
    <include:footer runat="server" ID="footer" />
    </form>
    <include:js runat="server" ID="js" />
    <link href="/css/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery.colorbox-min.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            /* Colorbox */
            $(".foto-galeri").colorbox({ rel: 'foto-galeri', iframe: true, slideshow: false, innerWidth: 620, innerHeight: 420, speed: 900 });
            /* Colorbox */
        })
    </script>
</body>
</html>