// ============================================================================
// PART A: Project & Structure
// ----------------------------------------------------------------------------
// .csproj   -> The project file. It is XML that tells the .NET SDK how to
//              build this code: what kind of output to produce (Exe/Library),
//              which framework to target, and which compiler switches
//              (ImplicitUsings, Nullable, etc.) are turned on. It also lists
//              NuGet package references and any files to include/exclude.
//              It does NOT contain C# code - it is build configuration only.
//
// Program.cs -> The actual source file containing our C# code. With
//              top-level statements, the statements in this file become the
//              body of the compiler-generated Main() entry point.
//
// obj/       -> "Intermediate" build output. Holds temporary files the
//              compiler produces while building (e.g. per-file .dll pieces,
//              generated AssemblyInfo, cached build state). Safe to delete;
//              it is regenerated on the next build. Should NOT be committed
//              to source control.
//
// bin/       -> "Binary" output folder. Holds the final compiled result you
//              actually run/ship - the .dll/.exe, its .pdb (debug symbols),
//              .deps.json and .runtimeconfig.json. Also safe to delete and
//              regenerate, and also normally excluded from source control.
// ============================================================================
//
// NAMESPACE NOTE:
// The assignment asks for both "top-level statements" AND "a file-scoped
// namespace" in Program.cs. In real C# these two things cannot both live in
// the SAME file: top-level statements must be the only executable content
// at the top of the file, and the compiler generates a hidden
// `internal partial class Program` to hold them in the GLOBAL namespace.
// A file-scoped namespace declaration (`namespace X;`) is not allowed to
// appear before or around top-level statements - if you try it you get
// compiler error CS8805 ("Program using top-level statements must be an
// executable"). So instead:
//   - Program.cs itself stays in the global namespace (required, because it
//     hosts the top-level statements).
//   - Order.cs uses a genuine file-scoped namespace
//     (`namespace CSharpBasicsAssignment;`), which is where the technique
//     actually applies. A file-scoped namespace removes one level of
//     indentation compared to the classic `namespace X { ... }` block
//     because every member of the file is implicitly inside the namespace
//     without needing an extra pair of braces wrapping the whole file.
//   - I "extend" the hidden Program class via `partial class Program` later
//     in this file, which is legal and lets me add real fields/methods to
//     the same type the top-level statements belong to (used for the D1
//     field-scope demo below).
//
// SLN vs SLNX:
// This project ships a classic CSharpBasicsAssignment.sln (the traditional,
// widely-supported text format understood by every version of Visual
// Studio, MSBuild, and CI tooling in use today).
// I did NOT pick the newer .slnx (XML) format. One advantage of .slnx is
// that it's much smaller and cleanly diffable/mergeable in source control
// (no GUIDs, no opaque per-configuration boilerplate), which makes solution
// changes far less painful to review in pull requests.
// ============================================================================

using System;
using CSharpBasicsAssignment; // brings Order (declared with a file-scoped
                                // namespace in Order.cs) into scope here

Console.WriteLine("=== PART A: Project & Structure ===");
Console.WriteLine("See the comments at the top of Program.cs for the written answers.");
Program.FieldScopeMethodA();
Program.FieldScopeMethodB();

Console.WriteLine();
Console.WriteLine("=== PART B: Variables, Types & Casting ===");
RunTypesDemo();

Console.WriteLine();
Console.WriteLine("=== PART C: Value vs. Reference Types ===");
RunValueVsReferenceDemo();

Console.WriteLine();
Console.WriteLine("=== PART D: Scope & Operators ===");
RunScopeAndOperatorsDemo();

Console.WriteLine();
Console.WriteLine("=== PART F: LeetCode 136 - Single Number ===");
RunSingleNumberDemo();

// ============================================================================
// PART B - Variables, Types & Casting
// ============================================================================
static void RunTypesDemo()
{
    // --- Every basic type, plus one inferred with var ---
    int wholeNumber = 42;
    long bigNumber = 9_000_000_000L;
    double doubleValue = 3.14159;
    decimal money = 19.99m;
    bool isReady = true;
    char letter = 'C';
    string text = "Hello, C#";
    var inferred = 2.5f; // compiler infers this is a float

    Console.WriteLine($"int      wholeNumber = {wholeNumber}  (type: {wholeNumber.GetType()})");
    Console.WriteLine($"long     bigNumber   = {bigNumber}  (type: {bigNumber.GetType()})");
    Console.WriteLine($"double   doubleValue = {doubleValue}  (type: {doubleValue.GetType()})");
    Console.WriteLine($"decimal  money       = {money}  (type: {money.GetType()})");
    Console.WriteLine($"bool     isReady     = {isReady}  (type: {isReady.GetType()})");
    Console.WriteLine($"char     letter      = {letter}  (type: {letter.GetType()})");
    Console.WriteLine($"string   text        = {text}  (type: {text.GetType()})");
    Console.WriteLine($"var      inferred    = {inferred}  (type: {inferred.GetType()})");

    // --- Implicit conversion ---
    int smallInt = 100;
    long widenedToLong = smallInt;   // int -> long: no cast needed, a long can
                                      // always hold every possible int value,
                                      // so there is no risk of losing data.
    char someChar = 'A';
    int charAsInt = someChar;        // char -> int: no cast needed either; a
                                      // char is really a 16-bit number under
                                      // the hood, and every char value fits
                                      // safely inside an int.
    Console.WriteLine($"Implicit int->long: {widenedToLong}");
    Console.WriteLine($"Implicit char->int: {charAsInt}");

    // --- Explicit conversion ---
    double pi = 9.87;
    int truncated = (int)pi;              // (int) cast TRUNCATES: it just
                                            // chops off the decimal part, so
                                            // 9.87 becomes 9.
    int rounded = Convert.ToInt32(pi);     // Convert.ToInt32 ROUNDS to the
                                            // nearest whole number (using
                                            // banker's rounding), so 9.87
                                            // becomes 10.
    Console.WriteLine($"(int)pi = {truncated}   Convert.ToInt32(pi) = {rounded}");

    // --- The integer division trap ---
    int intDivision = 5 / 2;        // Both operands are int, so C# performs
                                      // INTEGER division and throws away the
                                      // remainder entirely -> 2.
    double doubleDivision = 5.0 / 2; // Because one operand is a double, the
                                      // whole expression is promoted to
                                      // floating point division -> 2.5.
    Console.WriteLine($"5 / 2 (int) = {intDivision}   5.0 / 2 (double) = {doubleDivision}");

    // --- Boxing / unboxing ---
    int originalInt = 7;
    object boxed = originalInt;      // BOXING: the int value is copied onto
                                      // the heap and wrapped in an object.
    int unboxed = (int)boxed;        // UNBOXING: the value is copied back out
                                      // of the heap object into a plain int.
    Console.WriteLine($"Before boxing: {originalInt}   Boxed (as object): {boxed}   After unboxing: {unboxed}");

    // --- Parsing ---
    int parsedGood = int.Parse("42");
    Console.WriteLine($"int.Parse(\"42\") = {parsedGood}");

    bool parseSucceeded = int.TryParse("abc", out int parsedBad);
    if (parseSucceeded)
    {
        Console.WriteLine($"TryParse succeeded: {parsedBad}");
    }
    else
    {
        Console.WriteLine("TryParse failed for \"abc\" (handled gracefully, no exception thrown).");
    }

    // --- float -> decimal ---
    float sourceFloat = 12.5f;
    // decimal implicitFail = sourceFloat;
    // The line above will NOT compile. The compiler refuses this implicit
    // conversion because float and decimal use completely different internal
    // representations (binary floating point vs. base-10 fixed point) and
    // the conversion can lose precision, so C# forces you to be explicit
    // about acknowledging that risk.
    decimal explicitOk = (decimal)sourceFloat; // explicit cast: allowed
    Console.WriteLine($"(decimal)sourceFloat = {explicitOk}");
}

// ============================================================================
// PART C - Value vs. Reference Types
// ============================================================================
static void RunValueVsReferenceDemo()
{
    Console.WriteLine("-- Experiment 1: struct copy semantics --");
    Point p1 = new Point { X = 1, Y = 2 };
    Point p2 = p1; // COPIES the entire struct's data; p2 is independent of p1

    p2.X = 99;
    Console.WriteLine($"p1.X = {p1.X}   p2.X = {p2.X}");
    // p1.X stays 1 while p2.X becomes 99 because Point is a struct (value
    // type). "Point p2 = p1;" copied p1's actual data into a brand new,
    // separate block of memory for p2. Changing p2 afterward has no way to
    // reach back and affect p1's own copy.

    Console.WriteLine();
    Console.WriteLine("-- Experiment 2: class reference semantics (Order) --");
    Order o1 = new Order
    {
        OrderId = 1001,
        CustomerName = "Ali Hassan",
        Quantity = 3,
        UnitPrice = 250.00m,
        IsPaid = false,
        DiscountPercent = 10,
        ShippingCity = "Cairo",
        Priority = 'H',
        ItemCode = 55012345678L
    };
    o1.CalculateTotal();

    Order o2 = o1; // COPIES only the reference (the heap address), not the
                    // object's data. o1 and o2 now both point at the exact
                    // same Order object on the heap.

    o2.IsPaid = true;
    Console.WriteLine($"o1.IsPaid = {o1.IsPaid}   o2.IsPaid = {o2.IsPaid}");
    // Both are true, because o1 and o2 are two different variables that
    // hold the SAME address - there is only ever one Order object in
    // memory, so a change made through either variable is visible through
    // both (shared heap identity).

    object boxedOrder = o1; // No boxing here: Order is already a reference
                              // type, so this just stores the same address
                              // in a variable of static type "object".
    Order o3 = (Order)boxedOrder;
    Console.WriteLine($"object.ReferenceEquals(o1, o3) = {object.ReferenceEquals(o1, o3)}");

    o2.PrintSummary(); // Reflects IsPaid = true, proving o1/o2/o3 all point
                        // at one single Order object.

    Console.WriteLine();
    Console.WriteLine(
        "Value types (int, bool, struct Point, etc.) are stored directly where they are\n" +
        "declared - typically on the STACK when they are local variables. Reference types\n" +
        "(class instances like Order, arrays, strings) always live on the HEAP; a variable\n" +
        "of a reference type only holds a stack-resident ADDRESS pointing at that heap data.\n" +
        "Assigning one value-type variable to another (\"p2 = p1\") copies the actual bytes of\n" +
        "data, producing two fully independent values. Assigning one reference-type variable\n" +
        "to another (\"o2 = o1\") copies only the address, so both variables end up pointing\n" +
        "at the same underlying object. Storing a reference type inside an \"object\" variable\n" +
        "does not create a new object either - object is itself just another reference type,\n" +
        "so the object variable simply holds a copy of the same address, still pointing at\n" +
        "the one and only heap object that was originally created with \"new\".");
}

// ============================================================================
// PART D - Scope & Operators
// ============================================================================
static void RunScopeAndOperatorsDemo()
{
    Console.WriteLine("-- D1: Scope --");
    Console.WriteLine("(field scope demo already ran at the very top via Program.FieldScopeMethodA/B)");

    MethodScopeExample();

    for (int i = 0; i < 3; i++)
    {
        int insideLoopBody = i * 10; // destroyed at the end of every iteration
        Console.WriteLine($"Loop iteration {i}, insideLoopBody = {insideLoopBody}");
    }
    // Console.WriteLine(i); // COMPILE ERROR (CS0103: "The name 'i' does not
    // exist in the current context"). The for-loop's own braces define a
    // block scope: both the loop variable "i" and anything declared inside
    // the loop body (like "insideLoopBody") are created fresh each time the
    // loop is entered and are torn down the moment execution leaves the
    // loop's block, so nothing outside those braces can see them.

    Console.WriteLine();
    Console.WriteLine("-- D2: Compound assignment operators --");
    int total = 100;
    Console.WriteLine($"start: total = {total}");
    total += 25; Console.WriteLine($"total += 25  -> {total}");
    total -= 10; Console.WriteLine($"total -= 10  -> {total}");
    total *= 3;  Console.WriteLine($"total *= 3   -> {total}");
    total /= 5;  Console.WriteLine($"total /= 5   -> {total}");
    total %= 7;  Console.WriteLine($"total %= 7   -> {total}");
    // "total += 25;" is exactly equivalent to the long form:
    // total = total + 25;

    Console.WriteLine();
    Console.WriteLine("-- D3: Bitwise operators (&, |, ^) --");
    int a = 12; // binary 1100
    int b = 10; // binary 1010
    Console.WriteLine($"a & b = {a & b}"); // 1100 & 1010 = 1000 -> 8
    Console.WriteLine($"a | b = {a | b}"); // 1100 | 1010 = 1110 -> 14
    Console.WriteLine($"a ^ b = {a ^ b}"); // 1100 ^ 1010 = 0110 -> 6
    // a & b : 1100 & 1010 -> compare bit-by-bit, keep 1 only where BOTH
    //         bits are 1  -> 1000 = 8
    // a | b : 1100 | 1010 -> keep 1 where EITHER bit is 1 -> 1110 = 14
    // a ^ b : 1100 ^ 1010 -> keep 1 where the bits DIFFER   -> 0110 = 6
    //
    // Practical difference between & (bitwise) and && (logical): in an
    // if-condition, && short-circuits - if the left operand is false it
    // never even evaluates the right operand - whereas & always evaluates
    // and combines both operands regardless of whether the left one is
    // false.

    static void MethodScopeExample()
    {
        int localOnlyHere = 5; // method scope: only visible inside this method
        Console.WriteLine($"Inside MethodScopeExample, localOnlyHere = {localOnlyHere}");
    }
    // Console.WriteLine(localOnlyHere); // would not compile out here either
}

// ============================================================================
// PART F - LeetCode 136: Single Number
// ============================================================================
static void RunSingleNumberDemo()
{
    int[] testArray1 = { 4, 1, 2, 1, 2 };
    int[] testArray2 = { 7, 3, 9, 3, 7 };

    Console.WriteLine($"FindSingleNumber([4,1,2,1,2]) = {FindSingleNumber(testArray1)}"); // expected 4
    Console.WriteLine($"FindSingleNumber([7,3,9,3,7]) = {FindSingleNumber(testArray2)}"); // expected 9
}

static int FindSingleNumber(int[] nums)
{
    // XOR-ing a number with itself always produces 0 (x ^ x == 0), and
    // XOR-ing any number with 0 leaves it unchanged (x ^ 0 == x). XOR is
    // also commutative/associative, so order doesn't matter. If we XOR
    // every element in the array together, every value that appears exactly
    // twice cancels itself out to 0, and 0 XOR-ed with the one remaining
    // odd-count value just leaves that value behind - so the running XOR
    // result at the end is exactly the single number we're looking for.
    int result = 0;
    foreach (int num in nums)
    {
        result ^= num;
    }
    return result;
}

// ============================================================================
// Supporting types
// ============================================================================

// File-scoped namespace is NOT possible here alongside Program's top-level
// statements (see the note at the top of this file) - this struct simply
// lives in the global namespace next to Program, same as Program itself.
struct Point
{
    public int X;
    public int Y;
}

// Extends the compiler-generated top-level-statements Program class so we
// can attach real fields/methods to it (used for the D1 field-scope demo).
partial class Program
{
    // A private field shared by the whole Program class - this is FIELD
    // SCOPE: it lives for as long as the type exists and any method on the
    // type can see and modify it.
    private static int _sharedCounter = 0;

    public static void FieldScopeMethodA()
    {
        _sharedCounter++;
        Console.WriteLine($"FieldScopeMethodA incremented _sharedCounter to {_sharedCounter}");
    }

    public static void FieldScopeMethodB()
    {
        Console.WriteLine($"FieldScopeMethodB reads the SAME field: _sharedCounter = {_sharedCounter}");
    }
}
