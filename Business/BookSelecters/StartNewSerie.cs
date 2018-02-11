/*
 * Crée par SharpDevelop.
 * Utilisateur: FX08743
 * Date: 06/03/2014
 * Heure: 14:36
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using Calibre.Foundation;
using System.Linq;
using System.Collections.Generic;

namespace Calibre.Business.BookSelecters
{
  /// <summary>
  /// Description of StartNewSerie.
  /// </summary>
  public class StartNewSerie : IBookSelecter
  {
    public StartNewSerie()
    {
    }

    public List<Book> SelectBooks(List<Book> allBooks, BookSelecterCriteria criteria, User user)
    {
      Random rand = new Random();

      IEnumerable<Book> selection = Helper.GetBooksToRead(allBooks, user);
              
      //Filters
      selection = Helper.ApplySearchCriteria(selection, criteria, user);
      
      //Keep only series and first volume
      selection = selection.Where(x => x.Serie != string.Empty && x.SerieIndex == 1);

      selection = selection.OrderBy(emp => Guid.NewGuid()).ToList();

      return selection.ToList();
    }

    public string GetDescription()
    {
        return "Commencer une série";
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
