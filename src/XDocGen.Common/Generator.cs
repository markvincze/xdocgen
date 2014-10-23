using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDocGen.Common.Doc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.MSBuild;
using XDocGen.Common.Extensions;

namespace XDocGen.Common
{
    public class Generator
    {
        private readonly string solutionPath;

        private Generator(string solutionPath)
        {
            this.solutionPath = solutionPath;
        }

        public async Task<Documentation> GenerateAsync()
        {
            MSBuildWorkspace workspace = MSBuildWorkspace.Create();

            var solution = await workspace.OpenSolutionAsync(@"C:\Workspaces\SourceGit\SmartCasco\src\Server\SmartCasco.sln");

            var namespaces = await this.GenerateForProjects(solution);

            return new Documentation(namespaces);
        }

        private async Task<IList<Namespace>> GenerateForProjects(Solution solution)
        {
            Dictionary<string, Namespace> namespaces = new Dictionary<string, Namespace>();

            foreach (var project in solution.Projects)
            {
                var compilation = await project.GetCompilationAsync();

                this.GenerateForProject(compilation.GlobalNamespace, String.Empty, compilation.Assembly, namespaces);
            }

            return namespaces.Values.ToList();
        }

        private void GenerateForProject(INamespaceSymbol ns, string namespaceAccumulator, IAssemblySymbol projectAssembly, Dictionary<string, Namespace> namespaceDict)
        {
            if (!ns.DoesContainMemberFromAssembly(projectAssembly))
            {
                return;
            }

            IList<NamespaceMember> classMembers = new List<NamespaceMember>();

            foreach (var member in ns.GetMembers().Where(m => m.ContainingAssembly == projectAssembly && m.Kind == SymbolKind.NamedType))
            {
                var memberDoc = this.GenerateForNamespaceMember(member);
                classMembers.Add(memberDoc);

                Console.WriteLine(member.Name);
                Console.WriteLine(member.GetDocumentationCommentXml());
            }

            string nsFullName = this.ConcatNamespaceName(namespaceAccumulator, ns.Name);

            this.AddOrMergeNamespace(namespaceDict, new Namespace(nsFullName, classMembers));

            foreach (var innerNs in ns.GetNamespaceMembers())
            {
                this.GenerateForProject(innerNs, nsFullName, projectAssembly, namespaceDict);
            }
        }

        string ConcatNamespaceName(string ancestors, string ns)
        {
            if(String.IsNullOrEmpty(ancestors))
            {
                return ns;
            }
            else
            {
                return String.Format("{0}.{1}", ancestors, ns);
            }
        }

        private void AddOrMergeNamespace(Dictionary<string, Namespace> namespacesDict, Namespace ns)
        {
            if (namespacesDict.ContainsKey(ns.Name))
            {
                namespacesDict[ns.Name] = namespacesDict[ns.Name].Merge(ns);
            }
            else
            {
                namespacesDict.Add(ns.Name, ns);
            }
        }

        private NamespaceMember GenerateForNamespaceMember(INamespaceOrTypeSymbol namespaceMember)
        {
            return NamespaceMember.Create(namespaceMember);
        }

        public static Generator CreateForSolution(string solutionPath)
        {
            return new Generator(solutionPath);
        }
    }
}