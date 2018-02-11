using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calibre.Foundation;

namespace Calibre.Business.BookSelecters
{
    public class TerminerSerie : IBookSelecter
    {
        public List<Book> SelectBooks(List<Book> allBooks, BookSelecterCriteria criteria, User user)
        {
            var toRead = Helper.GetBooksToRead(allBooks,user);

            //Removed one-shots and prequels
            toRead.RemoveAll(x => x.SerieIndex == 0 || string.IsNullOrEmpty(x.Serie));

            toRead = Helper.ApplySearchCriteria(toRead, criteria, user);

            toRead.RemoveAll(x => x.SerieIndex == 1 || x.SerieIndex != allBooks.Where(y => y.Serie == x.Serie).Max(y => y.SerieIndex));
            toRead.RemoveAll(x => x.SerieIndex == allBooks.Where(y => x.Serie == y.Serie).Min(y => y.SerieIndex));

            toRead = toRead.OrderBy(emp => Guid.NewGuid()).ToList();

            return toRead;
        }

        public string GetDescription()
        {
            return "Terminer une série";
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
