<%@ Control Language="c#" AutoEventWireup="true" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<script runat="server">
protected void Page_Load(object sender, EventArgs e){
	
	
	 Sitecore.Web.UI.WebControls.Sublayout sublayout = Parent as Sitecore.Web.UI.WebControls.Sublayout;
 NameValueCollection sublayoutParameters = Sitecore.Web.WebUtil.
ParseUrlParameters(sublayout.Parameters);
 string selectedItemsValue = sublayoutParameters["ForumId"];
 
 lbl.Text=selectedItemsValue;
}
</script>

<h1>Zimbra</h1>
<asp:Label ID="lbl" runat="server"></asp:Label>