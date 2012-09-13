<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" 
    Inherits="EntityGenerator.Default" ValidateRequest="false" %>
<%@ Import namespace="System.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 311px;
        }
        .style2
        {
            width: 203px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    <table style="width:114%;">
                        <tr>
                            <td class="style2">
                                手机系统：</td>
                            <td>
                                <asp:DropDownList ID="ddlSystem" runat="server" Height="16px" Width="146px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="lblVariable" runat="server" Text="导入"></asp:Label>
                                ：</td>
                            <td>
                                <asp:TextBox ID="txtVariable" runat="server" Width="229px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                Mobile API:</td>
                            <td>
                                <asp:TextBox ID="txtMobileAPI" runat="server" Width="228px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td rowspan="2">
                    <asp:GridView ID="gvMappingList" AutoGenerateColumns="false" runat="server" Height="100%" Width="359px">
                        <Columns>
                            <asp:BoundField HeaderText="层级" ReadOnly="true" DataField="Level" />
                            <asp:TemplateField HeaderText="Json实体名">
                                <ItemTemplate>
                                    <asp:TextBox ID="EntityName" runat="server" Text=<%# Eval("EntityName")%>></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="转换前">
                                <ItemTemplate>
                                    <asp:TextBox ID="JsonFromField" runat="server" Text=<%# Eval("JsonFromField")%>></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="转换后">
                                <ItemTemplate>
                                    <asp:TextBox ID="JsonToField" runat="server" Text=<%# Eval("JsonToField")%>></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="类型">
                                <ItemTemplate>
                                    <asp:TextBox ID="JsonToType" runat="server" Text=<%# Eval("JsonToType")%>></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Json字符串：<br />
                    <asp:TextBox ID="txtJsonString" runat="server" Height="286px" 
                        TextMode="MultiLine" Width="357px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Button ID="btnLoadMapping" runat="server" onclick="btnLoadMapping_Click" 
                        Text="生成映射关系" />
                </td>
                <td align="right">
                    <asp:Button ID="btnGenerate" runat="server" onclick="btnGenerate_Click" 
                        Text="生成实体" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
