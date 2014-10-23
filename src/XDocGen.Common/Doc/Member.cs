using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDocGen.Common.Doc
{
    public class Member
    {
        public Member(string name, string xmlDoc)
        {
            this.name = name;
            this.xmlDocumentation = xmlDoc;
        }

        private readonly string name;
        public string Name { get { return this.name; } }

        private readonly string xmlDocumentation;
        public string XmlDocumentation { get { return this.xmlDocumentation; } }
    }
}
