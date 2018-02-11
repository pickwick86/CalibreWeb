/*
 * Crée par SharpDevelop.
 * Utilisateur: FX08743
 * Date: 05/03/2014
 * Heure: 09:46
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Linq;
using Calibre.Foundation;
using System.Configuration;

namespace Calibre.Business
{
  /// <summary>
  /// Description of BookManagerFactory.
  /// </summary>
  public static class BookManagerFactory
  {
    public static IBookManager GetBookManager()
    {
        if(!ConfigurationManager.AppSettings.AllKeys.Contains("UseTestBookManager"))
            return new TestBookManager();

      if (ConfigurationManager.AppSettings["UseTestBookManager"].ToLower().Equals(bool.TrueString.ToLower()))
      {
        return new TestBookManager();
      }
      else
      {
        return new BookManager();
      }
    }
  }
}
