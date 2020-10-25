<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits="rodanit_com.yonetim.menu" %>

<%@ Register TagPrefix="include" TagName="Head" Src="~/yonetim/ascx/head.ascx" %>
<%@ Register TagPrefix="include" TagName="Ust" Src="~/yonetim/ascx/ust.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <include:Head runat="server" ID="head" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".menu ul #icerik").addClass("secili");
            $(".menualt #icerik_detay").css("display", "inline");
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
                    Ana Sayfa » İçerik Yönetimi » Menüler</div>
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
                    <asp:GridView ID="kayitlar" runat="server" Width="100%" CssClass="gridstil" OnRowDataBound="kayitlar_RowDataBound"
                        AutoGenerateColumns="false" OnRowCommand="kayitlar_RowCommand" DataKeyNames="ID">
                        <EmptyDataTemplate>
                            Kayıt bulunmuyor!</EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="hepsinisec" runat="server" onClick="return coklusec(this);" ClientIDMode="Static" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="secim" runat="server" onClick="return coklusec(this);" ClientIDMode="Static" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40" />
                            <asp:BoundField DataField="ust_kategori" HeaderText="Üst Kategori" HtmlEncode="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="100" />
                            <asp:BoundField DataField="baslik" HeaderText="Başlık" HtmlEncode="false" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="onay" HeaderText="Durum" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="40" />
                            <asp:TemplateField HeaderText="Onay" ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButtonOnay" runat="server" CommandName="Onay" ImageUrl="img/komut-onay.png"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id")%>' OnClientClick="return confirm('İlgili kayıdın durumunu değiştirmek istediğinizden eminmisiniz?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="Düz." ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Sil" ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Sil" ImageUrl="img/komut-sil.png"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id")%>' OnClientClick="return confirm('İlgili kayıdı silmek istediğinizden eminmisiniz?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ekleyen" ItemStyle-Width="120" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ekleyen")%><br />
                                    <%# DataBinder.Eval(Container.DataItem, "tarih_ek")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Güncelleyen" ItemStyle-Width="120" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "guncelleyen")%><br />
                                    <%# DataBinder.Eval(Container.DataItem, "tarih_gun")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="h10">
                    </div>
                    <div class="blokkomut">
                        <asp:Button ID="secilenlerisil" runat="server" Text="Seçilenleri Sil" OnClick="secilenlerisil_Click"
                            OnClientClick="return confirm('Seçtiğiniz kayıtları silmek istediğinizden eminmisiniz?');" />&nbsp;<asp:Button
                                ID="yeniekle" runat="server" Text="Yeni Menü Ekle" OnClick="yeniekle_Click" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="secilenlerisil" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="yeniekle" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
