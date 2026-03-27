<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialsWebForm.aspx.cs" Inherits="Register_Login.Manufacturer.MaterialsWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Material Management</title>
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
            max-width: 400px;
            margin: 20px auto;
            background: white;
            padding: 1.2rem;
            border-radius: 6px;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
        }
        
        .form-header {
            text-align: center;
            margin-bottom: 1rem;
            padding-bottom: 0.8rem;
            border-bottom: 1px solid #eee;
        }
        
        .form-header h2 {
            color: var(--primary-color);
            margin: 0 0 0.3rem 0;
            font-size: 1.3rem;
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
            height: 32px;
            box-sizing: border-box;
        }
        
        .form-control:focus {
            border-color: var(--primary-color);
            outline: none;
            box-shadow: 0 0 0 2px rgba(106, 27, 154, 0.1);
        }
        
        .btn-submit {
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
        }
        
        .btn-submit:hover {
            background-color: var(--primary-hover);
        }
        
        .status-message {
            display: block;
            margin: 0.8rem 0;
            padding: 0.6rem;
            border-radius: 4px;
            text-align: center;
            font-size: 0.9rem;
        }
        
        .grid-view {
            width: 100%;
            margin-top: 1rem;
            font-size: 0.85rem;
            border-collapse: collapse;
        }
        
        .grid-view th {
            background-color: var(--primary-color);
            color: white;
            padding: 0.5rem;
            text-align: left;
        }
        
        .grid-view td {
            padding: 0.5rem;
            border-bottom: 1px solid #eee;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <div class="form-header">
                <h2>Material Management</h2>
            </div>
            
            <div class="form-group">
                <label for="txtMID">Material ID</label>
                <asp:TextBox ID="txtMID" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="txtMName">Material Name</label>
                <asp:TextBox ID="txtMName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <asp:Button ID="btnAddMaterial" runat="server" Text="Add Material" OnClick="btnAddMaterial_Click" CssClass="btn-submit" />
            
            <asp:Label ID="lblM" runat="server" CssClass="status-message" Visible="false"></asp:Label>
            
            <asp:GridView ID="gvMaterial" runat="server" CssClass="grid-view" AutoGenerateColumns="true">
            </asp:GridView>
        </div>
    </form>
</body>
</html>