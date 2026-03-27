<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManufacturerCurrentStockWebForm.aspx.cs" Inherits="Register_Login.Manufacturer.ManufacturerCurrentStockWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
            <title>Manufacturer Current Stock</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            padding: 20px;
            background-color: #f5f5f5;
            color: #333;
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
        
        .header h2 {
            color: #6a1b9a;
            margin: 0;
        }
        
        .section {
            margin-bottom: 30px;
            padding: 20px;
            background-color: #fafafa;
            border-radius: 6px;
        }
        
        .alert {
            padding: 12px;
            margin: 15px 0;
            border-radius: 4px;
            text-align: center;
            display: none;
        }
        
        .alert-success {
            background-color: #e8f5e9;
            color: #2e7d32;
            border: 1px solid #c8e6c9;
            display: block;
        }
        
        .alert-danger {
            background-color: #ffebee;
            color: #c62828;
            border: 1px solid #ffcdd2;
            display: block;
        }
        
        .grid-view {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }
        
        .grid-view th {
            background-color: #6a1b9a;
            color: white;
            padding: 12px;
            text-align: left;
        }
        
        .grid-view td {
            padding: 10px;
            border-bottom: 1px solid #eee;
        }
        
        .grid-view tr:nth-child(even) {
            background-color: #f9f9f9;
        }
        
        .grid-view tr:hover {
            background-color: #f0f0f0;
        }
        
        .filter-section {
            margin-bottom: 20px;
            display: flex;
            gap: 15px;
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
            padding: 8px 12px;
            border: 1px solid #ddd;
            border-radius: 4px;
            width: 200px;
        }
        
        .btn {
            padding: 8px 16px;
            background-color: #6a1b9a;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s;
        }
        
        .btn:hover {
            background-color: #5e1480;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h2>Manufacturer Current Stock</h2>
            </div>
            
            <asp:Label ID="lblMessage" runat="server" CssClass="alert" Visible="false"></asp:Label>
            
            <div class="section">
                <h3>My Current Inventory</h3>
                <asp:GridView ID="gvInventory" runat="server" CssClass="grid-view" AutoGenerateColumns="false" 
                    EmptyDataText="No inventory items found" EnableViewState="true">
                    <Columns>
                        <asp:BoundField DataField="BlanketModel" HeaderText="Blanket Model" />
                        <asp:BoundField DataField="CurrentQty" HeaderText="Quantity Available" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblInventoryMessage" runat="server" Visible="false"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>

