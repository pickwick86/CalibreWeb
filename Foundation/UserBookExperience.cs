using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calibre.Foundation
{
    [Serializable]
    public class UserBookExperience
    {
        #region Attributes

        private int _identifier;

        private int _bookId;

        private int _userId;

        private bool _read;

        private int _rating;

        private DateTime _dateRead;

        #endregion

        #region Properties

        public DateTime DateRead
        {
            get { return _dateRead; }
            set { _dateRead = value; }
        }
        
        public int Rating
        {
            get { return _rating; }
            set { _rating = value; }
        }
        
        public bool Read
        {
            get { return _read; }
            set { _read = value; }
        }
        
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        
        public int BookIdentifier
        {
            get { return _bookId; }
            set { _bookId = value; }
        }
        
        public int Identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }

        #endregion

        #region Constructor

        public UserBookExperience(int theBookId, int userId)
        {
            _bookId = theBookId;
            _userId = userId;
        }

        #endregion

    }
}
