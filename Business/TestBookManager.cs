/*
 * Crée par SharpDevelop.
 * Utilisateur: FX08743
 * Date: 05/03/2014
 * Heure: 09:42
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using Calibre.Foundation;
using System.Collections.Generic;

namespace Calibre.Business
{
  /// <summary>
  /// Description of TestBookManager.
  /// </summary>
  public class TestBookManager : IBookManager
  {
    private enum Tags
    {
      Thriller = 1,
      
      Fantasy = 2,
      
      SF = 3,
      
      Humour = 4,
      
      Policier = 5,
      
      Fantastique = 6
    }
    
    public TestBookManager()
    {
    }

    public List<string> GetAllLanguages() { return new List<string>() { "fra", "eng" }; }

    public List<string> GetAllTags()
    {
        return new List<string>() { "Autre", "Aventure", "Thriller", "Post-Apo", "Fantasy" };
    }
    
    public List<Book> GetAllBooks()
    {
      List<Book> result = new List<Book>();
      int bookId = 0;
      
      Random rand = new Random();
      for(int i = 0; i<1000; i++)
      {
        Book theBook = new Book();
        
        theBook.ID = bookId++;
        theBook.Title = "Titre" + i;
        
        theBook.Authors.Add("Auteur" + rand.Next(100));
        
        
        theBook.Tags.Add(((Tags)rand.Next(1,6)).ToString());


        theBook.DateAdded = DateTime.Now.AddDays(-rand.Next(0, 500));
        
        result.Add(theBook);
      }
      
      for(int i = 0; i<20; i++)
      {
        string author = "Auteur" + rand.Next(100);
        string serie = "Serie" + i;
        string tag = ((Tags)rand.Next(1,6)).ToString();
        
        int nbTomes = rand.Next(15);
        int nbLus = rand.Next(nbTomes);
        
        for(int j = 0; j < nbTomes; j++)
        {
          Book theBook = new Book();
          theBook.ID = bookId++;
          theBook.Authors.Add(author);
          theBook.Tags.Add(tag);
          theBook.Serie = serie;
          theBook.SerieIndex = j + 1;
          theBook.DateAdded = DateTime.Now.AddDays(-rand.Next(0, 500));
          result.Add(theBook);
        }
        
        
        
      }
            
      return result;
    }

    public List<User> GetAllUsers()
    {
        List<User> result = new List<User>();

        User admin = new User() { Identifier = 1, IsAdmin = true, Name = "admin", FullName = "Admin", Password = "1234" };
        User user = new User() { Identifier = 2, IsAdmin = false, Name = "user", FullName = "User", Password = "5678" };

        result.Add(admin);
        result.Add(user);

        return result;
    }

    public List<UserBookExperience> GetAllUserBookExperiences()
    {
        throw new NotImplementedException();
    }


    public void PersistUserBook(UserBookExperience userBook)
    {
        throw new NotImplementedException();
    }

    public void InitializeData()
    {
        throw new NotImplementedException();
    }
  }
}
