using System;
using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Open_Lab_04._05
{
    [TestFixture]
    public class Tests
    {

        private StringTools tools;
        private bool shouldStop;

        private const int RandRepeatsMin = 1;
        private const int RandRepeatsMax = 10;

        private const int RandStrMinSize = 2;
        private const int RandStrMaxSize = 20;

        private const int RandSeed = 405405405;
        private const int RandTestCasesCount = 97;

        [OneTimeSetUp]
        public void Init()
        {
            tools = new StringTools();
            shouldStop = false;
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure ||
                TestContext.CurrentContext.Result.Outcome == ResultState.Error)
                shouldStop = true;
        }

        [TestCase("mice", 5, "mmmmmiiiiiccccceeeee")]
        [TestCase("hello", 3, "hhheeellllllooo")]
        [TestCase("stop", 1, "stop")]
        public void RepeatTest(string str, int n, string expected) =>
            Assert.That(tools.Repeat(str, n), Is.EqualTo(expected));

        [TestCaseSource(nameof(GetRandom))]
        public void RepeatTestRandom(string str, int n, string expected)
        {
            if (shouldStop)
                Assert.Ignore("Previous test failed!");

            Assert.That(tools.Repeat(str, n), Is.EqualTo(expected));
        }

        private static IEnumerable GetRandom()
        {
            var rand = new Random(RandSeed);

            for (var i = 0; i < RandTestCasesCount; i++)
            {
                var repeats = rand.Next(RandRepeatsMin, RandRepeatsMax + 1);
                var oArr = new char[rand.Next(RandStrMinSize, RandStrMaxSize + 1)];
                var rArr = new char[oArr.Length * repeats];

                for (var j = 0; j < oArr.Length; j++)
                {
                    var ch = (char) rand.Next(' ', 'z' + 1);

                    oArr[j] = ch;
                    for (var k = 0; k < repeats; k++)
                        rArr[j * repeats + k] = ch;
                }

                yield return new TestCaseData(new string(oArr), repeats, new string(rArr));
            }
        }

    }
}
