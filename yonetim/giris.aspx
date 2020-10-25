<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="giris.aspx.cs" Inherits="rodanit_com.yonetim.giris" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="include" TagName="Head" Src="~/yonetim/ascx/head.ascx" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <include:Head runat="server" ID="head" />
    <style type="text/css">
        body
        {
            background: url(img/bg.jpg) #B0ABB5 no-repeat top left;
        }
        .giris_form
        {
            margin: 100px auto 0px auto;
            width: 550px;
            padding: 30px;
            background: #FFF;
            border: 1px dotted #757388;
            text-align: center;
        }
        .input
        {
            padding: 5px;
            width: 200px;
            margin-bottom: 10px;
        }
        input
        {
            padding: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="giris_form">
        <img src="img/logo.png" alt="Yönetim Paneli" /><br />
        <br />
        <br />
        <table align="center">
            <tbody>
                <tr>
                    <td align="right">
                        <strong>E-Posta Adresiniz</strong>
                    </td>
                    <td>
                        <strong>:</strong>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_kullanici" runat="server" CssClass="input" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <strong>Şifreniz</strong>
                    </td>
                    <td>
                        <strong>:</strong>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_sifre" runat="server" CssClass="input" TextMode="Password" />
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        &nbsp;
                    </td>
                    <td valign="top">
                        &nbsp;
                    </td>
                    <td align="left">
                        <recaptcha:RecaptchaControl ID="recaptcha" runat="server" PublicKey="6Lfqne0SAAAAAAYMW_gQr1LoXd7y5Xpey-D5d6MR " PrivateKey="6Lfqne0SAAAAAGOGYvhNajLDUq7EHSVOCKnekgJY" Language="tr" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="left">
                        <asp:Button ID="btn_GirisYap" runat="server" Text="Giriş Yap" OnClick="btn_GirisYap_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
