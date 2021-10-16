<%@ Page Title="" Language="C#" MasterPageFile="~/TSPSite.Master" AutoEventWireup="true" CodeBehind="ForumList.aspx.cs" Inherits="KPMAMS.ForumList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find(""))).dataTable();
        });
    </script>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="p-4 col">
        <div class="row">
            <div class="col">
                <center>
                    <h3>ForumList</h3>
                </center>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <hr>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <h3>
                    <asp:Label ID="lbClass" runat="server" Text=""></asp:Label>
                </h3>
            </div>
            <div class="col-md-auto" >
                <div class="from-group">
                    <asp:DropDownList class="form-control" ID="dlClassList" runat="server" Visible="false" OnSelectedIndexChanged ="dlClassList_SelectedIndexChanged" AutoPostBack="true" >
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col col-md-2">
                <asp:Button ID="btnCreateForum" class="btn btn-info btn-block btn-sm" Visible="false" runat="server" Text="Create" OnClick="btnCreateForum_Click" />
            </div>
        </div>
        <div class="row">
            <div class="col pt-3">
                <asp:GridView class="table table-striped table-bordered table-responsive-md" ID="GvForumList" runat="server" AutoGenerateColumns="False" DataKeyNames="ForumGUID" OnRowDataBound="GvForumList_RowDataBound" >
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hlView" Text="View" runat="server" ></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ForumGUID" HeaderText="Forum GUID" ReadOnly="True" SortExpression="ForumGUID" Visible="false"/>
                        <asp:BoundField DataField="CreateBy" HeaderText="Create By" ReadOnly="True" SortExpression="CreateDate" HtmlEncode="false" />
                        <asp:BoundField DataField="LastUpdateDate" HeaderText="Last Update Date" ReadOnly="True" SortExpression="LastUpdateDate" />
                        <asp:BoundField DataField="Content" HeaderText="Content" ReadOnly="True" SortExpression="Content" Visible="false"/>
                        <asp:BoundField DataField="Title" HeaderText="Title" ReadOnly="True" SortExpression="Title"/>
                        <asp:BoundField DataField="NoOfComment" HeaderText="Total comment" ReadOnly="True" SortExpression="TotalComment" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>        
    </div>
</asp:Content>
