using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Hours : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["id"] == null)
        {
            Session["loginReturn"] = "Hours.aspx";
            Response.Redirect("Login.aspx");
        }

        if(!(bool)Session["advisor"])
        {
            Response.Redirect("Calendar.aspx");
        }

        if (!IsPostBack)
        {
            int currentYear = DateTime.Today.Year;

            yearDropList.Items.Add(currentYear.ToString());
            int currentMonth = DateTime.Today.Month;

            string startMon = "Select";
            monthDropList.Items.Add(startMon);
            for (int i = currentMonth; i <= 12; i++)
            {
                ListItem month = new ListItem();
                month.Text = i.ToString();
                monthDropList.Items.Add(month);
            }

            string startDay = "Select Month";
            dayDropList.Items.Add(startDay);
            

            string startstr = "Start Time";
            startDropList.Items.Add(startstr);

            DateTime dt = Convert.ToDateTime("08:00"); //+for adding start time
            for (int i = 0; i <= 47; i++) //Set up every 30 minute interval
            {
                ListItem start = new ListItem(dt.ToShortTimeString(), dt.ToShortTimeString());
                start.Selected = false;
                startDropList.Items.Add(start);
                dt = dt.AddMinutes(15);
            }
            
            string endstr = "Select Start";
            endDropList.Items.Add(endstr);
        }
    }

    private class DisplayRow : TableRow
    {
        private int idHours;
        private int idStudent;

        private CheckBox cbSelect;

        public DisplayRow(int idHours, DateTime datetime, int idStudent, string nameStudent, string note)
        {
            this.idHours = idHours;
            cbSelect = new CheckBox();
            Label lblDate = new Label();
            lblDate.Text = datetime.ToShortDateString();
            Label lblTime = new Label();
            lblTime.Text = datetime.ToShortTimeString();

            Label lblStudent = new Label();
            lblStudent.Text = nameStudent;
            Label lblNote = new Label();
            lblNote.Text = note;

            TableCell c1 = new TableCell();
            c1.Controls.Add(cbSelect);
            TableCell c2 = new TableCell();
            c2.Controls.Add(lblDate);
            TableCell c3 = new TableCell();
            c3.Controls.Add(lblTime);
            TableCell c4 = new TableCell();
            c4.Controls.Add(lblStudent);
            TableCell c5 = new TableCell();
            c5.Controls.Add(lblNote);

            Cells.Add(c1);
            Cells.Add(c2);
            Cells.Add(c3);
            Cells.Add(c4);
            Cells.Add(c5);
        }

        public DisplayRow(int idHours, DateTime datetime)
        {
            this.idHours = idHours;
            cbSelect = new CheckBox();
            Label lblDate = new Label();
            lblDate.Text = datetime.ToShortDateString();
            Label lblTime = new Label();
            lblTime.Text = datetime.ToShortTimeString();

            Label lblStudent = new Label();
            lblStudent.Text = "None";
            Label lblNote = new Label();
            lblNote.Text = "None";

            TableCell c1 = new TableCell();
            c1.Controls.Add(cbSelect);
            TableCell c2 = new TableCell();
            c2.Controls.Add(lblDate);
            TableCell c3 = new TableCell();
            c3.Controls.Add(lblTime);
            TableCell c4 = new TableCell();
            c4.Controls.Add(lblStudent);
            TableCell c5 = new TableCell();
            c5.Controls.Add(lblNote);

            Cells.Add(c1);
            Cells.Add(c2);
            Cells.Add(c3);
            Cells.Add(c4);
            Cells.Add(c5);
        }

        public void select(bool val)
        {
            cbSelect.Checked = val;
        }

        public bool isSelected()
        {
            return cbSelect.Checked;
        }

        public int getId()
        {
            return idHours;
        }
    }

    protected void tblHours_Load(object sender, EventArgs e)
    {
        SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AWS_SQL"].ConnectionString);
        dbConnection.Open();

        string advCheck = "select officeHoursId, officeId, startTime, studentId, (p.firstName + ' ' + p.lastName) as fullname, note from officehours oh full join person p on oh.studentId = p.id where oh.advisorId = '" + (int)Session["id"] + "' order by startTime";
        SqlCommand advCommand = new SqlCommand(advCheck, dbConnection);
        SqlDataReader advReader = advCommand.ExecuteReader();
        while(advReader.Read())
        {
            if (!(advReader["fullname"] is DBNull))
            {
                tblHours.Rows.Add(new DisplayRow((int)advReader["officeHoursId"], (DateTime)advReader["startTime"], (int)advReader["studentId"], (string)advReader["fullname"], (string)advReader["note"]));
            } else
            {
                tblHours.Rows.Add(new DisplayRow((int)advReader["officeHoursId"], (DateTime)advReader["startTime"]));

            }
        }

        advReader.Close();
        dbConnection.Close();
    }

    protected void monthDropList_SelectedIndexChanged(object sender, EventArgs e)
    {

        int currentDay;
        int currentMonth = DateTime.Today.Month;
        dayDropList.Items.Clear();
        if (monthDropList.SelectedValue.Equals("1") ||
            monthDropList.SelectedValue.Equals("3") ||
            monthDropList.SelectedValue.Equals("5") ||
            monthDropList.SelectedValue.Equals("7") ||
            monthDropList.SelectedValue.Equals("8") ||
            monthDropList.SelectedValue.Equals("10") ||
            monthDropList.SelectedValue.Equals("12"))
        {
            if(monthDropList.SelectedValue.Equals(currentMonth.ToString()))
            {
                currentDay = DateTime.Today.Day;  
            }
            else {
                currentDay = 1;
            }
                
            for (int i = currentDay; i <= 31; i++)
            {
                ListItem day = new ListItem();
                day.Text = i.ToString();
                dayDropList.Items.Add(day);
            }
        }
        if (monthDropList.SelectedValue.Equals("4") ||
            monthDropList.SelectedValue.Equals("6") ||
            monthDropList.SelectedValue.Equals("9") ||
            monthDropList.SelectedValue.Equals("11"))
        {
            if (monthDropList.SelectedValue.Equals(currentMonth.ToString()))
            {
                currentDay = DateTime.Today.Day;
            }
            else
            {
                currentDay = 1;
            }
            for (int i = currentDay; i <= 30; i++)
            {
                ListItem day = new ListItem();
                day.Text = i.ToString();
                dayDropList.Items.Add(day);
            }
        }
        if (monthDropList.SelectedValue.Equals("2"))
        {
            if (monthDropList.SelectedValue.Equals(currentMonth.ToString()))
            {
                currentDay = DateTime.Today.Day;
            }
            else
            {
                currentDay = 1;
            }
            for (int i = currentDay; i <= 28; i++)
            {
                ListItem day = new ListItem();
                day.Text = i.ToString();
                dayDropList.Items.Add(day);
            }
        }
    }

    protected void startDropList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (startDropList.SelectedIndex.Equals(0))
        {

        }
        else
        {
            endDropList.Items.Clear();
            DateTime dt2 = Convert.ToDateTime(startDropList.SelectedItem.Text); //for adding start time
            DateTime dt3 = Convert.ToDateTime("20:00");
            double hours = (dt3 - dt2).TotalHours;
            double j = hours * 4;
            dt2 = dt2.AddMinutes(15);
            for (int i = 0; i <= j - 1; i++) //Set up every 30 minute interval
            {
                ListItem end = new ListItem(dt2.ToShortTimeString(), dt2.ToShortTimeString());
                end.Selected = false;
                dt2 = dt2.AddMinutes(15);
                endDropList.Items.Add(end);
            }
        }
    }

    protected void btnSelectAll_Click(object sender, EventArgs e)
    {
        bool blnChanged = false;
        foreach (TableRow tr in tblHours.Rows)
        {
            if (tr is DisplayRow)
            {
                DisplayRow dr = (DisplayRow)tr;
                if (!dr.isSelected()) {
                    dr.select(true);
                    blnChanged = true;
                }
            }
        }
        if (!blnChanged)
        {
            foreach(TableRow tr in tblHours.Rows)
            {
                if(tr is DisplayRow)
                {
                    DisplayRow dr = (DisplayRow)tr;
                    dr.select(false);
                }
            }
        }

    }

    protected void btnDeleteSelected_Click(object sender, EventArgs e)
    {
        SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AWS_SQL"].ConnectionString);
        dbConnection.Open();
        
        foreach(TableRow tr in tblHours.Rows)
        {
            if(tr is DisplayRow)
            {
                DisplayRow dr = (DisplayRow)tr;
                if(dr.isSelected())
                {
                    string delete = "DELETE officeHours WHERE officeHoursId = '" + dr.getId() + "'";
                    SqlCommand deleteHours = new SqlCommand(delete, dbConnection);
                    deleteHours.ExecuteNonQuery();
                }
            }
        }
  
        dbConnection.Close();
        Response.Redirect("hours.aspx");
    }

    protected void entrySubmit_Click(object sender, EventArgs e)
    {
        /*      Working on the Insert entry
        string update = "UPDATE person SET firstName = '" + firstName.Text + "'" + ", lastName = '" + lastName.Text + "'" + ", middleinit = '" + midInitial.Text + "'" + ", phonenumber = '" + phoneNum.Text + "'" + " WHERE email = '" + (string)Session["email"] + "' ";
        SqlCommand updatePerson = new SqlCommand(update, dbConnection);
        updatePerson.ExecuteNonQuery()
        */
        SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AWS_SQL"].ConnectionString);
        dbConnection.Open();

        string selectOfficeID = "select officeId from office where advisorId ='" + (int)Session["id"] + "'";
        SqlCommand selectID = new SqlCommand(selectOfficeID, dbConnection);
        SqlDataReader advReader = selectID.ExecuteReader();
        advReader.Read();
        int office = (int)advReader["officeId"];
        advReader.Close();

        DateTime startTime = Convert.ToDateTime(startDropList.SelectedItem.Text);
        DateTime endTime = Convert.ToDateTime(endDropList.SelectedItem.Text);
        TimeSpan differenceTime = endTime.Subtract(startTime);
        Double minTime = differenceTime.TotalMinutes;
        Double fifteenTime = minTime / 15;
        
        string time = startTime.ToString("HH:mm");
        string[] timeArray = time.Split(':');
        int hour = Convert.ToInt16(timeArray[0]);
        int min = Convert.ToInt16(timeArray[1]);

        DateTime finalDate = new DateTime(Convert.ToInt16(yearDropList.SelectedItem.Text), Convert.ToInt16(monthDropList.SelectedItem.Text), Convert.ToInt16(dayDropList.SelectedItem.Text), hour, min, 00);

        ArrayList times = new ArrayList();
        for(int i = 0; i <= fifteenTime; i++)
        {
            times.Add(finalDate.ToString());
            finalDate = finalDate.AddMinutes(15);
        }

        string sql = "insert into officehours values ";
        int intInserted = 0;

        for(int i = 0; i < times.Count; i++)
        {
            string strDup = "select count(officehoursid) as dup from officehours where startTime = '" + (string)times[i] + "' and advisorid = '" + Session["id"] + "'";
            SqlCommand selectDup = new SqlCommand(strDup, dbConnection);
            advReader = selectDup.ExecuteReader();
            advReader.Read();
            int intDup = (int)advReader["dup"];
            advReader.Close();
            if(intDup != 0)
                continue;

            intInserted++;
            sql += "('" + office + "', '" + times[i] + "', NULL, '" + Session["id"] + "', NULL)";
            if(!(i == times.Count - 1))
            {
                sql += ",";
            } 
        }
         if (intInserted > 0)
        {
            //lit.Text = sql;
            lit.Text = "Submitted: " + intInserted + " Records";
            SqlCommand insertTimes = new SqlCommand(sql, dbConnection);
            insertTimes.ExecuteNonQuery();
        }
        dbConnection.Close();
    }

    protected void entryClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("hours.aspx");
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("calendar.aspx");
    }
}