using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Calibre.Foundation;

namespace CalibreWeb
{
    /// <summary>
    /// 
    /// </summary>
    public class BookSelecterDropDownList : DropDownList
    {
        public override void DataBind()
        {
            this.Items.Clear();
            var selecters = ((List<IBookSelecter>)HttpContext.Current.Session["Selecters"]);

            if (((User)HttpContext.Current.Session["User"]).IsGuest)
            {
                selecters = selecters.Where(x => x.ForGuest()).ToList();
            }

            var sortedSelecters = selecters.OrderBy(x => x.GetPriority());

            foreach (IBookSelecter selecter in sortedSelecters)
            {
                Items.Add(new ListItem(selecter.GetDescription(), selecter.GetDescription()));
            }
        }

        public IBookSelecter SelectedSelecter
        {
            get 
            {
                List<IBookSelecter> selecters = (List<IBookSelecter>)HttpContext.Current.Session["Selecters"];
                return selecters.SingleOrDefault(x => x.GetDescription().Equals(SelectedItem.Value));
            }
        }
    }
}