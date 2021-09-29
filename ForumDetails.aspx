<%@ Page Title="" Language="C#" MasterPageFile="~/TSPSite.Master" AutoEventWireup="true" CodeBehind="ForumDetails.aspx.cs" Inherits="KPMAMS.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="container pt-2 pb-2">
        <div class="row">
            <div class="col mx-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="row d-flex">
                            <div class="col form-group">
                                <h3>
                                    <asp:Label ID="lbTitle" runat="server" Text=" "></asp:Label>
                                </h3>
                                <asp:TextBox ID="tbTitle" style="font-size: 1.17em; font-weight: bolder" class="form-control" runat="server" Visible="false" placeholder="Title of forum" ></asp:TextBox>
                            </div>
                            <div class="col-md-2 justify-content-end">
                                <div class="dropdown">
                                    <asp:LinkButton ID="lbMenu" CssClass="btn btn-info btn-block dropdown-toggle" role="button" type="button" data-bs-toggle="dropdown" Visible="false" runat="server">More</asp:LinkButton>
                                    <div class="dropdown-menu text-center">
                                        <asp:LinkButton ID="lbModify" CssClass="dropdown-item" runat="server" OnClick="lbModify_Click" >Modify</asp:LinkButton>
                                        <asp:LinkButton ID="lbDelete" CssClass="dropdown-item" style="color:red; font-weight: bold" runat="server" OnClick="lbDelete_Click">Delete</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lbCreated" runat="server" Text=" "></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lbLastUpdate" runat="server" Text=" "></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="lbClass" runat="server" Text=" "></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="lbContent" runat="server" Text=" "></asp:Label>
                                    <div class="form-group">
                                        <asp:TextBox ID="tbContent" CssClass="form-control" Visible="false" TextMode="MultiLine" placeholder="Contents of forum" Rows="5" runat="server"></asp:TextBox>
                                    </div>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row d-flex justify-content-end">
                            <div class="col-md-2 mr-auto">
                                <asp:Button ID="btnUpdate" CssClass="btn btn-block btn-warning" Visible="false" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnCancel" CssClass="btn btn-block btn-danger" Visible="false" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lbCreatedDate" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:Image ID="ImgProfilePic" runat="server" />
                                <asp:Label ID="lbUserName" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container pt-2 pb-2">
        <div class="row">
            <div class="col mx-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <h3>Comment</h3>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr>
                            </div>
                        </div>
                        <div class="row d-flex">
                            <div class="col">
                                <h3>
                                    <asp:Label ID="lblNoData" runat="server" Text="No comment yet" Visible="false"></asp:Label>
                                </h3>
                                <asp:GridView class="table table-striped table-bordered table-responsive-md" ID="GvCommentList" runat="server" AutoGenerateColumns="False" DataKeyNames="CommentGUID" >
                                    <Columns>
                                        <asp:BoundField DataField="CommentGUID" HeaderText="Comment GUID" ReadOnly="True" SortExpression="CommentGUID" Visible="false"/>
                                        <asp:TemplateField HeaderText="Comment">
                                            <ItemTemplate>
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col">
                                                            <asp:Label ID="lblCommentBy" runat="server" Text='<%# Eval("CommentBy") %>' Font-Bold="True"></asp:Label>
                                                            comment on
                                                            <asp:Label ID="lblCommentDate" runat="server" Text='<%# Eval("CreateDate") %>'></asp:Label>      
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col">
                                                            <asp:Label ID="lblCommentContent" runat="server" Text='<%# Eval("Content") %>' Font-Size="Small"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>Comment :</label>
                                <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbComment" runat="server" TextMode="MultiLine" placeholder="Contents of comment" Rows="5"></asp:TextBox>
                                </div>
                                <div class="from-group">
                                    <asp:Button ID="btnSubmit" runat="server" class="btn btn-block btn-info btn-lg" Text="Post comment" OnClick="btnSubmit_Click"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
