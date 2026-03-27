<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DistributerInventoryWebForm.aspx.cs" Inherits="Register_Login.Distributer.DistributerInventoryWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Distributer Inventory</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f5f0fa;
            margin: 0;
            padding: 20px;
        }
        
        .form-container {
            width: 700px;
            margin: 0 auto;
            padding: 25px;
            background-color: white;
            border-radius: 10px;
            box-shadow: 0 0 15px rgba(106, 13, 173, 0.1);
            border: 1px solid #e0d0f0;
        }
        
        h2 {
            color: #6a0dad;
            text-align: center;
            margin-bottom: 25px;
            font-size: 28px;
        }
        
        h3 {
            color: #6a0dad;
            border-bottom: 2px solid #e0d0f0;
            padding-bottom: 8px;
            margin-top: 30px;
        }
        
        .form-group {
            margin-bottom: 20px;
            display: flex;
            align-items: center;
        }
        
        label {
            display: inline-block;
            width: 150px;
            font-weight: 600;
            color: #6a0dad;
        }
        
        .form-control {
            padding: 8px 12px;
            border: 1px solid #d0b0e0;
            border-radius: 5px;
            font-size: 14px;
            flex-grow: 1;
        }
        
        .form-control:focus {
            outline: none;
            border-color: #9c27b0;
            box-shadow: 0 0 0 2px rgba(156, 39, 176, 0.2);
        }
        
        .buttons {
            margin-top: 25px;
            text-align: center;
        }
        
        .btn {
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s;
            margin: 0 10px;
        }
        
        .btn-add {
            background-color: #9c27b0;
            color: white;
        }
        
        .btn-add:hover {
            background-color: #7b1fa2;
        }
        
        .btn-remove {
            background-color: #e91e63;
            color: white;
        }
        
        .btn-remove:hover {
            background-color: #c2185b;
        }
        
        .message {
            display: block;
            margin-top: 15px;
            padding: 10px;
            border-radius: 5px;
            text-align: center;
            font-weight: 500;
        }
        
        .success {
            background-color: #e8f5e9;
            color: #2e7d32;
            border: 1px solid #c8e6c9;
        }
        
        .error {
            background-color: #ffebee;
            color: #c62828;
            border: 1px solid #ffcdd2;
        }
        
        /* GridView Styling */
        .grid-view {
            width: 100%;
            border-collapse: collapse;
            margin-top: 15px;
        }
        
        .grid-view th {
            background-color: #6a0dad;
            color: white;
            padding: 12px;
            text-align: left;
        }
        
        .grid-view td {
            padding: 10px;
            border-bottom: 1px solid #e0d0f0;
        }
        
        .grid-view tr:nth-child(even) {
            background-color: #f9f5ff;
        }
        
        .grid-view tr:hover {
            background-color: #f0e5ff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <h2>Manage Distributer Inventory</h2>
            <div class="form-group">
                <label for="txtInventoryID">Inventory ID:</label>
                <asp:TextBox ID="txtInventoryID" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>

            <div class="form-group">
                   <label for="dlBlanket">Blanket:</label>
                    <asp:DropDownList ID="dlBlanketModel" runat="server" CssClass="form-control" DataTextField="BlanketModel" DataValueField="BlanketID"></asp:DropDownList>
                </div>
            
            <div class="form-group">
                <label for="txtQty">Quantity:</label>
                <asp:TextBox ID="txtQty" runat="server" TextMode="Number" min="1" value="1" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="buttons">
                <asp:Button ID="btnAdd" runat="server" Text="Add to Inventory" CssClass="btn btn-add" OnClick="btnAdd_Click" />
                <asp:Button ID="btnRemove" runat="server" Text="Remove from Inventory" CssClass="btn btn-remove" OnClick="btnRemove_Click" />
            </div>
            
            <asp:Label ID="lblMessage" runat="server" CssClass="message" Visible="false"></asp:Label>
            
            <h3>Current Inventory</h3>
            <asp:GridView ID="gvInventory" runat="server" AutoGenerateColumns="false" CssClass="grid-view"
                EmptyDataText="No inventory items found" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="DistributerInventoryID" HeaderText="Inventory ID" />
                    <asp:BoundField DataField="BlanketModel" HeaderText="Blanket Model" />
                    <asp:BoundField DataField="CurrentQty" HeaderText="Quantity" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
