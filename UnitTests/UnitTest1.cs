using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using TestsGeneratorLib;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private List<ClassInfo> parcedClasses;
        private List<TestInfo> generatedTests;

        [TestInitialize]
        public void Initialize()
        {
            string path = "TestClass.cs";

            int countOfReadThreads = 2;
            int countOfProcessThreads = 3;
            int countOfWriteThreads = 4;
            var сonfig = new Config(countOfReadThreads, countOfProcessThreads, countOfWriteThreads);
            var generator = new TestsGenerator(сonfig);

            string sourceCode;
            using (StreamReader strmReader = new StreamReader(path))
            {
                sourceCode = strmReader.ReadToEnd();
            }

            var parcer = new SourceCodeParcer();
            parcedClasses = parcer.Parce(sourceCode);
            generatedTests = generator.GenerateTests(sourceCode);
        }

        [TestMethod]
        public void ClassAmountTest()
        {
            Assert.AreEqual(parcedClasses.Count, 2);
        }

        [TestMethod]
        public void ClassEqualNamespacesTest()
        {
            Assert.AreEqual(parcedClasses[0].ClassNamespace, parcedClasses[1].ClassNamespace);
        }

        [TestMethod]
        public void ClassInfoTest()
        {
            Assert.AreEqual(parcedClasses[0].ClassNamespace, "UnitTests");
            Assert.AreEqual(parcedClasses[1].ClassNamespace, "UnitTests");
            Assert.AreEqual(parcedClasses[0].ClassName, "ServerObject");
            Assert.AreEqual(parcedClasses[1].ClassName, "ClientObject");
            Assert.AreEqual(parcedClasses[0].ClassMethods[0], "AddConnection");
            Assert.AreEqual(parcedClasses[1].ClassMethods[0], "Process");
        }

        [TestMethod]
        public void TestClassesAmountTest()
        {
            Assert.AreEqual(generatedTests.Count, 2);
        }

        [TestMethod]
        public void TestNameTest()
        {
            Assert.AreEqual(generatedTests[0].TestName, "ServerObjectTest.cs");
            Assert.AreEqual(generatedTests[1].TestName, "ClientObjectTest.cs");
        }

        [TestMethod]
        public void TestContentTest()
        {
            generatedTests[0].TestContent.Contains("UnitTests.Tests");
            generatedTests[1].TestContent.Contains("UnitTests.Tests");
            generatedTests[0].TestContent.Contains("ServerObjectTest");
            generatedTests[1].TestContent.Contains("ClientObjectTest");
            generatedTests[0].TestContent.Contains("AddConnectionTest");
            generatedTests[1].TestContent.Contains("RemoveConnectionTest");
        }
    }
}
