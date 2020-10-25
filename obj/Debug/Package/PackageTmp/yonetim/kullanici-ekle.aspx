<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kullanici-ekle.aspx.cs" Inherits="rodanit_com.yonetim.kullanici_ekle"
    ClientIDMode="Static" ValidateRequest="false" %>

<%@ Register TagPrefix="include" TagName="Head" Src="~/yonetim/ascx/head.ascx" %>
<%@ Register TagPrefix="include" TagName="Ust" Src="~/yonetim/ascx/ust.ascx" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <include:Head runat="server" ID="head" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".menu ul #kullanici").addClass("secili");
            $(".menualt #kullanici_detay").css("display", "inline");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm">
    </asp:ScriptManager>
    <include:Ust runat="server" ID="ust" />
    <div class="icerik">
        <div class="blokbaslik">
            Ana Sayfa » Kullanıcı Yönetimi » Kullanıcılar » Yeni Kullanıcı Ekle</div>
        <div class="blokicerik">
            <asp:ValidationSummary CssClass="form_hata" ID="hatalar" runat="server" ForeColor="#CC0000"
                ValidationGroup="ekle" />
            <fieldset>
                <legend>Kullanıcı Bilgileri</legend>
                <div>
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    <strong>Ad</strong>
                                </td>
                                <td>
                                    <strong>:</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_ad" runat="server" Width="300px"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_ad"
                                        Display="None" ErrorMessage="Lütfen ad giriniz." ValidationGroup="ekle"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Soyad</strong>
                                </td>
                                <td>
                                    <strong>:</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_soyad" runat="server" Width="300px"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_soyad"
                                        Display="None" ErrorMessage="Lütfen soyad giriniz." ValidationGroup="ekle"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>E-Posta</strong>
                                </td>
                                <td>
                                    <strong>:</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_mail" runat="server" Width="300px"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_mail"
                                        Display="None" ErrorMessage="Lütfen e-posta adresi giriniz." ValidationGroup="ekle"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Şifre</strong>
                                </td>
                                <td>
                                    <strong>:</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_sifre" runat="server" Width="300px" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_sifre"
                                        Display="None" ErrorMessage="Lütfen şifre giriniz." ValidationGroup="ekle"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </fieldset>
            <br />
            <fieldset>
                <legend>Diğer</legend>
                <div>
<strong>Onay:</strong> <asp:DropDownList ID="ddl_onay" runat="server">
                                        <asp:ListItem Value="1">Evet</asp:ListItem>
                                        <asp:ListItem Value="0">Hayır</asp:ListItem>
                                    </asp:DropDownList>
                </div>
            </fieldset>
            <br />
            <fieldset>
                <legend>İşlemler</legend>
                <div>
                    <asp:Button ID="btn_kayitekle" runat="server" Text="Ekle" ValidationGroup="ekle"
                        OnClick="btn_kayitekle_Click" /></div>
            </fieldset>
        </div>
    </div>
    </form>
</body>
</html>
