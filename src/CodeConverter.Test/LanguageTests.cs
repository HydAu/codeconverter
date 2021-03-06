﻿using CodeConverter.Common;
using CodeConverter.CSharp;
using CodeConverter.PowerShell;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace CodeConverter.Test
{
    public class LanguageTests : IClassFixture<Fixture>
	{ 
        public LanguageTests(Fixture fixture)
        {

        }

        [Theory]
        [MemberData("TestData", MemberType = typeof(Fixture))]
        public void TestLanguages(ConversionTestCase testCase)
        {
            var mdBuilder = new StringBuilder();
            var sourceLanguage = Enum.GetName(typeof(Language), testCase.SyntaxTreeVisitor.Language);
            var targetLanguage = Enum.GetName(typeof(Language), testCase.CodeWriter.Language);

            var source = ReadTestData(testCase.TestDirectory, testCase.Name, testCase.SyntaxTreeVisitor.Language);
            var target = ReadTestData(testCase.TestDirectory, testCase.Name, testCase.CodeWriter.Language);

            var ast = testCase.SyntaxTreeVisitor.Visit(source);
            var actual = testCase.CodeWriter.Write(ast).Trim();

            Assert.Equal(target, actual);

            mdBuilder.AppendLine($"## {testCase.Description}");
            mdBuilder.AppendLine($"### Source: {sourceLanguage}");
            mdBuilder.AppendLine($"```{sourceLanguage.ToLower()}");
            mdBuilder.AppendLine(source);
            mdBuilder.AppendLine($"```");
            mdBuilder.AppendLine($"### Target: {targetLanguage}");
            mdBuilder.AppendLine($"```{targetLanguage.ToLower()}");
            mdBuilder.AppendLine(actual);
            mdBuilder.AppendLine($"```");
            mdBuilder.AppendLine();

			var outputFile = $"{sourceLanguage}To{targetLanguage}.md";

            var languageTestsMarkdownPath = Path.Combine(Fixture.GetTestDirectory(), @"..\..\..\..\..\docs\" + outputFile);
            File.AppendAllText(languageTestsMarkdownPath, mdBuilder.ToString());
        }

        private string GetLanguageExtension(Language language)
        {
            switch (language)
            {
                case Language.CSharp:
                    return ".cs";
                case Language.PowerShell:
                    return ".ps1";
            }

            throw new NotImplementedException();
        }

        private string ReadTestData(string testDirectory, string testName, Language language)
        {
            var languageName = Enum.GetName(typeof(Language), language);
            var extension = GetLanguageExtension(language);

            var testData = $"{testDirectory}\\{languageName}\\{testName}{extension}";

            if (!File.Exists(testData))
            {
                throw new Exception($"No test data found at {testData}");
            }

            return File.ReadAllText(testData);
        }
    }

    public class Fixture : IDisposable
    {
        public Fixture()
        {
			foreach (var syntaxTreeVisitor in SyntaxTreeVisitors)
			{
				foreach (var codeWriter in CodeWriters)
				{
					if (syntaxTreeVisitor.Language == codeWriter.Language)
					{
						continue;
					}

					var sourceLanguage = Enum.GetName(typeof(Language), syntaxTreeVisitor.Language);
					var targetLanguage = Enum.GetName(typeof(Language), codeWriter.Language);

					var outputFile = $"{sourceLanguage}To{targetLanguage}.md";
					var markdownPath = Path.Combine(GetTestDirectory(), @"..\..\..\..\..\docs\" + outputFile);
					File.Delete(markdownPath);

					var mdBuilder = new StringBuilder();

					mdBuilder.AppendLine("# Language Conversion Tests");
					mdBuilder.AppendLine($"### This file was generated by tests that were run on {DateTime.Now}.");
					mdBuilder.AppendLine($"# Convert {sourceLanguage} to {targetLanguage}");

					File.AppendAllText(markdownPath, mdBuilder.ToString());
				}
			}


        }

        public void Dispose()
        { 
        }

        public static string GetTestDirectory()
        {
            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            return Path.GetDirectoryName(codeBasePath);
        }

		public static IEnumerable<ISyntaxTreeVisitor> SyntaxTreeVisitors { get; private set; }
		public static IEnumerable<CodeWriter> CodeWriters { get; private set; }
		static Fixture()
		{
			SyntaxTreeVisitors = new ISyntaxTreeVisitor[]
			{
				new PowerShellSyntaxTreeVisitor(),
				new CSharpSyntaxTreeVisitor()
			};

			CodeWriters = new CodeWriter[]
			{
				new PowerShellCodeWriter(),
				new CSharpCodeWriter()
			};

			_data = new List<object[]>();

			foreach (var syntaxTreeVisitor in SyntaxTreeVisitors)
			{
				foreach (var codeWriter in CodeWriters)
				{
					if (syntaxTreeVisitor.Language == codeWriter.Language)
					{
						continue;
					}

					var sourceLanguage = Enum.GetName(typeof(Language), syntaxTreeVisitor.Language);
					var targetLanguage = Enum.GetName(typeof(Language), codeWriter.Language);

					var testDirectory = Fixture.GetTestDirectory();
					testDirectory = Path.Combine(Path.Combine(testDirectory, "Languages"), sourceLanguage + "To" + targetLanguage);
					var testManifestFile = Path.Combine(testDirectory, "tests.json");
					var testManifestContent = File.ReadAllText(testManifestFile);
					var testManifest = JsonConvert.DeserializeObject<TestManifest>(testManifestContent);

					foreach (var testCase in testManifest.TestCases)
					{
						_data.Add(new[] { new ConversionTestCase(testCase.Name, testCase.Description, syntaxTreeVisitor, codeWriter, testDirectory) });
					}
				}
			}
		}

		private static readonly List<object[]> _data;
		public static IEnumerable<object[]> TestData
		{
			get { return _data; }
		}
	}

    public class TestManifest
    {
        public Language Source { get; set; }
        public Language Target { get; set; }
        public List<TestCase> TestCases { get; set; }
    }

    public class TestCase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ConversionTestCase : IXunitSerializable
    {
        public ConversionTestCase()
        {

        }
        public ConversionTestCase(string name, string description, ISyntaxTreeVisitor syntaxTreeVisitor, CodeWriter codeWriter, string testDirectory)
        {
            Name = name;
            Description = description;
            SyntaxTreeVisitor = syntaxTreeVisitor;
            CodeWriter = codeWriter;
            TestDirectory = testDirectory;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ISyntaxTreeVisitor SyntaxTreeVisitor { get; set; }
        public CodeWriter CodeWriter { get; set; }
        public string TestDirectory { get; set; }

        public void Deserialize(IXunitSerializationInfo info)
        {
            Name = info.GetValue<string>(nameof(Name));
            Description = info.GetValue<string>(nameof(Description));
            var type = Type.GetType(info.GetValue<string>(nameof(SyntaxTreeVisitor)));
            SyntaxTreeVisitor = Activator.CreateInstance(type) as ISyntaxTreeVisitor;
            type = Type.GetType(info.GetValue<string>(nameof(CodeWriter)));
            CodeWriter = Activator.CreateInstance(type) as CodeWriter;
            TestDirectory = info.GetValue<string>(nameof(TestDirectory));
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(Name), Name);
            info.AddValue(nameof(Description), Description);
            info.AddValue(nameof(SyntaxTreeVisitor), SyntaxTreeVisitor.GetType().AssemblyQualifiedName);
            info.AddValue(nameof(CodeWriter), CodeWriter.GetType().AssemblyQualifiedName);
            info.AddValue(nameof(TestDirectory), TestDirectory);
        }

        public override string ToString()
        {
			var sourceLanguage = Enum.GetName(typeof(Language), SyntaxTreeVisitor.Language);
			var targetLanguage = Enum.GetName(typeof(Language), CodeWriter.Language);

			return $"{sourceLanguage} -> {targetLanguage}: {Name}";
        }
    }
}
