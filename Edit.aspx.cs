using System;

using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;


public partial class Edit : System.Web.UI.Page

{


    SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AWS_SQL"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["id"] == null)
        {
            Session["loginReturn"] = "Edit.aspx";
            Response.Redirect("Login.aspx");
        }

        if(!(bool)Session["advisor"])
        {
            Response.Redirect("Calendar.aspx");
        }

        firstError.Text = "";
        lastError.Text = "";
        midError.Text = "";
        phoneError.Text = "";
        buildError.Text = "";
        roomError.Text = "";
        suiteError.Text = "";
        

        dbConnection.Open();
        string advCheck = "SELECT isAdvisor FROM person WHERE email = '" + (string)Session["email"] + "' ";
        SqlCommand advCommand = new SqlCommand(advCheck, dbConnection);
        SqlDataReader advReader = advCommand.ExecuteReader();
        while (advReader.Read())
        {
            if (!(advReader["isAdvisor"].Equals(true)))
            {
                Response.Redirect("Calendar.aspx");

            }
            else
            {
                litEmail.Text = (string)Session["email"];
            }
        }
        advReader.Close();
        if (!Page.IsPostBack)
        {

            Session["storedPass"] = "";

            /* officehours.officehoursid, OfficeHours.startTime, OfficeHours.endTime */
            /* INNER JOIN OfficeHours ON office.officeId = OfficeHours.officeId */
            string select = "SELECT person.firstName, person.lastName, person.middleinit, person.phonenumber, office.officeid, office.buildingid, office.roomid, office.suiteid FROM person INNER JOIN office ON person.id = office.advisorId WHERE email = '" + (string)Session["email"] + "'";
            SqlCommand selectPerson = new SqlCommand(select, dbConnection);
            SqlDataReader personReader = selectPerson.ExecuteReader();
           
            while (personReader.Read())
            {
                Session["officeid"] = personReader["officeid"];
                // Session["officehoursid"] = personReader["officehoursid"];
                if (!(personReader["firstName"] == DBNull.Value))
                    firstName.Text = (string)personReader["firstName"];
                else
                    firstName.Text = "No Data";

                if (!(personReader["lastName"] == DBNull.Value))
                    lastName.Text = (string)personReader["lastName"];
                else
                    lastName.Text = "No Data";

                if (!(personReader["middleinit"] == DBNull.Value))
                    midInitial.Text = (string)personReader["middleinit"];
                else
                    midInitial.Text = "No Data";

                if (!(personReader["phonenumber"] == DBNull.Value))
                    phoneNum.Text = (string)personReader["phonenumber"];
                else
                    phoneNum.Text = "No Data";

                if (!(personReader["buildingid"] == DBNull.Value))
                    buildingCodeDrop.SelectedValue = personReader["buildingid"].ToString();
                else
                    buildingCodeDrop.SelectedValue = "N/A";

                if (!(personReader["roomid"] == DBNull.Value))
                    roomCode.Text = (string)personReader["roomid"];
                else
                    roomCode.Text = "No Data";
          
                if (!(personReader["suiteid"] == DBNull.Value))
                    suiteCodeDrop.SelectedValue = personReader["suiteid"].ToString();
                else
                    suiteCodeDrop.SelectedValue = "N/A";
        }

        personReader.Close();
        }
        dbConnection.Close();
        
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        if(passwordCheck.Text.Length > 0){
            dbConnection.Open();

            var oldPass = passwordCheck.Text;
            var oldHash = computeHash(oldPass, new SHA512Managed());
            string selectPassword = "SELECT password FROM person WHERE email='" + Session["email"] + "'";
            SqlCommand selectPasswordCMD = new SqlCommand(selectPassword, dbConnection);
            SqlDataReader passwordReader = selectPasswordCMD.ExecuteReader();
            while (passwordReader.Read())
            {
                //emailLogin.Text.Equals(personReader["email"].ToString()
                if (oldHash.Equals(passwordReader["password"].ToString())){
                   
                    if (passwordLogin.Text.Length > 6 && passwordConfirm.Text.Equals(passwordLogin.Text))
                    {

                        var pass = passwordLogin.Text;
                        var hashPass = computeHash(pass, new SHA512Managed());
                        string insertPassword = "UPDATE Person set password = '" + hashPass + "' WHERE email='" + Session["email"] + "'";
                        SqlCommand insertPasswordCMD = new SqlCommand(insertPassword, dbConnection);
                        passwordReader.Close();
                        insertPasswordCMD.ExecuteNonQuery();


                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert", "alert('Password update sucessful, redirecting');" +
                        "window.location ='calendar.aspx';",
                        true);
                        break;

                    }
                    else
                    {
                        litAlert.Text = "Password do not match or do not meet requirements: more than 6 characters";
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert", "alert('Password do not match or do not meet requirements: more than 6 characters');" +
                        "window.location ='edit.aspx';",
                        true);
                    }
                }
                else
                {
                    litAlert.Text = "Password does not match stored password, contact admin.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert", "alert('Password does not match stored password, contact admin.');" +
                    "window.location ='edit.aspx';",
                    true);
                }
            }
            
            dbConnection.Close();
        }
        

        bool hasError = false;
        long phoneNumParsed;
        if(phoneNum.Text.Length != 10 || !long.TryParse(phoneNum.Text, out phoneNumParsed))
        {
            phoneError.Text = "<span style='color:red;font-size:15px'>Wrong phone format";
            hasError = true;
        }
        if (firstName.Text.Length < 1 || firstName.Text.Length > 32 )
        {
            firstError.Text = "<span style='color:red;font-size:15px'>Wrong name format";
            hasError = true;
        }
        if (lastName.Text.Length < 1 || lastName.Text.Length > 32)
        {
            lastError.Text = "<span style='color:red;font-size:15px'>Wrong name format";
            hasError = true;
        }
        if (midInitial.Text.Length != 1)
        {
            midError.Text = "<span style='color:red;font-size:15px'>One letter for the initial";
            hasError = true;
        }
        if (roomCode.Text.Length < 1)
        {
            roomError.Text = "<span style='color:red;font-size:15px'>Wrong room code format";
            hasError = true;
        }
        if (hasError)
            return;

        dbConnection.Open();

        string update = "UPDATE person SET firstName = '" + firstName.Text + "'" + ", lastName = '" + lastName.Text + "'" + ", middleinit = '" + midInitial.Text + "'" + ", phonenumber = '" + phoneNum.Text + "'" + " WHERE email = '" + (string)Session["email"] + "' ";
        SqlCommand updatePerson = new SqlCommand(update, dbConnection);
        updatePerson.ExecuteNonQuery();

        update = "UPDATE office SET buildingid = '" + buildingCodeDrop.Text + "'" + ", roomid = '" + roomCode.Text + "'" + ", suiteid = '" + suiteCodeDrop.Text + "'" + " WHERE officeid = '" + Session["officeid"] + "' ";
        SqlCommand updateRoom = new SqlCommand(update, dbConnection);
        updateRoom.ExecuteNonQuery();
       
        dbConnection.Close();
        ScriptManager.RegisterStartupScript(this, this.GetType(),
        "alert","alert('Update sucessful, redirecting');" +
        "window.location ='calendar.aspx';",
        true);

    }
        

    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("edit.aspx");
    }



    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("calendar.aspx");
    }

    private static string computeHash(string input, SHA512Managed sHA512Managed)
    {
        if (input == null) { throw new ArgumentNullException("input"); }
        if (sHA512Managed == null) { throw new ArgumentNullException("hashprovider"); }

        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = sHA512Managed.ComputeHash(inputBytes);
        var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

        return hash;
    }
}