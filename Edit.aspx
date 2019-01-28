<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit</title>
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

    <div class="content row">
        <div class="wrapper">
    <form id="form1" runat="server">
        <div>
            <!-- 
                The personal info for the instructor 
            -->
            <p >
                <label for="litEmail">Email:</label>
                <asp:Literal ID="litEmail" runat="server"></asp:Literal>
            </p>
            <p>
                <label for="firstName">First Name:</label>
                <asp:TextBox ID="firstName" TextMode="SingleLine" runat="server"></asp:TextBox>
                <asp:Literal ID="firstError" runat="server"></asp:Literal>
            </p>
            <p>
                <label for="lastName">Last Name:</label>
                <asp:TextBox ID="lastName" TextMode="SingleLine" runat="server"></asp:TextBox>
                <asp:Literal ID="lastError" runat="server"></asp:Literal>
            </p>
            <p>
                <label for="midInitial">Middle Initial:</label>
                <asp:TextBox ID="midInitial" TextMode="SingleLine" runat="server"></asp:TextBox>
                <asp:Literal ID="midError" runat="server"></asp:Literal>
            </p>
            <p>
                <label for="phoneNum">Phone Number:</label>
                <asp:TextBox ID="phoneNum" TextMode="Phone" runat="server"></asp:TextBox>
                <asp:Literal ID="phoneError" runat="server"></asp:Literal>
            </p>

            <!-- 
                The location info for the instructor 
            -->
            <p>
                <label for="buildingCode">Building Code:</label>
                <!--<asp:TextBox ID="buildingCode" TextMode="SingleLine" runat="server"></asp:TextBox> -->
                <asp:DropDownList ID="buildingCodeDrop" runat="server">
                <asp:ListItem Text="N/A" Value=""></asp:ListItem>
                <asp:ListItem Text="AATC" Value="AATC"></asp:ListItem>
                <asp:ListItem Text="AC" Value="AC"></asp:ListItem>
                <asp:ListItem Text="ACC" Value="ACC"></asp:ListItem>
                <asp:ListItem Text="ATHS" Value="ATHS"></asp:ListItem>
                <asp:ListItem Text="BTC" Value="BTC"></asp:ListItem>
                <asp:ListItem Text="BWD" Value="BWD"></asp:ListItem>
                <asp:ListItem Text="CAL" Value="CAL"></asp:ListItem>
                <asp:ListItem Text="CC" Value="CC"></asp:ListItem>
                <asp:ListItem Text="CMB" Value="CMB"></asp:ListItem>
                <asp:ListItem Text="ERC" Value="ERC"></asp:ListItem>
                <asp:ListItem Text="ESC" Value="ESC"></asp:ListItem>
                <asp:ListItem Text="ETC" Value="ETC"></asp:ListItem>
                <asp:ListItem Text="FH" Value="FH"></asp:ListItem>
                <asp:ListItem Text="GN" Value="GN"></asp:ListItem>
                <asp:ListItem Text="LEC" Value="LEC"></asp:ListItem>
                <asp:ListItem Text="LIB" Value="LIB"></asp:ListItem>
                <asp:ListItem Text="MAC" Value="MAC"></asp:ListItem>
                <asp:ListItem Text="MTC" Value="MTC"></asp:ListItem>
                <asp:ListItem Text="PAC" Value="PAC"></asp:ListItem>
                <asp:ListItem Text="SASC" Value="SASC"></asp:ListItem>
            </asp:DropDownList>
                <asp:Literal ID="buildError" runat="server"></asp:Literal>
            </p>
            <p>
                <label for="roomCode">Room Code:</label>
                <asp:TextBox ID="roomCode" TextMode="SingleLine" runat="server"></asp:TextBox>
                <asp:Literal ID="roomError" runat="server"></asp:Literal>
            </p>
            <!--
            <p>
                <label for="suiteCode">Suite Code:</label>
                <asp:TextBox ID="suiteCode" TextMode="SingleLine" runat="server"></asp:TextBox>
                <asp:Literal ID="suiteError" runat="server"></asp:Literal>
            </p>
            -->
            <p>
            <label for="suiteCodeDrop">Suite Code:</label>
            <asp:DropDownList ID="suiteCodeDrop" runat="server">
                <asp:ListItem Text="N/A" Value=""></asp:ListItem>
                <asp:ListItem Text="A" Value="A"></asp:ListItem>
                <asp:ListItem Text="B" Value="B"></asp:ListItem>
                <asp:ListItem Text="C" Value="C"></asp:ListItem>
                <asp:ListItem Text="D" Value="D"></asp:ListItem>
                <asp:ListItem Text="E" Value="E"></asp:ListItem>
                <asp:ListItem Text="F" Value="F"></asp:ListItem>
            </asp:DropDownList>
           
            </p>
            <p>
                <label id="oldPass" for="passwordCheck" runat="server">Current Password:</label>
                <asp:TextBox ID="passwordCheck" TextMode="Password" runat="server"></asp:TextBox>
          
            </p>
            <p>
                <label id="passLabel" for="passwordLogin" runat="server">New Password:</label>
                <asp:TextBox ID="passwordLogin" TextMode="Password" runat="server"></asp:TextBox>
          
            </p>
            <p>
                <label id="conLabel" for="passwordConfirm" runat="server">Password Confirmation:</label>
                <asp:TextBox ID="passwordConfirm" TextMode="Password" runat="server"></asp:TextBox>
            </p>
             <!-- 
                Buttons
            -->
            <p>
                <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click" CssClass="button" />
                <asp:Button ID="btnClear" Text="Reset" runat="server" OnClick="btnClear_Click" CssClass="button" />
                <asp:Button ID="btnClose" Text="Close" runat="server" OnClick="btnClose_Click" CssClass="button" />
            </p>
            <p>
                 <asp:Literal ID="litAlert" runat="server"></asp:Literal>
            </p>
        </div>
    </form>
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
