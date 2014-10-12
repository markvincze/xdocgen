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

                this.GenerateForProject(compilation.GlobalNamespace, compilation.Assembly, namespaces);
            }

            return namespaces.Values.ToList();
        }

        private void GenerateForProject(INamespaceSymbol ns, IAssemblySymbol projectAssembly, Dictionary<string, Namespace> namespaceDict)
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

            this.AddOrMergeNamespace(namespaceDict, ns.Name, new Namespace(ns.Name, classMembers));

            foreach (var innerNs in ns.GetNamespaceMembers())
            {
                this.GenerateForProject(innerNs, projectAssembly, namespaceDict);
            }
        }

        private void AddOrMergeNamespace(Dictionary<string, Namespace> namespacesDict, string name, Namespace ns)
        {
            if (namespacesDict.ContainsKey(name))
            {
                namespacesDict[name] = namespacesDict[name].Merge(ns);
            }
            else
            {
                namespacesDict.Add(name, ns);
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