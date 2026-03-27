<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlanketsWebForm.aspx.cs" Inherits="Register_Login.Manufacturer.BlanketsWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Blanket Management</title>
    <style>
        :root {
            --primary-color: #6a1b9a;
            --primary-hover: #4a148c;
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
            max-width: 500px;
            margin: 0 auto;
            background: white;
            padding: 1.5rem;
            border-radius: 6px;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
        }
        
        .form-header {
            text-align: center;
            margin-bottom: 1.2rem;
            padding-bottom: 0.8rem;
            border-bottom: 1px solid #eee;
        }
        
        .form-header h2 {
            color: var(--primary-color);
            margin: 0 0 0.3rem 0;
            font-size: 1.4rem;
        }
        
        .form-header p {
            margin: 0;
            font-size: 0.9rem;
            color: #666;
        }
        
        .form-group {
            margin-bottom: 1rem;
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
            transition: border-color 0.2s;
            height: 34px;
            box-sizing: border-box;
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
            background-image: url("data:image/svg+xml;charset=UTF-8,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'%3e%3cpolyline points='6 9 12 15 18 9'%3e%3c/polyline%3e%3c/svg%3e");
            background-repeat: no-repeat;
            background-position: right 0.5rem center;
            background-size: 1rem;
        }
        
        .btn-submit {
            background-color: var(--primary-color);
            color: white;
            border: none;
            padding: 0.5rem 1rem;
            border-radius: 4px;
            cursor: pointer;
            font-size: 0.9rem;
            font-weight: 600;
            transition: background-color 0.2s;
            height: 34px;
            width: 100%;
            margin-top: 0.5rem;
        }
        
        .btn-submit:hover {
            background-color: var(--primary-hover);
        }
        
        .status-message {
            display: block;
            margin: 1rem 0;
            padding: 0.6rem;
            border-radius: 4px;
            text-align: center;
            font-weight: 500;
            font-size: 0.9rem;
        }
        
        .status-success {
            background-color: var(--success-bg);
            color: var(--success-text);
        }
        
        .status-error {
            background-color: var(--error-bg);
            color: var(--error-text);
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <div class="form-header">
                <h2>Blanket Management</h2>
                <p>Add new blanket products to inventory</p>
            </div>
            
            <div class="form-group">
                <label for="txtBID">Blanket ID</label>
                <asp:TextBox ID="txtBID" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="txtBModel">Blanket Model</label>
                <asp:TextBox ID="txtBModel" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="dlMaterial">Material</label>
                <asp:DropDownList ID="dlMaterial" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            
            <div class="form-group">
                <label for="txtCapasity">Capacity</label>
                <asp:TextBox ID="txtCapasity" runat="server" CssClass="form-control" TextMode="Number" min="1"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="txtPrice">Price ($)</label>
                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" TextMode="Number" step="0.01" min="0"></asp:TextBox>
            </div>
            
            <asp:Button ID="btnAddBlanket" runat="server" Text="Add Blanket" OnClick="btnAddBlanket_Click" CssClass="btn-submit" />
            
            <asp:Label ID="lblAddBlnket" runat="server" CssClass="status-message" Visible="false"></asp:Label>
            
            <asp:GridView ID="gvBlanket" runat="server" CssClass="grid-view" AutoGenerateColumns="true">
            </asp:GridView>
        </div>
    </form>
</body>
</html>