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
        public Dictionary<string, List<TypeModel>> Namespaces { get; private set; }

        public List<string> NamespaceNames 
        { 
            get 
            {
                return Namespaces.Select(a => a.Key).ToList();
            } 
        }


        //public Dictionary<string, TypeModel> TypesAsDictionary
        //{
        //    get
        //    {
        //        var result = new Dictionary<string, TypeModel>();

        //        foreach (var type in Types)
        //        {
        //            if (result.Keys.Contains(type.Name) == false)
        //            {
        //                result.Add(type.Name, type);
        //            }
        //        }

        //        return result;
        //    }
        //}

        public LibraryParser(string filePath)
        {
            _filePath = filePath;

            Assembly asm = Assembly.LoadFrom(_filePath);

            Types = new List<TypeModel>();
            Namespaces = new Dictionary<string, List<TypeModel>>();

            foreach (var item in asm.DefinedTypes)
            {
                var model = new TypeModel(item);

                if (string.IsNullOrEmpty(model.Namespace) == true) { continue; }                

                if (Namespaces.Keys.Contains(model.Namespace) == false)
                {
                    Namespaces.Add(model.Namespace, new List<TypeModel>());
                }

                Namespaces[model.Namespace].Add(model);
                Types.Add(model);
            }
        }

        //public IEnumerable<TypeModel> Parse()
        //{
        //    var result = new List<TypeModel>();

        //    foreach (var item in Types)
        //    {
        //        var model = InitModelChilds(item);
        //        result.Add(model);
        //    }

        //    return result;
        //}

        //public Dictionary<string, List<TypeModel>> Parse()
        //{


        //    return result;
        //}

        //private TypeModel InitModelChilds(TypeModel model)
        //{
        //    model.PropertiesChilds = GetChilds(model.Properties, TypesAsDictionary);
        //    model.FieldsChilds = GetChilds(model.Fields, TypesAsDictionary);

        //    return model;
        //}

        //private List<TypeModel> GetChilds(IEnumerable<TypeModel> fields, Dictionary<string, TypeModel> types)
        //{
        //    var result = new List<TypeModel>();

        //    foreach (var filed in fields)
        //    {
        //        if (types.Keys.Contains(filed.TypeName))
        //        {
        //            result.Add(types[filed.TypeName]);
        //        }
        //        else
        //        {
        //            result.Add(filed);
        //        }
        //    }

        //    return result;
        //}

        public TypeModel GetTypeModel(string typeName)
        {
            return Types.Where(a => a.Name == typeName).FirstOrDefault();
        }
    }
}
