<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hours.aspx.cs" Inherits="Hours" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hours</title>
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
                <div class="wrapper text">
                    <h2>Hours Entry</h2>
                    <asp:Table ID="tblHoursInsert" runat="server" CssClass="text">
                        <asp:TableRow ID="tblHoursRowInsert">
                            <asp:TableCell>
                                <asp:Label runat="server" Text="Year: "></asp:Label>
                                <asp:DropDownList ID="yearDropList" runat="server"></asp:DropDownList>
                            </asp:TableCell>   
                            <asp:TableCell>
                                <asp:Label runat="server" Text="Month: "></asp:Label>
                                <asp:DropDownList ID="monthDropList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="monthDropList_SelectedIndexChanged"></asp:DropDownList>
                            </asp:TableCell> 
                        </asp:TableRow> 
                        <asp:TableRow ID="tblHoursBtn2">
                            <asp:TableCell>
                                <asp:Label runat="server" Text="Day: "></asp:Label>
                                <asp:DropDownList ID="dayDropList" runat="server"></asp:DropDownList>
                            </asp:TableCell> 
                        </asp:TableRow> 
                        <asp:TableRow ID="tblHoursBtn3">
                            <asp:TableCell>
                                <asp:Label runat="server" Text="Time Start: "></asp:Label>
                                <asp:DropDownList ID="startDropList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="startDropList_SelectedIndexChanged"></asp:DropDownList>
                            </asp:TableCell>  
                            <asp:TableCell>
                                <asp:Label runat="server" Text="Time End: "></asp:Label>
                                <asp:DropDownList ID="endDropList" runat="server"></asp:DropDownList>
                            </asp:TableCell>  
                        </asp:TableRow> 
                        <asp:TableRow ID="tblHoursBtn4">
                            <asp:TableCell>
                                <asp:Button ID="entrySubmit" runat="server" text="Submit Entry" OnClick="entrySubmit_Click" CssClass="button"/>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="entryClear" runat="server" Text="Clear Entry" OnClick="entryClear_Click" CssClass="button"/>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
            
                    <p><asp:Literal ID="lit" runat="server"></asp:Literal></p>

                    <h2>Hours Display</h2>
                    <asp:Button ID="btnSelectAll" runat="server" text="Select All" OnClick="btnSelectAll_Click" CssClass="button"/>
                    <asp:Button ID="btnDeleteSelected" runat="server" text="Delete Selected" OnClick="btnDeleteSelected_Click" CssClass="button"/>

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
                                <asp:Label runat="server" Text="Student"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" Text="Note"></asp:Label>
                            </asp:TableCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
           
                    <p><asp:Button ID="btnClose" Text="Close" runat="server" OnClick="btnClose_Click" CssClass="button" /></p> 
            
 
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
