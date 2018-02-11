/*
 * Crée par SharpDevelop.
 * Utilisateur: FX08743
 * Date: 06/03/2014
 * Heure: 14:34
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;

namespace Calibre.Foundation
{
  /// <summary>
  /// Description of IBookSelecter.
  /// </summary>
  public interface IBookSelecter
  {
    List<Book> SelectBooks(List<Book> allBooks, BookSelecterCriteria criteria, User user);

    string GetDescription();

    int GetPriority();

    bool ForGuest();
  }
}
