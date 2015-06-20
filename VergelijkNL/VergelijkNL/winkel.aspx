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
        <div id="content">
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
    </div>
</body>
</html>
