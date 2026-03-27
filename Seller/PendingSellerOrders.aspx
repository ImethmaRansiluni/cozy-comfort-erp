<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PendingSellerOrders.aspx.cs" 
    Inherits="Register_Login.Seller.PendingSellerOrders" EnableEventValidation="false" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pending Orders Management</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            padding: 20px;
            background-color: #f5f5f5;
        }
        
        .container {
            max-width: 1200px;
            margin: 0 auto;
            background: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }
        
        .header {
            margin-bottom: 20px;
            padding-bottom: 10px;
            border-bottom: 1px solid #eee;
        }
        
        .filter-section {
            margin-bottom: 20px;
            display: flex;
            gap: 15px;
            align-items: center;
            flex-wrap: wrap;
        }
        
        .form-group {
            margin-bottom: 15px;
        }
        
        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: 600;
            color: #555;
        }
        
        .form-control {
            padding: 8px;
            border: 1px solid #ddd;
            border-radius: 4px;
            min-width: 250px;
        }
        
        select.form-control {
            height: 36px;
        }
        
        .grid-view {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }
        
        .grid-view th {
            background-color: #6a1b9a;
            color: white;
            padding: 10px;
            text-align: left;
        }
        
        .grid-view td {
            padding: 10px;
            border-bottom: 1px solid #eee;
        }
        
        .grid-view tr:nth-child(even) {
            background-color: #f9f9f9;
        }
        
        .action-button {
            padding: 5px 10px;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            margin-right: 5px;
        }
        
        .action-button.reject {
            background-color: #f44336;
        }
        
        .alert {
            padding: 10px;
            margin: 10px 0;
            border-radius: 4px;
            text-align: center;
        }
        
        .alert-success {
            background-color: #e8f5e9;
            color: #2e7d32;
        }
        
        .alert-danger {
            background-color: #ffebee;
            color: #c62828;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h2>Pending Orders Management</h2>
            </div>
            
            <asp:Label ID="lblMessage" runat="server" CssClass="alert" Visible="false"></asp:Label>
            
            <h3>My Inventory</h3>
            <asp:GridView ID="gvInventory" runat="server" CssClass="grid-view" AutoGenerateColumns="false" 
                EmptyDataText="No inventory items found" EnableViewState="true">
                <Columns>
                    <asp:BoundField DataField="BlanketModel" HeaderText="Blanket Model" />
                    <asp:BoundField DataField="CurrentQty" HeaderText="Quantity Available" />
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblInventoryMessage" runat="server" Visible="false"></asp:Label>
            
            <div class="filter-section">
                <div class="form-group">
                    <label for="dlBlanketModel">Filter by Blanket Model</label>
                    <asp:DropDownList ID="dlBlanketModel" runat="server" CssClass="form-control" 
                        AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="dlBlanketModel_SelectedIndexChanged"
                        EnableViewState="true">
                        <asp:ListItem Text="-- All Blankets --" Value="" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            
            <h3>Pending Orders</h3>
            <asp:GridView ID="gvPendingOrders" runat="server" CssClass="grid-view" AutoGenerateColumns="false" 
                EmptyDataText="No pending orders found" OnRowCommand="gvPendingOrders_RowCommand" 
                DataKeyNames="OrderID" OnRowDataBound="gvPendingOrders_RowDataBound"
                EnableViewState="true">
                <Columns>
                    <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                    <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
                    <asp:BoundField DataField="BlanketModel" HeaderText="Blanket Model" />
                    <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnApprove" runat="server" Text="Confirm" CssClass="action-button" 
                                 CommandName="Approve" CommandArgument='<%# Eval("OrderID") %>'
                                 CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblOrdersMessage" runat="server" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>