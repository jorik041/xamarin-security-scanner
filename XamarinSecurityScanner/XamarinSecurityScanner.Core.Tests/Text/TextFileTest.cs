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

using System.IO;
using XamarinSecurityScanner.Core.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XamarinSecurityScanner.Core.Tests.Text
{
    [TestClass]
    public class TextFileTest
    {
        [TestMethod]
        public void NonExistingFile()
        {
            TextFile textFile = GetTextFile("NonExistingFile.cs");

            string text = textFile.GetText();

            Assert.AreEqual("", text);
        }

        [TestMethod]
        public void SmallerThanMaxSize()
        {
            TextFile textFile = GetTextFile("Example.txt");

            TextFile.MaxSize = 14;
            string text = textFile.GetText();

            Assert.AreEqual("", text);
        }

        [TestMethod]
        public void EqualToMaxSize()
        {
            TextFile textFile = GetTextFile("Example.txt");

            TextFile.MaxSize = 15;
            string text = textFile.GetText();

            Assert.AreEqual("Hello world!", text);
        }

        private static TextFile GetTextFile(string fileName)
        {
            string path = Path.Combine("TestFiles", "Text", fileName);
            return new TextFile(path);
        }
    }
}
