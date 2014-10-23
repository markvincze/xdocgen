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
        public Documentation(IList<Namespace> namespaces)
        {
            this.namespaces = namespaces;
        }

        private readonly IList<Namespace> namespaces;
        public IList<Namespace> Namespaces { get { return this.namespaces; } }

    }
}
