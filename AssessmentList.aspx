<%@ Page Title="" Language="C#" MasterPageFile="~/TSPSite.Master" AutoEventWireup="true" CodeBehind="AssessmentList.aspx.cs" Inherits="KPMAMS.AssessmentList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find(""))).dataTable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="col pt-4">
        <div class="row">
            <div class="col">
                <center>
                    <h3>
                        Assessment List
                    </h3>
                </center>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <hr />
            </div>
        </div>
        <div class="row">
            <div class="col">
                <h3>
                    <asp:Label ID="lbClass" runat="server" Text=""></asp:Label>
                </h3>
            </div>
            <div class="col-md-auto">
                <asp:Button ID="btnCreateAssessment" class="btn btn-info btn-block btn-sm" Visible="false" runat="server" Text="Create" OnClick="btnCreateAssessment_Click" />
            </div>
            <div class="col col-md-2">
                <div class="from-group">
                    <asp:DropDownList class="form-control" ID="dlClassList" runat="server" Visible="false" OnSelectedIndexChanged="dlClassList_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:DropDownList class="form-control" ID="dlStatus" runat="server" Visible="false" OnSelectedIndexChanged="dlStatus_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col pt-3">
                <asp:GridView class="table table-striped table-bordered table-responsive-md" ID="GvAssessmentList" runat="server" AutoGenerateColumns="False" OnRowDataBound="GvAssessmentList_RowDataBound" >
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hlView" Text="View" runat="server" ></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AssessmentGUID" HeaderText="Assessment GUID" ReadOnly="True" SortExpression="AssessmentGUID" Visible="false"/>
                        <asp:BoundField DataField="CreateBy" HeaderText="Create By" ReadOnly="True" SortExpression="CreateDate" HtmlEncode="false" />
                        <asp:BoundField DataField="Title" HeaderText="Title" ReadOnly="True" SortExpression="Title"/>
                        <asp:BoundField DataField="DueDate" HeaderText="Due date" ReadOnly="True" SortExpression="DueDate" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
