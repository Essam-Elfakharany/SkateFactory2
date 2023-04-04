<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageSkateboards.aspx.cs" Inherits="SkateFactory2.WebUI.Management.ManageSkateboards" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Skateboards</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1" />

    <link href="/Site.css" rel="stylesheet" />
    <!--
        Bootstrap xs, sm, md, lg meaning:
        xs = extra small screens (mobile phones <576px)
        sm = small screens (tablets >=576px)
        md = medium screens (desktops >=768px)
        lg = large screens (desktops >=992px)
        xl = extra large screens (desktops >=1200px)
        
        Nice illustration showing col-lg, col-md, col-md, etc:
        https://i.stack.imgur.com/S1RYa.png
    -->
    <link href="/lib/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script src="/lib/bootstrap/js/bootstrap.js"></script>

    <style>
        .addUpdateBtn {
            width: 100%; 
            margin: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class="row">
            <h2>Manage Skateboards</h2>
            <asp:LinkButton ID="lkbSignOut" Text="(Click here to sign out)" CausesValidation="false" runat="server" OnClick="lkbSignOut_Click"></asp:LinkButton>
        </div>

        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <asp:Label Text="Name:" AssociatedControlID="txtName" runat="server"></asp:Label>
                    <asp:RequiredFieldValidator Display="Dynamic" ErrorMessage="Name is required" ControlToValidate="txtName" runat="server" />
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label Text="Weight:" AssociatedControlID="txtWeight" runat="server"></asp:Label>
                    <asp:RequiredFieldValidator Display="Dynamic" ErrorMessage="Weight is required" ControlToValidate="txtWeight" runat="server" />
                    <asp:CustomValidator ID="cvWeight" Display="Dynamic" ErrorMessage="Weight must be numeric" runat="server" OnServerValidate="cvWeight_ServerValidate" />
                    <asp:TextBox ID="txtWeight" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label Text="Skate Type:" AssociatedControlID="ddlSkateType" runat="server"></asp:Label>
                    <asp:CustomValidator ID="cvSkateType" Display="Dynamic" ErrorMessage="Skate Type is required" runat="server" OnServerValidate="cvSkateType_ServerValidate" />
                    <asp:DropDownList ID="ddlSkateType" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Undefined"></asp:ListItem>
                        <asp:ListItem Text="Electric" Value="Electric"></asp:ListItem>
                        <asp:ListItem Text="Regular" Value="Regular"></asp:ListItem>
                    </asp:DropDownList>

                    <asp:Label Text="Color:" AssociatedControlID="ddlColor" runat="server"></asp:Label>
                    <asp:CustomValidator ID="cvColor" Display="Dynamic" ErrorMessage="Color is required" runat="server" OnServerValidate="cvColor_ServerValidate" />
                    <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Undefined"></asp:ListItem>
                        <asp:ListItem Text="Blue"></asp:ListItem>
                        <asp:ListItem Text="Green"></asp:ListItem>
                        <asp:ListItem Text="Red"></asp:ListItem>
                    </asp:DropDownList>

                    <asp:Label Text="Brake Type:" AssociatedControlID="ddlBrakeType" runat="server"></asp:Label>
                    <asp:CustomValidator ID="cvBrakeType" Display="Dynamic" ErrorMessage="Brake Type is required" runat="server" OnServerValidate="cvBrakeType_ServerValidate" />
                    <asp:DropDownList ID="ddlBrakeType" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Undefined"></asp:ListItem>
                        <asp:ListItem Text="Dynamic"></asp:ListItem>
                        <asp:ListItem Text="Friction"></asp:ListItem>
                    </asp:DropDownList>

                    <asp:Label Text="Shape Type:" AssociatedControlID="ddlShapeType" runat="server"></asp:Label>
                    <asp:CustomValidator ID="cvShapeType" Display="Dynamic" ErrorMessage="Shape Type is required" runat="server" OnServerValidate="cvShapeType_ServerValidate" />
                    <asp:DropDownList ID="ddlShapeType" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Undefined"></asp:ListItem>
                        <asp:ListItem Text="Plastic"></asp:ListItem>
                        <asp:ListItem Text="Wood"></asp:ListItem>
                    </asp:DropDownList>

                    <br />

                    <div class="row">
                        <div class="col-sm-6">
                            <asp:Button ID="btnAdd" Text="Add" CausesValidation="true" runat="server" CssClass="btn btn-primary addUpdateBtn" OnClick="btnAdd_Click" />
                        </div>
                        <div class="col-sm-6">
                            <asp:Button ID="btnUpdate" Text="Update" CausesValidation="true" runat="server" CssClass="btn btn-primary addUpdateBtn" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    <asp:Label Text="Display All, Regular, or Electric Skates:" AssociatedControlID="ddlShapeType" runat="server"></asp:Label>

                    <asp:RadioButton ID="rbDisplayAll" Text="All" GroupName="Filter" Checked="true" AutoPostBack="true" runat="server" OnCheckedChanged="rbDisplayAll_CheckedChanged"></asp:RadioButton>
                    <asp:RadioButton ID="rbDisplayRegular" Text="Regular" GroupName="Filter" AutoPostBack="true" runat="server" OnCheckedChanged="rbDisplayRegular_CheckedChanged"></asp:RadioButton>
                    <asp:RadioButton ID="rbDisplayElectric" Text="Electric" GroupName="Filter" AutoPostBack="true" runat="server" OnCheckedChanged="rbDisplayElectric_CheckedChanged"></asp:RadioButton>

                    <asp:DataGrid ID="dgSkateboards" Width="100%" CssClass="hoverable" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnSelectedIndexChanged="dgSkateboards_SelectedIndexChanged">
                        <Columns>
                            <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="Id" HeaderText="Id"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Name" HeaderText="Name"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Color" HeaderText="Color"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Weight" HeaderText="Weight"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SkateType" HeaderText="Skate<br/>Type"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BrakeType" HeaderText="Brake<br/>Type"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ShapeType" HeaderText="Shape<br/>Type"></asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                        <ItemStyle BackColor="White" ForeColor="#003399" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" Font-Italic="False" />
                    </asp:DataGrid>

                    <br />

                    <div class="row">

                        <asp:Button ID="btnRemove" Text="Remove Selected Skate" CausesValidation="false" runat="server" CssClass="btn btn-danger col-5" OnClick="btnRemove_Click" OnClientClick="return btnRemoveClientClick();" />

                        <div class="col-6">
                            <div class="input-group">
                                <asp:TextBox ID="txtSearchById" placeholder="Search by Id" CssClass="form-control" runat="server" />
                                <asp:Button ID="btnSearch" Text="Search" CausesValidation="false" runat="server" CssClass="btn btn-secondary" OnClick="btnSearch_Click" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>

    <script>
        function btnRemoveClientClick() {
            if (!confirm("Do you confirm this deletion?"))
                return false;

            return true;
        }
    </script>
</body>
</html>
