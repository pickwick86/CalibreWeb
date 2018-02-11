using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Calibre.Foundation;

namespace CalibreWeb
{
    public class TagsDropDownList : DropDownList
    {
        public override void DataBind()
        {
            Items.Clear();
            Items.Add(new ListItem("N'importe quel genre", string.Empty));
            List<string> tags = (List<string>)HttpContext.Current.Session["Tags"];

            foreach (string tag in tags)
            {
                Items.Add(new ListItem(tag, tag));
            }
        }
    }
}