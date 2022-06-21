using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BNSharp.Tests
{
    [TestFixture, Category("BNBinary")]
    public class BNBinaryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Shl()
        {
            //should shl numbers
            // TODO(indutny): add negative numbers when the time will come
            Assert.AreEqual(new BN("69527932928").Shln(13).ToString(16),
                "2060602000000");
            Assert.AreEqual(new BN("69527932928").Shln(45).ToString(16),
                "206060200000000000000");

            //should ushl numbers
            Assert.AreEqual(new BN("69527932928").Ushln(13).ToString(16),
                "2060602000000");
            Assert.AreEqual(new BN("69527932928").Ushln(45).ToString(16),
                "206060200000000000000");

            Assert.Pass();
        }

        [Test]
        public void Shr()
        {
            //should shr numbers
            // TODO(indutny): add negative numbers when the time will come
            Assert.AreEqual(new BN("69527932928").Shrn(13).ToString(16),
                "818180");
            Assert.AreEqual(new BN("69527932928").Shrn(17).ToString(16),
                "81818");
            Assert.AreEqual(new BN("69527932928").Shrn(256).ToString(16),
                "0");

            //should ushr numbers
            Assert.AreEqual(new BN("69527932928").Ushrn(13).ToString(16),
                "818180");
            Assert.AreEqual(new BN("69527932928").Ushrn(17).ToString(16),
                "81818");
            Assert.AreEqual(new BN("69527932928").Ushrn(256).ToString(16),
                "0");

            Assert.Pass();
        }

        [Test]
        public void Bincn()
        {
            //should increment bit
            Assert.AreEqual(new BN(0).Bincn(1).ToString(16), "2");
            Assert.AreEqual(new BN(2).Bincn(1).ToString(16), "4");
            Assert.AreEqual(new BN(2).Bincn(1).Bincn(1).ToString(16),
                new BN(2).Bincn(2).ToString(16));
            Assert.AreEqual(new BN(0xffffff).Bincn(1).ToString(16), "1000001");
            Assert.AreEqual(new BN(2).Bincn(63).ToString(16),
                "8000000000000002");

            Assert.Pass();
        }

        [Test]
        public void Imaskn()
        {
            //should mask bits in-place
            Assert.AreEqual(new BN(0).Imaskn(1).ToString(16), "0");
            Assert.AreEqual(new BN(3).Imaskn(1).ToString(16), "1");
            Assert.AreEqual(new BN("123456789", 16).Imaskn(4).ToString(16), "9");
            Assert.AreEqual(new BN("123456789", 16).Imaskn(16).ToString(16), "6789");
            Assert.AreEqual(new BN("123456789", 16).Imaskn(28).ToString(16), "3456789");

            //should not mask when number is bigger than length
            Assert.AreEqual(new BN(0xe3).Imaskn(56).ToString(16), "e3");
            Assert.AreEqual(new BN(0xe3).Imaskn(26).ToString(16), "e3");

            Assert.Pass();
        }

        [Test]
        public void Testn()
        {
            //should support test specific bit
            var list = new List<string>
            {
                "ff",
                "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff"
            };
            list.ForEach((hex) =>
            {
                var bn = new BN(hex, 16);
                var bl = bn.BitLength();

                for (var i = 0; i < bl; ++i)
                {
                    Assert.AreEqual(bn.Testn(i), true);
                }

                // test off the end
                Assert.AreEqual(bn.Testn(bl), false);
            });

            var xbits = "01111001010111001001000100011101" +
                "11010011101100011000111001011101" +
                "10010100111000000001011000111101" +
                "01011111001111100100011110000010" +
                "01011010100111010001010011000100" +
                "01101001011110100001001111100110" +
                "001110010111";

            var x1 = new BN(
                "23478905234580795234378912401239784125643978256123048348957342"
            );
            for (var i = 0; i < x1.BitLength(); ++i)
            {
                Assert.AreEqual(x1.Testn(i), (xbits.ElementAt(i) == '1'), "Failed @ bit " + i);
            }

            //should have short-cuts
            var x = new BN("abcd", 16);
            Assert.IsTrue(!x.Testn(128));

            Assert.Pass();
        }

        [Test]
        public void And()
        {
            //should and numbers
            Assert.AreEqual(new BN("1010101010101010101010101010101010101010", 2)
                .And(new BN("101010101010101010101010101010101010101", 2))
                .ToString(2), "0");

            //should and numbers of different limb-length
            Assert.AreEqual(
                new BN("abcd0000ffff", 16)
                    .And(new BN("abcd", 16)).ToString(16),
                "abcd");

            Assert.Pass();
        }

        [Test]
        public void Iand()
        {
            //should iand numbers
            Assert.AreEqual(new BN("1010101010101010101010101010101010101010", 2)
                .Iand(new BN("101010101010101010101010101010101010101", 2))
                .ToString(2), "0");
            Assert.AreEqual(new BN("1000000000000000000000000000000000000001", 2)
                .Iand(new BN("1", 2))
                .ToString(2), "1");
            Assert.AreEqual(new BN("1", 2)
                .Iand(new BN("1000000000000000000000000000000000000001", 2))
                .ToString(2), "1");

            Assert.Pass();
        }

        [Test]
        public void Or()
        {
            //should or numbers
            Assert.AreEqual(new BN("1010101010101010101010101010101010101010", 2)
                .Or(new BN("101010101010101010101010101010101010101", 2))
                .ToString(2), "1111111111111111111111111111111111111111");

            //should or numbers of different limb-length
            Assert.AreEqual(
                new BN("abcd00000000", 16)
                    .Or(new BN("abcd", 16)).ToString(16),
                "abcd0000abcd");

            Assert.Pass();
        }

        [Test]
        public void Ior()
        {
            //should ior numbers
            Assert.AreEqual(new BN("1010101010101010101010101010101010101010", 2)
                .Ior(new BN("101010101010101010101010101010101010101", 2))
                .ToString(2), "1111111111111111111111111111111111111111");
            Assert.AreEqual(new BN("1000000000000000000000000000000000000000", 2)
                .Ior(new BN("1", 2))
                .ToString(2), "1000000000000000000000000000000000000001");
            Assert.AreEqual(new BN("1", 2)
                .Ior(new BN("1000000000000000000000000000000000000000", 2))
                .ToString(2), "1000000000000000000000000000000000000001");

            Assert.Pass();
        }

        [Test]
        public void Xor()
        {
            //should xor numbers
            Assert.AreEqual(new BN("11001100110011001100110011001100", 2)
                .Xor(new BN("1100110011001100110011001100110", 2))
                .ToString(2), "10101010101010101010101010101010");

            Assert.Pass();
        }

        [Test]
        public void Ixor()
        {
            //should ixor numbers
            Assert.AreEqual(new BN("11001100110011001100110011001100", 2)
                .Ixor(new BN("1100110011001100110011001100110", 2))
                .ToString(2), "10101010101010101010101010101010");
            Assert.AreEqual(new BN("11001100110011001100110011001100", 2)
                .Ixor(new BN("1", 2))
                .ToString(2), "11001100110011001100110011001101");
            Assert.AreEqual(new BN("1", 2)
                .Ixor(new BN("11001100110011001100110011001100", 2))
                .ToString(2), "11001100110011001100110011001101");

            //should and numbers of different limb-length
            Assert.AreEqual(
                new BN("abcd0000ffff", 16)
                    .Xor(new BN("abcd", 16)).ToString(16),
                "abcd00005432");

            Assert.Pass();
        }

        [Test]
        public void Setn()
        {
            //should allow single bits to be set
            Assert.AreEqual(new BN(0).Setn(2, 1).ToString(2), "100");
            Assert.AreEqual(new BN(0).Setn(27, 1).ToString(2),
                "1000000000000000000000000000");
            Assert.AreEqual(new BN(0).Setn(63, 1).ToString(16),
                new BN(1).Iushln(63).ToString(16));
            Assert.AreEqual(new BN("1000000000000000000000000001", 2).Setn(27, 0)
                .ToString(2), "1");
            Assert.AreEqual(new BN("101", 2).Setn(2, 0).ToString(2), "1");

            Assert.Pass();
        }

        [Test]
        public void Notn()
        {
            //should allow bitwise negation
            Assert.AreEqual(new BN("111000111", 2).Notn(9).ToString(2),
                "111000");
            Assert.AreEqual(new BN("000111000", 2).Notn(9).ToString(2),
                "111000111");
            Assert.AreEqual(new BN("111000111", 2).Notn(9).ToString(2),
                "111000");
            Assert.AreEqual(new BN("000111000", 2).Notn(9).ToString(2),
                "111000111");
            Assert.AreEqual(new BN("111000111", 2).Notn(32).ToString(2),
                "11111111111111111111111000111000");
            Assert.AreEqual(new BN("000111000", 2).Notn(32).ToString(2),
                "11111111111111111111111111000111");
            Assert.AreEqual(new BN("111000111", 2).Notn(68).ToString(2),
                "11111111111111111111111111111111" +
                "111111111111111111111111111000111000");
            Assert.AreEqual(new BN("000111000", 2).Notn(68).ToString(2),
                "11111111111111111111111111111111" +
                "111111111111111111111111111111000111");

            Assert.Pass();
        }
    }
}