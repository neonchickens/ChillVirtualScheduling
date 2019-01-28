using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Upload:System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["id"] == null)
        {
            Session["loginReturn"] = "Email.aspx";
            Response.Redirect("Login.aspx");
        }

        if(!(bool)Session["advisor"])
        {
            Response.Redirect("Calendar.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //Accept user csv file
        string filename = Path.GetFileName(fileExcel.PostedFile.FileName);
        string savepath = MapPath("~/uploads") + "\\" + filename;

        //If it isn't already in our system
        if(!File.Exists(savepath) && System.IO.Path.GetExtension(fileExcel.PostedFile.FileName).Equals(".csv"))
        {
            //Save it before continuing
            fileExcel.PostedFile.SaveAs(savepath);
            litAlert.Text = "File has been saved.";

            //Open stream to read
            StreamReader sr = new StreamReader(savepath);

            //Clear header
            if(!sr.EndOfStream)
            {
                sr.ReadLine();
            }


            //Setup insert statements for csv
            string sql_statement_person = "Insert into Person (id, firstName, lastName, middleInit, email, major, verification) Values ";
            string sql_statement_advisor = "Insert into Advisor (advisorId, studentId) Values ";
            
            //Open up the database for inserting
            SqlConnection dbConnect = new SqlConnection(ConfigurationManager.ConnectionStrings["AWS_SQL"].ConnectionString);
            dbConnect.Open();
            int amt_submitted = 0;

            while(!sr.EndOfStream)
            {
                //Break csv record up and into an object
                String[] strRecord = sr.ReadLine().Split(',');
                Student s = new Student(strRecord);

                string sql_student_check = "Select count(id) s from person where id = '" + s.id + "'";
                SqlCommand student_cmd = new SqlCommand(sql_student_check, dbConnect);
                SqlDataReader student_cmd_num = student_cmd.ExecuteReader();
                student_cmd_num.Read();
                if ((int)student_cmd_num["s"] == 0)
                {        
                    //Append values to insert strings
                    sql_statement_person += "('" + s.id + "','" + s.firstname + "','" + s.lastname +
                                        "','" + s.middleinit + "','" + s.email + "','" + s.major + "','" + GenRandomString(4) + "')";
                    sql_statement_advisor += "('" + Session["id"] + "','" + s.id + "')";

                    //If it's not the last record, add a comma
                    if(!sr.EndOfStream)
                    {
                        sql_statement_person += ",";
                        sql_statement_advisor += ",";
                    }

                    amt_submitted++;
                }
                student_cmd_num.Close();
            }
            
            if (amt_submitted > 0)
            {
                //Connect and execute
                SqlCommand sql_person = new SqlCommand(sql_statement_person, dbConnect);
                sql_person.ExecuteNonQuery();
                SqlCommand sql_advisor = new SqlCommand(sql_statement_advisor, dbConnect);
                sql_advisor.ExecuteNonQuery();
            }

            //Clean up
            dbConnect.Close();
            sr.Close();

            File.Delete(savepath);
            litAlert.Text = "Success.";
        } else if (File.Exists(savepath))
        {
            litAlert.Text = "File already exists.";
        } else
        {
            litAlert.Text = "File must be in .csv format. Use example for reference.";
        }
    }
    
    private string GenRandomString(int length)
    {
        string possibilities = "ABCDEF0123456789";
        string code = "";
        Random r = new Random();
        for(int i = 0;i < length;i++)
        {
            code += possibilities[r.Next(0, possibilities.Length)];
        }

        return code;
    }

    private class Student
    {
        public String firstname, lastname, middleinit, id, major, email, netid;
        public Student(String[] args)
        {
            this.firstname = args[1];
            this.lastname = args[0];
            this.middleinit = args[2];
            this.major = args[3];
            this.id = args[4];
            this.email = args[5];
            this.netid = args[6];
        }
    }

    protected void btnExample_Click(object sender, EventArgs e)
    {
        string filePath = "C:\\inetpub\\wwwroot\\resources\\example.csv";
        FileInfo file = new FileInfo(filePath);
        if(file.Exists)
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "text/plain";
            Response.Flush();
            Response.TransmitFile(file.FullName);
            Response.End();
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("calendar.aspx");
    }
}