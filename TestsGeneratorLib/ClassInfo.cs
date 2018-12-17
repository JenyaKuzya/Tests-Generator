using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLib
{
    public class ClassInfo
    {
        public ClassInfo(string className, string classNamespace, List<string> classMethods)
        {
            ClassName = className;
            ClassNamespace = classNamespace;
            ClassMethods = classMethods;
        }

        public string ClassName { get; set; }
        public string ClassNamespace { get; set; }
        public List<string> ClassMethods { get; set; }
    }
}
