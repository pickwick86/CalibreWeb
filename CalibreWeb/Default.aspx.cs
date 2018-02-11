using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Calibre.Foundation;
using Calibre.Business;
using Calibre.Business.BookSelecters;

namespace CalibreWeb
{
    public partial class Default2 : System.Web.UI.Page
    {
        private int BookIndex
        {
            get { return (int)ViewState["BookIndex"]; }
            set { ViewState["BookIndex"] = value; }
        }

        private List<Book> SelectedBooks
        {
            get { return (List<Book>) Session["SelectedBooks"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (((User)Session["User"]).Identifier == 0)
            {
                Response.Redirect("login.aspx");
            }

            if (!IsPostBack)
            {
                BookIndex = 0;
                Display();
            }
            

            
        }

        private void Display()
        {
            mlblSelecter.Text = ((IBookSelecter)Session["Selecter"]).GetDescription();
            mlblIndex.Text = (BookIndex + 1) + " / " + SelectedBooks.Count;
            mbtnPrevious.Enabled = BookIndex != 0;
            mbtnNext.Enabled = BookIndex != SelectedBooks.Count - 1;
            mUserControl.SetBook(SelectedBooks[BookIndex]);
        }
                
        protected void mbtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("search.aspx");
        }

        protected void mbtnPrevious_Click(object sender, EventArgs e)
        {
            BookIndex--;
            Display();
        }

        protected void mbtnNext_Click(object sender, EventArgs e)
        {
            BookIndex++;
            Display();
        }
    }
}