using BNSharp.Tests.Extensions;
using NUnit.Framework;
using System;

namespace BNSharp.Tests
{
    [TestFixture, Category("BNArithmetic")]
    public class BNArithmeticTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Add()
        {
            //should add numbers
            Assert.AreEqual(new BN(14).Add(new BN(26)).ToString(16), "28");
            var k1 = new BN(0x1234);
            var r1 = k1;

            for (var i = 0; i < 257; i++)
            {
                r1 = r1.Add(k1);
            }

            Assert.AreEqual(r1.ToString(16), "125868");

            //should handle carry properly (in-place)
            var k2 = new BN("abcdefabcdefabcdef", 16);
            var r2 = new BN("deadbeef", 16);

            for (var i = 0; i < 257; i++)
            {
                r2.Iadd(k2);
            }

            Assert.AreEqual(r2.ToString(16), "ac79bd9b79be7a277bde");

            //should properly do positive + negative
            var a = new BN("abcd", 16);
            var b = new BN("-abce", 16);

            Assert.AreEqual(a.Iadd(b).ToString(16), "-1");

            a = new BN("abcd", 16);
            b = new BN("-abce", 16);

            Assert.AreEqual(a.Add(b).ToString(16), "-1");
            Assert.AreEqual(b.Add(a).ToString(16), "-1");

            Assert.Pass();
        }

        [Test]
        public void Iaddn()
        {
            //should allow a sign change
            var a1 = new BN(-100);
            Assert.AreEqual(a1.Negative, 1);

            a1.Iaddn(200);

            Assert.AreEqual(a1.Negative, 0);
            Assert.AreEqual(a1.ToString(), "100");

            //should add negative number
            var a2 = new BN(-100);
            Assert.AreEqual(a2.Negative, 1);

            a2.Iaddn(-200);

            Assert.AreEqual(a2.ToString(), "-300");

            //should allow neg + pos with big number
            var a3 = new BN("-1000000000", 10);
            Assert.AreEqual(a3.Negative, 1);

            a3.Iaddn(200);

            Assert.AreEqual(a3.ToString(), "-999999800");

            //should carry limb
            var a4 = new BN("3ffffff", 16);

            Assert.AreEqual(a4.Iaddn(1).ToString(16), "4000000");

            //should throw error with num eq 0x4000000
            Assert.Throws<BNException>(() => {
                new BN(0).Iaddn(0x4000000);
            });

            //should reset sign if value equal to value in instance
            var a5 = new BN(-1);
            Assert.AreEqual(a5.Addn(1).ToString(), "0");

            Assert.Pass();
        }

        [Test]
        public void Sub()
        {
            //should subtract small numbers
            Assert.AreEqual(new BN(26).Sub(new BN(14)).ToString(16), "c");
            Assert.AreEqual(new BN(14).Sub(new BN(26)).ToString(16), "-c");
            Assert.AreEqual(new BN(26).Sub(new BN(26)).ToString(16), "0");
            Assert.AreEqual(new BN(-26).Sub(new BN(26)).ToString(16), "-34");

            //
            var a1 = new BN(
                "31ff3c61db2db84b9823d320907a573f6ad37c437abe458b1802cda041d6384" +
                "a7d8daef41395491e2",
                16);
            var b1 = new BN(
                "6f0e4d9f1d6071c183677f601af9305721c91d31b0bbbae8fb790000",
                16);
            var r1 = new BN(
                "31ff3c61db2db84b9823d3208989726578fd75276287cd9516533a9acfb9a67" +
                "76281f34583ddb91e2",
                16);

            //should subtract big numbers
            Assert.AreEqual(a1.Sub(b1).Cmp(r1), 0);

            //should subtract numbers in place
            Assert.AreEqual(b1.Clone().Isub(a1).Neg().Cmp(r1), 0);

            //should subtract with carry
            var a2 = new BN("12345", 16);
            var b2 = new BN("1000000000000", 16);
            Assert.AreEqual(a2.Isub(b2).ToString(16), "-fffffffedcbb");

            a2 = new BN("12345", 16);
            b2 = new BN("1000000000000", 16);
            Assert.AreEqual(b2.Isub(a2).ToString(16), "fffffffedcbb");

            Assert.Pass();
        }

        [Test]
        public void Isubn()
        {
            //should subtract negative number
            var r = new BN(
                "7fffffffffffffffffffffffffffffff5d576e7357a4501ddfe92f46681b", 16);
            Assert.AreEqual(r.Isubn(-1).ToString(16),
                "7fffffffffffffffffffffffffffffff5d576e7357a4501ddfe92f46681c");

            //should work for positive numbers
            var a1 = new BN(-100);
            Assert.AreEqual(a1.Negative, 1);

            a1.Isubn(200);
            Assert.AreEqual(a1.Negative, 1);
            Assert.AreEqual(a1.ToString(), "-300");

            //should not allow a sign change
            var a2 = new BN(-100);
            Assert.AreEqual(a2.Negative, 1);

            a2.Isubn(-200);
            Assert.AreEqual(a2.Negative, 0);
            Assert.AreEqual(a2.ToString(), "100");

            //should change sign on small numbers at 0
            var a3 = new BN(0).Subn(2);
            Assert.AreEqual(a3.ToString(), "-2");

            //should change sign on small numbers at 1
            var a4 = new BN(1).Subn(2);
            Assert.AreEqual(a4.ToString(), "-1");

            //should throw error with num eq 0x4000000
            Assert.Throws<BNException>(() => {
                new BN(0).Isubn(0x4000000);
            });

            Assert.Pass();
        }

        private void MulTestMethod(Func<BN, BN, BN> mul)
        {
            //should multiply numbers of different signs
            var offsets = new int[] {
                1, // smallMulTo
                250, // comb10MulTo
                1000, // bigMulTo
                15000 // jumboMulTo
            };

            for (var i = 0; i < offsets.Length; ++i)
            {
                var x = new BN(1).Ishln(offsets[i]);

                Assert.AreEqual(mul(x, x).IsNeg(), false);
                Assert.AreEqual(mul(x, x.Neg()).IsNeg(), true);
                Assert.AreEqual(mul(x.Neg(), x).IsNeg(), true);
                Assert.AreEqual(mul(x.Neg(), x.Neg()).IsNeg(), false);
            }

            //should multiply with carry
            var n1 = new BN(0x1001);
            var r = n1;

            for (var i = 0; i < 4; i++)
            {
                r = mul(r, n1);
            }

            Assert.AreEqual(r.ToString(16), "100500a00a005001");

            //should correctly multiply big numbers
            var n2 = new BN(
                "79be667ef9dcbbac55a06295ce870b07029bfcdb2dce28d959f2815b16f81798",
                16
            );
            Assert.AreEqual(
                mul(n2, n2).ToString(16),
                "39e58a8055b6fb264b75ec8c646509784204ac15a8c24e05babc9729ab9" +
                "b055c3a9458e4ce3289560a38e08ba8175a9446ce14e608245ab3a9" +
                "978a8bd8acaa40");
            Assert.AreEqual(
                mul(mul(n2, n2), n2).ToString(16),
                "1b888e01a06e974017a28a5b4da436169761c9730b7aeedf75fc60f687b" +
                "46e0cf2cb11667f795d5569482640fe5f628939467a01a612b02350" +
                "0d0161e9730279a7561043af6197798e41b7432458463e64fa81158" +
                "907322dc330562697d0d600");

            //should multiply neg number on 0
            Assert.AreEqual(
                mul(new BN("-100000000000"), new BN("3").Div(new BN("4")))
                    .ToString(16),
                    "0"
            );

            //should regress mul big numbers
            var q = Fixtures.DhGroups["p17"]["q"];
            var qs = Fixtures.DhGroups["p17"]["qs"];

            var qBN = new BN(q, 16);
            Assert.AreEqual(mul(qBN, qBN).ToString(16), qs);
        }

        [Test]
        public void Mul()
        {
            MulTestMethod((x, y) =>
            {
                return x.Mul(y);
            });

            Assert.Pass();
        }

        [Test]
        public void Mulf()
        {
            MulTestMethod((x, y) =>
            {
                return x.Mulf(y);
            });

            Assert.Pass();
        }

        [Test]
        public void Imul()
        {
            //should multiply numbers in-place
            var a1 = new BN("abcdef01234567890abcd", 16);
            var b1 = new BN("deadbeefa551edebabba8", 16);
            var c1 = a1.Mul(b1);

            Assert.AreEqual(a1.Imul(b1).ToString(16), c1.ToString(16));

            a1 = new BN("abcdef01234567890abcd214a25123f512361e6d236", 16);
            b1 = new BN("deadbeefa551edebabba8121234fd21bac0341324dd", 16);
            c1 = a1.Mul(b1);

            Assert.AreEqual(a1.Imul(b1).ToString(16), c1.ToString(16));

            //should multiply by 0
            var a2 = new BN("abcdef01234567890abcd", 16);
            var b2 = new BN("0", 16);
            var c2 = a2.Mul(b2);

            Assert.AreEqual(a2.Imul(b2).ToString(16), c2.ToString(16));

            //should regress mul big numbers in-place
            var q = Fixtures.DhGroups["p17"]["q"];
            var qs = Fixtures.DhGroups["p17"]["qs"];

            var qBN = new BN(q, 16);

            Assert.AreEqual(qBN.Isqr().ToString(16), qs);

            Assert.Pass();
        }

        [Test]
        public void Muln()
        {
            //should multiply number by small number
            var a1 = new BN("abcdef01234567890abcd", 16);
            var b1 = new BN("dead", 16);
            var c1 = a1.Mul(b1);

            Assert.AreEqual(a1.Muln(0xdead).ToString(16), c1.ToString(16));

            //should throw error with num eq 0x4000000
            Assert.Throws<BNException>(() => {
                new BN(0).Imuln(0x4000000);
            });

            //should negate number if number is negative
            var a2 = new BN("dead", 16);
            Assert.AreEqual(a2.Clone().Imuln(-1).ToString(16), a2.Clone().Neg().ToString(16));
            Assert.AreEqual(a2.Clone().Muln(-1).ToString(16), a2.Clone().Neg().ToString(16));

            var b2 = new BN("dead", 16);
            Assert.AreEqual(b2.Clone().Imuln(-42).ToString(16), b2.Clone().Neg().Muln(42).ToString(16));
            Assert.AreEqual(b2.Clone().Muln(-42).ToString(16), b2.Clone().Neg().Muln(42).ToString(16));

            Assert.Pass();
        }

        [Test]
        public void Pow()
        {
            //should raise number to the power
            var a = new BN("ab", 16);
            var b = new BN("13", 10);
            var c = a.Pow(b);

            Assert.AreEqual(c.ToString(16), "15963da06977df51909c9ba5b");

            Assert.Pass();
        }

        [Test]
        public void Div()
        {
            //should divide small numbers (<=26 bits)
            Assert.AreEqual(new BN("256").Div(new BN(10)).ToString(10),
                "25");
            Assert.AreEqual(new BN("-256").Div(new BN(10)).ToString(10),
                "-25");
            Assert.AreEqual(new BN("256").Div(new BN(-10)).ToString(10),
                "-25");
            Assert.AreEqual(new BN("-256").Div(new BN(-10)).ToString(10),
                "25");

            Assert.AreEqual(new BN("10").Div(new BN(256)).ToString(10),
                "0");
            Assert.AreEqual(new BN("-10").Div(new BN(256)).ToString(10),
                "0");
            Assert.AreEqual(new BN("10").Div(new BN(-256)).ToString(10),
                "0");
            Assert.AreEqual(new BN("-10").Div(new BN(-256)).ToString(10),
                "0");

            //should divide large numbers (>53 bits)
            Assert.AreEqual(new BN("1222222225255589").Div(new BN("611111124969028"))
                .ToString(10), "1");
            Assert.AreEqual(new BN("-1222222225255589").Div(new BN("611111124969028"))
                .ToString(10), "-1");
            Assert.AreEqual(new BN("1222222225255589").Div(new BN("-611111124969028"))
                .ToString(10), "-1");
            Assert.AreEqual(new BN("-1222222225255589").Div(new BN("-611111124969028"))
                .ToString(10), "1");

            Assert.AreEqual(new BN("611111124969028").Div(new BN("1222222225255589"))
                .ToString(10), "0");
            Assert.AreEqual(new BN("-611111124969028").Div(new BN("1222222225255589"))
                .ToString(10), "0");
            Assert.AreEqual(new BN("611111124969028").Div(new BN("-1222222225255589"))
                .ToString(10), "0");
            Assert.AreEqual(new BN("-611111124969028").Div(new BN("-1222222225255589"))
                .ToString(10), "0");

            //should divide numbers
            Assert.AreEqual(new BN("69527932928").Div(new BN("16974594")).ToString(16),
                "fff");
            Assert.AreEqual(new BN("-69527932928").Div(new BN("16974594")).ToString(16),
                "-fff");

            var b = new BN(
                "39e58a8055b6fb264b75ec8c646509784204ac15a8c24e05babc9729ab9" +
                "b055c3a9458e4ce3289560a38e08ba8175a9446ce14e608245ab3a9" +
                "978a8bd8acaa40",
                16);
            var n = new BN(
                "79be667ef9dcbbac55a06295ce870b07029bfcdb2dce28d959f2815b16f81798",
                16
            );
            Assert.AreEqual(b.Div(n).ToString(16), n.ToString(16));

            Assert.AreEqual(new BN("1").Div(new BN("-5")).ToString(10), "0");

            //should not fail on regression after moving to _wordDiv
            // Regression after moving to word div
            var p = new BN(
                "fffffffffffffffffffffffffffffffffffffffffffffffffffffffefffffc2f",
                16);
            var a = new BN(
                "79be667ef9dcbbac55a06295ce870b07029bfcdb2dce28d959f2815b16f81798",
                16);
            var @as = a.Sqr();
            Assert.AreEqual(
                @as.Div(p).ToString(16),
                "39e58a8055b6fb264b75ec8c646509784204ac15a8c24e05babc9729e58090b9");

            p = new BN(
                "ffffffff00000001000000000000000000000000ffffffffffffffffffffffff",
                16);
            a = new BN(
                "fffffffe00000003fffffffd0000000200000001fffffffe00000002ffffffff" +
                "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff",
                16);
            Assert.AreEqual(
                a.Div(p).ToString(16),
                "ffffffff00000002000000000000000000000001000000000000000000000001");

            Assert.Pass();
        }

        [Test]
        public void Idivn()
        {
            //should divide numbers in-place
            Assert.AreEqual(new BN("10", 16).Idivn(3).ToString(16), "5");
            Assert.AreEqual(new BN("10", 16).Idivn(-3).ToString(16), "-5");
            Assert.AreEqual(new BN("12", 16).Idivn(3).ToString(16), "6");
            Assert.AreEqual(new BN("10000000000000000").Idivn(3).ToString(10),
                "3333333333333333");
            Assert.AreEqual(
                new BN("100000000000000000000000000000").Idivn(3).ToString(10),
                "33333333333333333333333333333");

            var t = new BN(3);
            Assert.AreEqual(
                new BN("12345678901234567890123456", 16).Idivn(3).ToString(16),
                new BN("12345678901234567890123456", 16).Div(t).ToString(16));

            Assert.Pass();
        }

        [Test]
        public void DivRound()
        {
            //should divide numbers with rounding
            Assert.AreEqual(new BN(9).DivRound(new BN(20)).ToString(10),
                "0");
            Assert.AreEqual(new BN(10).DivRound(new BN(20)).ToString(10),
                "1");
            Assert.AreEqual(new BN(150).DivRound(new BN(20)).ToString(10),
                "8");
            Assert.AreEqual(new BN(149).DivRound(new BN(20)).ToString(10),
                "7");
            Assert.AreEqual(new BN(149).DivRound(new BN(17)).ToString(10),
                "9");
            Assert.AreEqual(new BN(144).DivRound(new BN(17)).ToString(10),
                "8");
            Assert.AreEqual(new BN(-144).DivRound(new BN(17)).ToString(10),
                "-8");

            //should return 1 on exact division
            Assert.AreEqual(new BN(144).DivRound(new BN(144)).ToString(10), "1");

            Assert.Pass();
        }

        [Test]
        public void Mod()
        {
            //should modulo small numbers (<=26 bits)
            Assert.AreEqual(new BN("256").Mod(new BN(10)).ToString(10),
                "6");
            Assert.AreEqual(new BN("-256").Mod(new BN(10)).ToString(10),
                "-6");
            Assert.AreEqual(new BN("256").Mod(new BN(-10)).ToString(10),
                "6");
            Assert.AreEqual(new BN("-256").Mod(new BN(-10)).ToString(10),
                "-6");

            Assert.AreEqual(new BN("10").Mod(new BN(256)).ToString(10),
                "10");
            Assert.AreEqual(new BN("-10").Mod(new BN(256)).ToString(10),
                "-10");
            Assert.AreEqual(new BN("10").Mod(new BN(-256)).ToString(10),
                "10");
            Assert.AreEqual(new BN("-10").Mod(new BN(-256)).ToString(10),
                "-10");

            //should modulo large numbers (>53 bits)
            Assert.AreEqual(new BN("1222222225255589").Mod(new BN("611111124969028"))
                .ToString(10), "611111100286561");
            Assert.AreEqual(new BN("-1222222225255589").Mod(new BN("611111124969028"))
                .ToString(10), "-611111100286561");
            Assert.AreEqual(new BN("1222222225255589").Mod(new BN("-611111124969028"))
                .ToString(10), "611111100286561");
            Assert.AreEqual(new BN("-1222222225255589").Mod(new BN("-611111124969028"))
                .ToString(10), "-611111100286561");

            Assert.AreEqual(new BN("611111124969028").Mod(new BN("1222222225255589"))
                .ToString(10), "611111124969028");
            Assert.AreEqual(new BN("-611111124969028").Mod(new BN("1222222225255589"))
                .ToString(10), "-611111124969028");
            Assert.AreEqual(new BN("611111124969028").Mod(new BN("-1222222225255589"))
                .ToString(10), "611111124969028");
            Assert.AreEqual(new BN("-611111124969028").Mod(new BN("-1222222225255589"))
                .ToString(10), "-611111124969028");

            //should mod numbers
            Assert.AreEqual(new BN("10").Mod(new BN(256)).ToString(16),
                "a");
            Assert.AreEqual(new BN("69527932928").Mod(new BN("16974594")).ToString(16),
                "102f302");

            // 178 = 10 * 17 + 8
            Assert.AreEqual(new BN(178).Div(new BN(10)).ToNumber(), 17);
            Assert.AreEqual(new BN(178).Mod(new BN(10)).ToNumber(), 8);
            Assert.AreEqual(new BN(178).Umod(new BN(10)).ToNumber(), 8);

            // -178 = 10 * (-17) + (-8)
            Assert.AreEqual(new BN(-178).Div(new BN(10)).ToNumber(), -17);
            Assert.AreEqual(new BN(-178).Mod(new BN(10)).ToNumber(), -8);
            Assert.AreEqual(new BN(-178).Umod(new BN(10)).ToNumber(), 2);

            // 178 = -10 * (-17) + 8
            Assert.AreEqual(new BN(178).Div(new BN(-10)).ToNumber(), -17);
            Assert.AreEqual(new BN(178).Mod(new BN(-10)).ToNumber(), 8);
            Assert.AreEqual(new BN(178).Umod(new BN(-10)).ToNumber(), 8);

            // -178 = -10 * (17) + (-8)
            Assert.AreEqual(new BN(-178).Div(new BN(-10)).ToNumber(), 17);
            Assert.AreEqual(new BN(-178).Mod(new BN(-10)).ToNumber(), -8);
            Assert.AreEqual(new BN(-178).Umod(new BN(-10)).ToNumber(), 2);

            // -4 = 1 * (-3) + -1
            Assert.AreEqual(new BN(-4).Div(new BN(-3)).ToNumber(), 1);
            Assert.AreEqual(new BN(-4).Mod(new BN(-3)).ToNumber(), -1);

            // -4 = -1 * (3) + -1
            Assert.AreEqual(new BN(-4).Mod(new BN(3)).ToNumber(), -1);
            // -4 = 1 * (-3) + (-1 + 3)
            Assert.AreEqual(new BN(-4).Umod(new BN(-3)).ToNumber(), 2);

            var p = new BN(
                "ffffffff00000001000000000000000000000000ffffffffffffffffffffffff",
                16);
            var a1 = new BN(
                "fffffffe00000003fffffffd0000000200000001fffffffe00000002ffffffff" +
                "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff",
                16);
            Assert.AreEqual(
                a1.Mod(p).ToString(16),
                "0");

            //should properly carry the sign inside division
            var a2 = new BN("945304eb96065b2a98b57a48a06ae28d285a71b5", "hex");
            var b = new BN(
              "fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffe",
              "hex");

            Assert.AreEqual(a2.Mul(b).Mod(a2).Cmpn(0), 0);

            Assert.Pass();
        }

        [Test]
        public void Modrn()
        {
            //should act like .mod() on small numbers
            Assert.AreEqual(new BN("10", 16).Modrn(256).ToString(16), "10");
            Assert.AreEqual(new BN("10", 16).Modrn(-256).ToString(16), "-10");
            Assert.AreEqual(new BN("100", 16).Modrn(256).ToString(16), "0");
            Assert.AreEqual(new BN("1001", 16).Modrn(256).ToString(16), "1");
            Assert.AreEqual(new BN("100000000001", 16).Modrn(256).ToString(16), "1");
            Assert.AreEqual(new BN("100000000001", 16).Modrn(257).ToString(16),
                new BN("100000000001", 16).Mod(new BN(257)).ToString(16));
            Assert.AreEqual(new BN("123456789012", 16).Modrn(3).ToString(16),
                new BN("123456789012", 16).Mod(new BN(3)).ToString(16));

            Assert.Pass();
        }

        [Test]
        public void Abs()
        {
            //should return absolute value
            Assert.AreEqual(new BN(0x1001).Abs().ToString(), "4097");
            Assert.AreEqual(new BN(-0x1001).Abs().ToString(), "4097");
            Assert.AreEqual(new BN("ffffffff", 16).Abs().ToString(), "4294967295");

            Assert.Pass();
        }

        [Test]
        public void Invm()
        {
            //should invert relatively-prime numbers
            var p = new BN(257);
            var a = new BN(3);
            var b = a.Invm(p);
            Assert.AreEqual(a.Mul(b).Mod(p).ToString(16), "1");

            var p192 = new BN(
                "fffffffffffffffffffffffffffffffeffffffffffffffff",
                16);
            a = new BN("deadbeef", 16);
            b = a.Invm(p192);
            Assert.AreEqual(a.Mul(b).Mod(p192).ToString(16), "1");

            // Even base
            var phi = new BN("872d9b030ba368706b68932cf07a0e0c", 16);
            var e = new BN(65537);
            var d = e.Invm(phi);
            Assert.AreEqual(e.Mul(d).Mod(phi).ToString(16), "1");

            // Even base (take #2)
            a = new BN("5");
            b = new BN("6");
            var r = a.Invm(b);
            Assert.AreEqual(r.Mul(a).Mod(b).ToString(16), "1");

            Assert.Pass();
        }

        [Test]
        public void Gcd()
        {
            //should return GCD
            Assert.AreEqual(new BN(3).Gcd(new BN(2)).ToString(10), "1");
            Assert.AreEqual(new BN(18).Gcd(new BN(12)).ToString(10), "6");
            Assert.AreEqual(new BN(-18).Gcd(new BN(12)).ToString(10), "6");
            Assert.AreEqual(new BN(-18).Gcd(new BN(-12)).ToString(10), "6");
            Assert.AreEqual(new BN(-18).Gcd(new BN(0)).ToString(10), "18");
            Assert.AreEqual(new BN(0).Gcd(new BN(-18)).ToString(10), "18");
            Assert.AreEqual(new BN(2).Gcd(new BN(0)).ToString(10), "2");
            Assert.AreEqual(new BN(0).Gcd(new BN(3)).ToString(10), "3");
            Assert.AreEqual(new BN(0).Gcd(new BN(0)).ToString(10), "0");

            Assert.Pass();
        }

        [Test]
        public void Egcd()
        {
            //should return EGCD
            Assert.AreEqual(new BN(3).Egcd(new BN(2)).Gcd.ToString(10), "1");
            Assert.AreEqual(new BN(18).Egcd(new BN(12)).Gcd.ToString(10), "6");
            Assert.AreEqual(new BN(-18).Egcd(new BN(12)).Gcd.ToString(10), "6");
            Assert.AreEqual(new BN(0).Egcd(new BN(12)).Gcd.ToString(10), "12");

            //should not allow 0 input
            Assert.Throws<BNException>(() => {
                new BN(1).Egcd(new BN(0));
            });

            //should not allow negative input
            Assert.Throws<BNException>(() => {
                new BN(1).Egcd(new BN(-1));
            });

            Assert.Pass();
        }

        [Test]
        public void Max()
        {
            //should return maximum
            Assert.AreEqual(BN.Max(new BN(3), new BN(2)).ToString(16), "3");
            Assert.AreEqual(BN.Max(new BN(2), new BN(3)).ToString(16), "3");
            Assert.AreEqual(BN.Max(new BN(2), new BN(2)).ToString(16), "2");
            Assert.AreEqual(BN.Max(new BN(2), new BN(-2)).ToString(16), "2");

            Assert.Pass();
        }

        [Test]
        public void Min()
        {
            //should return minimum
            Assert.AreEqual(BN.Min(new BN(3), new BN(2)).ToString(16), "2");
            Assert.AreEqual(BN.Min(new BN(2), new BN(3)).ToString(16), "2");
            Assert.AreEqual(BN.Min(new BN(2), new BN(2)).ToString(16), "2");
            Assert.AreEqual(BN.Min(new BN(2), new BN(-2)).ToString(16), "-2");

            Assert.Pass();
        }

        [Test]
        public void Ineg()
        {
            //shouldn\"t change sign for zero
            Assert.AreEqual(new BN(0).Ineg().ToString(10), "0");

            Assert.Pass();
        }
    }
}