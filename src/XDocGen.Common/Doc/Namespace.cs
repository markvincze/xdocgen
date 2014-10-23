using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDocGen.Common.Doc
{
    public class Namespace
    {
        public Namespace(string name, IList<NamespaceMember> members)
        {
            this.name = name;
            this.members = members;
        }

        private readonly string name;
        public string Name { get { return this.name; } }

        private readonly IList<NamespaceMember> members;
        public IList<NamespaceMember> Members { get { return this.members; } }

        internal Namespace Merge(Namespace @namespace)
        {
            List<NamespaceMember> mergedList = new List<NamespaceMember>(this.Members);
            mergedList.AddRange(@namespace.Members);

            return new Namespace(this.Name, mergedList);
        }

        public override string ToString()
        {
            return String.Format("Namespace {0}", this.Name);
        }
    }
}