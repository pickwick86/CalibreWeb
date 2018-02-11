using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calibre.Foundation
{
    [Serializable]
    public class User
    {
        private int _identifier;

        private string _name;

        private string _fullName;

        private string _password;

        private bool _isAdmin;

        private bool _isGuest;

        public bool IsGuest
        {
            get { return _isGuest; }
            set { _isGuest = value; }
        }

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; }
        }
        

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }
        

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        public int Identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }
        
    }
}
