using NUnit.Framework;
using System;

namespace BNSharp.Tests
{
    [TestFixture, Category("BNDH")]
    public class BNDHTests
    {
        [SetUp]
        public void Setup()
        {
        }

        private static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "").ToLowerInvariant();
        }

        [Test]
        public void DHTest()
        {
            var groups = Fixtures.DhGroups;
            foreach (var groupKVP in groups) {
                var name = groupKVP.Key;
                var group = groups[name];

                var @base = new BN(2);
                var mont = BN.Red(new BN(group["prime"], 16));
                var priv = new BN(group["priv"], 16);
                var multed = @base.ToRed(mont).RedPow(priv).FromRed();
                var actual = ByteArrayToString(multed.ToArray());
                Assert.AreEqual(actual, group["pub"]);
            };

            Assert.Pass();
        }
    }
}