using Microsoft.CodeAnalysis.CSharp;
using Basic.Reference.Assemblies;
using System.Reflection;

var code = @"
using System;
public static class Example
{
    public static void Main()
    {
        var tuple = (Part1: ""hello"", Part2: ""world"");
        Console.WriteLine($""{tuple.Part1} {tuple.Part2}"");
    }
}
";

var compilation = CSharpCompilation
    .Create(
        "HelloWorld.dll",
        new[] { CSharpSyntaxTree.ParseText(code) },
        references: ReferenceAssemblies.Net50);

using var stream = new MemoryStream();
var emitResults = compilation.Emit(stream);
stream.Position = 0;
var assembly = Assembly.Load(stream.ToArray());
var method = assembly.GetType("Example")!.GetMethod("Main");
method!.Invoke(null, null); // Prints "Hello World"
