<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ayar-statik-kelime.aspx.cs"
    EnableEventValidation="False" ValidateRequest="false" Inherits="rodanit_com.yonetim.ayar_statik_kelime" %>

<%@ Register TagPrefix="include" TagName="Head" Src="~/yonetim/ascx/head.ascx" %>
<%@ Register TagPrefix="include" TagName="Ust" Src="~/yonetim/ascx/ust.ascx" %>
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
        <asp:UpdatePanel runat="server" ID="update_tablo">
            <ContentTemplate>
                <div class="blokbaslik">
                    Ana Sayfa » Ayarlar » Statik Kelime Ayarları</div>
                <div class="blokicerik">
                    <div class="blokkomut">
                        <div style="display: inline; float: right; margin-left: 10px;">
                            Sayfa:
                            <asp:DropDownList runat="server" ID="sayfa" AutoPostBack="true" OnSelectedIndexChanged="sayfasec" />
                        </div>
                        <div style="display: inline; float: right; margin-left: 10px;">
                            Kayıt Sayısı:
                            <asp:DropDownList runat="server" ID="kayitsayi" AutoPostBack="true" OnSelectedIndexChanged="kayitsayisec" />
                        </div>
                        <div style="display: inline; float: right;">
                            <asp:UpdateProgress runat="server" ID="ds">
                                <ProgressTemplate>
                                    <img src="img/yukleniyor.gif" alt="Yükleniyor..." /></ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="h10">
                    </div>
                    <asp:GridView ID="kayitlar" runat="server" Width="100%" CssClass="gridstil" AutoGenerateColumns="False"
                        DataKeyNames="id" GridLines="None" OnRowCancelingEdit="gridcancel" OnRowEditing="gridedit"
                        OnRowUpdating="gridupdate">
                        <EmptyDataTemplate>
                            Kayıt bulunmuyor!</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="baslik" HeaderText="Tanımlama" HtmlEncode="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tr" HeaderText="TR" HtmlEncode="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ru" HeaderText="RU" HtmlEncode="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="en" HeaderText="EN" HtmlEncode="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="de" HeaderText="DE" HtmlEncode="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fr" HeaderText="FR" HtmlEncode="false" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:CommandField ShowEditButton="true" CancelText="İptal" DeleteText="Sil" EditText="Düzenle"
                                InsertText="Ekle" NewText="Yeni" SelectText="Seç" UpdateText="Güncelle" />
                        </Columns>
                        <AlternatingRowStyle BackColor="#f5f5f5" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
