using DllParser.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllParser.Core.Models
{
    public class BaseModel
    {
        protected ModelType _type;        
        public string Name { get; set; }

        public string Type 
        { 
            get
            { 
                return _type.ToString(); 
            } 
        }

        public virtual string Description { get; set; }
        public virtual bool IsHasChild { get; set; }

        public BaseModel(ModelType type)
        {
            _type = type;
        }
    }
}
