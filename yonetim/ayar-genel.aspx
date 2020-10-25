<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ayar-genel.aspx.cs" Inherits="rodanit_com.yonetim.ayar_genel" %>

<%@ Register TagPrefix="include" TagName="Head" Src="~/yonetim/ascx/head.ascx" %>
<%@ Register TagPrefix="include" TagName="Ust" Src="~/yonetim/ascx/ust.ascx" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <include:Head runat="server" ID="head" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".menu ul #ayar").addClass("secili");
            $(".menualt #ayar_detay").css("display", "inline");
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
            Ana Sayfa » Ayarlar » Genel Ayarlar</div>
        <div class="blokicerik">
            <asp:ValidationSummary CssClass="form_hata" ID="hatalar" runat="server" ForeColor="#CC0000"
                ValidationGroup="ekle" />
            <fieldset>
                <legend>Tanımlama</legend>
                <div>
                    <table>
                        <tbody>
                            <tr>
                                <td><strong>Domain</strong></td>
                                <td><strong>:</strong></td>
                                <td><asp:TextBox ID="txt_domain" runat="server" Width="500px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><strong>Marka</strong></td>
                                <td><strong>:</strong></td>
                                <td><asp:TextBox ID="txt_marka" runat="server" Width="500px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><strong>Şirket Ünvan</strong></td>
                                <td><strong>:</strong></td>
                                <td><asp:TextBox ID="txt_sirket_unvan" runat="server" Width="500px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><strong>Yetkili</strong></td>
                                <td><strong>:</strong></td>
                                <td><asp:TextBox ID="txt_yetkili" runat="server" Width="500px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><strong>Telefon</strong></td>
                                <td><strong>:</strong></td>
                                <td><asp:TextBox ID="txt_telefon" runat="server" Width="500px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_telefon"
                        Display="None" ErrorMessage="Lütfen telefon no giriniz." ValidationGroup="ekle"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td><strong>Faks</strong></td>
                                <td><strong>:</strong></td>
                                <td><asp:TextBox ID="txt_faks" runat="server" Width="500px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_faks"
                        Display="None" ErrorMessage="Lütfen faks no giriniz." ValidationGroup="ekle"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td><strong>Mail</strong></td>
                                <td><strong>:</strong></td>
                                <td><asp:TextBox ID="txt_mail" runat="server" Width="500px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><strong>Adres</strong></td>
                                <td><strong>:</strong></td>
                                <td><asp:TextBox ID="txt_adres" runat="server" Width="500px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_adres"
                        Display="None" ErrorMessage="Lütfen adres giriniz." ValidationGroup="ekle"></asp:RequiredFieldValidator></td>
                            </tr>
                        </tbody>
                    </table>
                    </div>
            </fieldset>
            <br />
            <fieldset>
                <legend>İşlemler</legend>
                <div>
                    <asp:Button ID="btn_kayitekle" runat="server" Text="Düzenle" ValidationGroup="ekle"
                        OnClick="btn_kayitekle_Click" /></div>
            </fieldset>
        </div>
        <div class="h10">
        </div>
        <div class="blokbaslik">
            Kayıt Bilgileri</div>
        <div class="blokicerik">
            <strong>Güncelleyen:</strong>
            <asp:Literal ID="lit_gucelleyen" runat="server"></asp:Literal><br />
            <strong>Güncellenme Tarihi:</strong>
            <asp:Literal ID="lit_guncelleme_tarih" runat="server"></asp:Literal>
        </div>
    </div>
    </form>
</body>
</html>
