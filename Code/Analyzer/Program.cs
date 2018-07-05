using Buildalyzer;
using Buildalyzer.Workspaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace Analyzer
{
    class Program
    {
        private const string MVCAPP = @"../../../../MVCApp/MVCApp.csproj";
        static void Main(string[] args)
        {
            var mgr = new AnalyzerManager(new AnalyzerManagerOptions());

            var proj = mgr.GetProject(MVCAPP);
            var cop = proj.Compile();

            Workspace workspace = mgr.GetWorkspace();
            Compilation compilation = workspace.CurrentSolution.Projects.First().GetCompilationAsync().Result;
            var controllerSymbols = compilation.GetSymbolsWithName(x => x.EndsWith("Controller"));

            foreach (var controller in controllerSymbols)
            {
                foreach (var controllerTree in controller.DeclaringSyntaxReferences)
                {
                    AnalyseController(controllerTree.SyntaxTree);
                }
            }
        }

        private static void AnalyseController(SyntaxTree tree)
        {
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var publicmethods = root.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Where(x => x.Modifiers.Any(SyntaxKind.PublicKeyword));

            foreach (var method in publicmethods)
            {
                var attributes = method.AttributeLists.SelectMany(x => x.Attributes);
                if (attributes.Any(x => x.Name.ToString() == "HttpPost"))
                {
                    if (!attributes.Any(x => x.Name.ToString() == "ValidateAntiForgeryToken"))
                    {
                        //No CSRF token
                    }

                    var invocations = method.DescendantNodes().OfType<MemberAccessExpressionSyntax>().Where(x => x.Name.ToFullString() == "ModelState.IsValid").ToList();
                    if (invocations.Count == 0)
                    {
                        //If no invocations are found flag method for not validating modelstate.
                    }
                }
            }
        }
    }
}
