using DllParser.Core.Helpers;
using DllParser.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DllParser.Core
{
    public class LibraryParser
    {
        private string _filePath;

        public LibraryParser(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<TypeModel> Parse()
        {
            var items = new List<TypeModel>();

            var asm = Assembly.LoadFrom(_filePath);

            foreach (TypeInfo type in asm.DefinedTypes)
            {
                items.Add(AssemblyHelper.GetTypeModel(type));
            }

            //foreach (TypeInfo type in asm.DefinedTypes)
            //{
            //    items.Add(new TypeModel
            //    {
            //        Childs = new List<TypeModel>(),
            //        IsHasChild = true,
            //        Name = type.Name
            //    });
            //}

            return items;
        }
    }
}
