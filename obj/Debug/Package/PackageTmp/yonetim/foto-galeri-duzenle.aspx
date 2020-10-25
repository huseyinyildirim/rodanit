<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="foto-galeri-duzenle.aspx.cs" Inherits="rodanit_com.yonetim.foto_galeri_duzenle"
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
            $(".menu ul #galeri").addClass("secili");
            $(".menualt #galeri_detay").css("display", "inline");
        });
        $(function () {
            $("#tabs").tabs();
        });
    </script>
    <link href="../Scripts/swfupload/css/default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/swfupload/swf/swfupload.js"></script>
    <script type="text/javascript" src="../Scripts/swfupload/js/handlers.js"></script>
    <script type="text/javascript">
	    var swfu;
	    window.onload = function () {
	        swfu = new SWFUpload({
	            upload_url: "../Scripts/swfupload/foto-galeri-upload.aspx",
	            post_params: {
	                "ASPSESSID": "<%=Session.SessionID %>",
	                "resim_id": "<%=Request.QueryString["ID"] %>"
	            },
	            file_size_limit: "2 MB",
	            file_types: "*.jpg",
	            file_types_description: "JPG Images",
	            file_upload_limit: "0",
	            file_queue_error_handler: fileQueueError,
	            file_dialog_complete_handler: fileDialogComplete,
	            upload_progress_handler: uploadProgress,
	            upload_error_handler: uploadError,
	            upload_success_handler: uploadSuccess,
	            upload_complete_handler: uploadComplete,
	            button_image_url: "../Scripts/swfupload/images/XPButtonNoText_160x22.png",
	            button_placeholder_id: "spanButtonPlaceholder",
	            button_width: 160,
	            button_height: 22,
	            button_text: '<span class="button">Resimleri Seç <span class="buttonSmall">(2 MB Max)</span></span>',
	            button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
	            button_text_top_padding: 1,
	            button_text_left_padding: 5,
	            flash_url: "../Scripts/swfupload/swf/swfupload.swf",
	            custom_settings: {
	                upload_target: "divFileProgressContainer"
	            },
	            debug: false
	        });
	    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm">
    </asp:ScriptManager>
    <include:Ust runat="server" ID="ust" />
    <div class="icerik">
        <div class="blokbaslik">
            Ana Sayfa » Galeri Yönetimi » Foto Galeriler » Video Galeri Düzenle</div>
        <div class="blokicerik">
            <asp:ValidationSummary CssClass="form_hata" ID="hatalar" runat="server" ForeColor="#CC0000"
                ValidationGroup="ekle" />
            <fieldset>
                <legend>Tanımlama Bilgileri</legend>
                <div>
                    <strong>Başlık:</strong> <asp:TextBox ID="txt_tanimlama_baslik" runat="server" Width="300px"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_tanimlama_baslik"
                                        Display="None" ErrorMessage="Lütfen tanımlama başlığını giriniz." ValidationGroup="ekle"></asp:RequiredFieldValidator>
                </div>
            </fieldset>
            <br />
            <asp:PlaceHolder runat="server" ID="lit_icerik"></asp:PlaceHolder>
            <br />
            <fieldset>
                <legend>Resim Yükleme ve Önizleme</legend>
                <div>
                    <div id="swfu_container">
                        <div>
                            <span id="spanButtonPlaceholder"></span>
                        </div>
                        <div id="divFileProgressContainer">
                        </div>
                    </div>
                    <br />
                    <asp:ListView runat="server" ID="lv_foto_veriler" GroupItemCount="5">
                        <LayoutTemplate>
                            <table width="100%" cellpadding="5">
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
                                <img src="/ashx/resim-getir.ashx?i=upload/foto/<%#Eval("dosya_ad")%>&amp;w=150&amp;h=150&amp;k=t"
                                    style="border: 1px dotted #CCC;" alt="" /><br />
                                <a href="?islem=sil&resim_id=<%#Eval("id")%>&ID=<%Response.Write(Request.QueryString["ID"].ToString()); %>">
                                    Sil</a> - <a href="?islem=varsayilan&resim_id=<%#Eval("id")%>&ID=<%Response.Write(Request.QueryString["ID"].ToString()); %>">
                                        Varsayılan</a> (<%#Eval("varsayilan")%>) - <a href="?islem=onay&resim_id=<%#Eval("id")%>&ID=<%Response.Write(Request.QueryString["ID"].ToString()); %>">
                                            Onay</a> (<%#Eval("onay")%>)
                            </td>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <p>
                                Eklenmiş fotoğraf bulunmuyor!</p>
                        </EmptyDataTemplate>
                    </asp:ListView>
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
