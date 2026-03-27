<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SellerOrdersWebForm.aspx.cs"  EnableEventValidation="false" Inherits="Register_Login.Distributer.SellerOrdersWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pending Seller Requests Management</title>
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
                <h2>Pending Seller Requests Management</h2>
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
            
            <h3>Pending Requests</h3>
            <asp:GridView ID="gvPendingRequests" runat="server" CssClass="grid-view" AutoGenerateColumns="false" 
                    EmptyDataText="No pending requests found" OnRowCommand="gvPendingRequests_RowCommand" 
                    DataKeyNames="SellerRequestID" OnRowDataBound="gvPendingRequests_RowDataBound"
                    EnableViewState="true">
                <Columns>
                    <asp:BoundField DataField="SellerRequestID" HeaderText="Request ID" />
                    <asp:BoundField DataField="SellerName" HeaderText="Seller" />
                    <asp:BoundField DataField="BlanketModel" HeaderText="Blanket Model" />
                    <asp:BoundField DataField="RequestQty" HeaderText="Quantity" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRequestDate" runat="server" TextMode="Date" 
                                Text='<%# Eval("UpdateDate", "{0:yyyy-MM-dd}") %>'
                                CssClass="form-control" Width="120px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Notes">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRequestNotes" runat="server" 
                                Text='<%# Eval("Note") %>'
                                TextMode="MultiLine" Rows="2" Width="200px"
                                CssClass="form-control"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnApprove" runat="server" Text="Confirm" CssClass="action-button" 
                                 CommandName="Approve" CommandArgument='<%# Container.DataItemIndex %>'
                                 CausesValidation="false" />
                            <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="action-button reject" 
                                 CommandName="Reject" CommandArgument='<%# Container.DataItemIndex %>'
                                 CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblRequestsMessage" runat="server" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>