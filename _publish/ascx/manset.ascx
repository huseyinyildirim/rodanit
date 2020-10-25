<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="manset.ascx.cs" Inherits="rodanit_com.ascx.manset" %>

<div class="container">
<!-- Manşet -->
        <div id="carousel-example-generic" class="carousel slide" data-ride="carousel" style="border:10px solid #FFF;">
            <ol class="carousel-indicators">
                <asp:Literal runat="server" ID="lit_manset_sayi" />
            </ol>
            <div class="carousel-inner">
            <asp:Literal runat="server" ID="lit_manset_icerik" />
                
            </div>
            <a class="left carousel-control" href="#carousel-example-generic" data-slide="prev">
                <span class="glyphicon glyphicon-chevron-left"></span></a><a class="right carousel-control"
                    href="#carousel-example-generic" data-slide="next"><span class="glyphicon glyphicon-chevron-right">
                    </span></a>
        </div>
        <!-- Manşet -->
        </div>