/*
 * Crée par SharpDevelop.
 * Utilisateur: FX08743
 * Date: 06/03/2014
 * Heure: 13:43
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.IO;
using System.Linq;

namespace Calibre.Business
{
  /// <summary>
  /// Description of FileHelper.
  /// </summary>
  public static class FileHelper
  {
    private const string EPUB_EXTENSION = ".epub";
    
    private const string COVER_FILE_NAME = "cover.jpg";
    
    public static FileInfo GetEpub(string path)
    {
      FileInfo result = null;
      
      DirectoryInfo directory = new DirectoryInfo(path);
      if(directory.Exists)
      {
        result = directory.GetFiles().SingleOrDefault(x => x.Extension == EPUB_EXTENSION);
      }
      
      return result;
    }
    
    public static FileInfo GetCover(string path)
    {
      FileInfo result = null;
      
      DirectoryInfo directory = new DirectoryInfo(path);
      if(directory.Exists)
      {
        result = directory.GetFiles().SingleOrDefault(x => x.Name == COVER_FILE_NAME);
      }
      
      return result;
    }
  }
}
