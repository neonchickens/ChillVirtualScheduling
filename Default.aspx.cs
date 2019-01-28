using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AWS_SQL"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("calendar.aspx", false);
    }

}