<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SellerAddOrderWebForm.aspx.cs" Inherits="Register_Login.Seller.SellerAddOrderWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Create Order</title>
    <style>
        :root {
            --primary-color: #6a1b9a;
            --primary-hover: #4a148c;
            --secondary-color: #ff9800;
            --secondary-hover: #e68a00;
            --text-color: #333;
            --border-color: #ddd;
            --success-bg: #e8f5e9;
            --success-text: #2e7d32;
            --error-bg: #ffebee;
            --error-text: #c62828;
        }
        
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f9f9f9;
            margin: 0;
            padding: 10px;
            color: var(--text-color);
            line-height: 1.4;
            font-size: 14px;
        }
        
        .form-container {
            max-width: 650px;
            margin: 10px auto;
            background: white;
            padding: 1.2rem;
            border-radius: 6px;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
        }
        
        .form-header {
            text-align: center;
            margin-bottom: 1rem;
        }
        
        .form-header h2 {
            color: var(--primary-color);
            margin: 0 0 0.2rem 0;
            font-size: 1.3rem;
        }
        
        .form-header p {
            margin: 0;
            font-size: 0.85rem;
            color: #666;
        }
        
        .form-group {
            margin-bottom: 0.8rem;
        }
        
        .form-group label {
            display: block;
            margin-bottom: 0.3rem;
            font-weight: 600;
            color: #555;
            font-size: 0.9rem;
        }
        
        .form-control {
            width: 100%;
            padding: 0.5rem;
            border: 1px solid var(--border-color);
            border-radius: 4px;
            font-size: 0.9rem;
            height: 34px;
            box-sizing: border-box;
            transition: border-color 0.2s;
        }
        
        .form-control:focus {
            border-color: var(--primary-color);
            outline: none;
            box-shadow: 0 0 0 2px rgba(106, 27, 154, 0.1);
        }
        
        select.form-control {
            appearance: none;
            -webkit-appearance: none;
            -moz-appearance: none;
            background-image: url("data:image/svg+xml;charset=UTF-8,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' fill='none' stroke='%236a1b9a' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'%3e%3cpolyline points='6 9 12 15 18 9'%3e%3c/polyline%3e%3c/svg%3e");
            background-repeat: no-repeat;
            background-position: right 0.7rem center;
            background-size: 0.8rem;
            padding-right: 1.5rem;
        }
        
        .btn-primary {
            background-color: var(--primary-color);
            color: white;
            border: none;
            padding: 0.5rem;
            border-radius: 4px;
            cursor: pointer;
            font-size: 0.9rem;
            font-weight: 600;
            height: 34px;
            width: 100%;
            margin-top: 0.5rem;
            transition: background-color 0.2s;
        }
        
        .btn-primary:hover {
            background-color: var(--primary-hover);
        }
        
        .btn-secondary {
            background-color: var(--secondary-color);
            color: white;
            border: none;
            padding: 0.5rem;
            border-radius: 4px;
            cursor: pointer;
            font-size: 0.9rem;
            font-weight: 600;
            height: 34px;
            width: 100%;
            margin-top: 0.5rem;
            transition: background-color 0.2s;
        }
        
        .btn-secondary:hover {
            background-color: var(--secondary-hover);
        }
        
        .status-message {
            display: block;
            margin: 0.8rem 0;
            padding: 0.6rem;
            border-radius: 4px;
            text-align: center;
            font-size: 0.9rem;
        }
        
        .success-message {
            background-color: var(--success-bg);
            color: var(--success-text);
        }
        
        .error-message {
            background-color: var(--error-bg);
            color: var(--error-text);
        }
        
        .order-summary {
            margin-top: 1.5rem;
            padding: 1rem;
            background-color: #f5f5f5;
            border-radius: 4px;
        }
        
        .order-summary h3 {
            margin-top: 0;
            color: var(--primary-color);
        }
        
        .grid-view {
            width: 100%;
            margin-top: 1.5rem;
            border-collapse: collapse;
            font-size: 0.85rem;
        }

        .grid-view th {
            background-color: var(--primary-color);
            color: white;
            padding: 0.5rem;
            text-align: left;
            font-size: 0.9rem;
        }

        .grid-view td {
            padding: 0.5rem;
            border-bottom: 1px solid var(--border-color);
        }

        .grid-view tr:nth-child(even) {
            background-color: #f9f9f9;
        }
        
        .inventory-section {
            margin-top: 1.5rem;
        }
        
        .order-confirmation {
            margin-top: 1.5rem;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <div class="form-header">
                <h2>Create New Order</h2>
                <p>Enter order details below</p>
            </div>
            
            <div class="form-group">
                <label for="txtOrderID">Order ID</label>
                <asp:TextBox ID="txtOrderID" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="txtCustomerName">Customer Name</label>
                <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="dlBlanketModel">Blanket Model</label>
                <asp:DropDownList ID="dlBlanketModel" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlBlanketModel_SelectedIndexChanged"></asp:DropDownList>
            </div>
            
            <div class="form-group">
                <label for="txtOrderQty">Quantity</label>
                <asp:TextBox ID="txtOrderQty" runat="server" CssClass="form-control" TextMode="Number" min="1"></asp:TextBox>
            </div>
            
            <asp:Button ID="btnAddOrder" runat="server" Text="Submit Order" OnClick="btnAddOrder_Click" CssClass="btn-primary" />
            
            <div class="order-summary" id="orderSummary" runat="server" visible="false">
                <h3>Order Summary</h3>
                <p><strong>Model:</strong> <asp:Literal ID="litSelectedModel" runat="server"></asp:Literal></p>
                <p><strong>Quantity:</strong> <asp:Literal ID="litOrderQty" runat="server"></asp:Literal></p>
                <p><strong>Available:</strong> <asp:Literal ID="litAvailableQty" runat="server"></asp:Literal></p>
            </div>
            
            <div class="inventory-section" id="inventorySection" runat="server" visible="false">
                <h3>Inventory Information</h3>
                <asp:GridView ID="gvInventory" runat="server" CssClass="grid-view" AutoGenerateColumns="true">
                </asp:GridView>
                
                <asp:Button ID="btnConfirmOrder" runat="server" Text="Confirm Order" OnClick="btnConfirmOrder_Click" CssClass="btn-secondary" />
            </div>
            
            <div class="order-confirmation" id="orderConfirmation" runat="server" visible="false">
                <h3>Order Confirmation</h3>
                <asp:GridView ID="gvOrderConfirmation" runat="server" CssClass="grid-view" AutoGenerateColumns="true">
                </asp:GridView>
            </div>
            
            <asp:Label ID="lblMessage" runat="server" CssClass="status-message" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>