using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllParser.Core.TestEntityes
{
    class ClassA
    {
        private string somePrivateField;
        public string someField;

        public delegate void SampleEventHandler(object sender);

        public event SampleEventHandler SampleEvent;

        public ClassB B { get; set; }

        public ClassC C { get; set; }

        public string SomeStringPropertyA { get; set; }

        public void SomeMethodA()
        {

        }
    }
}
