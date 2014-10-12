//using Microsoft.CodeAnalysis;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using XDocGen.Common.Doc;

//namespace XDocGen.Common.Extensions
//{
//    public static class SymbolKindExtensions
//    {
//        public static ClassMemberKind ToClassMemberKind(this SymbolKind symbolKind)
//        {
//            switch(symbolKind)
//            {
//                case SymbolKind.Field: return ClassMemberKind.Field;
//                case SymbolKind.Property: return ClassMemberKind.Property;
//                case SymbolKind.Event: return ClassMemberKind.Event;
//                case SymbolKind.Method: return ClassMemberKind.Method;
//                default: throw new NotSupportedException("Unknown SymbolKind.");
//            }
//        }
//    }
//}
