using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Email:System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["id"] == null)
        {
            Session["loginReturn"] = "Email.aspx";
            Response.Redirect("Login.aspx");
        }

        if(!(bool)Session["advisor"])
        {
            Response.Redirect("Calendar.aspx");
        }
    }

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AWS_SQL"].ConnectionString);
        dbConnection.Open();

        string advCheck = "select (firstname + ' ' + lastname) as fullname, email, verification from person inner join advisor on id = studentId where advisorId = '" + (int)Session["id"] + "'";
        SqlCommand advCommand = new SqlCommand(advCheck, dbConnection);
        SqlDataReader advReader = advCommand.ExecuteReader();
        string output = "<p>" + txtEmailBody.Text + "</p></ br>";
        output += "<p>Register at http://34.201.242.17 using your school email and the code.</p></ br>";
        string myoutput = "<p>" + txtEmailBody.Text + "</p></ br>";
        myoutput += "<p>Register at http://34.201.242.17 using your school email and the code.</p></ br>";
        while(advReader.Read())
        {
            //litAlert.Text += "<p>I would have messaged " + advReader["email"] + " the verification code " + advReader["verification"] + ". </p></ br>";
            string mymessage = myoutput + "<p>Verification code " + advReader["verification"] + ". </p></ br>";
            email((string)advReader["email"], mymessage);

            output += "<p>I would have messaged " + advReader["email"] + " the verification code " + advReader["verification"] + ". </p></ br>";
            //Email specific student
            //email((string)advReader["email"], "Register at http://54.87.131.79 using your school email and the code '" + (string)advReader["verification"] + "'."); Do not do this. You do want to email everyone taking this class.
        }

        //email("null", output);
        //in case that doesn't work lol

        advReader.Close();
        dbConnection.Close();


    }

    public void email(String strStudentEmail, String strMessageBody)
    {
        
        // Create email message
        string emailMessage = "";
        emailMessage += "<html>";
        /*emailMessage += " <head>";
        emailMessage += " <style>";
        emailMessage += " h1{margin: 0;}";
        emailMessage += " p{margin: 0; height: 15px;}";
        emailMessage += " </style>";
        emailMessage += " </head>";*/
        emailMessage += " <body>";
        //emailMessage += " <h1>This is an awsome email ...</h1>";
        emailMessage += strMessageBody;
        emailMessage += " <body>";
        emailMessage += "</html>";
        MailMessage message = new MailMessage();
        SmtpClient smtpClient = new SmtpClient();
        string msg = string.Empty;
        try
        {
            MailAddress fromAddress = new MailAddress("spyke.krepshaw@gmail.com");
            message.From = fromAddress;
            message.To.Add("wxl1@pct.edu"); //Additional trap to stop you from emailing anyone you shouldn't
            message.Subject = "CIT386: Advisor Scheduling";
            message.IsBodyHtml = true;
            message.Body = emailMessage;
            //message.Attachments.Add(new Attachment(Server.MapPath("uploads/Appointment2.ics")));
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential("PCTCIT386@gmail.com", "pctCIT386!");
            smtpClient.Send(message);
            litAlert.Text = "Successful";
        } catch(Exception ex)
        {
            litAlert.Text = ex.Message;
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("calendar.aspx");
    }
}