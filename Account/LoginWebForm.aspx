<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginWebForm.aspx.cs" Inherits="Register_Login.Account.LoginWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Form</title>
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 20px;
        }
        .form-container {
            background-color: #fff;
            max-width: 500px;
            margin: 0 auto;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        h1 {
            color: #333;
            text-align: center;
            margin-bottom: 20px;
        }
        .form-table {
            width: 100%;
        }
        .form-table td {
            padding: 8px;
        }
        .form-table h3 {
            margin: 0;
            font-size: 14px;
            color: #555;
        }
        .form-control {
            width: 100%;
            padding: 8px;
            border: 1px solid #ddd;
            border-radius: 4px;
            box-sizing: border-box;
        }
        .btn-login {
            background-color: #9C27B0;
            color: white;
            padding: 10px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
            width: 100%;
        }
        .btn-login:hover {
            background-color: #7A1CAC;
        }
        .checkbox-label {
            font-size: 13px;
            color: #555;
        }
        .error-message {
            color: #d9534f;
            text-align: center;
            margin-top: 15px;
        }
        .success-message {
            color: #5cb85c;
            text-align: center;
            margin-top: 15px;
        }
        .register-redirect {
            text-align: center;
            margin-top: 15px;
            color: #555;
        }
        .register-link {
            color: #9C27B0;
            text-decoration: none;
        }
        .register-link:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <h1>Login Form</h1>
            <table class="form-table">
                <tr>
                    <td><h3>User Type</h3></td>
                    <td>
                        <asp:DropDownList ID="dlUserType" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select User Type" Value="" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Seller" Value="seller"></asp:ListItem>
                            <asp:ListItem Text="Distributer" Value="distributer"></asp:ListItem>
                            <asp:ListItem Text="Manufacturer" Value="manufacturer"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td><h3>Email</h3></td>
                    <td><asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="chkPassword" runat="server" CssClass="checkbox-label" 
                            AutoPostBack="true" OnCheckedChanged="chkPassword_CheckedChanged" 
                            Text="Show Password" />
                    </td>
                </tr>
                <tr>
                    <td><h3>Password</h3></td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnLogin" runat="server" Text="Login" 
                            CssClass="btn-login" OnClick="btnLogin_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="register-redirect">
                        Don't have an account? <a href="RegisterWebForm.aspx" class="register-link">Register here</a>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblLoginMessage" runat="server" CssClass="error-message" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>