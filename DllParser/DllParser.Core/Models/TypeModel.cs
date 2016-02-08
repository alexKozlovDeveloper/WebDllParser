using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllParser.Core.Models
{
    public class TypeModel
    {
        public bool IsHasChild { get; set; }
        public string Name { get; set; }

        public IEnumerable<TypeModel> Childs { get; set; }
    }
}
