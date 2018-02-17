<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="CalibreWeb.Search" %>
<%@ Register TagPrefix="myControls" Namespace="CalibreWeb" assembly="Calibre.CalibreWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <table width="500">
        <tr><td style="width:50%">
            <myControls:BookSelecterDropDownList ID="mSelectersDropDownList" runat="server" 
                Width="100%" CausesValidation="true" 
                OnSelectedIndexChanged="mSelectersDropDownList_SelectedIndexChanged" /><br />
            <myControls:TagsDropDownList ID="mTagsDropDownList" runat="server" width="100%" 
                CausesValidation="true" 
                OnSelectedIndexChanged="mTagsDropDownList_SelectedIndexChanged"/><br />
            <myControls:LanguagesDropDownList ID="mLanguagesDropDownList" runat="server" 
                CausesValidation="true" width="100%" 
                OnSelectedIndexChanged="mLanguagesDropDownList_SelectedIndexChanged"/><br />
            <asp:Label ID="mlblAuthor" runat="server" Text="Auteur :"  Width="45%" /><asp:TextBox ID="mtxtAuthor" runat="server" Width="50%" /><br />
            <asp:Label ID="mlblTitle" runat="server" Text="Titre :"  Width="45%" /><asp:TextBox ID="mtxtTitle" runat="server" Width="50%" /><br />
            <asp:Label ID="mlblSerie" runat="server" Text="Série :"  Width="45%" /><asp:TextBox ID="mtxtSerie" runat="server" Width="50%" /><br />
            <asp:Label ID="mlblRatingMin" runat="server" Text="Note min. :"  Width="45%" /><asp:TextBox ID="mtxtRating" runat="server" Width="50%" /><br />
            <asp:Label ID="mlblMinPages" runat="server" Text="Pages min. :"  Width="45%" /><asp:TextBox ID="mtxtMinPages" runat="server" Width="50%" /><br />
            <asp:Label ID="mlblMaxPages" runat="server" Text="Pages max. :"  Width="45%" /><asp:TextBox ID="mtxtMaxPages" runat="server" Width="50%" /><br />
        </td>
        <td  style="width:50%">
            <center>
            <asp:Button Text="Find" ID="mButton" runat="server" onclick="mButton_Click" 
                    Height="45px" Width="40%" />
            </center>
        </td>
        <td><asp:Label ID="mlblMessage" runat="server" /></td>
        </tr>
        </table>
    </center>
    </form>
</body>
</html>
