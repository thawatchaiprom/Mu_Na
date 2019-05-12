<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="StockSearch.aspx.cs" Inherits="StockMUNA.StockSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="upgrid" runat="server">
           <ContentTemplate>
                <div class="container">
        <div class="form-horizontal">
            <h2>Search Stock</h2>
            <hr />
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" CssClass="col-md-2 control-label" Text="รหัส (Stock code)"></asp:Label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtStockCode" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="Label2" runat="server" CssClass="col-md-2 control-label" Text="ประเภท"></asp:Label>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlGroup" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="--Please Select--" Value=""></asp:ListItem>
                        <asp:ListItem Text="วัสดุ/ครุภัณฑ์ทางการแพทย์" Value="MED"></asp:ListItem>
                        <asp:ListItem Text="วัสดุ/ครุภัณฑ์สำนักงาน" Value="OFF"></asp:ListItem>
                        <asp:ListItem Text="วัสดุ/ครุภัณฑ์คอมพิวเตอร์" Value="ICT"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="Label3" runat="server" CssClass="col-md-2 control-label" Text="กลุ่ม"></asp:Label>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlSubCode" CssClass="form-control" runat="server" Enabled="false">

                    </asp:DropDownList>
                </div>
            </div>
            <%--<div class="form-group">
                <asp:Label ID="Label4" runat="server" CssClass="col-md-2 control-label" Text="inventory"></asp:Label>
                <div class="col-md-3">
                    <asp:TextBox ID="txtinv" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>--%>
            <div class="form-group">
                <div class="col-md-2"></div>
                <div class="col-md-6">
                    <asp:Button ID="btnSearch"  runat="server" Text="Search" CssClass="btn btn-info"  OnClick="btnSearch_Click"  />
                </div>
            </div>
             <hr />
       <div class="panel panel-default">
            <!-- Default panel contents -->
            <div class="panel-heading">Search Result</div>
            <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="False"  Width="100%" OnRowDataBound="grvData_RowDataBound"  BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal">
                                    <EmptyDataTemplate>ไม่พบข้อมูลที่ค้นหา</EmptyDataTemplate>
                                    <Columns>
                                 
                                        <asp:BoundField DataField="stock_code" HeaderText="รหัส"  ItemStyle-HorizontalAlign="Left" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="category" HeaderText="กลุ่ม" ItemStyle-HorizontalAlign="Left" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="department" HeaderText="แผนก" ItemStyle-HorizontalAlign="Left" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="sub_category_name" HeaderText="ประเภท" ItemStyle-HorizontalAlign="Left" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="inv_no" HeaderText="inventory" ItemStyle-HorizontalAlign="Left" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>--%>
                                       
                                         <asp:TemplateField HeaderText="">
		                                <ItemTemplate>
			                                <asp:HyperLink id="hplLink" runat="server" Text=""  ImageUrl="~/Images/icon-license.jpg"></asp:HyperLink>
                                            <asp:HyperLink id="hplLinkView" runat="server" Text="" ImageUrl="~/Images/icon_search_24.png" ></asp:HyperLink>
                                        
		                                </ItemTemplate>
	                                    </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle Font-Size="Larger"  Font-Bold="true" ForeColor="#ff9933" HorizontalAlign="Center" />

                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#487575" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#275353" />

                                </asp:GridView>
           </div>
      </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
