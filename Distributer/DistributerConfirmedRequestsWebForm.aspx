<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DistributerConfirmedRequestsWebForm.aspx.cs" Inherits="Register_Login.Distributer.DistributerConfirmedRequestsWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Confirmed Requests</title>
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
            font-size: 24px;
        }
        
        .message {
            padding: 12px;
            margin: 15px 0;
            border-radius: 4px;
            text-align: center;
            display: none;
        }
        
        .info {
            background-color: #e8f5e9;
            color: #2e7d32;
            border: 1px solid #c8e6c9;
            display: block;
        }
        
        .error {
            background-color: #ffebee;
            color: #c62828;
            border: 1px solid #ffcdd2;
            display: block;
        }
        
        .grid-view {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
            font-size: 14px;
        }
        
        .grid-view th {
            background-color: #6a1b9a;
            color: white;
            padding: 12px;
            text-align: left;
            font-weight: 600;
        }
        
        .grid-view td {
            padding: 10px 12px;
            border-bottom: 1px solid #eee;
        }
        
        .grid-view tr:nth-child(even) {
            background-color: #f9f9f9;
        }
        
        .grid-view tr:hover {
            background-color: #f0e6ff;
        }
        
        .status-confirmed {
            color: #4CAF50;
            font-weight: bold;
        }
        
        .empty-data {
            text-align: center;
            padding: 20px;
            color: #666;
            font-style: italic;
        }
        
        .action-buttons {
            margin-top: 20px;
            display: flex;
            gap: 10px;
        }
        
        .btn {
            padding: 8px 16px;
            background-color: #6a1b9a;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s;
            font-size: 14px;
            text-decoration: none;
            display: inline-block;
        }
        
        .btn:hover {
            background-color: #5e1480;
        }
        
        .btn-secondary {
            background-color: #757575;
        }
        
        .btn-secondary:hover {
            background-color: #616161;
        }
        
        .date-column {
            white-space: nowrap;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h2>Confirmed Requests</h2>
            </div>
            
            <asp:Label ID="lblMessage" runat="server" CssClass="message" Visible="false"></asp:Label>
            
            <asp:GridView ID="gvConfirmedRequests" runat="server" CssClass="grid-view" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="SellerRequestID" HeaderText="Request ID" />
                    <asp:BoundField DataField="SellerName" HeaderText="Seller Name" />
                    <asp:BoundField DataField="BlanketModel" HeaderText="Blanket Model" />
                    <asp:BoundField DataField="RequestQty" HeaderText="Quantity" />
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <span class="status-confirmed"><%# Eval("Status") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="UpdateDate" HeaderText="Updated On" DataFormatString="{0:dd-MMM-yyyy HH:mm}" ItemStyle-CssClass="date-column" />
                    <asp:BoundField DataField="Note" HeaderText="Notes" />
                </Columns>
                <EmptyDataTemplate>
                    <div class="empty-data">No confirmed requests found</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </form>
</body>
</html>