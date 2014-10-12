using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDocGen.Common.Extensions
{
    public static class INamespaceSymbolExtensions
    {
        public static bool DoesContainMemberFromAssembly(this INamespaceSymbol ns, IAssemblySymbol assembly)
        {
            if (ns.GetMembers().Any(m => m.ContainingAssembly == assembly))
            {
                return true;
            }

            foreach (var innerNs in ns.GetNamespaceMembers())
            {
                if (DoesContainMemberFromAssembly(innerNs, assembly))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
