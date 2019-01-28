<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calendar.aspx.cs" Inherits="Calendar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calendar</title>
     <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
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
            <p class="col-4 welcome hide">Welcome, <asp:Literal ID="litEmail" runat="server"></asp:Literal>
                                              <asp:Literal ID="firstName" runat="server"></asp:Literal>!</p>
        </div>
    </header>

    <div class="content">
        <div class="wrapper">
              <div class="row">

                   <form id="form1" class="col-6 s-col-12 forms" runat="server">
                        <div>     
                            <p>
                                <asp:Button ID="editLink" runat="server" Text="Edit Profile" OnClick="editLink_Click" CssClass="button"/>
                            </p>

                            <p>
                                <asp:Button ID="uploadBtn" runat="server" Text="Upload CSV" OnClick="uploadBtn_Click" CssClass="button"/>
                            </p>

                            <p>
                                <asp:Button ID="hourBtn" runat="server" Text="Set Hours" OnClick="hourBtn_Click" CssClass="button"/>
                            </p>

                            <p>
                                <asp:Button ID="emailBtn"  runat="server" Text="Email" OnClick="emailBtn_Click" CssClass="button"/>
                            </p>

                            <p>
                                <asp:Button ID="logoutBtn" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="button"/>
                            </p>

                            <p>
                                <a href="https://www.cvs.com/" target="_blank" class="button cvs">Upload CVS</a>
                            </p>
                        </div>
                    </form>

                    <div class="col-6 hide">
                        <img class="logImg" src="images/img4.png" alt="Login image"/>

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
                </ul>
            </div>
        </footer>
        </div>
</body>
</html>
