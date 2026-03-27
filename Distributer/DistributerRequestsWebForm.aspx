<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DistributerRequestsWebForm.aspx.cs" Inherits="Register_Login.Distributer.DistributerRequestsWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>Distributer Requests Form</title>
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
        
        .section {
            margin-bottom: 30px;
            padding: 20px;
            background-color: #fafafa;
            border-radius: 6px;
        }
        
        .form-row {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
            margin-bottom: 15px;
        }
        
        .form-group {
            margin-bottom: 15px;
            flex: 1;
            min-width: 250px;
        }
        
        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: 600;
            color: #555;
        }
        
        .form-control {
            width: 100%;
            padding: 8px 12px;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-size: 14px;
        }
        
        select.form-control {
            height: 38px;
        }
        
        .btn {
            padding: 8px 16px;
            background-color: #6a1b9a;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 14px;
            transition: background-color 0.3s;
        }
        
        .btn:hover {
            background-color: #5e1480;
        }
        
        .btn-request {
            background-color: #4CAF50;
        }
        
        .btn-request:hover {
            background-color: #3e8e41;
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
        
        @media (max-width: 768px) {
            .form-row {
                flex-direction: column;
                gap: 10px;
            }
            
            .form-group {
                min-width: 100%;
            }
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h2>Distributer Inventory Management</h2>
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
            
            <div class="section">
                <h3>Request Additional Inventory</h3>
                <div class="form-row">
                    <div class="form-group">
                        <label for="ddlBlanketModel">Blanket Model</label>
                        <asp:DropDownList ID="dlBlanketModel" runat="server" CssClass="form-control" 
                            DataTextField="BlanketModel" DataValueField="BlanketModelID">
                        </asp:DropDownList>
                    </div>
                    
                    <div class="form-group">
                        <label for="txtQuantity">Quantity</label>
                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" 
                            TextMode="Number" min="1" placeholder="Enter quantity"></asp:TextBox>
                    </div>
                </div>
                
                <div class="form-group">
                    <asp:Button ID="btnRequest" runat="server" Text="Submit Request" 
                        CssClass="btn btn-request" OnClick="btnRequest_Click" />
                </div>
            </div>
            <h3>My Requests</h3>
            <asp:GridView ID="gvRequests" runat="server" AutoGenerateColumns="false" CssClass="grid-view"
                EmptyDataText="No requests found" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="DistributerRrequestID" HeaderText="Request ID" />
                    <asp:BoundField DataField="BlanketModel" HeaderText="Blanket Model" />
                    <asp:BoundField DataField="RequestQty" HeaderText="Quantity" />
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblRequestsMessage" runat="server" Visible="false" CssClass="alert alert-info"></asp:Label>
        </div>
    </form>
</body>
</html>