using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDocGen.Common.Extensions;

namespace XDocGen.Common.Doc
{
    public class NamespaceMember : Member
    {
        public NamespaceMember(ISymbol symbol, IList<ClassMember> classMembers, IList<NamespaceMember> innerMembers) : base(symbol.Name, symbol.GetDocumentationCommentXml())
        {
            this.members = classMembers;
            this.innerNamespaceMembers = innerMembers;
            this.kind = symbol.GetNamespaceMemberKind();
        }

        private readonly NamespaceMemberKind kind;
        public NamespaceMemberKind Kind { get { return this.kind; } }

        private readonly IList<ClassMember> members;
        public IList<ClassMember> Members { get { return this.members; } }

        private readonly IList<NamespaceMember> innerNamespaceMembers;
        public IList<NamespaceMember> InnerNamespaceMembers { get { return this.innerNamespaceMembers; } }

        public static NamespaceMember Create(INamespaceOrTypeSymbol symbol)
        {
            IList<ClassMember> classMembers = new List<ClassMember>();
            IList<NamespaceMember> innerMembers = new List<NamespaceMember>();

            foreach (var member in symbol.GetMembers())
            {
                if (member is ITypeSymbol)
                {
                    innerMembers.Add(NamespaceMember.Create((ITypeSymbol)member));
                }
                else if(member.CanBeReferencedByName)
                {
                    classMembers.Add(new ClassMember(member));
                }
            }

            return new NamespaceMember(symbol, classMembers, innerMembers);
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", this.Kind.ToString(), this.Name);
        }
    }
}
