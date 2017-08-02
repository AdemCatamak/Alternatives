using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Alternatives.UnitTest.ExtensionsTestClass
{
    public class DummyClass
    {
        public string StringField { get; set; }
        public int IntField { get; set; }

        public InnerDummyClass InnerClassField { get; set; }
    }

    public class InnerDummyClass
    {
        public string InnerDummyStringField { get; set; }
    }
}