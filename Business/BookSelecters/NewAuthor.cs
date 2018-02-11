using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calibre.Foundation;

namespace Calibre.Business.BookSelecters
{
    public class NewAuthor : IBookSelecter
    {
        public List<Book> SelectBooks(List<Book> allBooks, BookSelecterCriteria criteria, User user)
        {
            Random rand = new Random();

            var knownAuthors = allBooks.Where(x => x.Experiences.Any(y => y.Read)).SelectMany(x => x.Authors).Distinct();

            //var knownAuthors = allBooks.Where(x => x.GetUserBook(user).Read).SelectMany(x => x.Authors).Distinct();

            IEnumerable<Book> selection = Helper.GetBooksToRead(allBooks, user);

            selection = Helper.ApplySearchCriteria(selection, criteria, user);
            
            selection = selection.Where(x => !(x.Authors.Intersect(knownAuthors).Any()));
                        
            selection = selection.OrderBy(emp => Guid.NewGuid()).ToList();
            
            return selection.ToList();
        }

        public string GetDescription()
        {
            return "Découvrir un nouvel auteur";
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
