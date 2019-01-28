using System;
using System.Collections.Generic;
using System.Configuration;

using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Data;

public partial class Login : System.Web.UI.Page
{
    SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AWS_SQL"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //litAlert.Text = "Welcome";
            passwordLogin.Visible = false;
            passwordConfirm.Visible = false;
            passLabel.Visible = false;
            conLabel.Visible = false;
            verifyLabel.Visible = false;
            verifyText.Visible = false;

            Session["existsEmail"] = false;
            Session["existsPassword"] = false;
            Session["storedPass"] = "";
            Session["verify"] = "";
            Session["email"] = "";
            Session["valid"] = false;
            Session["advisor"] = false;
        }
        
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        dbConnection.Open();

        if ((bool)Session["existsEmail"] && !(bool)Session["existsPassword"])
        {
            if (verifyText.Text.Equals(Session["verify"].ToString()))
            {
                if(passwordLogin.Text.Length > 6 && passwordConfirm.Text.Equals(passwordLogin.Text))
                {
                    var pass = passwordLogin.Text;
                    var hashPass = computeHash(pass, new SHA512Managed());
                    string insertPassword = "UPDATE Person set password = '" + hashPass + "' WHERE email='" + Session["email"] + "'";
                    SqlCommand insertPasswordCMD = new SqlCommand(insertPassword, dbConnection);
                    insertPasswordCMD.ExecuteNonQuery();
                    passwordConfirm.Visible = false;
                    conLabel.Visible = false;
                    Response.Redirect("login.aspx");
                } else
                {
                    litAlert.Text = "Password do not match or do not meet requirements: more than 6 characters";
                }
            } else
            {
                litAlert.Text = "Verification code is wrong.";
            }

        }
        else if ((bool)Session["existsEmail"] && (bool)Session["existsPassword"])
        {
            var pass = passwordLogin.Text;
            var hashPass = computeHash(pass, new SHA512Managed());
            if(hashPass.Equals(Session["storedPass"])){
                Session["valid"] = true;
                if(Session["loginReturn"] != null)
                {
                    string address = (string)Session["loginReturn"];
                    Session["loginReturn"] = null;
                    Response.Redirect(address);
                }
                Response.Redirect("Calendar.aspx");
            }
            else{
                litAlert.Text = "Login Failed";
            }           
        }
        else
        {
            string select = "SELECT * FROM Person";
            SqlCommand selectPerson = new SqlCommand(select, dbConnection);
            SqlDataReader personReader = selectPerson.ExecuteReader();
            while (personReader.Read() && !(bool)Session["existsEmail"]){
                if (emailLogin.Text.Equals(personReader["email"].ToString())){
                    if (personReader["password"] == DBNull.Value){
                        passwordConfirm.Visible = true;
                        passwordLogin.Visible = true;
                        verifyLabel.Visible = true;
                        verifyText.Visible = true;
                        conLabel.Visible = true;
                        passLabel.Visible = true;
                        Session["existsEmail"] = true;
                        Session["verify"] = personReader["verification"].ToString();
                        Session["existsPassword"] = false;
                        Session["email"] = emailLogin.Text;
                        Session["advisor"] = (bool)personReader["isAdvisor"];
                        Session["id"] = personReader["id"];
                    }
                    else if (!(personReader["password"] == DBNull.Value)){
                        passwordLogin.Visible = true;
                        passwordConfirm.Visible = false;
                        passLabel.Visible = true;
                        conLabel.Visible = false;
                        Session["existsEmail"] = true;
                        Session["existsPassword"] = true;
                        Session["email"] = emailLogin.Text;
                        Session["storedPass"] = personReader["password"].ToString();
                        Session["verify"] = personReader["verification"].ToString();
                        Session["advisor"] = (bool)personReader["isAdvisor"];
                        Session["id"] = personReader["id"];
                    }
                }
            }
            personReader.Close();
        }
        dbConnection.Close();
    }

    private static string computeHash(string input, SHA512Managed sHA512Managed)
    {
        if(input == null) { throw new ArgumentNullException("input"); }
        if(sHA512Managed == null) { throw new ArgumentNullException("hashprovider"); }

        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = sHA512Managed.ComputeHash(inputBytes);
        var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

        return hash;
    }

    /*
    private void edit() {

        dbConnection.Open();

        SqlCommand insertCommand = new SqlCommand("insertPerson", dbConnection);
        insertCommand.CommandType = CommandType.StoredProcedure;
        insertCommand.Parameters.Add("@firstname", SqlDbType.VarChar).Value = txtFirstName.Text;
        insertCommand.Parameters.Add("@lastname", SqlDbType.VarChar).Value = txtLastName.Text;
        insertCommand.Parameters.Add("@personID", SqlDbType.Int);

        insertCommand.Parameters["@personID"].Direction = ParameterDirection.Output;

        insertCommand.ExecuteNonQuery();

        int personID = (int)insertCommand.Parameters["@personID"].Value;

       dbConnection.Close();

    }
    */
    
}