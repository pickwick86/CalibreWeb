using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calibre.Foundation;

namespace Calibre.Business.Cache
{
    public class BookCache : AbstractCache<BookCache, List<Book>>
    {
        protected override List<Book> GetData()
        {
            IBookManager bm = BookManagerFactory.GetBookManager();

            return bm.GetAllBooks();
        }
    }
}
