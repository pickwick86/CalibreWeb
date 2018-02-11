<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookUserControl.ascx.cs" Inherits="CalibreWeb.BookUserControl" %>
<table>
    <tr>
        <td style="width:205px">
            <center>
                <asp:HyperLink ID="mLink" runat="server" Width="100%">
                    <asp:Image ID="mImage" runat="server" ClientIDMode="Static" Width="200px"/>
                </asp:HyperLink>
            </center>
        </td>
        <td valign="top">
            <center>
                <div style="max-width:350px">
                    <center>
                        <asp:Label Width="100%" runat="server" ID="mTitre" Font-Size="X-Large" Font-Bold="True" Text="MonTitre" /><br />
                        <asp:Label Width="100%" runat="server" ID="mAuthors" Font-Size="Larger" /><br />
                        <asp:Label runat="server" ID="mSerie" Font-Size="Medium" Width="100%" />
                    </center>
                    <p>
                        <asp:Label ID="mPages" runat="server" /> pages<br />
                        Lu : <asp:Label ID="mRead" runat="server"/><br />
                        Ajout : <asp:Label runat="server" ID="mAdded" /><br />
                        Genre : <asp:Label runat="server" ID="mTags"/><br />
                        Langue : <asp:Label runat="server" ID="mLanguage" /><br />
                        Envie : <asp:Button ID="mbtnEnvieMoins" runat="server" Text="-" onclick="mbtnEnvieMoins_Click" />
                        <asp:Label runat="server" ID="mEnvie" />&nbsp;/&nbsp;5  
                        <asp:Button ID="mbtnEnviePlus" runat="server" Text="+" onclick="mbtnEnviePlus_Click" /><br />
                    </p>
                    <center>
                        <asp:Button ID="mbtnMarkAsRead" runat="server" Text="Marquer comme lu" 
                            onclick="mbtnMarkAsRead_Click" Height="55px" Width="150px" />
                    </center>
                </div>
            </center>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <p style="font-size:medium;text-align:justify" >
                <strong>Synopsis : </strong> <br />
                <asp:Label ID="mSummary" runat="server" />
            </p>
        </td>
    </tr>
</table>
<asp:Label ID="mPath" runat="server" Visible="False"></asp:Label>
<%--<script type="text/javascript">
    alert('coucou');
    img = document.getElementById("mImage");
    img.style.width = screen.width * 0.3;
</script>--%>
