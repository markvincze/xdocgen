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
            this.Name = name;
            this.XmlDocumentation = xmlDoc;
        }

        public string Name { get; set; }

        public string XmlDocumentation { get; set; }
    }
}
