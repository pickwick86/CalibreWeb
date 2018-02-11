using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calibre.Foundation;

namespace Calibre.Business.BookSelecters
{
    public class VieuxTruc : IBookSelecter
    {
        public List<Book> SelectBooks(List<Book> allBooks, BookSelecterCriteria criteria, User user)
        {
            List<Book> booksToRead = Helper.GetBooksToRead(allBooks, user);

            var ordered = booksToRead.OrderBy(x => x.DateAdded);
            DateTime limit = ordered.ElementAt(ordered.Count() / 10).DateAdded; //keep only the 10% oldest books

            var selection = booksToRead.Where(x => x.DateAdded < limit).ToList();

            selection = Helper.ApplySearchCriteria(selection, criteria, user);

            selection = selection.OrderBy(emp => Guid.NewGuid()).ToList();

            return selection;
        }

        public string GetDescription()
        {
            return "Lire un vieux truc";
        }

        public int GetPriority()
        {
            return 10;
        }

        public bool ForGuest()
        {
            return false;
        }
    }
}
