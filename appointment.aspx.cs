using System;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class appointment:System.Web.UI.Page
{
    private static int blocks_reserved = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["id"] == null)
        {
            Session["loginReturn"] = "appointment.aspx";
            Response.Redirect("Login.aspx");
        }

        if((bool)Session["advisor"])
        {
            Response.Redirect("Hours.aspx");
        }
    }

    protected void tblHours_Load(object sender, EventArgs e)
    {
        SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AWS_SQL"].ConnectionString);
        dbConnection.Open();

        string advCheck = "select oh.officeHoursId, (o.buildingId + ' ' + o.roomId + o.suiteId) as office, oh.startTime " +
                          "from OfficeHours oh " +
                          "join Advisor a on oh.advisorId = a.advisorId " +
                          "join Office o on oh.officeId = o.officeId " +
                          "where oh.studentId is NULL " +
                          "and a.studentId = '" + (int)Session["id"] + "'";
        SqlCommand advCommand = new SqlCommand(advCheck, dbConnection);
        SqlDataReader advReader = advCommand.ExecuteReader();
        while(advReader.Read())
        {
            DisplayRow dr = new DisplayRow((int)advReader["officeHoursId"], (DateTime)advReader["startTime"], (string)advReader["office"]);
            tblHours.Rows.Add(dr);
        }

        advReader.Close();
        dbConnection.Close();
    }

    private class DisplayRow : TableRow
    {
        private int idHours;
        private CheckBox chkSelect;
        private DateTime dt;
        public string office;

        public DisplayRow(int idHours, DateTime datetime, string office)
        {
            this.idHours = idHours;
            chkSelect = new CheckBox();
            dt = datetime;
            this.office = office;
            Label lblDate = new Label();
            lblDate.Text = datetime.ToShortDateString();
            Label lblTime = new Label();
            lblTime.Text = datetime.ToShortTimeString();
            Label lblOffice = new Label();
            lblOffice.Text = office;

            TableCell c1 = new TableCell();
            c1.Controls.Add(chkSelect);
            TableCell c2 = new TableCell();
            c2.Controls.Add(lblDate);
            TableCell c3 = new TableCell();
            c3.Controls.Add(lblTime);
            TableCell c4 = new TableCell();
            c4.Controls.Add(lblOffice);

            Cells.Add(c1);
            Cells.Add(c2);
            Cells.Add(c3);
            Cells.Add(c4);
        }

        public void select(bool val)
        {
            chkSelect.Checked = val;
        }

        public bool isSelected()
        {
            return chkSelect.Checked;
        }

        public int getId()
        {
            return idHours;
        }

        public DateTime getDate()
        {
            return dt;
        }
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string strOfficeHoursSelected = "";
        TableRow[] trRows = new TableRow[tblHours.Rows.Count];
        tblHours.Rows.CopyTo(trRows, 0);
        int counter = 0;
        string office = "";
        ArrayList lstDT = new ArrayList();
        foreach (TableRow tr in trRows)
        {
            litAlert.Text += 1;
            litAlert.Text += "Rows: " + tblHours.Rows.Count;
            if(tr is DisplayRow)
            {
                litAlert.Text += 2;
                DisplayRow dr = (DisplayRow)tr;
                if(dr.isSelected())
                {
                    counter += 1;
                    lstDT.Add(dr.getDate());
                    office = dr.office;
                    strOfficeHoursSelected += ",'" + dr.getId().ToString() + "'";
                }
            }
        }
        
        if (strOfficeHoursSelected.Length > 0 && blocks_reserved + counter < 3)
        {
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AWS_SQL"].ConnectionString);
            dbConnection.Open();

            string advCheck = "update officehours " +
                              "set studentId = '" + (int)Session["id"] + "', " +
                              "note = '" + txtNote.Text + "' " + 
                              "where officehoursid in (" + strOfficeHoursSelected.Substring(1) + ")";
            SqlCommand updateHours = new SqlCommand(advCheck, dbConnection);
            updateHours.ExecuteNonQuery();
            
            dbConnection.Close();
            litAlert.Text = advCheck;
            email((string)Session["email"], "You have reserved an advising apointment!", office, lstDT.ToArray());
            Response.Redirect("appointment.aspx");
        } else
        {
            litAlert.Text = "Please select up to two time slots for advising. You have " + (2 - blocks_reserved).ToString() + " left. ";
            if (blocks_reserved > 0)
            {
                litAlert.Text += "If you want to reserve a different time slot from what you've already selected, please remove those times from the first table. ";
            }
        }

    }
    
    protected void tblHoursSelected_Load(object sender, EventArgs e)
    {
        blocks_reserved = 0;
        SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AWS_SQL"].ConnectionString);
        dbConnection.Open();

        string advCheck = "select oh.officeHoursId, (o.buildingId + ' ' + o.roomId + o.suiteId) as office, oh.startTime " +
                          "from OfficeHours oh " +
                          "join Advisor a on oh.advisorId = a.advisorId " +
                          "join Office o on oh.officeId = o.officeId " +
                          "where oh.studentId = '" + (int)Session["id"] + "' " +
                          "and a.studentId = '" + (int)Session["id"] + "'";
        SqlCommand advCommand = new SqlCommand(advCheck, dbConnection);
        SqlDataReader advReader = advCommand.ExecuteReader();
        while(advReader.Read())
        {
            DisplayRow dr = new DisplayRow((int)advReader["officeHoursId"], (DateTime)advReader["startTime"], (string)advReader["office"]);
            tblHoursSelected.Rows.Add(dr);
            blocks_reserved += 1;
        }

        advReader.Close();
        dbConnection.Close();
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("login.aspx");
    }

    protected void btnRemoveSelected_Click(object sender, EventArgs e)
    {
        string strOfficeHoursSelected = "";
        TableRow[] trRows = new TableRow[tblHoursSelected.Rows.Count];
        tblHoursSelected.Rows.CopyTo(trRows, 0);

        foreach(TableRow tr in trRows)
        {
            if(tr is DisplayRow)
            {
                DisplayRow dr = (DisplayRow)tr;
                if(dr.isSelected())
                {
                    strOfficeHoursSelected += ",'" + dr.getId() + "'";
                }
            }
        }

        if(strOfficeHoursSelected.Length > 0)
        {
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AWS_SQL"].ConnectionString);
            dbConnection.Open();

            string advCheck = "update officehours " +
                              "set studentId = NULL, " +
                              "note = NULL " +
                              "where officehoursid in (" + strOfficeHoursSelected.Substring(1) + ")";
            SqlCommand updateHours = new SqlCommand(advCheck, dbConnection);
            updateHours.ExecuteNonQuery();

            dbConnection.Close();
            Response.Redirect("appointment.aspx");
        }
    }


    public void email(String strStudentEmail, String strMessageBody, string location, object[] dt)
    {

        // Create the email appointment/attachment
        ArrayList lstFile = new ArrayList();
        int counter = 0;
        foreach (Object block in dt)
        {
            if (block is DateTime)
            {
                DateTime dtBlock = (DateTime)block;
                String[] contents = {
                "BEGIN:VCALENDAR",
                    "PRODID:-//Flo Inc.//FloSoft//EN",
                    "BEGIN:VEVENT",
                        "DTSTART:" + dtBlock.AddHours(5).ToString("yyyyMMdd\\THHmmss\\Z"),
                        "DTEND:" + dtBlock.AddHours(5).AddMinutes(15).ToString("yyyyMMdd\\THHmmss\\Z"),
                        "LOCATION: " + location,
                        "DESCRIPTION;ENCODING=QUOTED-PRINTABLE: " + strMessageBody,
                        "SUMMARY: Scheduling",
                        "PRIORITY:3",
                    "END:VEVENT",
                "END:VCALENDAR"
                };
                counter += 1;
                String path = MapPath("~/uploads") + "\\" + "appt" + counter + ".ics";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                StreamWriter swICS = new StreamWriter(path, true);
                foreach (String s in contents)
                {
                    swICS.WriteLine(s); // Write the file.
                }
                swICS.Close(); // Close the instance of StreamWriter.
                swICS.Dispose(); // Dispose from memory.  
                lstFile.Add(path);
            }
        }

        // Create email message
        string emailMessage = "";
        emailMessage += "<html>";
        emailMessage += " <body>";
        emailMessage += " <p>" + strMessageBody + "</p>";
        emailMessage += " <body>";
        emailMessage += "</html>";
        MailMessage message = new MailMessage();
        SmtpClient smtpClient = new SmtpClient();
        string msg = string.Empty;
        try
        {
            MailAddress fromAddress = new MailAddress("spyke.krepshaw@gmail.com");
            message.From = fromAddress;
            message.To.Add(strStudentEmail); //Additional trap to stop you from emailing anyone you shouldn't
            message.Subject = "CIT386: Advisor Scheduling";
            message.IsBodyHtml = true;
            message.Body = emailMessage;
            foreach (string f in lstFile)
            {
                message.Attachments.Add(new Attachment(f));
            }
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

        smtpClient.Dispose();
        message.Dispose();

        foreach (String f in lstFile)
        {
            File.Delete(f);
        }
    }

}