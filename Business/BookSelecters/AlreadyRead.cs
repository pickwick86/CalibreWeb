using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calibre.Foundation;

namespace Calibre.Business.BookSelecters
{
    public class AlreadyRead : IBookSelecter
    {
        public List<Book> SelectBooks(List<Book> allBooks, BookSelecterCriteria criteria, User user)
        {

            var alreadyRead = allBooks.Where(x => x.HasBeenReadBy(user));

            var selection = Helper.ApplySearchCriteria(alreadyRead, criteria, user);

            return selection.OrderByDescending(x => x.Experiences.Single(y=> y.UserId == user.Identifier).DateRead).ToList();
        }

        public string GetDescription()
        {
            return "Mes lectures";
        }

        public int GetPriority()
        {
            return 20;
        }

        public bool ForGuest()
        {
            return false;
        }
    }
}
