<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Email.aspx.cs" Inherits="Email" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>Email</title>
         <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <!-- <link rel="stylesheet" type="text/css" href="css/style.css" />  -->
        <link rel="stylesheet" type="text/css" href="css/calendar.css" /> 
        <link href="https://fonts.googleapis.com/css?family=Karla" rel="stylesheet"/>
        <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet"/>
        <link rel="shortcut icon" type="image/png" href="images/favicon-32x32.png"/>
</head>
<body>
    <div class="main">
    <header>
        <div class="row wrapper">
            <p class="col-8 titl s-col-12"><span class="letter">C</span>hill <span class="letter">V</span>irtual <span class="letter">S</span>cheduling</p>
                <p class="col-4 welcome hide">Welcome to C.V.S.!</p>
        </div>
    </header>

    <div class="content">
        <div class="wrapper">
        <div class="row">
            <h1>Please enter e-mail body:</h1>
        </div>

        <div class="row">
        <form class="emailForm" id="form1" runat="server">
               <div class="textboxdiv">
                    <asp:TextBox ID="txtEmailBody" TextMode="multiline" Columns="50" Rows="5" runat="server" text="Included with this message is the verification code you need to register for office hours." CssClass="textbox" Wrap="true"></asp:TextBox>
                    <asp:Label ID="litAlert" runat="server"></asp:Label>
                </div>
            <div>
                <p>
                    <asp:Button ID="btnSendEmail" Text="Submit" runat="server" OnClick="btnSendEmail_Click" CssClass="button"/>
                    <asp:Button ID="btnClose" Text="Close" runat="server" OnClick="btnClose_Click" CssClass="button"/>
                </p>
            </div>
        </form>
        </div>

        
        </div>
        
      </div>
        <footer>
            <div class="wrapper">
                <ul class="info">
	                <li>One College Avenue, Williamsport, PA 17701</li>
	                <li>570.320.2400</li>
	                <li>800.367.9222</li>
                </ul>
            </div>
        </footer>
        </div>
</body>
</html>
