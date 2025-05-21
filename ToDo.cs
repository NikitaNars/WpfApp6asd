using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp6
{
    public class ToDo
    {
        private string Name { get; set; }
        public DateTime DateImpl { get; set; }
        public string Description { get; set; }

        public ToDo(string name, DateTime dateImpl, string description)
        {
            Name = name;
            DateImpl = dateImpl;
            Description = description;
        }
        public string GetName { get { return Name; } }
        public DateTime GetDateImpl { get { return DateImpl; } }
        public string GetDescription { get { return Description; } }

    }
}
