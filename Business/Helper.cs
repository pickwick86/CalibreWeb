using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Calibre.Foundation;

namespace Calibre.Business
{
    public static class Helper
    {
        public static List<IBookSelecter> GetAllSelecters()
        {
            List<IBookSelecter> result = new List<IBookSelecter>();
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IBookSelecter)));

            foreach (Type t in types)
            {
                result.Add((IBookSelecter) Activator.CreateInstance(t,true));
            }

            return result;
        }

        public static List<Book> GetBooksToRead(List<Book> allBooks, User user)
        {
            List<Book> result = new List<Book>();

            //remove read books

            var toRead = allBooks.Where(x => !x.HasBeenReadBy(user));
            
            //Add one-shots and prequels
            result.AddRange(toRead.Where(x => string.IsNullOrEmpty(x.Serie) || x.SerieIndex == 0));

            var seriesNames = toRead.Select(x => x.Serie).Distinct();

            foreach (string serie in seriesNames)
            {
                if (!string.IsNullOrEmpty(serie))
                {
                    var booksOfTheSerie = allBooks.Where(x => x.Serie.Equals(serie) && x.SerieIndex != 0);

                    //if (booksOfTheSerie.Any(x => !x.Read) && booksOfTheSerie.Any(x => x.Read))
                    if (booksOfTheSerie.Any(x => !x.HasBeenReadBy(user)))
                    {
                        //La série contient des non lus
                        booksOfTheSerie = booksOfTheSerie.OrderBy(x => x.SerieIndex);

                        Book nextBookForThisSerie = booksOfTheSerie.First(x => !x.HasBeenReadBy(user));

                        if (booksOfTheSerie.Where(x => x.SerieIndex < nextBookForThisSerie.SerieIndex).All(x => x.HasBeenReadBy(user)))
                        {
                            //Les livres précédents ont été lus
                            result.Add(nextBookForThisSerie);
                        }
                    }

                }
            }

            return result;
        }

        
        public static List<Book> ApplySearchCriteria(IEnumerable<Book> selection, BookSelecterCriteria criteria, User user)
        {
            if (!string.IsNullOrEmpty(criteria.AuthorLike))
                selection = selection.Where(x => x.Authors.Any(y => y.CaseContains(criteria.AuthorLike)));

            if (!string.IsNullOrEmpty(criteria.Tag))
                selection = selection.Where(x => x.Tags.Contains(criteria.Tag));

            if (!string.IsNullOrEmpty(criteria.Language))
                selection = selection.Where(x => x.Language.Equals(criteria.Language));

            if (!string.IsNullOrEmpty(criteria.TitleLike))
            {
                selection = selection.Where(x => x.Title.CaseContains(criteria.TitleLike));
            }

            if (!string.IsNullOrEmpty(criteria.SerieLike))
            {
                selection = selection.Where(x => x.Serie.CaseContains(criteria.SerieLike));
            }

            if (criteria.EnvieMin > 0)
            {
                selection = selection.Where(x => x.GetRating(user) >= criteria.EnvieMin);
            }

            if (criteria.MinPages > 0)
            {
                selection = selection.Where(x => x.Pages >= criteria.MinPages);
            }

            if (criteria.MaxPages > 0)
            {
                selection = selection.Where(x => x.Pages <= criteria.MaxPages);
            }
            
            return selection.ToList();
        }

        public static UserBookExperience GetUserBookFromBook(User theUser, Book theBook, List<UserBookExperience> allUserBooks)
        {
            UserBookExperience defaultResult = new UserBookExperience(theBook.ID, theUser.Identifier);

            UserBookExperience theUserBook = allUserBooks.SingleOrDefault(x => x.BookIdentifier == theBook.ID && x.UserId == theUser.Identifier);

            return theUserBook ?? defaultResult;
            
        }
    }
}
