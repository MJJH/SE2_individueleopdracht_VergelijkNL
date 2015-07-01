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
        <div id="newReview" runat="server">
            <form runat="server">
                <div class="form-group">
                    <label for="name">Naam: </label>
                    <input type="text" class="form-control" id="name" runat="server" name="name" required />
                </div>
                 <div class="form-group">
                    <label for="name">Bericht: </label>
                    <input type="text" class="form-control" id="message" name="message" required />
                </div>
                <div class="form-group">
                    <label for="rating">Beoordeling: </label>
                    <input type="number" class="form-control" min="0" max="5" value="3" step="0.5" id="rating" name="rating" required />
                </div>
                <div id="rate" runat="server">
                    ...
                </div>
                <input type="submit" class="btn btn-default" id="submit" value="verstuur" runat="server" />
            </form>
        </div>
    </div>
</body>
</html>
