<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SellerManagementWebForm.aspx.cs" Inherits="Register_Login.Distributer.SellerManagementWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Manage Sellers</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 20px;
            color: #333;
        }
        .container {
            max-width: 900px;
            margin: 0 auto;
            background: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        h1 {
            color: #6a1b9a;
            margin-top: 0;
        }
        .form-group {
            margin-bottom: 15px;
        }
        label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }
        .form-control {
            width: 100%;
            padding: 8px;
            border: 1px solid #ddd;
            border-radius: 4px;
            box-sizing: border-box;
        }
        .btn {
            background-color: #6a1b9a;
            color: white;
            border: none;
            padding: 10px 15px;
            border-radius: 4px;
            cursor: pointer;
            font-size: 14px;
        }
        .btn:hover {
            background-color: #7b2cbf;
        }
        .message {
            padding: 10px;
            margin: 10px 0;
            border-radius: 4px;
        }
        .success {
            background-color: #dff0d8;
            color: #3c763d;
        }
        .error {
            background-color: #f2dede;
            color: #a94442;
        }
        .grid-view {
            width: 100%;
            margin-top: 20px;
            border-collapse: collapse;
        }
        .grid-view th {
            background-color: #6a1b9a;
            color: white;
            padding: 10px;
            text-align: left;
        }
        .grid-view td {
            padding: 8px;
            border-bottom: 1px solid #ddd;
        }
        .grid-view tr:nth-child(even) {
            background-color: #f9f9f9;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Manage Sellers</h1>
            
            <div class="form-group">
                <label for="txtSellerID">Seller ID:</label>
                <asp:TextBox ID="txtSellerID" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="txtSellerName">Seller Name:</label>
                <asp:TextBox ID="txtSellerName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <asp:Button ID="btnAddSeller" runat="server" Text="Assign Seller" CssClass="btn" OnClick="btnAddSeller_Click" />
            
            <asp:Label ID="lblMessage" runat="server" CssClass="message" Visible="false"></asp:Label>
            
            <h2>Currently Assigned Sellers</h2>
            <asp:GridView ID="gvAssignedSellers" runat="server" CssClass="grid-view" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="SellerID" HeaderText="Seller ID" />
                    <asp:BoundField DataField="SellerName" HeaderText="Seller Name" />
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn" 
                                CommandArgument='<%# Eval("SellerManagementID") %>' 
                                OnClick="btnRemove_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>