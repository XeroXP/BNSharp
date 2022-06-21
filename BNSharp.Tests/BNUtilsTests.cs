using NUnit.Framework;

namespace BNSharp.Tests
{
    [TestFixture, Category("BNUtils")]
    public class BNUtilsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public new void ToString()
        {
            //hex no padding
            //should have same length as input
            var hex = "1";
            for (var i = 1; i <= 128; i++)
            {
                var n = new BN(hex, 16);
                Assert.AreEqual(n.ToString(16).Length, i);
                hex = hex + "0";
            }

            //binary padding
            //should have a length of 256
            var a1 = new BN(0);

            Assert.AreEqual(a1.ToString(2, 256).Length, 256);

            //hex padding
            //should have length of 8 from leading 15
            var a2 = new BN("ffb9602", 16);

            Assert.AreEqual(a2.ToString("hex", 2).Length, 8);

            //should have length of 8 from leading zero
            var a3 = new BN("fb9604", 16);

            Assert.AreEqual(a3.ToString("hex", 8).Length, 8);

            //should have length of 8 from leading zeros
            var a4 = new BN(0);

            Assert.AreEqual(a4.ToString("hex", 8).Length, 8);

            //should have length of 64 from leading 15
            var a5 = new BN(
                "ffb96ff654e61130ba8422f0debca77a0ea74ae5ea8bca9b54ab64aabf01003",
                16);

            Assert.AreEqual(a5.ToString("hex", 2).Length, 64);

            //should have length of 64 from leading zero
            var a6 = new BN(
                "fb96ff654e61130ba8422f0debca77a0ea74ae5ea8bca9b54ab64aabf01003",
                16);

            Assert.AreEqual(a6.ToString("hex", 64).Length, 64);

            Assert.Pass();
        }

        [Test]
        public void IsNeg()
        {
            //should return true for negative numbers
            Assert.AreEqual(new BN(-1).IsNeg(), true);
            Assert.AreEqual(new BN(1).IsNeg(), false);
            Assert.AreEqual(new BN(0).IsNeg(), false);
            Assert.AreEqual(new BN("-0", 10).IsNeg(), false);

            Assert.Pass();
        }

        [Test]
        public void IsOdd()
        {
            //should return true for odd numbers
            Assert.AreEqual(new BN(0).IsOdd(), false);
            Assert.AreEqual(new BN(1).IsOdd(), true);
            Assert.AreEqual(new BN(2).IsOdd(), false);
            Assert.AreEqual(new BN("-0", 10).IsOdd(), false);
            Assert.AreEqual(new BN("-1", 10).IsOdd(), true);
            Assert.AreEqual(new BN("-2", 10).IsOdd(), false);

            Assert.Pass();
        }

        [Test]
        public void IsEven()
        {
            //should return true for even numbers
            Assert.AreEqual(new BN(0).IsEven(), true);
            Assert.AreEqual(new BN(1).IsEven(), false);
            Assert.AreEqual(new BN(2).IsEven(), true);
            Assert.AreEqual(new BN("-0", 10).IsEven(), true);
            Assert.AreEqual(new BN("-1", 10).IsEven(), false);
            Assert.AreEqual(new BN("-2", 10).IsEven(), true);

            Assert.Pass();
        }

        [Test]
        public void IsZero()
        {
            //should return true for zero
            Assert.AreEqual(new BN(0).IsZero(), true);
            Assert.AreEqual(new BN(1).IsZero(), false);
            Assert.AreEqual(new BN(0xffffffff).IsZero(), false);

            Assert.Pass();
        }

        [Test]
        public void BitLength()
        {
            //should return proper bitLength
            Assert.AreEqual(new BN(0).BitLength(), 0);
            Assert.AreEqual(new BN(0x1).BitLength(), 1);
            Assert.AreEqual(new BN(0x2).BitLength(), 2);
            Assert.AreEqual(new BN(0x3).BitLength(), 2);
            Assert.AreEqual(new BN(0x4).BitLength(), 3);
            Assert.AreEqual(new BN(0x8).BitLength(), 4);
            Assert.AreEqual(new BN(0x10).BitLength(), 5);
            Assert.AreEqual(new BN(0x100).BitLength(), 9);
            Assert.AreEqual(new BN(0x123456).BitLength(), 21);
            Assert.AreEqual(new BN("123456789", 16).BitLength(), 33);
            Assert.AreEqual(new BN("8023456789", 16).BitLength(), 40);

            Assert.Pass();
        }

        [Test]
        public void ByteLength()
        {
            //should return proper byteLength
            Assert.AreEqual(new BN(0).ByteLength(), 0);
            Assert.AreEqual(new BN(0x1).ByteLength(), 1);
            Assert.AreEqual(new BN(0x2).ByteLength(), 1);
            Assert.AreEqual(new BN(0x3).ByteLength(), 1);
            Assert.AreEqual(new BN(0x4).ByteLength(), 1);
            Assert.AreEqual(new BN(0x8).ByteLength(), 1);
            Assert.AreEqual(new BN(0x10).ByteLength(), 1);
            Assert.AreEqual(new BN(0x100).ByteLength(), 2);
            Assert.AreEqual(new BN(0x123456).ByteLength(), 3);
            Assert.AreEqual(new BN("123456789", 16).ByteLength(), 5);
            Assert.AreEqual(new BN("8023456789", 16).ByteLength(), 5);

            Assert.Pass();
        }

        [Test]
        public void ToArray()
        {
            //should return [ 0 ] for `0`
            var n1 = new BN(0);
            CollectionAssert.AreEqual(n1.ToArray(Endian.BigEndian), new byte[] { 0 });
            CollectionAssert.AreEqual(n1.ToArray(Endian.LittleEndian), new byte[] { 0 });

            //should zero pad to desired lengths
            var n2 = new BN(0x123456);
            CollectionAssert.AreEqual(n2.ToArray(Endian.BigEndian, 5), new byte[] { 0x00, 0x00, 0x12, 0x34, 0x56 });
            CollectionAssert.AreEqual(n2.ToArray(Endian.LittleEndian, 5), new byte[] { 0x56, 0x34, 0x12, 0x00, 0x00 });

            //should throw when naturally larger than desired length
            var n3 = new BN(0x123456);
            Assert.Throws<BNException>(() => {
                n3.ToArray(Endian.BigEndian, 2);
            });

            Assert.Pass();
        }

        [Test]
        public void ToNumber()
        {
            //should return proper Number if below the limit
            Assert.AreEqual(new BN(0x123456).ToNumber(), 0x123456);
            Assert.AreEqual(new BN(0x3ffffff).ToNumber(), 0x3ffffff);
            Assert.AreEqual(new BN(0x4000000).ToNumber(), 0x4000000);
            Assert.AreEqual(new BN(0x10000000000000).ToNumber(), 0x10000000000000);
            Assert.AreEqual(new BN(0x10040004004000).ToNumber(), 0x10040004004000);
            Assert.AreEqual(new BN(-0x123456).ToNumber(), -0x123456);
            Assert.AreEqual(new BN(-0x3ffffff).ToNumber(), -0x3ffffff);
            Assert.AreEqual(new BN(-0x4000000).ToNumber(), -0x4000000);
            Assert.AreEqual(new BN(-0x10000000000000).ToNumber(), -0x10000000000000);
            Assert.AreEqual(new BN(-0x10040004004000).ToNumber(), -0x10040004004000);

            //should throw when number exceeds 53 bits
            var n = new BN(1).Iushln(54);
            Assert.Throws<BNException>(() => {
                n.ToNumber();
            });

            Assert.Pass();
        }

        [Test]
        public void ZeroBits()
        {
            //should return proper zeroBits
            Assert.AreEqual(new BN(0).ZeroBits(), 0);
            Assert.AreEqual(new BN(0x1).ZeroBits(), 0);
            Assert.AreEqual(new BN(0x2).ZeroBits(), 1);
            Assert.AreEqual(new BN(0x3).ZeroBits(), 0);
            Assert.AreEqual(new BN(0x4).ZeroBits(), 2);
            Assert.AreEqual(new BN(0x8).ZeroBits(), 3);
            Assert.AreEqual(new BN(0x10).ZeroBits(), 4);
            Assert.AreEqual(new BN(0x100).ZeroBits(), 8);
            Assert.AreEqual(new BN(0x1000000).ZeroBits(), 24);
            Assert.AreEqual(new BN(0x123456).ZeroBits(), 1);

            Assert.Pass();
        }

        [Test]
        public void ToJSON()
        {
            //should return hex string
            Assert.AreEqual(new BN(0x123).ToJSON(), "0123");

            //should be padded to multiple of 2 bytes for interop
            Assert.AreEqual(new BN(0x1).ToJSON(), "01");

            Assert.Pass();
        }

        [Test]
        public void Cmpn()
        {
            //should return -1, 0, 1 correctly
            Assert.AreEqual(new BN(42).Cmpn(42), 0);
            Assert.AreEqual(new BN(42).Cmpn(43), -1);
            Assert.AreEqual(new BN(42).Cmpn(41), 1);
            Assert.AreEqual(new BN(0x3fffffe).Cmpn(0x3fffffe), 0);
            Assert.AreEqual(new BN(0x3fffffe).Cmpn(0x3ffffff), -1);
            Assert.AreEqual(new BN(0x3fffffe).Cmpn(0x3fffffd), 1);
            Assert.Throws<BNException>(() => {
                new BN(0x3fffffe).Cmpn(0x4000000);
            });
            Assert.AreEqual(new BN(42).Cmpn(-42), 1);
            Assert.AreEqual(new BN(-42).Cmpn(42), -1);
            Assert.AreEqual(new BN(-42).Cmpn(-42), 0);
            //?
            //Assert.AreEqual(1 / new BN(-42).cmpn(-42), Infinity);

            Assert.Pass();
        }

        [Test]
        public void Cmp()
        {
            //should return -1, 0, 1 correctly
            Assert.AreEqual(new BN(42).Cmp(new BN(42)), 0);
            Assert.AreEqual(new BN(42).Cmp(new BN(43)), -1);
            Assert.AreEqual(new BN(42).Cmp(new BN(41)), 1);
            Assert.AreEqual(new BN(0x3fffffe).Cmp(new BN(0x3fffffe)), 0);
            Assert.AreEqual(new BN(0x3fffffe).Cmp(new BN(0x3ffffff)), -1);
            Assert.AreEqual(new BN(0x3fffffe).Cmp(new BN(0x3fffffd)), 1);
            Assert.AreEqual(new BN(0x3fffffe).Cmp(new BN(0x4000000)), -1);
            Assert.AreEqual(new BN(42).Cmp(new BN(-42)), 1);
            Assert.AreEqual(new BN(-42).Cmp(new BN(42)), -1);
            Assert.AreEqual(new BN(-42).Cmp(new BN(-42)), 0);
            //?
            //Assert.AreEqual(1 / new BN(-42).cmp(new BN(-42)), Infinity);

            Assert.Pass();
        }

        [Test]
        public void ComparisonShorthands()
        {
            //.gtn greater than
            Assert.AreEqual(new BN(3).Gtn(2), true);
            Assert.AreEqual(new BN(3).Gtn(3), false);
            Assert.AreEqual(new BN(3).Gtn(4), false);

            //.gt greater than
            Assert.AreEqual(new BN(3).Gt(new BN(2)), true);
            Assert.AreEqual(new BN(3).Gt(new BN(3)), false);
            Assert.AreEqual(new BN(3).Gt(new BN(4)), false);

            //.gten greater than or equal
            Assert.AreEqual(new BN(3).Gten(3), true);
            Assert.AreEqual(new BN(3).Gten(2), true);
            Assert.AreEqual(new BN(3).Gten(4), false);

            //.gte greater than or equal
            Assert.AreEqual(new BN(3).Gte(new BN(3)), true);
            Assert.AreEqual(new BN(3).Gte(new BN(2)), true);
            Assert.AreEqual(new BN(3).Gte(new BN(4)), false);

            //.ltn less than
            Assert.AreEqual(new BN(2).Ltn(3), true);
            Assert.AreEqual(new BN(2).Ltn(2), false);
            Assert.AreEqual(new BN(2).Ltn(1), false);

            //.lt less than
            Assert.AreEqual(new BN(2).Lt(new BN(3)), true);
            Assert.AreEqual(new BN(2).Lt(new BN(2)), false);
            Assert.AreEqual(new BN(2).Lt(new BN(1)), false);

            //.lten less than or equal
            Assert.AreEqual(new BN(3).Lten(3), true);
            Assert.AreEqual(new BN(3).Lten(2), false);
            Assert.AreEqual(new BN(3).Lten(4), true);

            //.lte less than or equal
            Assert.AreEqual(new BN(3).Lte(new BN(3)), true);
            Assert.AreEqual(new BN(3).Lte(new BN(2)), false);
            Assert.AreEqual(new BN(3).Lte(new BN(4)), true);

            //.eqn equal
            Assert.AreEqual(new BN(3).Eqn(3), true);
            Assert.AreEqual(new BN(3).Eqn(2), false);
            Assert.AreEqual(new BN(3).Eqn(4), false);

            //.eq equal
            Assert.AreEqual(new BN(3).Eq(new BN(3)), true);
            Assert.AreEqual(new BN(3).Eq(new BN(2)), false);
            Assert.AreEqual(new BN(3).Eq(new BN(4)), false);

            Assert.Pass();
        }

        [Test]
        public void FromTwos()
        {
            //should convert from two\"s complement to negative number
            Assert.AreEqual(new BN("00000000", 16).FromTwos(32).ToNumber(), 0);
            Assert.AreEqual(new BN("00000001", 16).FromTwos(32).ToNumber(), 1);
            Assert.AreEqual(new BN("7fffffff", 16).FromTwos(32).ToNumber(), 2147483647);
            Assert.AreEqual(new BN("80000000", 16).FromTwos(32).ToNumber(), -2147483648);
            Assert.AreEqual(new BN("f0000000", 16).FromTwos(32).ToNumber(), -268435456);
            Assert.AreEqual(new BN("f1234567", 16).FromTwos(32).ToNumber(), -249346713);
            Assert.AreEqual(new BN("ffffffff", 16).FromTwos(32).ToNumber(), -1);
            Assert.AreEqual(new BN("fffffffe", 16).FromTwos(32).ToNumber(), -2);
            Assert.AreEqual(new BN("fffffffffffffffffffffffffffffffe", 16)
                .FromTwos(128).ToNumber(), -2);
            Assert.AreEqual(new BN("ffffffffffffffffffffffffffffffff" +
                "fffffffffffffffffffffffffffffffe", 16).FromTwos(256).ToNumber(), -2);
            Assert.AreEqual(new BN("ffffffffffffffffffffffffffffffff" +
                "ffffffffffffffffffffffffffffffff", 16).FromTwos(256).ToNumber(), -1);
            Assert.AreEqual(
                new BN("7fffffffffffffffffffffffffffffff" +
                "ffffffffffffffffffffffffffffffff", 16).FromTwos(256).ToString(10),
                new BN("5789604461865809771178549250434395392663499" +
                "2332820282019728792003956564819967", 10).ToString(10));
            Assert.AreEqual(
                new BN("80000000000000000000000000000000" +
                "00000000000000000000000000000000", 16).FromTwos(256).ToString(10),
                new BN("-578960446186580977117854925043439539266349" +
                "92332820282019728792003956564819968", 10).ToString(10));

            Assert.Pass();
        }

        [Test]
        public void ToTwos()
        {
            //should convert from negative number to two\"s complement
            Assert.AreEqual(new BN(0).ToTwos(32).ToString(16), "0");
            Assert.AreEqual(new BN(1).ToTwos(32).ToString(16), "1");
            Assert.AreEqual(new BN(2147483647).ToTwos(32).ToString(16), "7fffffff");
            Assert.AreEqual(new BN("-2147483648", 10).ToTwos(32).ToString(16), "80000000");
            Assert.AreEqual(new BN("-268435456", 10).ToTwos(32).ToString(16), "f0000000");
            Assert.AreEqual(new BN("-249346713", 10).ToTwos(32).ToString(16), "f1234567");
            Assert.AreEqual(new BN("-1", 10).ToTwos(32).ToString(16), "ffffffff");
            Assert.AreEqual(new BN("-2", 10).ToTwos(32).ToString(16), "fffffffe");
            Assert.AreEqual(new BN("-2", 10).ToTwos(128).ToString(16),
                "fffffffffffffffffffffffffffffffe");
            Assert.AreEqual(new BN("-2", 10).ToTwos(256).ToString(16),
                "fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffe");
            Assert.AreEqual(new BN("-1", 10).ToTwos(256).ToString(16),
                "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff");
            Assert.AreEqual(
                new BN("5789604461865809771178549250434395392663" +
                "4992332820282019728792003956564819967", 10).ToTwos(256).ToString(16),
                "7fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff");
            Assert.AreEqual(
                new BN("-578960446186580977117854925043439539266" +
                "34992332820282019728792003956564819968", 10).ToTwos(256).ToString(16),
                "8000000000000000000000000000000000000000000000000000000000000000");

            Assert.Pass();
        }

        [Test]
        public void IsBN()
        {
            //should return true for BN
            Assert.AreEqual(BN.IsBN(new BN()), true);

            //should return false for everything else
            Assert.AreEqual(BN.IsBN(1), false);
            Assert.AreEqual(BN.IsBN(new byte[0]), false);
            Assert.AreEqual(BN.IsBN(new { }), false);

            Assert.Pass();
        }
    }
}