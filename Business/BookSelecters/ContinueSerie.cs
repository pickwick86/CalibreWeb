using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calibre.Foundation;

namespace Calibre.Business.BookSelecters
{
    public class ContinueSerie : IBookSelecter
    {
        public List<Book> SelectBooks(List<Book> allBooks, BookSelecterCriteria criteria, User user)
        {
            Random rand = new Random();

            List<Book> result = new List<Book>();

            //remove already read books
            var selection = allBooks.Where(x => !string.IsNullOrEmpty(x.Serie) && x.SerieIndex > 0);

            //Filters
            selection = Helper.ApplySearchCriteria(selection, criteria, user);

            //selection = selection.Where(x => x.SerieIndex != 1 && x.SerieIndex == selection.Where(y => y.Serie.Equals(x.Serie) && y.Read == false).Select(z => z.SerieIndex).Min());
            var seriesNames = selection.Select(x => x.Serie).Distinct();

            foreach (string serie in seriesNames)
            {
                var booksOfTheSerie = selection.Where(x => x.Serie.Equals(serie));

                if (booksOfTheSerie.Any(x => !x.HasBeenReadBy(user)) && booksOfTheSerie.Any(x => x.HasBeenReadBy(user)))
                {
                    //La série contient des non lus
                    booksOfTheSerie = booksOfTheSerie.OrderBy(x => x.SerieIndex);

                    Book nextBookForThisSerie = booksOfTheSerie.First(x => !x.HasBeenReadBy(user));

                    if (booksOfTheSerie.Where(x => x.SerieIndex < nextBookForThisSerie.SerieIndex).All(x => x.HasBeenReadBy(user)) && nextBookForThisSerie.SerieIndex > 1)
                    {
                        //Les livres précédents ont été lus
                        result.Add(nextBookForThisSerie);
                    }
                }
            }

            result = result.OrderBy(emp => Guid.NewGuid()).ToList();

            return result;
        }

        public string GetDescription()
        {
            return "Continuer une série";
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
