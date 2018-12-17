using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TestsGeneratorLib
{
    public class TestsGenerator
    {
        private Config config;

        public TestsGenerator(Config _config)
        {
            config = _config;
        }

        public async Task Generate(List<string> inputFiles, string outputPath)
        {
            var linkOptions = new DataflowLinkOptions();
            linkOptions.PropagateCompletion = true;
            var readOptions = new ExecutionDataflowBlockOptions();
            readOptions.MaxDegreeOfParallelism = config.CountOfReadThreads;
            var processOptions = new ExecutionDataflowBlockOptions();
            processOptions.MaxDegreeOfParallelism = config.CountOfProcessThreads;
            var writeOptions = new ExecutionDataflowBlockOptions();
            writeOptions.MaxDegreeOfParallelism = config.CountOfWriteThreads;

            var readBlock = new TransformBlock<string, string>(fileName => AsyncReader.Read(fileName), readOptions);
            var processBlock = new TransformBlock<string, List<TestInfo>>(sourceCode => GenerateTests(sourceCode), processOptions);
            var writeBlock = new ActionBlock<List<TestInfo>>(output => AsyncWriter.Write(outputPath, output).Wait(), writeOptions);

            readBlock.LinkTo(processBlock, linkOptions);
            processBlock.LinkTo(writeBlock, linkOptions);
            foreach (string file in inputFiles)
            {
                readBlock.Post(file);
            }
            readBlock.Complete();
            await writeBlock.Completion;
        }

        public List<TestInfo> GenerateTests(string sourceCode)
        {
            var parcer = new SourceCodeParcer();
            List<ClassInfo> res = parcer.Parce(sourceCode);
            // tests generation
            var tmplGenerator = new TemplateGenerator();
            List<TestInfo> tests = tmplGenerator.MakeTemplates(res);
            return tests;
        }
    }
}
