/*
Copyright 2019 Info Support B.V.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System.Collections.Generic;
using XamarinSecurityScanner.Core.Cs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using XamarinSecurityScanner.Core.Models;
using XamarinSecurityScanner.Analyzers.Cs;

namespace XamarinSecurityScanner.Analyzers.Tests.Cs
{
    [TestClass]
    public class JavaScriptEnabledAnalyzerTest
    {
        private CsAnalyzer _analyzer;
        private List<Vulnerability> _vulnerabilities;

        [TestInitialize]
        public void Initialize()
        {
            _analyzer = new JavaScriptEnabledAnalyzer();
            _vulnerabilities = new List<Vulnerability>();
            _analyzer.VulnerabilityDiscovered += OnVulnerabilityDiscovered;
        }

        [TestMethod]
        public void WebViewJavaScriptEnabled()
        {
            CsFile csFile = GetCsFile("WebViewJavaScriptEnabled.cs");

            _analyzer.Analyze(csFile);
            
            Assert.AreEqual(1, _vulnerabilities.Count);
            Vulnerability vulnerability = _vulnerabilities[0];
            Assert.AreEqual("JavaScriptEnabled", vulnerability.Code);
            Assert.AreEqual("JavaScript enabled in WebView", vulnerability.Title);
            Assert.AreEqual("Enabling JavaScript in a WebView opens the door to XSS attacks.", vulnerability.Description);
            string expectedPath = Path.Combine("TestFiles", "JavaScriptEnabled", "WebViewJavaScriptEnabled.cs");
            Assert.AreEqual(expectedPath, vulnerability.FilePath);
            Assert.AreEqual("BankingApp.TestFiles.WebViewJavaScriptEnabled", vulnerability.FullyQualifiedName);
            Assert.AreEqual(17, vulnerability.LineNumber);
        }

        [TestMethod]
        public void WebViewJavaScriptEnabledAsVariableName()
        {
            CsFile csFile = GetCsFile("WebViewJavaScriptEnabledAsVariableName.cs");

            _analyzer.Analyze(csFile);
            
            Assert.AreEqual(0, _vulnerabilities.Count());
        }

        private void OnVulnerabilityDiscovered(Vulnerability vulnerability)
        {
            _vulnerabilities.Add(vulnerability);
        }

        private static CsFile GetCsFile(string fileName)
        {
            string path = Path.Combine("TestFiles", "JavaScriptEnabled", fileName);
            return new CsFile(path);
        }
    }
}
