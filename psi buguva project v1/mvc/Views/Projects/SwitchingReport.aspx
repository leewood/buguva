<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="SwitchingReport.aspx.cs" Inherits="mvc.Views.Projects.SwitchingReport" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class = "path">
   	  <img src="../../Content/Images/Icons/Refresh_big.png" alt="logo"><span class="title"><%= ViewData["Title"]%></span>
   	</div>   
    <div class = "errors">
      <%= Html.ErrorSummary("Įvyko klaida:", TempData) %>
    </div>   	
   	<div class="pager">   	
	  <%= Html.Pager((int)ViewData["size"], (int)ViewData["page"], (int)ViewData["total"])%>
	</div>   
   

</asp:Content>
