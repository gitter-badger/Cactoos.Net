﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cactoos.IO;

using static System.Collections.Generic.Create;
using static System.Functional.FlowControl;
using System.IO;
using Cactoos.Text;
using System.Linq;

namespace Test.IO
{
    [TestClass]
    public class OutputAsEnumerableTest
    {
        [TestMethod]
        public void should_write_to_output()
        {
            array<byte>(
                new OutputEnumerable(
                    new StringInput("nice try fascist"),
                    new PathOutput("file2.txt", FileMode.Truncate)
                )
            );
            Assert.AreEqual("nice try fascist", 
                new BytesAsText(
                    new InputEnumerable(
                        new PathInput("file2.txt")
                    )
                ).String());
        }

        [TestMethod]
        public void should_read_from_input()
        {
            byte[] trg = array<byte>(1024);
            var name = Path.GetTempFileName();
            File.WriteAllBytes(name, array<byte>(0, 1, 2, 2, 2, 5));
            use(
                new OutputEnumerable(
                    new PathInput(name),
                    new BytesAsOutput(trg)
                ),
                output => array(output)
            );
            var test = new InputEnumerable(new PathInput(name)).ToArray();
                
            Assert.IsTrue(test.SequenceEqual(part(trg, 0, test.Length)));
        }
    }
}