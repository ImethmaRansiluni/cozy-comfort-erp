<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterWebForm.aspx.cs" Inherits="Register_Login.Account.RegisterWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Registration Form</title>
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
        .btn-register {
            background-color: #9C27B0;
            color: white;
            padding: 10px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
            width: 100%;
        }
        .btn-register:hover {
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
        .login-redirect {
            text-align: center;
            margin-top: 15px;
            color: #555;
        }
        .login-link {
            color: #9C27B0;
            text-decoration: none;
        }
        .login-link:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <h1>Register Form</h1>
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
                    <td><h3>Full Name</h3></td>
                    <td><asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox></td>
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
                        <asp:CheckBox ID="chkConfirmPassword" runat="server" CssClass="checkbox-label" 
                            AutoPostBack="true" OnCheckedChanged="chkConfirmPassword_CheckedChanged" 
                            Text="Show Confirm Password" />
                    </td>
                </tr>
                <tr>
                    <td><h3>Confirm Password</h3></td>
                    <td>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnRegister" runat="server" Text="Register" 
                            CssClass="btn-register" OnClick="btnRegister_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="login-redirect">
                        Already have an account? <a href="LoginWebForm.aspx" class="login-link">Login here</a>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblRegister" runat="server" CssClass="error-message" Visible="false"></asp:Label>
        </div>
       
        <asp:Panel ID="pnlSuccess" runat="server" Visible="false" style="position: fixed; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(0,0,0,0.5); z-index: 1000; display: flex; justify-content: center; align-items: center;">
            <div style="background-color: white; padding: 20px; border-radius: 5px; max-width: 400px; text-align: center;">
                <h2>Registration Successful!</h2>
                <p>Your account has been created successfully.</p>
                <asp:Button ID="btnClosePopup" runat="server" Text="OK" OnClick="btnClosePopup_Click" CssClass="btn-register" />
            </div>
        </asp:Panel>
    </form>
</body>
</html>