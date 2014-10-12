﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using XDocGen.Common.Extensions;

namespace XDocGen.Common.Doc
{
    public class ClassMember : Member
    {
        public ClassMember(ISymbol symbol) : base(symbol.Name, symbol.GetDocumentationCommentXml())
        {
            this.Kind = symbol.GetClassMemberKind();
        }

        public ClassMemberKind Kind { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1}", this.Kind.ToString(), this.Name);
        }
    }
}
