<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="rodanit_com.contact" %>

<%@ Register TagPrefix="include" TagName="head" Src="~/ascx/head.ascx" %>
<%@ Register TagPrefix="include" TagName="header" Src="~/ascx/header.ascx" %>
<%@ Register TagPrefix="include" TagName="footer" Src="~/ascx/footer.ascx" %>
<%@ Register TagPrefix="include" TagName="js" Src="~/ascx/js.ascx" %>
<%@ Register TagPrefix="include" TagName="sag" Src="~/ascx/sag.ascx" %>
<%@ Register TagPrefix="include" TagName="recaptcha" Src="~/ascx/recaptcha.ascx" %>
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
                                <% Response.Write(rodanit_com.Class.Fonksiyonlar.Textler(2)); %></small></h1>
                    </div>
                    <address>
                        
                        <% 
                            if (!string.IsNullOrEmpty(rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.sirket_unvan).FirstOrDefault()))
                            {
                                Response.Write("<strong>" + rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.sirket_unvan).FirstOrDefault() + "</strong><br />");
                            }
                            
                            if (!string.IsNullOrEmpty(rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.adres).FirstOrDefault()))
                            {
                                Response.Write(rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.adres).FirstOrDefault() + "<br />");
                            }
                            
                            if (!string.IsNullOrEmpty(rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.telefon).FirstOrDefault()))
                            {
                                Response.Write("<abbr title=\"Phone\">T:</abbr> " + rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.telefon).FirstOrDefault() + "<br />");
                            }
                            
                            if (!string.IsNullOrEmpty(rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.faks).FirstOrDefault()))
                            {
                                Response.Write("<abbr title=\"Fax\">F:</abbr> " + rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.faks).FirstOrDefault() + "<br />");
                            }
                            
                            if (!string.IsNullOrEmpty(rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.gsm).FirstOrDefault()))
                            {
                                Response.Write("<abbr title=\"Mobile\">M:</abbr> " + rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.gsm).FirstOrDefault() + "<br />");
                            }
                            
                            if (!string.IsNullOrEmpty(rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.mail).FirstOrDefault()))
                            {
                                Response.Write("<abbr title=\"Mail\">E:</abbr> " + rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.mail).FirstOrDefault() + "<br />");
                            }
      %>
                    </address>
                    <br />
                    <br />
                    <div class="page-header sayfa-baslik">
                        <h1>
                            <small>
                                <% Response.Write(rodanit_com.Class.Fonksiyonlar.Textler(4)); %></small></h1>
                    </div>
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td>
                                    <div class="form-group">
                                        <label for="txt_iletisim_ad">
                                            <% Response.Write(rodanit_com.Class.Fonksiyonlar.Textler(5)); %></label>
                                        <asp:TextBox runat="server" ID="txt_iletisim_ad" CssClass="form-control" /></div>
                                </td>
                                <td>
                                    <div class="form-group">
                                        <label for="txt_iletisim_soyad">
                                            <% Response.Write(rodanit_com.Class.Fonksiyonlar.Textler(6)); %></label>
                                        <asp:TextBox runat="server" ID="txt_iletisim_soyad" CssClass="form-control" /></div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="form-group">
                                        <label for="txt_iletisim_mail">
                                            <% Response.Write(rodanit_com.Class.Fonksiyonlar.Textler(7)); %></label>
                                        <asp:TextBox runat="server" ID="txt_iletisim_mail" CssClass="form-control" /></div>
                                </td>
                                <td>
                                    <div class="form-group">
                                        <label for="txt_iletisim_telefon">
                                            <% Response.Write(rodanit_com.Class.Fonksiyonlar.Textler(8)); %></label>
                                        <asp:TextBox runat="server" ID="txt_iletisim_telefon" CssClass="form-control" /></div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div class="form-group">
                                        <label for="txt_iletisim_mesaj">
                                            <% Response.Write(rodanit_com.Class.Fonksiyonlar.Textler(9)); %></label>
                                        <asp:TextBox runat="server" ID="txt_iletisim_mesaj" TextMode="MultiLine" CssClass="form-control"
                                            Height="100" /></div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <include:recaptcha runat="server" ID="recaptcha" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button runat="server" ID="btn_iletisim_gonder" CssClass="btn btn-default" OnClick="btn_iletisim_gonder_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
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
</body>
</html>
