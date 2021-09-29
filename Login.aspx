<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="KPMAMS.Login" %>

<!DOCTYPE html>

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <title>LoginPage</title>
    <link href="Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/Login.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" integrity="sha512-iBBXm8fW90+nuLcSKlbmrPcLa0OT92xO1BIsZ+ywDWZCvqsWgccV3gFoRBv0z+8dLJgyAHIhR35VZc2oM/gI1w==" crossorigin="anonymous" referrerpolicy="no-referrer" />

</head>

<body>
    <form runat="server">
        <div class="col-lg-3 col-md-6 col-sm-8 col-xs-12 align-middle">
            <div data-toggle="play-animation" data-play="fadeInUp" data-offset="0" class="panel panel-default panel-flat anim-running anim-done" style="">
                <p class="text-center mb-lg">
                    <br>
                    <a href="#">
                        <img src="img/logoKPM.png" alt="Image" class="block-center img-rounded">
                    </a>
                </p>
                <p class="text-center mb-lg">
                    <strong>SIGN IN TO CONTINUE.</strong>
                </p>
                <div class="panel-body">
                    <form role="form" class="mb-lg">
                        <div class="text-right mb-sm"><a href="#" class="text-muted">Need to Signup?</a>
                        </div>
                        <div class="form-group has-feedback">
                            <asp:TextBox ID="tbUserID" type="id" placeholder="UserID" runat="server" class="form-control"></asp:TextBox>
                            <span class="fa fa-envelope form-control-feedback text-muted"></span>
                        </div>
                        <div class="form-group has-feedback">
                            <asp:TextBox ID="tbPassword" runat="server" type="password" placeholder="Password" class="form-control"></asp:TextBox>
                            <span class="fa fa-lock form-control-feedback text-muted"></span>
                        </div>
                        <div class="clearfix">
                            <div class="checkbox c-checkbox pull-left mt0">
                            <label>
                                <input type="checkbox" value="">
                                <span class="fa fa-check"></span>Remember Me</label>
                            </div>
                            <div class="pull-right"><a href="#" class="text-muted">Forgot your password?</a>
                            </div>
                        </div>
                        <asp:Button ID="btnLogin" class="btn btn-primary btn-lg btn-block" runat="server" Text="Login" OnClick="btnLogin_Click" />
                    </form>
                </div>
            </div>
        </div>
    </form>
</body>
