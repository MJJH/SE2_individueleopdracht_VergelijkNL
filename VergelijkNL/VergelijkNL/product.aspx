<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="product.aspx.cs" Inherits="VergelijkNL.product" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/bootstrap.min.css" rel="stylesheet" />
    <link href="Styles/Content.css" rel="stylesheet" />
</head>
<body>
    <header>
        <div id="sameHier" runat="server"></div>
        <div id="children" runat="server"></div>
    </header>
    <div class="container">
        <div id="path" runat="server"></div>
        <div id="content" class="empty" runat="server">
            ...
        </div>
    </div>
</body>
</html>
