/*
 * Crée par SharpDevelop.
 * Utilisateur: FX08743
 * Date: 05/03/2014
 * Heure: 09:38
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;

namespace Calibre.Foundation
{
  /// <summary>
  /// Description of IBookManager.
  /// </summary>
  public interface IBookManager
  {
    List<Book> GetAllBooks();

    List<string> GetAllTags();

    List<string> GetAllLanguages();

    List<User> GetAllUsers();

    List<UserBookExperience> GetAllUserBookExperiences();

    void PersistUserBook(UserBookExperience userBook);

    void InitializeData();
  }
}
