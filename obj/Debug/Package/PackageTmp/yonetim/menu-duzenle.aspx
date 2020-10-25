<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu-duzenle.aspx.cs" Inherits="rodanit_com.yonetim.menu_duzenle"
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
            $(".menu ul #icerik").addClass("secili");
            $(".menualt #icerik_detay").css("display", "inline");

            $('#txt_sira').bind('keyup', function () {
                SayisalKarakter($(this))
            });
        });
        $(function () {
            $("#tabs").tabs();
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
            Ana Sayfa » İçerik Yönetimi » Menüler » Menü Düzenle</div>
        <div class="blokicerik">
            <asp:ValidationSummary CssClass="form_hata" ID="hatalar" runat="server" ForeColor="#CC0000"
                ValidationGroup="ekle" />
            <fieldset>
                <legend>Tanımlama Bilgileri</legend>
                <div>
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    <strong>Başlık</strong>
                                </td>
                                <td>
                                    <strong>:</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_tanimlama_baslik" runat="server" Width="300px"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_tanimlama_baslik"
                                        Display="None" ErrorMessage="Lütfen tanımlama başlığını giriniz." ValidationGroup="ekle"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Üst Kategori</strong>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_ust_kategori_id" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Menü Türü</strong>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_tur" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Foto Galeri</strong></td>
                                <td>
                                    <strong>:</strong></td>
                                <td>
                                    <asp:DropDownList ID="ddl_foto_galeri" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Url</strong>
                                </td>
                                <td>
                                    <strong>:</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_url" runat="server" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Sıra</strong>
                                </td>
                                <td>
                                    <strong>:</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_sira" runat="server" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </fieldset>
            <br />
            <asp:PlaceHolder runat="server" ID="lit_icerik"></asp:PlaceHolder>
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
