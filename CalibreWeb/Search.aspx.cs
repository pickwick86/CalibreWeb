using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Calibre.Foundation;

namespace CalibreWeb
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (((User)Session["User"]).Identifier == 0)
            {
                Response.Redirect("login.aspx");
            }

            if (!IsPostBack)
            {
                mTagsDropDownList.DataBind();
                mSelectersDropDownList.DataBind();
                mLanguagesDropDownList.DataBind();
                mtxtRating.Text = "0";
                mtxtMaxPages.Text = "0";
                mtxtMinPages.Text = "0";
            }
        }

        protected void mButton_Click(object sender, EventArgs e)
        {
            IBookSelecter selecter = mSelectersDropDownList.SelectedSelecter;
            Session["Selecter"] = selecter;
            List<Book> allBooks = (List<Book>)Session["Books"];
            User user = (User)Session["User"];
            int rating = 0;
            int.TryParse(mtxtRating.Text, out rating);
            int minPages = 0;
            int.TryParse(mtxtMinPages.Text, out minPages);
            int maxPages = 0;
            int.TryParse(mtxtMaxPages.Text, out maxPages);

            var criteria = new BookSelecterCriteria()
            {
                Tag = mTagsDropDownList.SelectedValue,
                Language = mLanguagesDropDownList.SelectedValue,
                AuthorLike = mtxtAuthor.Text,
                SerieLike = mtxtSerie.Text,
                TitleLike = mtxtTitle.Text,
                EnvieMin = rating,
                MinPages = minPages,
                MaxPages = maxPages
            };
            List<Book> myBooks = selecter.SelectBooks(allBooks, criteria, user);

            Session["SelectedBooks"] = myBooks;
            if (myBooks.Any())
            {
                Response.Redirect("~/");
            }
            else
            {
                mlblMessage.Text = "Aucun résultat";
            }
        }

        protected void mTagsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void mLanguagesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void mSelectersDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}