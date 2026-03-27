<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManufacturerDashboardWebForm.aspx.cs" Inherits="Register_Login.Manufacturer.ManufacturerDashboardWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Manufacturer Dashboard</title>
    <style>
        body { 
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
            margin: 0; 
            padding: 0; 
            background-color: #f5f5f5;
        }
        
        .dashboard-container {
            display: flex;
            min-height: 100vh;
        }
        
        .sidebar {
            width: 250px;
            background: #6a1b9a;
            color: white;
            padding: 20px 0;
            box-shadow: 2px 0 5px rgba(0,0,0,0.1);
        }
        
        .header {
            padding: 15px 20px;
            border-bottom: 1px solid rgba(255,255,255,0.1);
            margin-bottom: 20px;
        }
        
        .header h1 {
            margin: 0;
            font-size: 1.5rem;
        }
        
        .header p {
            margin: 5px 0 0;
            font-size: 0.9rem;
            opacity: 0.8;
        }
        
        .menu {
            display: flex;
            flex-direction: column;
        }
        
        .menu-item {
            padding: 12px 20px;
            color: white;
            text-decoration: none;
            transition: all 0.3s;
            cursor: pointer;
            display: flex;
            align-items: center;
            gap: 10px;
        }
        
        .menu-item:hover {
            background: rgba(255,255,255,0.1);
        }
        
        .menu-item.active {
            background: rgba(255,255,255,0.2);
            border-left: 3px solid white;
        }
        
        .menu-item i {
            width: 20px;
            text-align: center;
        }
        
        .content-area {
            flex: 1;
            padding: 20px;
            background: white;
            overflow-y: auto;
        }
        
        .logout-container {
            margin-top: auto;
            padding: 20px;
            border-top: 1px solid rgba(255,255,255,0.1);
        }
        
        .btn-logout {
            background: transparent;
            border: 1px solid rgba(255,255,255,0.3);
            color: white;
            padding: 8px 15px;
            border-radius: 4px;
            cursor: pointer;
            width: 100%;
            transition: all 0.3s;
        }
        
        .btn-logout:hover {
            background: rgba(255,255,255,0.1);
        }
        
        .iframe-container {
            width: 100%;
            height: calc(100vh - 40px);
            border: none;
            background: white;
        }
        
        .welcome-message {
            padding: 20px;
            text-align: center;
            color: #666;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-container">
            <!-- Sidebar Navigation -->
            <div class="sidebar">
                <div class="header">
                    <h1>Manufacturer Dashboard</h1>
                    <p>Welcome, <%= Session["Email"] %></p>
                </div>
                
                <div class="menu">
                    <div class="menu-item" onclick="loadPage('DistributerOrdersWebForm.aspx')">
                        <i>📨</i> Distributer Pending Requests
                    </div>
                    <div class="menu-item" onclick="loadPage('ManufacturerConfirmedRequestsWebForm.aspx')">
                        <i>📨</i> Confirmed Requests
                    </div>
                    <div class="menu-item" onclick="loadPage('ManufacturerRejectRequestsWebForm.aspx')">
                        <i>📨</i> Rejected Requests
                    </div>
                    <div class="menu-item" onclick="loadPage('ManufacturerCurrentStockWebForm.aspx')">
                        <i>📊</i> Current Stock
                    </div>
                    <div class="menu-item" onclick="loadPage('ManufacturerInventoryWebForm.aspx')">
                        <i>🔄</i> Manual Inventory Handler
                    </div>
                    <div class="menu-item" onclick="loadPage('MaterialsWebForm.aspx')">
                        <i>🧵</i> Add Materials
                    </div>
                    <div class="menu-item" onclick="loadPage('BlanketsWebForm.aspx')">
                        <i>🛏️</i> Add Blankets
                    </div>
                </div>
                
                <div class="logout-container">
                    <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="btn-logout" />
                </div>
            </div>

            <div class="content-area">
                <iframe id="contentFrame" class="iframe-container" src="about:blank"></iframe>
                <asp:Label ID="lblWelcome" runat="server" Text="" CssClass="welcome-message"></asp:Label>
            </div>
        </div>
        
        <script>
            function loadPage(url) {
                document.getElementById('contentFrame').src = url;
                
                const menuItems = document.querySelectorAll('.menu-item');
                menuItems.forEach(item => {
                    item.classList.remove('active');
                });
                event.currentTarget.classList.add('active');
                
                document.getElementById('lblWelcome').style.display = 'none';
            }
            
            document.getElementById('lblWelcome').style.display = 'block';
            document.getElementById('lblWelcome').innerHTML = '<h2>Welcome to Manufacturer Dashboard</h2><p>Please select an option from the sidebar to get started.</p>';
        </script>
    </form>
</body>
</html>