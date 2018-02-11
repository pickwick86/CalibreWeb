using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calibre.Foundation;

namespace Calibre.Business.BookSelecters
{
    public class RecentlyAdded : IBookSelecter
    {
        public List<Book> SelectBooks(List<Book> allBooks, BookSelecterCriteria criteria, User user)
        {
            List<Book> result = Helper.GetBooksToRead(allBooks, user);

            result = Helper.ApplySearchCriteria(result, criteria, user);

            result.RemoveAll(x => x.DateAdded < DateTime.Now.AddMonths(-1));

            result = result.OrderBy(emp => Guid.NewGuid()).ToList();

            return result;

        }

        public string GetDescription()
        {
            return "Livre ajouté récemment";
        }

        public int GetPriority()
        {
            return 10;
        }

        public bool ForGuest()
        {
            return true;
        }
    }
}
