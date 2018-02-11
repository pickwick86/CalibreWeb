using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calibre.Foundation;

namespace Calibre.Business.BookSelecters
{
    public class AllBooks : IBookSelecter
    {
        public List<Book> SelectBooks(List<Book> allBooks, BookSelecterCriteria criteria, User user)
        {
            var selection = Helper.ApplySearchCriteria(allBooks, criteria, user);

            return selection.OrderBy(emp => Guid.NewGuid()).ToList();
        }

        public string GetDescription()
        {
            return "Tous les livres";
        }

        public int GetPriority()
        {
            return 2;
        }

        public bool ForGuest()
        {
            return true;
        }
    }
}
