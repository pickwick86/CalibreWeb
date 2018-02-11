using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calibre.Foundation;

namespace Calibre.Business.BookSelecters
{
    public class AnyBook : IBookSelecter
    {
        public List<Book> SelectBooks(List<Book> allBooks, BookSelecterCriteria criteria, User user)
        {
            List<Book> booksToRead = Helper.GetBooksToRead(allBooks, user);

            booksToRead = Helper.ApplySearchCriteria(booksToRead, criteria, user).ToList();

            booksToRead = booksToRead.OrderBy(emp => Guid.NewGuid()).ToList();

            return booksToRead;
        }

        public string GetDescription()
        {
            return "N'importe quel livre à lire";
        }

        public int GetPriority()
        {
            return 0;
        }

        public bool ForGuest()
        {
            return true;
        }
    }
}
