<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <!-- <link rel="stylesheet" type="text/css" href="css/style.css" />  -->
    <link rel="stylesheet" type="text/css" href="css/calendar.css" /> 
    <link href="https://fonts.googleapis.com/css?family=Karla" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet"/>
    <link rel="shortcut icon" type="image/png" href="images/favicon-32x32.png"/>
</head>


<body>
    <div class="main" >
        <header>
            <div class="row wrapper">
                <p class="col-8 titl s-col-12"><span class="letter">C</span>hill <span class="letter">V</span>irtual <span class="letter">S</span>cheduling</p>
                <p class="col-4 welcome hide">Welcome to C.V.S.!</p>
            </div>
        </header>
        <div class="content">
            <div class="wrapper">
                  <div class="row">

                       <div class="col-6 hide">
                            <img class="logImg" src="images/img2.png" alt="Login image"/>

                    </div>
                      <div class="wrapper">
                    <form id="login" runat="server" class="col-6 ">
                            <div class="login">
                                <p>
                                    <label for="emailLogin">Email:</label>
                                    <asp:TextBox ID="emailLogin" TextMode="Email" runat="server"></asp:TextBox>
                                </p>
                                <p>
                                    <label id="passLabel" for="passwordLogin" runat="server">Password:</label>
                                    <asp:TextBox ID="passwordLogin" TextMode="Password" runat="server"></asp:TextBox>
          
                                </p>
                                <p>
                                    <label id="conLabel" for="passwordConfirm" runat="server">Password Confirmation:</label>
                                    <asp:TextBox ID="passwordConfirm" TextMode="Password" runat="server"></asp:TextBox>
                                </p>
                                <p>
                                    <label id="verifyLabel" for="verificationConfirm" runat="server">Email verification code:</label>
                                    <asp:TextBox ID="verifyText" runat="server"></asp:TextBox>
                                </p>
                                <p>
                                    <asp:Button ID="btnLogin" Text="Login" runat="server" OnClick="btnLogin_Click" CssClass="button" />
                                </p>
            
                                 <p class="alertCss"><asp:Literal ID="litAlert" runat="server"></asp:Literal></p>
                            </div>
                        </form>
                          </div>
                    </div>
            </div>
        </div>
        <footer>
            <div class="wrapper">
                <ul class="info">
	                <li>One College Avenue, Williamsport, PA 17701</li>
	                <li>570.320.2400</li>
	                <li>800.367.9222</li>
                    <li>570.396.6449</li>
                </ul>
            </div>
        </footer>
    </div>
</body>
</html>
