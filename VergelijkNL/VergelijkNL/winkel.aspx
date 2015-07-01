<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="winkel.aspx.cs" Inherits="VergelijkNL.winkel" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/bootstrap.min.css" rel="stylesheet" />
    <link href="Styles/Content.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <div id="content" runat="server">
            <div id="naam" runat="server"></div>
            <div id="website"><a runat="server" id="url">Ga naar de website!</a></div>
            <hr />
            <div class="table-responsive">
                <table id="info" class="table table-striped">
                    <thead>
                        <tr>
                            <th>Eigenschap</th>
                            <th>Waarde</th>
                        </tr>
                    </thead>
                    <tbody id="fill" runat="server"></tbody>
                </table>
            </div>
        </div>
        <div id="newReview">
            <form runat="server">
                <div class="form-group">
                    <label for="username">Naam: </label>
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
    </div>
</body>
</html>
