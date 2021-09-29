<%@ Page Title="" Language="C#" MasterPageFile="~/TSPSite.Master" AutoEventWireup="true" CodeBehind="CreateForum.aspx.cs" Inherits="KPMAMS.CreateForum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-8 mx-auto">
                <div class="card pt-2 pb-2">
                     <div class="card-body">
                         <div class="row">
                             <div class="col">
                                 <h3>Create Forum</h3>
                             </div>
                         </div>
                         <div class="row">
                             <div class="col">
                                 <hr>
                             </div>
                         </div>
                         <div class="row">
                             <div class="col-md-10">
                                 <label>Forum Title:</label>
                                 <div class="form-group">
                                    <asp:TextBox class="form-control" ID="tbTitle" runat="server" placeholder="Title of forum"></asp:TextBox>
                                 </div>
                             </div>
                             <div class="col-md-4">
                                 <label>Class:</label>
                                 <div class="from-group">
                                    <asp:DropDownList class="form-control" ID="dlClassList" runat="server">
                                    </asp:DropDownList>
                                 </div>
                             </div>
                         </div>
                         <div class="row">
                             <div class="col">
                                 <label>Forum Content:</label>
                                 <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="tbContent" runat="server" TextMode="MultiLine" placeholder="Contents of forum" Rows="5"></asp:TextBox>
                                 </div>
                                 <div class="from-group">
                                     <asp:Button ID="btnCreate" runat="server" class="btn btn-block btn-info btn-lg" Text="Create Forum" OnClick="btnCreate_Click" />
                                 </div>
                             </div>
                         </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
