using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Calibre.Foundation;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Calibre.Business;

namespace CalibreWeb
{
    public partial class BookUserControl : System.Web.UI.UserControl
    {

        private User CurrentUser
        {
            get { return (User)Session["User"]; }
        }

        private Book CurrentBook
        {
            get { return (Book) ViewState["CurrentBook"]; }
            set { ViewState["CurrentBook"] = value; }
        }
        
        private string DisplayList(List<string> liste, string sep)
        {
            StringBuilder sb = new StringBuilder();
            liste.ForEach(x => sb.Append(x + sep));
            sb.Remove(sb.Length - sep.Length,sep.Length);
            return sb.ToString();

        }

        public void SetBook(Book theBook)
        {
            CurrentBook = theBook;

            //titre
            mTitre.Text = theBook.Title;

            //auteurs
            mAuthors.Text = DisplayList(theBook.Authors, " & ");

            //Tags
            mTags.Text = DisplayList(theBook.Tags, ", ");

            if (!string.IsNullOrEmpty(theBook.Serie))
            {
                mSerie.Text = theBook.Serie + " [" + theBook.SerieIndex + "]";
            }
            else
            {
                mSerie.Text = string.Empty;
            }

            //Read
            

            mPath.Text = theBook.FullBookPath;
            mSummary.Text = CleanText(theBook.Summary);
            //mSummary.Text = theBook.Summary;
            mLink.NavigateUrl = "ebooks/" + theBook.URL;
            mImage.ImageUrl = "ebooks/" + theBook.CoverURL;
            mAdded.Text = theBook.DateAdded.ToString("dd MMM yyyy");
            mPages.Text = theBook.Pages.ToString();

            UserBookExperience xp = theBook.Experiences.SingleOrDefault(x => x.UserId == CurrentUser.Identifier);
            if (xp != null)
            {
                mEnvie.Text = xp.Rating.ToString();
            }
            else
            {
                mEnvie.Text = "0";
            }

            mRead.Text = theBook.HasBeenReadBy(CurrentUser) ? xp.DateRead.ToString("dd MMM yyyy") : "Non";
            mbtnMarkAsRead.Text = theBook.HasBeenReadBy(CurrentUser) ? "Marquer" + Environment.NewLine + "comme non-lu" : "Marquer" + Environment.NewLine + "comme lu";
            mbtnMarkAsRead.Visible = !CurrentUser.IsGuest;

            Dictionary<string, string> dico = (Dictionary<string, string>)HttpContext.Current.Session["DicoLanguages"];

            mLanguage.Text = dico.ContainsKey(theBook.Language) ? dico[theBook.Language] : theBook.Language;
        }

        private string CleanText(string original)
        {
            string temp = "$£ùµ*ù";
            string result = original;
            result = result.Replace("<br/>", temp);
            result = result.Replace("<br />", temp);
            result = WebUtility.HtmlDecode(Regex.Replace(result, "<[^>]*(>|$)", string.Empty));
            int linkIndex = result.IndexOf("http:");
            if (linkIndex > 0)
            {
                result = result.Substring(0, linkIndex);
            }
            result.Replace(temp, "<br />");
            return result;
        }

        protected void mbtnMarkAsRead_Click(object sender, EventArgs e)
        {
            IBookManager bm = BookManagerFactory.GetBookManager();

            UserBookExperience ube = CurrentBook.Experiences.SingleOrDefault(x => x.UserId == CurrentUser.Identifier) ?? new UserBookExperience(CurrentBook.ID, CurrentUser.Identifier);

            ube.Read = !ube.Read;
            ube.DateRead = ube.Read ? DateTime.Now : DateTime.MinValue;

            if (ube.Identifier == 0)
            {
                CurrentBook.Experiences.Add(ube);
            }

            bm.PersistUserBook(ube);
            UpdateBookInSession(CurrentBook, ube);
            SetBook(CurrentBook);
        }

        protected void mbtnEnvieMoins_Click(object sender, EventArgs e)
        {
            IBookManager bm = BookManagerFactory.GetBookManager();

            UserBookExperience ube = CurrentBook.Experiences.SingleOrDefault(x => x.UserId == CurrentUser.Identifier) ?? new UserBookExperience(CurrentBook.ID, CurrentUser.Identifier);

            ube.Rating = Math.Max(0, ube.Rating - 1);

            if (ube.Identifier == 0)
            {
                CurrentBook.Experiences.Add(ube);
            }

            bm.PersistUserBook(ube);
            UpdateBookInSession(CurrentBook, ube);
            SetBook(CurrentBook);
        }

        protected void mbtnEnviePlus_Click(object sender, EventArgs e)
        {
            IBookManager bm = BookManagerFactory.GetBookManager();

            UserBookExperience ube = CurrentBook.Experiences.SingleOrDefault(x => x.UserId == CurrentUser.Identifier) ?? new UserBookExperience(CurrentBook.ID, CurrentUser.Identifier);

            ube.Rating = Math.Min(5, ube.Rating + 1);

            if (ube.Identifier == 0)
            {
                CurrentBook.Experiences.Add(ube);
            }

            bm.PersistUserBook(ube);
            UpdateBookInSession(CurrentBook, ube);
            SetBook(CurrentBook);
        }

        private void UpdateBookInSession(Book theBook, UserBookExperience userXp)
        {
            Book theBookInSession = ((List<Book>)Session["Books"]).SingleOrDefault(x => x.ID == theBook.ID);

            theBookInSession.Experiences.RemoveAll(x => x.UserId == userXp.UserId);
            theBookInSession.Experiences.Add(userXp);
        }

    }
}