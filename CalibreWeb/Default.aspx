<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CalibreWeb.Default2" %>
<%@ Register TagPrefix="my" TagName="BookUserControl" Src="~/BookUserControl.ascx" %>
<%@ Register TagPrefix="myControls" Namespace="CalibreWeb" assembly="CalibreWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CalibreWeb</title>
    <meta name="viewport" content="width=device-width" />
</head>
<body>
    <form id="form1" runat="server">

    <div style="width:100%; height:100%">
        <table>
            <tr>
                <td style="width:30px"><asp:Button runat="server" ID="mbtnBack" onclick="mbtnBack_Click" Text="Retour" /></td>
                <td style="width:30px"><asp:Button runat="server" ID="mbtnPrevious" Text="<<" onclick="mbtnPrevious_Click" /></td>
                <td style="width:100%"><center><asp:Label runat="server" ID="mlblSelecter" /><br /><asp:Label runat="server" ID="mlblIndex" /></center></td>
                <td style="width:30px"><asp:Button runat="server" ID="mbtnNext" Text=">>" onclick="mbtnNext_Click" /></td>
            </tr>
        </table>
        <center>
        <div style="max-width:1024px">
            
            <My:BookUserControl ID="mUserControl" runat="server" />
            
        </div>
        </center>
    </div>
    </form>
</body>
</html>
