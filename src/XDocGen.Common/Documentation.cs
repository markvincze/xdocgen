using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDocGen.Common.Doc;

namespace XDocGen.Common
{
    public class Documentation
    {
        public readonly IList<Namespace> Namespaces;

        public Documentation(IList<Namespace> namespaces)
        {
            this.Namespaces = namespaces;
        }
    }
}
