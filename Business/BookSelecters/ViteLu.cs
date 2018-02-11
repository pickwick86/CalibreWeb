using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calibre.Foundation;

namespace Calibre.Business.BookSelecters
{
    public class ViteLu : IBookSelecter
    {
        public List<Book> SelectBooks(List<Book> allBooks, BookSelecterCriteria criteria, User user)
        {
            List<Book> booksToRead = Helper.GetBooksToRead(allBooks, user);

            booksToRead.RemoveAll(x => x.Pages >= 100);

            booksToRead = Helper.ApplySearchCriteria(booksToRead, criteria, user).ToList();

            booksToRead = booksToRead.OrderBy(emp => Guid.NewGuid()).ToList();

            return booksToRead;
        }

        public string GetDescription()
        {
           return "Moins de 100 pages";
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
