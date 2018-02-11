using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Calibre.Foundation;

namespace CalibreWeb
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                List<User> users = (List<User>)Session["Users"];

                User theUser = users.SingleOrDefault(x => x.Name == mtxtLogin.Text && x.Password == mtxtPassword.Text);

                if (theUser != null)
                {
                    Session["User"] = theUser;
                    Response.Redirect("search.aspx");
                }
            }
        }
    }
}