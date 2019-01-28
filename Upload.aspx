<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload</title>
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
                <div class="wrapper">
                    <p><asp:Button ID="btnExample" runat="server" Text="Download Example" OnClick="btnExample_Click" CssClass="button"/></p>

                    <p><asp:FileUpload ID="fileExcel" runat="server" CssClass="upload" /></p>


                    <p>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="button"/>
                        <asp:Button ID="btnClose" Text="Close" runat="server" OnClick="btnClose_Click" CssClass="button" />
                    </p>

                    <p><asp:Literal ID="litAlert" runat="server" ></asp:Literal></p>
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
