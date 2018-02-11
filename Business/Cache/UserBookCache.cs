using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calibre.Foundation;
using System.Reflection;

namespace Calibre.Business.Cache
{
    public class UserBookCache : AbstractCache<UserBookCache,List<UserBookExperience>>
    {
        protected override List<UserBookExperience> GetData()
        {
            
            IBookManager bm = BookManagerFactory.GetBookManager();

            return bm.GetAllUserBookExperiences();
        }
    }
}
