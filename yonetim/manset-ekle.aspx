<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manset-ekle.aspx.cs" Inherits="rodanit_com.yonetim.manset_ekle" ValidateRequest="false" %>

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
        });
        $(function () {
            $("#tabs").tabs();
            $("#txt_son_gosterim").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd.mm.yy"
            });
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
            Ana Sayfa » İçerik Yönetimi » Manşetler » Manşet Ekle</div>
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
                                    <strong>Son Gösterim Tarihi</strong>
                                </td>
                                <td>
                                    <strong>:</strong>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txt_son_gosterim" Width="300px"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_son_gosterim"
                                        Display="None" ErrorMessage="Lütfen son gösterim tarihini giriniz." ValidationGroup="ekle"></asp:RequiredFieldValidator>
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
                <legend>Resim Yükleme</legend>
                <div>
                    <strong>Resim Yükle: </strong>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
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
                                    <strong>Onay:</strong>
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
                    <asp:Button ID="btn_kayitekle" runat="server" Text="Ekle" ValidationGroup="ekle"
                        OnClick="btn_kayitekle_Click" /></div>
            </fieldset>
        </div>
    </div>
    </form>
</body>
</html>
