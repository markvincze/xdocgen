using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDocGen.Common.Doc;

namespace XDocGen.Common.Extensions
{
    public static class ISymbolExtensions
    {
        public static NamespaceMemberKind GetNamespaceMemberKind(this ISymbol symbol)
        {
            ITypeSymbol typeSymbol = symbol as ITypeSymbol;

            if(typeSymbol != null)
            {
                switch(typeSymbol.TypeKind)
                {
                    case TypeKind.Class: return NamespaceMemberKind.Class;
                    case TypeKind.Delegate: return NamespaceMemberKind.Delegate;
                    case TypeKind.Struct: return NamespaceMemberKind.Struct;
                    case TypeKind.Enum: return NamespaceMemberKind.Enum;
                    case TypeKind.Interface: return NamespaceMemberKind.Interface;
                    default: throw new NotSupportedException("Unknown TypeKind for NamespaceMember.");
                }
            }
            else
            {
                throw new InvalidOperationException("Symbol is not a type symbol.");
            }
        }

        public static ClassMemberKind GetClassMemberKind(this ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Field: return ClassMemberKind.Field;
                case SymbolKind.Property: return ClassMemberKind.Property;
                case SymbolKind.Event: return ClassMemberKind.Event;
                case SymbolKind.Method: return ClassMemberKind.Method;
                default: throw new NotSupportedException("Unknown SymbolKind for ClassMember.");
            }
        }
    }
}