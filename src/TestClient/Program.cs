using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using XDocGen.Common;
using XDocGen.Common.Doc;
using XDocGen.Common.Extensions;
using XDocGen.Common.Xml;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator generator = Generator.CreateForSolution(@"C:\Workspaces\SourceGit\SmartCasco\src\Server\SmartCasco.sln");

            var doc = generator.GenerateAsync().Result;

            StringBuilder mb = new StringBuilder();

            foreach (var ns in doc.Namespaces)
            {
                mb.AppendFormatLine("# Namespace {0}", ns.Name);

                foreach (var member in ns.Members)
                {
                    Gen(mb, member);

                    foreach (var classMember in member.Members)
                    {

                    }
                }
            }

            File.WriteAllText(@"C:\Temp\xdoc2.md", mb.ToString());
        }

        static void Gen(StringBuilder sb, NamespaceMember member)
        {
            sb.AppendFormatLine("## {0}", member.Name);
            string summary, remarks, returns;
            XmlHelper.GetMainParts(member.XmlDocumentation, out summary, out remarks, out returns);

            if (!String.IsNullOrEmpty(summary))
            {
                sb.AppendLine(summary);
            }

            if (!String.IsNullOrEmpty(remarks))
            {
                sb.AppendLine("### Remarks");
                sb.AppendLine(remarks);
            }

            if (!String.IsNullOrEmpty(returns))
            {
                sb.AppendLine("### Returns");
                sb.AppendLine(returns);
            }

            foreach (var classMember in member.Members)
            {
                Gen(sb, classMember);
            }
        }

        static void Gen(StringBuilder sb, ClassMember member)
        {
            sb.AppendFormatLine("### {0}", member.Name);

            string summary, remarks, returns;
            XmlHelper.GetMainParts(member.XmlDocumentation, out summary, out remarks, out returns);

            if (!String.IsNullOrEmpty(summary))
            {
                sb.AppendLine(summary);
            }

            if (!String.IsNullOrEmpty(remarks))
            {
                sb.AppendLine("#### Remarks");
                sb.AppendLine(remarks);
            }

            if (!String.IsNullOrEmpty(returns))
            {
                sb.AppendLine("#### Returns");
                sb.AppendLine(returns);
            }

            //sb.AppendLine(member.XmlDocumentation);
        }
    }
}