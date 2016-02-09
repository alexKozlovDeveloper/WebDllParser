using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllParser.Core.Models
{
    public class TypeModel
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

        public List<string> Functions { get; set; }
        public List<string> Events { get; set; }
        public List<string> ChildsTypeName { get; set; }

        public List<TypeModel> Childs { get; set; }

        public bool IsHasChild
        {
            get
            {
                return Functions.Count > 0 || Events.Count > 0 || ChildsTypeName.Count > 0;
            }
        }

        public TypeModel()
        {
            Functions = new List<string>();
            Events = new List<string>();
            ChildsTypeName = new List<string>();
            Childs = new List<TypeModel>();
        }
    }
}
