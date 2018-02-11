/*
 * Crée par SharpDevelop.
 * Utilisateur: FX08743
 * Date: 05/03/2014
 * Heure: 09:29
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;
using Calibre.Business;
using Calibre.Business.BookSelecters;
using Calibre.Foundation;
using System.Linq;

namespace ConsoleDeTest
{
  class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      
      // TODO: Implement Functionality Here
      IBookManager bm = BookManagerFactory.GetBookManager();

      User moi = bm.GetAllUsers().SingleOrDefault(x => x.Identifier == 1);
      
      List<Book> allBooks = bm.GetAllBooks();
      //var allUserBooks = bm.GetAllUserBooks();

      IBookSelecter selecter = new ContinueSerie();
      IBookSelecter selecter2 = new NewAuthor();
      IBookSelecter selecter3 = new TerminerSerie();
      IBookSelecter selecter4 = new VieuxTruc();
      IBookSelecter selecter5 = new ViteLu();
      IBookSelecter selecter6 = new RecentlyAdded();
      IBookSelecter selecter7 = new OneShot();
      IBookSelecter selecter8 = new StartNewSerie();


      BookSelecterCriteria criteria = new BookSelecterCriteria() { AuthorLike = "nath" };

        TrySelecter(selecter7, allBooks, criteria, moi);
       // TrySelecter(selecter2, allBooks, criteria, moi);


      Console.Write("Press any key to continue . . . ");
      Console.ReadKey(true);
    }

    private static void TestUsers(IBookManager bm)
    {
        var users = bm.GetAllUsers();
        foreach (User user in users)
        {
            Console.WriteLine(user.Name + " " + user.Password + " " + user.IsAdmin.ToString());
        }
    }

    private static void TrySelecter(IBookSelecter selecter, List<Book> allBooks, BookSelecterCriteria criteria, User user)
    {
        Console.WriteLine("=========================================================");
        Console.WriteLine("Testing Selecter " + selecter.GetDescription());
        Console.WriteLine();

        List<Book> theBooks = selecter.SelectBooks(allBooks, criteria, user);

        Console.WriteLine("Books count : " + theBooks.Count);

        for (int i = 1; i <= theBooks.Count; i++)
        {
            Console.WriteLine(string.Format("{0}/{1} : {2}",i, theBooks.Count, theBooks[i-1]));
        }

    }
  }
}