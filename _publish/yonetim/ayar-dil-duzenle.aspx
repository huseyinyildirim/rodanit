<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ayar-dil-duzenle.aspx.cs"
    Inherits="rodanit_com.yonetim.ayar_dil_duzenle" ValidateRequest="false" %>

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
            Ana Sayfa » Ayarlar » Dil Ayarları » Dil Düzenle</div>
        <div class="blokicerik">
            <asp:ValidationSummary CssClass="form_hata" ID="hatalar" runat="server" ForeColor="#CC0000"
                ValidationGroup="ekle" />
            <fieldset>
                <legend>Tanımlama</legend>
                <div>
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    <strong>Dil</strong>
                                </td>
                                <td>
                                    <strong>:</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_dil" runat="server" Width="300px"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_dil" Display="None"
                                        ErrorMessage="Lütfen dil adını giriniz." ValidationGroup="ekle"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Dil Kodu</strong>
                                </td>
                                <td>
                                    <strong>:</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_dil_kodu" runat="server" Width="300px"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_dil_kodu"
                                        Display="None" ErrorMessage="Lütfen dil kodunu giriniz." ValidationGroup="ekle"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>İkon Dosya Adı</strong>
                                </td>
                                <td>
                                    <strong>:</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_dosya_ad" runat="server" Width="300px"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_dosya_ad"
                                        Display="None" ErrorMessage="Lütfen ikon dosya adını giriniz." ValidationGroup="ekle"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Kodlama</strong>
                                </td>
                                <td>
                                    <strong>:</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_kodlama" runat="server" Width="300px"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_kodlama" Display="None"
                                        ErrorMessage="Lütfen kodu giriniz." ValidationGroup="ekle"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Culture Kodu</strong>
                                </td>
                                <td>
                                    <strong>:</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_culture_kod" runat="server" Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_culture_kod"
                                        Display="None" ErrorMessage="Lütfen culture kodunu giriniz." ValidationGroup="ekle"></asp:RequiredFieldValidator>
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
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    <strong>Onay</strong>
                                </td>
                                <td>
                                    <strong>:</strong>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_onay" runat="server">
                                        <asp:ListItem Value="1">Evet</asp:ListItem>
                                        <asp:ListItem Value="0">Hayır</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
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
            <strong>Ekleyen:</strong>
            <asp:Literal ID="lit_ekleyen" runat="server"></asp:Literal><br />
            <strong>Kayıt Tarihi:</strong>
            <asp:Literal ID="lit_kayit_tarih" runat="server"></asp:Literal><br />
            <br />
            <strong>Güncelleyen:</strong>
            <asp:Literal ID="lit_gucelleyen" runat="server"></asp:Literal><br />
            <strong>Güncellenme Tarihi:</strong>
            <asp:Literal ID="lit_guncelleme_tarih" runat="server"></asp:Literal>
        </div>
    </div>
    </form>
</body>
</html>