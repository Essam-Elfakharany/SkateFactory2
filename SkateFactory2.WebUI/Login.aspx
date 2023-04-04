<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SkateFactory2.WebUI.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="/Site.css" rel="stylesheet" />
    <link href="/lib/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script src="/lib/bootstrap/js/bootstrap.js"></script>
    <style>
        fieldset {
            margin: 50px;
            max-width: 512px;
        }

        .fieldSetBtn {
            margin-bottom: 20px;
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <h2>Log in</h2>

        <fieldset class="border p-2">
            <legend class="float-none w-auto">Sign In</legend>
            <asp:Label Text="E-mail:" AssociatedControlID="txtEmail" runat="server"></asp:Label>
            <asp:TextBox ID="txtEmail" placeholder="Type your e-mail here" CssClass="form-control" ValidationGroup="signin" runat="server" TextMode="Email"></asp:TextBox>
            <div class="row">
                <asp:RequiredFieldValidator ControlToValidate="txtEmail" ValidationGroup="signin" Display="Dynamic" ErrorMessage="E-mail is required" runat="server" />
            </div>

            <asp:Label Text="Password:" AssociatedControlID="txtPassword" runat="server"></asp:Label>
            <asp:TextBox ID="txtPassword" placeholder="Type your password here" CssClass="form-control" ValidationGroup="signin" runat="server" TextMode="Password"></asp:TextBox>
            <div class="row">
                <asp:RequiredFieldValidator ControlToValidate="txtPassword" ValidationGroup="signin" Display="Dynamic" ErrorMessage="Password is required" runat="server" />
            </div>

            <asp:Button ID="btnSignIn" Text="Sign in" CssClass="btn btn-primary fieldSetBtn" ValidationGroup="signin" runat="server" OnClick="btnSignIn_Click" />
        </fieldset>

        <fieldset class="border p-2">
            <legend class="float-none w-auto">New User (register here)</legend>
            <asp:Label Text="E-mail:" AssociatedControlID="txtNewEmail" runat="server"></asp:Label>
            <asp:TextBox ID="txtNewEmail" placeholder="Type your e-mail here" CssClass="form-control" ValidationGroup="newuser" runat="server" TextMode="Email"></asp:TextBox>
            <div class="row">
                <asp:RequiredFieldValidator ControlToValidate="txtNewEmail" ValidationGroup="newuser" Display="Dynamic" ErrorMessage="E-mail is required" runat="server" />
                <asp:RegularExpressionValidator ControlToValidate="txtNewEmail" ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" Display="Dynamic" ErrorMessage="E-mail is invalid" runat="server" ValidationGroup="newuser" />
            </div>

            <asp:Label Text="Password:" AssociatedControlID="txtNewPassword1" runat="server"></asp:Label>
            <asp:TextBox ID="txtNewPassword1" placeholder="Type your password here" CssClass="form-control" ValidationGroup="newuser" Display="Dynamic" runat="server" TextMode="Password"></asp:TextBox>
            <div class="row">
                <asp:RequiredFieldValidator ControlToValidate="txtNewPassword1" ValidationGroup="newuser" Display="Dynamic" ErrorMessage="Password is required" runat="server" />
            </div>

            <asp:Label Text="Repeat your password:" AssociatedControlID="txtNewPassword2" runat="server"></asp:Label>
            <asp:TextBox ID="txtNewPassword2" placeholder="Repeat your password here" CssClass="form-control" ValidationGroup="newuser" Display="Dynamic" runat="server" TextMode="Password"></asp:TextBox>
            <div class="row">
                <asp:CompareValidator ControlToValidate="txtNewPassword1" ControlToCompare="txtNewPassword2" Display="Dynamic" ValidationGroup="newuser" ErrorMessage="The passwords do not match" runat="server" />
            </div>

            <asp:Button ID="btnRegister" Text="Register" CssClass="btn btn-primary fieldSetBtn" ValidationGroup="newuser" runat="server" OnClick="btnRegister_Click" />
        </fieldset>
    </form>
</body>
</html>
