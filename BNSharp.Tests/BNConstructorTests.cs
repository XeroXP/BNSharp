using BNSharp.Tests.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BNSharp.Tests
{
    [TestFixture, Category("BNConstructor")]
    public class BNConstructorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WithSmiInput()
        {
            //should accept one limb number
            Assert.AreEqual(new BN(12345).ToString(16), "3039");

            //should accept two-limb number
            Assert.AreEqual(new BN(0x4123456).ToString(16), "4123456");

            //should accept 52 bits of precision
            var num1 = (long)Math.Pow(2, 52);
            Assert.AreEqual(new BN(num1, 10).ToString(10), num1.ToString(10));

            //should accept max safe integer
            var num2 = (long)Math.Pow(2, 53) - 1;
            Assert.AreEqual(new BN(num2, 10).ToString(10), num2.ToString(10));

            //should not accept an unsafe integer
            var num3 = (long)Math.Pow(2, 53);

            Assert.Throws<BNException>(() => {
                new BN(num3, 10);
            });

            //should accept two-limb LE number
            Assert.AreEqual(new BN(0x4123456, null, Endian.LittleEndian).ToString(16), "56341204");

            Assert.Pass();
        }

        [Test]
        public void WithStringInput()
        {
            //should accept base-16
            Assert.AreEqual(new BN("1A6B765D8CDF", 16).ToString(16), "1a6b765d8cdf");
            Assert.AreEqual(new BN("1A6B765D8CDF", 16).ToString(), "29048849665247");

            //should accept base-hex
            Assert.AreEqual(new BN("FF", "hex").ToString(), "255");

            //should accept base-16 with spaces
            var num1 = "a89c e5af8724 c0a23e0e 0ff77500";
            Assert.AreEqual(new BN(num1, 16).ToString(16), Regex.Replace(num1, " ", ""));

            //should accept long base-16
            var num2 = "123456789abcdef123456789abcdef123456789abcdef";
            Assert.AreEqual(new BN(num2, 16).ToString(16), num2);

            //should accept positive base-10
            Assert.AreEqual(new BN("10654321").ToString(), "10654321");
            Assert.AreEqual(new BN("29048849665247").ToString(16), "1a6b765d8cdf");

            //should accept negative base-10
            Assert.AreEqual(new BN("-29048849665247").ToString(16), "-1a6b765d8cdf");

            //should accept long base-10
            var num3 = "10000000000000000";
            Assert.AreEqual(new BN(num3).ToString(10), num3);

            //should accept base-2
            var base2 = "11111111111111111111111111111111111111111111111111111";
            Assert.AreEqual(new BN(base2, 2).ToString(2), base2);

            //should accept base-36
            var base36 = "zzZzzzZzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz";
            Assert.AreEqual(new BN(base36, 36).ToString(36), base36.ToLowerInvariant());

            //should not overflow limbs during base-10
            var num4 = "65820182292848241686198767302293" +
                "20890292528855852623664389292032";
            Assert.IsTrue(new BN(num4).words[0] < 0x4000000);

            //should accept base-16 LE integer
            Assert.AreEqual(new BN("1A6B765D8CDF", 16, Endian.LittleEndian).ToString(16),
                "df8c5d766b1a");

            //should accept base-16 LE integer with leading zeros
            Assert.AreEqual(new BN("0010", 16, Endian.LittleEndian).ToNumber(), 4096);
            Assert.AreEqual(new BN("-010", 16, Endian.LittleEndian).ToNumber(), -4096);
            Assert.AreEqual(new BN("010", 16, Endian.LittleEndian).ToNumber(), 4096);

            //should not accept wrong characters for base
            Assert.Throws<BNException>(() => {
                new BN("01FF");
            });

            //should not accept decimal
            Assert.Throws<BNException>(() => {
                new BN("10.00", 10); // eslint-disable-line no-new
            });

            Assert.Throws<BNException>(() => {
                new BN("16.00", 16); // eslint-disable-line no-new
            });

            //should not accept non-hex characters
            var list = new List<string>
            {
                "0000000z",
                "000000gg",
                "0000gg00",
                "fffggfff",
                "/0000000",
                "0-000000", // if -, is first, that is OK
                "ff.fffff",
                "hexadecimal"
            };
            list.ForEach((str) =>
            {
                Assert.Throws<BNException>(() => {
                    new BN(str, 16); // eslint-disable-line no-new
                }, "Invalid character in");
            });

            Assert.Pass();
        }

        [Test]
        public void WithArrayInput()
        {
            //should not fail on empty array
            Assert.AreEqual(new BN(new byte[0]).ToString(16), "0");

            //should import/export big endian
            Assert.AreEqual(new BN(new byte[] { 0, 1 }, 16).ToString(16), "1");
            Assert.AreEqual(new BN(new byte[] { 1, 2, 3 }).ToString(16), "10203");
            Assert.AreEqual(new BN(new byte[] { 1, 2, 3, 4 }).ToString(16), "1020304");
            Assert.AreEqual(new BN(new byte[] { 1, 2, 3, 4, 5 }).ToString(16), "102030405");
            Assert.AreEqual(new BN(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }).ToString(16),
                "102030405060708");
            Assert.AreEqual(string.Join(",", new BN(new byte[] { 1, 2, 3, 4 }).ToArray()), "1,2,3,4");
            Assert.AreEqual(string.Join(",", new BN(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }).ToArray()),
                "1,2,3,4,5,6,7,8");

            //should import little endian
            Assert.AreEqual(new BN(new byte[] { 0, 1 }, 16, Endian.LittleEndian).ToString(16), "100");
            Assert.AreEqual(new BN(new byte[] { 1, 2, 3 }, 16, Endian.LittleEndian).ToString(16), "30201");
            Assert.AreEqual(new BN(new byte[] { 1, 2, 3, 4 }, 16, Endian.LittleEndian).ToString(16), "4030201");
            Assert.AreEqual(new BN(new byte[] { 1, 2, 3, 4, 5 }, 16, Endian.LittleEndian).ToString(16),
                "504030201");
            Assert.AreEqual(new BN(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }, Endian.LittleEndian).ToString(16),
                "807060504030201");
            Assert.AreEqual(string.Join(",", new BN(new byte[] { 1, 2, 3, 4 }).ToArray(Endian.LittleEndian)), "4,3,2,1");
            Assert.AreEqual(string.Join(",", new BN(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }).ToArray(Endian.LittleEndian)),
                "8,7,6,5,4,3,2,1");

            //should import big endian with implicit base
            Assert.AreEqual(new BN(new byte[] { 1, 2, 3, 4, 5 }, Endian.LittleEndian).ToString(16), "504030201");

            Assert.Pass();
        }

        [Test]
        public void WithBNInput()
        {
            //should clone BN
            var num = new BN(12345);
            Assert.AreEqual(new BN(num).ToString(10), "12345");

            Assert.Pass();
        }
    }
}