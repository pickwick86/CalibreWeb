/*
 * Crée par SharpDevelop.
 * Utilisateur: FX08743
 * Date: 05/03/2014
 * Heure: 09:39
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Globalization;

using Calibre.Foundation;
using System.Text;

namespace Calibre.Business
{
  /// <summary>
  /// Description of BookManager.
  /// </summary>
  public class BookManager : IBookManager
  {
    private const string FILENAME = "metadata.db";


    public List<string> GetAllTags()
    {
        List<string> result = new List<string>();
         string connectionString = string.Format("data source={0}",  Path.Combine(ConfigurationManager.AppSettings["DBPath"], FILENAME));

         using (SQLiteConnection myConnection = new SQLiteConnection(connectionString))
         {
             myConnection.Open();

             using (SQLiteCommand myCommand = myConnection.CreateCommand())
             {
                 myCommand.CommandText = "select tags.name from tags";

                 var reader = myCommand.ExecuteReader();

                 while (reader.Read())
                 {
                     result.Add(reader[0].ToString());
                 }
             }
         }
         result = result.OrderBy(x => x).ToList();
         return result;
    }

    public List<string> GetAllLanguages()
    {
        List<string> result = new List<string>();
        string connectionString = string.Format("data source={0}", Path.Combine(ConfigurationManager.AppSettings["DBPath"], FILENAME));

        using (SQLiteConnection myConnection = new SQLiteConnection(connectionString))
        {
            myConnection.Open();

            using (SQLiteCommand myCommand = myConnection.CreateCommand())
            {
                myCommand.CommandText = "select distinct(lang_code) from languages";

                var reader = myCommand.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(reader[0].ToString());
                }
            }
        }
        result = result.OrderBy(x => x).ToList();
        return result;
    }


    public List<User> GetAllUsers()
    {
        List<User> result = new List<User>();
        string connectionString = string.Format("data source={0}", Path.Combine(ConfigurationManager.AppSettings["DBPath"], FILENAME));

        using (SQLiteConnection myConnection = new SQLiteConnection(connectionString))
        {
            myConnection.Open();

            using (SQLiteCommand myCommand = myConnection.CreateCommand())
            {
                myCommand.CommandText = "select id, Name, Fullname, Password, IsAdmin, IsGuest from user";

                var reader = myCommand.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User();
                    user.Identifier = reader.GetInt32(reader.GetOrdinal("id"));
                    user.Name = reader[reader.GetOrdinal("Name")] as string ?? string.Empty;
                    user.FullName = reader[reader.GetOrdinal("FullName")] as string ?? string.Empty;
                    user.Password = reader[reader.GetOrdinal("Password")] as string ?? string.Empty;
                    user.IsAdmin = reader.GetInt32(reader.GetOrdinal("IsAdmin")) == 1;
                    user.IsGuest = reader.GetInt32(reader.GetOrdinal("IsGuest")) == 1;

                    result.Add(user);
                }
            }
        }

        return result;
    }
    
    public List<Book> GetAllBooks()
    {
        CultureInfo provider = CultureInfo.InvariantCulture;
        List<Book> bookList = new List<Book>();
      
        string connectionString = string.Format("data source={0}",  Path.Combine(ConfigurationManager.AppSettings["DBPath"], FILENAME));

        string readColumnName = "custom_column_" + ConfigurationManager.AppSettings["ReadCustomColumnNumber"];
        string pagesColumnName = "custom_column_" + ConfigurationManager.AppSettings["PagesCustomColumnNumber"];
      
        using(SQLiteConnection myConnection = new SQLiteConnection(connectionString))
	    {
  			myConnection.Open();

            using (SQLiteCommand myCommand = myConnection.CreateCommand())
            {
                //myCommand.CommandText = "select books.id,books.timestamp, books.title, books.series_index, series.name as serie, authors.name as author, books.path , tags.name as tag, ifnull(" + readColumnName + ".value,0) as read,ifnull(" + pagesColumnName + ".value,0) as pages, comments.text  from books left join books_series_link on books_series_link.book = books.id left join series on books_series_link.series = series.id inner join books_authors_link on  books_authors_link.book = books.id inner join authors on authors.id =  books_authors_link.author inner join books_tags_link on books_tags_link.book = books.id inner join tags on tags.id = books_tags_link.tag left outer join " + readColumnName + " on " + readColumnName + ".book = books.id left join comments on books.id = comments.book left outer join " + pagesColumnName + " on " + pagesColumnName + ".book = books.id";
                myCommand.CommandText = "select books.id,books.timestamp, books.title, books.series_index, series.name as serie, authors.name as author, books.path , tags.name as tag, ifnull(" + pagesColumnName + ".value,0) as pages, comments.text, ifnull(languages.lang_code,'fra') as language  from books left join books_series_link on books_series_link.book = books.id left join series on books_series_link.series = series.id inner join books_authors_link on  books_authors_link.book = books.id inner join authors on authors.id =  books_authors_link.author left join books_tags_link on books_tags_link.book = books.id left join tags on tags.id = books_tags_link.tag left join comments on books.id = comments.book left outer join " + pagesColumnName + " on " + pagesColumnName + ".book = books.id INNER JOIN books_languages_link ON books_languages_link.book = books.id INNER JOIN languages on books_languages_link.lang_code = languages.id";

                var reader = myCommand.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(reader.GetOrdinal("id"));

                    Book existingBook = bookList.SingleOrDefault(x => x.ID == id);
                    if (existingBook == null)
                    {

                        Book theBook = new Book();



                        theBook.Title = reader[reader.GetOrdinal("title")] as string ?? string.Empty;
                        theBook.ID = reader.GetInt32(reader.GetOrdinal("id"));
                        theBook.Serie = reader[reader.GetOrdinal("serie")] as string ?? string.Empty;
                        theBook.SerieIndex = (int)(reader[reader.GetOrdinal("series_index")] as double? ?? 0);
                        theBook.Path = (reader[reader.GetOrdinal("path")] as string ?? string.Empty)/*.Replace('/','\\')*/;

                        theBook.Authors.Add(reader[reader.GetOrdinal("author")] as string ?? string.Empty);
                        theBook.Tags.Add(reader[reader.GetOrdinal("tag")] as string ?? string.Empty);
                        //theBook.Read = Convert.ToBoolean(reader[reader.GetOrdinal("read")]);
                        theBook.Summary = reader[reader.GetOrdinal("text")] as string ?? string.Empty;
                        theBook.DateAdded = (DateTime)reader[reader.GetOrdinal("timestamp")]; //
                        theBook.Pages = reader.GetInt32(reader.GetOrdinal("pages"));
                        theBook.Language = reader[reader.GetOrdinal("language")] as string ?? string.Empty;

                        bookList.Add(theBook);
                    }
                    else
                    {
                        string author = reader[reader.GetOrdinal("author")] as string ?? string.Empty;
                        if (!existingBook.Authors.Contains(author))
                            existingBook.Authors.Add(author);

                        string tag = reader[reader.GetOrdinal("tag")] as string ?? string.Empty;
                        if (!existingBook.Tags.Contains(tag))
                            existingBook.Tags.Add(tag);
                    }

                }
            }
	    

            using (SQLiteCommand myCommand = myConnection.CreateCommand())
            {
                myCommand.CommandText = "SELECT id, book, user, read, rating, datetime(date_read) as date_read FROM user_books";

                var reader = myCommand.ExecuteReader();

                while (reader.Read())
                {
                    int bookId = reader.GetInt32(reader.GetOrdinal("book"));
                    int userId = reader.GetInt32(reader.GetOrdinal("user"));

                    UserBookExperience ub = new UserBookExperience(bookId, userId);

                    ub.Identifier = reader.GetInt32(reader.GetOrdinal("id"));
                    ub.Read = Convert.ToBoolean(reader[reader.GetOrdinal("read")]);
                    ub.Rating = reader.GetInt32(reader.GetOrdinal("rating"));
                    try
                    {
                        ub.DateRead = DateTime.ParseExact(reader[reader.GetOrdinal("date_read")].ToString(), "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);
                    }
                    catch (FormatException)
                    {
                        ub.DateRead = DateTime.MinValue;
                    }

                    bookList.Single(x => x.ID == ub.BookIdentifier).Experiences.Add(ub);
                }
            }

        }
      
      return bookList;
    }

    public List<UserBookExperience> GetAllUserBookExperiences()
    {
        List<UserBookExperience> result = new List<UserBookExperience>();
        string connectionString = string.Format("data source={0}",  Path.Combine(ConfigurationManager.AppSettings["DBPath"], FILENAME));


        using (SQLiteConnection myConnection = new SQLiteConnection(connectionString))
        {
            myConnection.Open();

            using (SQLiteCommand myCommand = myConnection.CreateCommand())
            {
                myCommand.CommandText = "SELECT id, book, user, read, rating, datetime(date_read) as date_read FROM user_books";

                var reader = myCommand.ExecuteReader();

                while (reader.Read())
                {
                    int bookId = reader.GetInt32(reader.GetOrdinal("book"));
                    int userId = reader.GetInt32(reader.GetOrdinal("user"));
                    
                    UserBookExperience ub = new UserBookExperience(bookId, userId);

                    ub.Identifier = reader.GetInt32(reader.GetOrdinal("id"));
                    ub.Read = Convert.ToBoolean(reader[reader.GetOrdinal("read")]);
                    ub.Rating = reader.GetInt32(reader.GetOrdinal("rating"));
                    try
                    {
                        ub.DateRead = DateTime.ParseExact(reader[reader.GetOrdinal("date_read")].ToString(),"yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);
                    }
                    catch (FormatException)
                    {
                        ub.DateRead = DateTime.MinValue;
                    }
                    result.Add(ub);
                }
            }
        }

        return result;
    }


    public void PersistUserBook(UserBookExperience userBook)
    {
        string connectionString = string.Format("data source={0}", Path.Combine(ConfigurationManager.AppSettings["DBPath"], FILENAME));

        using (SQLiteConnection myConnection = new SQLiteConnection(connectionString))
        {
            myConnection.Open();

            using (SQLiteCommand myCommand = myConnection.CreateCommand())
            {
                if (userBook.Identifier == 0)
                {
                    //INSERT
                    myCommand.CommandText = string.Format("INSERT INTO user_books(book, user, read, rating, date_read) values ({0},{1},{2},{3},'{4}')", userBook.BookIdentifier, userBook.UserId, Convert.ToInt32(userBook.Read), userBook.Rating, userBook.DateRead.ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture));
                    myCommand.ExecuteNonQuery();

                    myCommand.CommandText = "SELECT MAX(id) FROM user_books";
                    int id = Convert.ToInt32((long) myCommand.ExecuteScalar());

                    userBook.Identifier = id;
                    
                }
                else
                {
                    //UPDATE
                    myCommand.CommandText = string.Format("UPDATE user_books SET read={0}, rating={1}, date_read='{2}' WHERE id={3}", Convert.ToInt32(userBook.Read), userBook.Rating, userBook.DateRead.ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture), userBook.Identifier);
                    myCommand.ExecuteNonQuery();
                }
                
            }
        }
    }

    public void InitializeData()
    {
        string connectionString = string.Format("data source={0}", Path.Combine(ConfigurationManager.AppSettings["DBPath"], FILENAME));

        using (SQLiteConnection myConnection = new SQLiteConnection(connectionString))
        {
            myConnection.Open();
            long nbTableUser;

            using (SQLiteCommand myCommand = myConnection.CreateCommand())
            {
                myCommand.CommandText = "SELECT count(*) FROM sqlite_master where type=\"table\" and name =\"user\"";
                nbTableUser = (long) myCommand.ExecuteScalar();
            }

            if (nbTableUser == 0)
            {
                //Creation de la table user
                using (SQLiteCommand command = myConnection.CreateCommand())
                {

                    command.CommandText = "CREATE TABLE [user] ([id] integer PRIMARY KEY NOT NULL, [Name] text NOT NULL, [FullName] text, [Password] text NOT NULL, [IsAdmin] integer NOT NULL, [IsGuest] INTEGER NOT NULL  DEFAULT 0)";
                    command.ExecuteNonQuery();
                    
                }

                using (SQLiteCommand command = myConnection.CreateCommand())
                {

                    command.CommandText = "CREATE TRIGGER user_delete_trg AFTER DELETE ON user BEGIN DELETE FROM user_books WHERE user=OLD.id; END";
                    command.ExecuteNonQuery();
                    
                }

                
                                
                //Creation de la table userbook
                using (SQLiteCommand command = myConnection.CreateCommand())
                {

                    command.CommandText = "CREATE TABLE [user_books] ([id] integer PRIMARY KEY  NOT NULL ,[book] integer NOT NULL ,[user] integer NOT NULL ,[read] integer NOT NULL ,[rating] integer NOT NULL ,[date_read] datetime NOT NULL  DEFAULT ('0001-01-01 00:00:00') )";
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = myConnection.CreateCommand())
                {
                    command.CommandText = "DROP TRIGGER books_delete_trg";
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = myConnection.CreateCommand())
                {
                    StringBuilder sb = new StringBuilder("CREATE TRIGGER books_delete_trg AFTER DELETE ON books BEGIN ");
                    sb.Append(" DELETE FROM books_authors_link WHERE book=OLD.id; ");
                    sb.Append(" DELETE FROM books_publishers_link WHERE book=OLD.id; ");
                    sb.Append(" DELETE FROM books_ratings_link WHERE book=OLD.id; ");
                    sb.Append(" DELETE FROM books_series_link WHERE book=OLD.id; ");
                    sb.Append(" DELETE FROM books_tags_link WHERE book=OLD.id; ");
                    sb.Append(" DELETE FROM books_languages_link WHERE book=OLD.id; ");
                    sb.Append(" DELETE FROM data WHERE book=OLD.id; ");
                    sb.Append(" DELETE FROM comments WHERE book=OLD.id; ");
                    sb.Append(" DELETE FROM conversion_options WHERE book=OLD.id; ");
                    sb.Append(" DELETE FROM books_plugin_data WHERE book=OLD.id; ");
                    sb.Append(" DELETE FROM identifiers WHERE book=OLD.id; ");
                    sb.Append(" DELETE FROM user_books WHERE book=OLD.id; ");
                    sb.Append(" END");
                    command.CommandText = sb.ToString();
                    command.ExecuteNonQuery();
                }

                


                using (SQLiteCommand command = myConnection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO user ( id, Name, FullName, Password, IsAdmin, IsGuest) VALUES (1, 'user','user','password', 1, 0);";
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = myConnection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO user ( id, Name, FullName, Password, IsAdmin, IsGuest) VALUES (2, 'guest','guest','', 0, 1);";
                    command.ExecuteNonQuery();
                }

                string readColumnName = "custom_column_" + ConfigurationManager.AppSettings["ReadCustomColumnNumber"];

                //Insertion des données depuis l'ancien format
                using (SQLiteCommand command = myConnection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO user_books ( book, user, read, rating) SELECT books.id, 1, ifnull(" + readColumnName + ".value,0),  0 FROM books LEFT OUTER JOIN " + readColumnName + " on " + readColumnName + ".book = books.id ";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
  }
}
