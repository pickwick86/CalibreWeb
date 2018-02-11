/*
 * Crée par SharpDevelop.
 * Utilisateur: FX08743
 * Date: 05/03/2014
 * Heure: 09:34
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Linq;

namespace Calibre.Foundation
{
  /// <summary>
  /// Description of Book.
  /// </summary>
  [Serializable]
  public class Book
  {
    #region Attributes

    protected string mSerie = string.Empty;
    
    protected string mTitle = string.Empty;
    
    protected string mPath = string.Empty;

    protected string mURL = string.Empty;

    protected string mCoverURL = string.Empty;
    
    protected List<string> mAuthors = new List<string>();
    
    protected List<string> mTags = new List<string>();

    protected string mFullCoverPath = string.Empty;

    protected string mFullBookPath = string.Empty;

    protected DirectoryInfo mFolder = null;

    protected DateTime mDateAdded;
      
    protected int mPages = 0;

    protected string mLanguage = string.Empty;

    protected List<UserBookExperience> mExperiences = new List<UserBookExperience>();
    
    #endregion
    
    #region Properties

    public List<UserBookExperience> Experiences
    {
        get { return mExperiences; }
        set { mExperiences = value; }
    }

    /// <summary>
    /// Language
    /// </summary>
    public string Language
    {
        get { return mLanguage; }
        set { mLanguage = value; }
    }

    public int Pages
    {
        get { return mPages; }
        set { mPages = value; }
    }
      
    public DateTime DateAdded
    {
        get { return mDateAdded; }
        set { mDateAdded = value; }
    }

    public string URL
    {
       get 
       {
          if (string.IsNullOrEmpty(mURL))
          {
             string epubName = Folder.GetFiles().FirstOrDefault(x => x.Extension.ToLower().Equals(".epub")).Name;
             mURL = Path + "/" + epubName;
          }
          return mURL;
       }
    }

    public string CoverURL
    {
        get
        {
            if (string.IsNullOrEmpty(mCoverURL))
            {
                mCoverURL = Path + "/cover.jpg";
            }
            return mCoverURL;
        }
    }
    
    public string FullCoverPath
    {
        get
        {
            if (string.IsNullOrEmpty(mFullCoverPath))
            {
                if (Folder.Exists)
                {
                    mFullCoverPath = System.IO.Path.Combine(Folder.FullName, "cover.jpg");
                }
            }
            return mFullCoverPath;
        }
    }

    public string FullBookPath
    {
        get
        {
            if (string.IsNullOrEmpty(mFullBookPath))
            {
                if (Folder.Exists)
                {
                    mFullBookPath = Folder.GetFiles().FirstOrDefault(x => x.Extension.ToLower().Equals(".epub")).FullName;
                }
                
            }
            return mFullBookPath;
        }
    }

    protected DirectoryInfo Folder
    {
        get
        {
            if (mFolder == null)
            {
                mFolder = new DirectoryInfo(System.IO.Path.Combine(ConfigurationManager.AppSettings["DBPath"], Path.Replace('/', '\\')));
            }

            return mFolder;
        }
    }
      
    public int ID
    {
      get;
      set;
    }
  
    public string Title
    {
      get { return mTitle;}
      set { mTitle = value;}
    }
    
    public string Serie
    {
      get { return mSerie;}
      set { mSerie = value;}
    }
    
    public int SerieIndex
    {
      get;
      set;
    }
    
    public List<string> Authors
    {
      get { return mAuthors;}
      set { mAuthors = value;}
    }
    
    public string Path
    {
      get { return mPath;}
      set { mPath = value;}
    }
    
    public List<string> Tags
    {
      get { return mTags;}
      set { mTags = value;}
    }
    
    public string Summary
    {
      get;
      set;
    }
        
    #endregion
    
    #region Constructor
    
    public Book()
    {
    }

    public Book(Book previousBook)
    {
        mAuthors = previousBook.mAuthors;
        mCoverURL = previousBook.mCoverURL;
        mDateAdded = previousBook.mDateAdded;
        mFolder = previousBook.mFolder;
        mFullBookPath = previousBook.mFullBookPath;
        mFullCoverPath = previousBook.mFullCoverPath;
        mLanguage = previousBook.mLanguage;
        mPages = previousBook.mPages;
        mPath = previousBook.mPath;
        mSerie = previousBook.mSerie;
        mTags = previousBook.mTags;
        mTitle = previousBook.mTitle;
        mURL = previousBook.mURL;
        
    }
    
    #endregion
    
    #region Operations
    
    public override string ToString()
    {
        return "'" + mTitle + "', by " + mAuthors[0];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public bool HasBeenReadBy(User user)
    {
        return mExperiences.Any(x => x.Read && x.UserId == user.Identifier);
    }


    public int GetRating(User user)
    {
        if (mExperiences.Any(x => x.UserId == user.Identifier))
        {
            return mExperiences.First(x => x.UserId == user.Identifier).Rating;
        }
        else
        {
            return 0;
        }
    }
    
    #endregion
  }
}
