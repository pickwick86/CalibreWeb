using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CalibreWeb
{
    public class LanguagesDropDownList : DropDownList
    {
        public override void DataBind()
        {
            Items.Clear();
            Items.Add(new ListItem("N'importe quelle langue", string.Empty));
            List<string> languages = (List<string>)HttpContext.Current.Session["Languages"];
            Dictionary<string, string> dico = (Dictionary<string, string>)HttpContext.Current.Session["DicoLanguages"];

            foreach (string language in languages)
            {
                if (dico.ContainsKey(language))
                {
                    Items.Add(new ListItem(dico[language], language));
                }
                else
                {
                    Items.Add(new ListItem(language, language));
                }
            }
        }
    }
}