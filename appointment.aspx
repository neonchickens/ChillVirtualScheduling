<%@ Page Language="C#" AutoEventWireup="true" CodeFile="appointment.aspx.cs" Inherits="appointment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Appointment</title>
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
                            <form id="form1" runat="server">
                <div class="row">
                    <div class="wrapper">
                        <div class="text">
                            <h2>Hours Selected</h2>
                            <asp:Table ID="tblHoursSelected" runat="server" OnLoad="tblHoursSelected_Load"  CssClass="text">
                                <asp:TableHeaderRow>
                                    <asp:TableCell>
                                        <asp:Label runat="server" Text="Select"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label runat="server" Text="Date"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label runat="server" Text="Time"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label runat="server" Text="Office"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableHeaderRow>
                            </asp:Table>
                            <asp:Button ID="btnRemoveSelected" runat="server" text="Remove Selected" OnClick="btnRemoveSelected_Click" CssClass="button"></asp:Button>
            
                            <h2>Hours Available</h2>
                            <asp:Table ID="tblHours" runat="server" OnLoad="tblHours_Load">
                                <asp:TableHeaderRow>
                                    <asp:TableCell>
                                        <asp:Label runat="server" Text="Select"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label runat="server" Text="Date"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label runat="server" Text="Time"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label runat="server" Text="Office"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableHeaderRow>
                            </asp:Table>
                            <p>Add note for prof. here (optional): </p>
                            <asp:TextBox ID="txtNote" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSubmit" runat="server" text="Submit" OnClick="btnSubmit_Click" CssClass="button"></asp:Button>
                            <p>
                                 <asp:Literal ID="litAlert" runat="server"></asp:Literal>
                            </p>
                        </div>

                    </div>
                        <p><asp:Button ID="logoutBtn" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="button"/></p>
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
