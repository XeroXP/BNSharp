using BNSharp.Tests.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BNSharp.Tests
{
    [TestFixture, Category("BNRed")]
    public class BNRedTests
    {
        [SetUp]
        public void Setup()
        {
        }

        private void RedTestMethod(Func<BN, Red> fn)
        {
            //should support add, iadd, sub, isub operations
            var p1 = new BN(257);
            var m1 = fn(p1);
            var a1 = new BN(123).ToRed(m1);
            var b1 = new BN(231).ToRed(m1);

            Assert.AreEqual(a1.RedAdd(b1).FromRed().ToString(10), "97");
            Assert.AreEqual(a1.RedSub(b1).FromRed().ToString(10), "149");
            Assert.AreEqual(b1.RedSub(a1).FromRed().ToString(10), "108");

            Assert.AreEqual(a1.Clone().RedIAdd(b1).FromRed().ToString(10), "97");
            Assert.AreEqual(a1.Clone().RedISub(b1).FromRed().ToString(10), "149");
            Assert.AreEqual(b1.Clone().RedISub(a1).FromRed().ToString(10), "108");

            //should support pow and mul operations
            var p192 = new BN(
                "fffffffffffffffffffffffffffffffeffffffffffffffff",
                16);
            var m2 = fn(p192);
            var a2 = new BN(123);
            var b2 = new BN(231);
            var c2 = a2.ToRed(m2).RedMul(b2.ToRed(m2)).FromRed();
            Assert.IsTrue(c2.Cmp(a2.Mul(b2).Mod(p192)) == 0);

            Assert.AreEqual(a2.ToRed(m2).RedPow(new BN(0)).FromRed()
                .Cmp(new BN(1)), 0);
            Assert.AreEqual(a2.ToRed(m2).RedPow(new BN(3)).FromRed()
                .Cmp(a2.Sqr().Mul(a2)), 0);
            Assert.AreEqual(a2.ToRed(m2).RedPow(new BN(4)).FromRed()
                .Cmp(a2.Sqr().Sqr()), 0);
            Assert.AreEqual(a2.ToRed(m2).RedPow(new BN(8)).FromRed()
                .Cmp(a2.Sqr().Sqr().Sqr()), 0);
            Assert.AreEqual(a2.ToRed(m2).RedPow(new BN(9)).FromRed()
                .Cmp(a2.Sqr().Sqr().Sqr().Mul(a2)), 0);
            Assert.AreEqual(a2.ToRed(m2).RedPow(new BN(17)).FromRed()
                .Cmp(a2.Sqr().Sqr().Sqr().Sqr().Mul(a2)), 0);
            Assert.AreEqual(
                a2.ToRed(m2).RedPow(new BN("deadbeefabbadead", 16)).FromRed()
                    .ToString(16),
                "3aa0e7e304e320b68ef61592bcb00341866d6fa66e11a4d6");

            //should sqrtm numbers
            var p3 = new BN(263);
            var m3 = fn(p3);
            var q3 = new BN(11).ToRed(m3);

            var qr = q3.RedSqrt();
            Assert.AreEqual(qr.RedSqr().Cmp(q3), 0);

            qr = q3.RedSqrt();
            Assert.AreEqual(qr.RedSqr().Cmp(q3), 0);

            //?
            /*p3 = new BN(
                "fffffffffffffffffffffffffffffffeffffffffffffffff",
                16);
            m3 = fn(p3);

            q3 = new BN(13).toRed(m3);
            qr = q3.redSqrt(true, p3);
            Assert.AreEqual(qr.redSqr().cmp(q3), 0);

            qr = q3.redSqrt(false, p3);
            Assert.AreEqual(qr.redSqr().cmp(q3), 0);*/

            // Tonelli-shanks
            p3 = new BN(13);
            m3 = fn(p3);
            q3 = new BN(10).ToRed(m3);
            Assert.AreEqual(q3.RedSqrt().FromRed().ToString(10), "7");

            //should invm numbers
            var p4 = new BN(257);
            var m4 = fn(p4);
            var a4 = new BN(3).ToRed(m4);
            var b4 = a4.RedInvm();
            Assert.AreEqual(a4.RedMul(b4).FromRed().ToString(16), "1");

            //should invm numbers (regression)
            var p5 = new BN(
                "ffffffff00000001000000000000000000000000ffffffffffffffffffffffff",
                16);
            var a5 = new BN(
                "e1d969b8192fbac73ea5b7921896d6a2263d4d4077bb8e5055361d1f7f8163f3",
                16);

            var m5 = fn(p5);
            a5 = a5.ToRed(m5);

            Assert.AreEqual(a5.RedInvm().FromRed().Negative, 0);

            //should imul numbers
            var p6 = new BN(
                "fffffffffffffffffffffffffffffffeffffffffffffffff",
                16);
            var m6 = fn(p6);

            var a6 = new BN("deadbeefabbadead", 16);
            var b6 = new BN("abbadeadbeefdead", 16);
            var c6 = a6.Mul(b6).Mod(p6);

            Assert.AreEqual(a6.ToRed(m6).RedIMul(b6.ToRed(m6)).FromRed().ToString(16),
                c6.ToString(16));

            //should pow(base, 0) == 1
            var base1 = new BN(256).ToRed(BN.Red("k256"));
            var exponent1 = new BN(0);
            var result1 = base1.RedPow(exponent1);
            Assert.AreEqual(result1.ToString(), "1");

            //should shl numbers
            var base2 = new BN(256).ToRed(BN.Red("k256"));
            var result2 = base2.RedShl(1);
            Assert.AreEqual(result2.ToString(), "512");

            //should reduce when converting to red
            var p7 = new BN(257);
            var m7 = fn(p7);
            var a7 = new BN(5).ToRed(m7);

            Assert.DoesNotThrow(() => {
                var b7 = a7.RedISub(new BN(512).ToRed(m7));
                b7.RedISub(new BN(512).ToRed(m7));
            });

            //redNeg and zero value
            var a8 = new BN(0).ToRed(BN.Red("k256")).RedNeg();
            Assert.AreEqual(a8.IsZero(), true);

            //should not allow modulus <= 1
            Assert.Throws<BNException>(() => {
                BN.Red(new BN(0));
            });

            Assert.Throws<BNException>(() => {
                BN.Red(new BN(1));
            });

            Assert.DoesNotThrow(() => {
                BN.Red(new BN(2));
            });
        }

        [Test]
        public void Plain()
        {
            RedTestMethod((num) =>
            {
                return BN.Red(num);
            });

            Assert.Pass();
        }

        [Test]
        public void Montgomery()
        {
            RedTestMethod((num) =>
            {
                return BN.Mont(num);
            });

            Assert.Pass();
        }

        [Test]
        public void PseudoMersennePrimes()
        {
            //should reduce numbers mod k256
            var p1 = BN.Prime("k256");

            Assert.AreEqual(p1.Ireduce(new BN(0xdead)).ToString(16), "dead");
            Assert.AreEqual(p1.Ireduce(new BN("deadbeef", 16)).ToString(16), "deadbeef");

            var num = new BN(
                "fedcba9876543210fedcba9876543210dead" +
                "fedcba9876543210fedcba9876543210dead",
                16);
            var exp = num.Mod(p1.p).ToString(16);
            Assert.AreEqual(p1.Ireduce(num).ToString(16), exp);

            var regr = new BN(
                "f7e46df64c1815962bf7bc9c56128798" +
                "3f4fcef9cb1979573163b477eab93959" +
                "335dfb29ef07a4d835d22aa3b6797760" +
                "70a8b8f59ba73d56d01a79af9",
                16);
            exp = regr.Mod(p1.p).ToString(16);

            Assert.AreEqual(p1.Ireduce(regr).ToString(16), exp);

            //should not fail to invm number mod k256
            var regr2 = new BN(
                "6c150c4aa9a8cf1934485d40674d4a7cd494675537bda36d49405c5d2c6f496f", 16);
            regr2 = regr2.ToRed(BN.Red("k256"));
            Assert.AreEqual(regr2.RedInvm().RedMul(regr2).FromRed().Cmpn(1), 0);

            //should correctly square the number
            var p2 = BN.Prime("k256").p;
            var red = BN.Red("k256");

            var n2 = new BN(
                "9cd8cb48c3281596139f147c1364a3ed" +
                "e88d3f310fdb0eb98c924e599ca1b3c9",
                16);
            var expected = n2.Sqr().Mod(p2);
            var actual2 = n2.ToRed(red).RedSqr().FromRed();

            Assert.AreEqual(actual2.ToString(16), expected.ToString(16));

            //redISqr should return right result
            var n3 = new BN("30f28939", 16);
            var actual3 = n3.ToRed(BN.Red("k256")).RedISqr().FromRed();
            Assert.AreEqual(actual3.ToString(16), "95bd93d19520eb1");

            Assert.Pass();
        }

        private BN Bits2Int(byte[] obits, BN q)
        {
            var bits = new BN(obits);
            var shift = (obits.Length << 3) - q.BitLength();
            if (shift > 0)
            {
                bits.Ishrn(shift);
            }
            return bits;
        }

        private byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        [Test]
        public void ShouldAvoid410Regresion()
        {
            var t = StringToByteArray("aff1651e4cd6036d57aa8b2a05ccf1a9d5a40166340ecbbdc55" +
                "be10b568aa0aa3d05ce9a2fcec9df8ed018e29683c6051cb83e" +
                "46ce31ba4edb045356a8d0d80b");
            var g = new BN(
                "5c7ff6b06f8f143fe8288433493e4769c4d988ace5be25a0e24809670" +
                "716c613d7b0cee6932f8faa7c44d2cb24523da53fbe4f6ec3595892d1" +
                "aa58c4328a06c46a15662e7eaa703a1decf8bbb2d05dbe2eb956c142a" +
                "338661d10461c0d135472085057f3494309ffa73c611f78b32adbb574" +
                "0c361c9f35be90997db2014e2ef5aa61782f52abeb8bd6432c4dd097b" +
                "c5423b285dafb60dc364e8161f4a2a35aca3a10b1c4d203cc76a470a3" +
                "3afdcbdd92959859abd8b56e1725252d78eac66e71ba9ae3f1dd24871" +
                "99874393cd4d832186800654760e1e34c09e4d155179f9ec0dc4473f9" +
                "96bdce6eed1cabed8b6f116f7ad9cf505df0f998e34ab27514b0ffe7",
                16);
            var p = new BN(
                "9db6fb5951b66bb6fe1e140f1d2ce5502374161fd6538df1648218642" +
                "f0b5c48c8f7a41aadfa187324b87674fa1822b00f1ecf8136943d7c55" +
                "757264e5a1a44ffe012e9936e00c1d3e9310b01c7d179805d3058b2a9" +
                "f4bb6f9716bfe6117c6b5b3cc4d9be341104ad4a80ad6c94e005f4b99" +
                "3e14f091eb51743bf33050c38de235567e1b34c3d6a5c0ceaa1a0f368" +
                "213c3d19843d0b4b09dcb9fc72d39c8de41f1bf14d4bb4563ca283716" +
                "21cad3324b6a2d392145bebfac748805236f5ca2fe92b871cd8f9c36d" +
                "3292b5509ca8caa77a2adfc7bfd77dda6f71125a7456fea153e433256" +
                "a2261c6a06ed3693797e7995fad5aabbcfbe3eda2741e375404ae25b",
                16);
            var q = new BN("f2c3119374ce76c9356990b465374a17f23f9ed35089bd969f61c6dde" +
                "9998c1f", 16);
            var k = Bits2Int(t, q);
            var expectedR = "89ec4bb1400eccff8e7d9aa515cd1de7803f2daff09693ee7fd1353e" +
                "90a68307";
            var r = g.ToRed(BN.Mont(p)).RedPow(k).FromRed().Mod(q);
            Assert.AreEqual(r.ToString(16), expectedR);

            Assert.Pass();
        }

        [Test]
        public void K256SplitFor512BitsNumberShouldReturnEqualNumbers()
        {
            var red = BN.Red("k256");
            var input = new BN(1).Iushln(512).Subn(1);
            Assert.AreEqual(input.BitLength(), 512);
            var output = new BN(0);
            red.prime.Split(input, output);
            Assert.AreEqual(input.Cmp(output), 0);

            Assert.Pass();
        }

        [Test]
        public void ImodShouldChangeHostObject ()
        {
            var red = BN.Red(new BN(13));
            var a = new BN(2).ToRed(red);
            var b = new BN(7).ToRed(red);
            var c = a.RedIMul(b);
            Assert.AreEqual(a.ToNumber(), 1);
            Assert.AreEqual(c.ToNumber(), 1);

            Assert.Pass();
        }
    }
}