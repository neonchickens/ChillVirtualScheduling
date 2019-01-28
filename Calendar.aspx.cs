using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Calendar:System.Web.UI.Page
{
    SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AWS_SQL"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["id"] == null)
        {
            Session["loginReturn"] = "Calendar.aspx";
            Response.Redirect("Login.aspx");
        }
        
        litEmail.Text = (string)Session["email"];
        
        dbConnection.Open();
        string advCheck = "SELECT isAdvisor FROM person WHERE email = '" + (string)Session["email"] + "' ";
        SqlCommand advCommand = new SqlCommand(advCheck, dbConnection);
        SqlDataReader advReader = advCommand.ExecuteReader();
        while (advReader.Read())
        {
            if (!(advReader["isAdvisor"].Equals(true)))
            {
                Response.Redirect("appointment.aspx");
            }
        }
        advReader.Close();
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("login.aspx");
    }

    protected void editLink_Click(object sender, EventArgs e)
    {
        Response.Redirect("Edit.aspx");
    }

    protected void uploadBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Upload.aspx");
    }

    protected void hourBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Hours.aspx");
    }

    protected void emailBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Email.aspx");
    }
}