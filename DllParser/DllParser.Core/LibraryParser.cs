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

        public List<TypeModel> Types { get; private set; }

        public Dictionary<string, TypeModel> TypesAsDictionary
        {
            get
            {
                var result = new Dictionary<string, TypeModel>();

                foreach (var type in Types)
                {
                    if (result.Keys.Contains(type.Name) == false)
                    {
                        result.Add(type.Name, type);
                    }
                }

                return result;
            }
        }

        public LibraryParser(string filePath)
        {
            _filePath = filePath;

            Assembly asm = Assembly.LoadFrom(_filePath);

            Types = asm.DefinedTypes.Select(a => AssemblyHelper.GetTypeModel(a)).ToList();
        }

        public IEnumerable<TypeModel> Parse()
        {
            var result = new List<TypeModel>();

            foreach (var item in Types)
            {
                var model = AssemblyHelper.InitModelChilds(item, TypesAsDictionary);
                result.Add(model);
            }

            return result;
        }
    }
}
