using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDocGen.Common;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator generator = Generator.CreateForSolution(@"C:\Workspaces\SourceGit\SmartCasco\src\Server\SmartCasco.sln");

            var doc = generator.GenerateAsync().Result;
        }
    }
}
