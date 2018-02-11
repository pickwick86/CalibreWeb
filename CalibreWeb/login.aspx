<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="CalibreWeb.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="margin-right:auto;margin-left:auto;margin-top:100px;margin-bottom:auto;">
            <tr>
                <td>Login</td>
                <td><asp:TextBox runat="server" ID="mtxtLogin" /></td>
            </tr>
            <tr>
                <td>Password</td>
                <td><asp:TextBox runat="server" ID="mtxtPassword" TextMode="Password" /></td>
            </tr>
            <tr>
                <td colspan="2"><center><asp:Button runat="server" ID="mValidate" Text="Validate"/></center></td>
            </tr>
        </table>
        

        
     </div>
    </form>
</body>
</html>
