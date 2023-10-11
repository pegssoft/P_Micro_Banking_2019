using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_Micro_Banking.Models.Menu
{
    public class MenuViewModel
    {
        public int ID { get; set; }
        public string MenuNames { get; set; }
        public string Paths { get; set; }
        public string IconClass { get; set; }
        public string Url { get; set; }
        public string OnClick { get; set; }
        public string ListClass { get; set; }
        public string aClass { get; set; }
        public List<MenuChildViewModel> Children { get; set; }
    }

    public class Menu
    {
        public int ID { get; set; }
        public string MenuNames { get; set; }
        public string Paths { get; set; }
        public string IconClass { get; set; }
        public string Url { get; set; }
        public string OnClick { get; set; }
        public string ListClass { get; set; }
        public string aClass { get; set; }
    }

    public class MenuChild
    {
        public int Menu_ID { get; set; }
        public string MenuNames { get; set; }
        public string Paths { get; set; }
        public string IconClass { get; set; }
        public string Url { get; set; }
        public string OnClick { get; set; }
        public string ListClass { get; set; }
        public string aClass { get; set; }
    }
    public class MenuChildViewModel
    {
        public int Menu_ID { get; set; }
        public string MenuNames { get; set; }
        public string Paths { get; set; }
        public string IconClass { get; set; }
        public string Url { get; set; }
        public string OnClick { get; set; }
        public string ListClass { get; set; }
        public string aClass { get; set; }
    }
}
