/*
 * Crée par SharpDevelop.
 * Utilisateur: FX08743
 * Date: 06/03/2014
 * Heure: 14:32
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;

namespace Calibre.Foundation
{
  /// <summary>
  /// Description of BookSelecterCriteria.
  /// </summary>
  public class BookSelecterCriteria
  {
    public string Tag {get; set;}
    public string AuthorLike { get; set;}
    public string Language { get; set; }
    public string TitleLike { get; set; }
    public string SerieLike { get; set; }
    public int EnvieMin { get; set; }
    public int MinPages { get; set; }
    public int MaxPages { get; set; }
  }
}
