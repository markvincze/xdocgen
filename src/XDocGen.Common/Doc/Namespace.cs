using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDocGen.Common.Doc
{
    public class Namespace
    {
        public string FullName;

        public string Name;

        public IList<Namespace> ChildNamespaces;

        public IList<NamespaceMember> Members;

        public Namespace(string name, IList<NamespaceMember> members)
        {
            this.Name = name;
            this.FullName = name;
            this.Members = members;
        }

        internal Namespace Merge(Namespace @namespace)
        {
            List<NamespaceMember> mergedList = new List<NamespaceMember>(this.Members);
            mergedList.AddRange(@namespace.Members);

            return new Namespace(this.Name, mergedList);
        }

        public override string ToString()
        {
            return String.Format("Namespace {0}", this.FullName);
        }
    }
}