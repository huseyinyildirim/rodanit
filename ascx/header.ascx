<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="rodanit_com.ascx.header" %>
<div class="container">
<!-- Logo -->
<div class="row">
        <div class="col-xs-12 col-md-3">
            <img src="/img/logo.jpg" alt="" /></div>
        <div class="col-xs-12 col-md-9 text-right">
        <asp:Literal runat="server" ID="lit_diller" /><br /><br /><%
                if (!string.IsNullOrEmpty( rodanit_com.Class.Fonksiyonlar.Ayar().Select(k=>k.facebook).FirstOrDefault()))
                {
                    Response.Write("<a href=\""+rodanit_com.Class.Fonksiyonlar.Ayar().Select(k=>k.facebook).FirstOrDefault()+"\"><img src=\"/img/facebook.jpg\" alt=\"facebook\"/></a>&nbsp;");
                }
                
                if (!string.IsNullOrEmpty(rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.twitter).FirstOrDefault()))
                {
                    Response.Write("<a href=\"" + rodanit_com.Class.Fonksiyonlar.Ayar().Select(k => k.twitter).FirstOrDefault() + "\"><img src=\"/img/twitter.jpg\" alt=\"facebook\"/></a>&nbsp;");
                }
                 %>
            </div>
    </div>
    <!-- Logo -->
    </div>
<!-- Menü -->
<nav class="navbar navbar-default navbar-kirmizi pah-none" role="navigation" style="margin-bottom:0px;">
<div class="container">
  <div class="navbar-header">
    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
      <span class="sr-only">Toggle navigation</span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
      <span class="icon-bar"></span>
    </button>
  </div>

  <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
    <ul class="nav navbar-nav">

      <li><a href="/default.aspx"><%Response.Write(rodanit_com.Class.Fonksiyonlar.Textler(1).ToUpper()); %></a></li>

      <asp:Literal runat="server" ID="lit_ana_menu" />
    </ul>
  </div>
  </div>
</nav>

<!-- Menü -->