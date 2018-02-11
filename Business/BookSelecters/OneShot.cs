/*
 * Crée par SharpDevelop.
 * Utilisateur: FX08743
 * Date: 01/04/2014
 * Heure: 13:47
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Linq;
using Calibre.Foundation;
using System.Collections.Generic;

namespace Calibre.Business.BookSelecters
{
  /// <summary>
  /// Description of OneShot.
  /// </summary>
    public class OneShot : IBookSelecter
    {

        public List<Book> SelectBooks(List<Book> allBooks, BookSelecterCriteria criteria, User user)
        {
            Random rand = new Random();


            var selection = Helper.GetBooksToRead(allBooks, user).Where(x => string.IsNullOrEmpty(x.Serie));

            //Filters
            selection = Helper.ApplySearchCriteria(selection, criteria, user);

            selection = selection.OrderBy(emp => Guid.NewGuid()).ToList();

            return selection.ToList();
        }

        public string GetDescription()
        {
            return "Lire un livre one-shot";
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
