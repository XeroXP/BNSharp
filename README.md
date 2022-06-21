BNSharp
============

![](https://img.shields.io/nuget/v/BNSharp)
![](https://img.shields.io/nuget/dt/BNSharp?color=laim)
![](https://img.shields.io/appveyor/build/XeroXP/bnsharp/master)
![](https://img.shields.io/appveyor/tests/XeroXP/bnsharp/master)

BigNum in C#. Port of [bn.js](https://github.com/indutny/bn.js/). Public domain.


Documentation
=============

* [Overview](#overview)
* [Installation](#installation)
* [Notation](#notation)
  * [Prefixes](#prefixes)
  * [Postfixes](#postfixes)
  * [Examples](#examples)
* [Instructions](#instructions)
  * [Utilities](#utilities)
  * [Arithmetics](#arithmetics)
  * [Bit operations](#bit-operations)
  * [Reduction](#reduction)
* [Fast reduction](#fast-reduction)
  * [Reduction context](#reduction-context)
  * [Converting numbers](#converting-numbers)
  * [Red instructions](#red-instructions)
  * [Number Size](#number-size)
* [System requirements](#system-requirements)
* [Development and testing](#development-and-testing)
* [Contributors](#contributors)


Overview
--------

The primary goal of this project is to produce a translation of bn.js to
C# which is as close as possible to the original implementation.


Installation
------------

You can install BNSharp via [NuGet](https://www.nuget.org/):

package manager:

    $ PM> Install-Package BNSharp

NET CLI:

	$ dotnet add package BNSharp

or [download source code](../../releases).


Notation
--------

### Prefixes

There are several prefixes to instructions that affect the way they work. Here
is the list of them in the order of appearance in the function name:

* `i` - perform operation in-place, storing the result in the host object (on
  which the method was invoked). Might be used to avoid number allocation costs
* `u` - unsigned, ignore the sign of operands when performing operation, or
  always return positive value. Second case applies to reduction operations
  like `Mod()`. In such cases if the result will be negative - modulo will be
  added to the result to make it positive

### Postfixes

* `n` - the argument of the function must be a plain JavaScript
  Number. Decimals are not supported.
* `rn` - both argument and return value of the function are plain JavaScript
  Numbers. Decimals are not supported.

### Examples

* `a.Iadd(b)` - perform addition on `a` and `b`, storing the result in `a`
* `a.Umod(b)` - reduce `a` modulo `b`, returning positive value
* `a.Iushln(13)` - shift bits of `a` left by 13


Instructions
-----

Prefixes/postfixes are put in parens at the end of the line. `endian` - could be
either `LittleEndian` or `BigEndian`.

### Utilities

* `a.Clone()` - clone number
* `a.ToString(base, length)` - convert to base-string and pad with zeroes
* `a.ToNumber()` - convert to Number (limited to 53 bits)
* `a.ToJSON()` - convert to JSON compatible hex string (alias of `ToString(16)`)
* `a.ToArray(endian, length)` - convert to byte `Array`, and optionally zero
  pad to length, throwing if already exceeding
* `a.BitLength()` - get number of bits occupied
* `a.ZeroBits()` - return number of less-significant consequent zero bits
  (example: `1010000` has 4 zero bits)
* `a.ByteLength()` - return number of bytes occupied
* `a.IsNeg()` - true if the number is negative
* `a.IsEven()` - no comments
* `a.IsOdd()` - no comments
* `a.IsZero()` - no comments
* `a.Cmp(b)` - compare numbers and return `-1` (a `<` b), `0` (a `==` b), or `1` (a `>` b)
  depending on the comparison result (`Ucmp`, `Cmpn`)
* `a.Lt(b)` - `a` less than `b` (`n`)
* `a.Lte(b)` - `a` less than or equals `b` (`n`)
* `a.Gt(b)` - `a` greater than `b` (`n`)
* `a.Gte(b)` - `a` greater than or equals `b` (`n`)
* `a.Eq(b)` - `a` equals `b` (`n`)
* `a.ToTwos(width)` - convert to two's complement representation, where `width` is bit width
* `a.FromTwos(width)` - convert from two's complement representation, where `width` is the bit width
* `BN.IsBN(object)` - returns true if the supplied `object` is a BN instance
* `BN.Max(a, b)` - return `a` if `a` bigger than `b`
* `BN.Min(a, b)` - return `a` if `a` less than `b`

### Arithmetics

* `a.Neg()` - negate sign (`i`)
* `a.Abs()` - absolute value (`i`)
* `a.Add(b)` - addition (`i`, `n`, `in`)
* `a.Sub(b)` - subtraction (`i`, `n`, `in`)
* `a.Mul(b)` - multiply (`i`, `n`, `in`)
* `a.Sqr()` - square (`i`)
* `a.Pow(b)` - raise `a` to the power of `b`
* `a.Div(b)` - divide (`Divn`, `Idivn`)
* `a.Mod(b)` - reduct (`u`, `n`) (but no `Umodn`)
* `a.Divmod(b)` - quotient and modulus obtained by dividing
* `a.DivRound(b)` - rounded division

### Bit operations

* `a.Or(b)` - or (`i`, `u`, `iu`)
* `a.And(b)` - and (`i`, `u`, `iu`, `Andln`) (NOTE: `Andln` is going to be replaced
  with `Andn` in future)
* `a.Xor(b)` - xor (`i`, `u`, `iu`)
* `a.Setn(b, value)` - set specified bit to `value`
* `a.Shln(b)` - shift left (`i`, `u`, `iu`)
* `a.Shrn(b)` - shift right (`i`, `u`, `iu`)
* `a.Testn(b)` - test if specified bit is set
* `a.Maskn(b)` - clear bits with indexes higher or equal to `b` (`i`)
* `a.Bincn(b)` - add `1 << b` to the number
* `a.Notn(w)` - not (for the width specified by `w`) (`i`)

### Reduction

* `a.Gcd(b)` - GCD
* `a.Egcd(b)` - Extended GCD results (`{ A: ..., B: ..., Gcd: ... }`)
* `a.Invm(b)` - inverse `a` modulo `b`


Fast reduction
-----

When doing lots of reductions using the same modulo, it might be beneficial to
use some tricks: like [Montgomery multiplication][0], or using special algorithm
for [Mersenne Prime][1].

### Reduction context

To enable this trick one should create a reduction context:

```csharp
var red = BN.Red(num);
```
where `num` is just a BN instance.

Or:

```csharp
var red = BN.Red(primeName);
```

Where `primeName` is either of these [Mersenne Primes][1]:

* `'k256'`
* `'p224'`
* `'p192'`
* `'p25519'`

Or:

```csharp
var red = BN.Mont(num);
```

To reduce numbers with [Montgomery trick][0]. `.Mont()` is generally faster than
`.Red(num)`, but slower than `BN.Red(primeName)`.

### Converting numbers

Before performing anything in reduction context - numbers should be converted
to it. Usually, this means that one should:

* Convert inputs to reducted ones
* Operate on them in reduction context
* Convert outputs back from the reduction context

Here is how one may convert numbers to `red`:

```csharp
var redA = a.ToRed(red);
```
Where `red` is a reduction context created using instructions above

Here is how to convert them back:

```csharp
var a = redA.FromRed();
```

### Red instructions

Most of the instructions from the very start of this readme have their
counterparts in red context:

* `a.RedAdd(b)`, `a.RedIAdd(b)`
* `a.RedSub(b)`, `a.RedISub(b)`
* `a.RedShl(num)`
* `a.RedMul(b)`, `a.RedIMul(b)`
* `a.RedSqr()`, `a.RedISqr()`
* `a.RedSqrt()` - square root modulo reduction context's prime
* `a.RedInvm()` - modular inverse of the number
* `a.RedNeg()`
* `a.RedPow(b)` - modular exponentiation

### Number Size

Optimized for elliptic curves that work with 256-bit numbers.
There is no limitation on the size of the numbers.


System requirements
-------------------

BNSharp supports:

* Net 6


Development and testing
------------------------

Make sure to rebuild projects every time you change code for testing.

### Testing

To run tests:

    $ dotnet test


Contributors
------------

[XeroXP](../../../).


[0]: https://en.wikipedia.org/wiki/Montgomery_modular_multiplication
[1]: https://en.wikipedia.org/wiki/Mersenne_prime
