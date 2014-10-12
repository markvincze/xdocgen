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
            this.Members = classMembers;
            this.InnerNamespaceMembers = innerMembers;
            this.Kind = symbol.GetNamespaceMemberKind();
        }

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

        public NamespaceMemberKind Kind { get; set; }

        public IList<ClassMember> Members;

        public IList<NamespaceMember> InnerNamespaceMembers { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1}", this.Kind.ToString(), this.Name);
        }
    }
}
