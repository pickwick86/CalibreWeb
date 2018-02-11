using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calibre.Foundation;
using Calibre.Business.Cache;

namespace Calibre.Business
{
    public static class Extensions
    {
        public static bool CaseContains(this string baseString, string textToSearch)
        {
            return (baseString.IndexOf(textToSearch, StringComparison.OrdinalIgnoreCase) != -1);
        }
        

    }
}
