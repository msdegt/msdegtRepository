<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SmartHouseWebForms.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>Умный дом</title>
    <link runat="server" href="Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <header runat="server" class="header">		
	</header>
    <form id="form1" runat="server" class="wrapper">
        <div>
            <asp:DropDownList ID="dropDownDevicesList" runat="server">
                <asp:ListItem>Телевизор</asp:ListItem>
                <asp:ListItem>Холодильник</asp:ListItem>
                <asp:ListItem>Бойлер</asp:ListItem>
                <asp:ListItem>Жалюзи</asp:ListItem>
                <asp:ListItem>Система капельного полива</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="addDevicesButton" runat="server" Text="Добавить" />
            <br />
            <asp:Panel ID="devicesPanel" runat="server" ></asp:Panel>
        </div>
    </form>
</body>
</html>