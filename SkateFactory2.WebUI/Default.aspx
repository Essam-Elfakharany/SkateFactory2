<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SkateFactory2.WebUI.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Skateboard Management System</title>
    <link href="Site.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" style="width:100%;" runat="server">
        <div style="width:100%; text-align:center">
            <h2>Welcome to the Skate Factory Management System</h2>
            <img src="images/logo.svg" style="max-width:512px;" />
            <br />
            <a style="font-size:20pt;" href="Management/ManageSkateboards.aspx">Click here to start</a>
            <br />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </form>
</body>