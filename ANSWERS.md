# Part G — Short Answers

## Q1. Paste your `.csproj` contents and confirm the four properties are present.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
```

All four required properties are present:
- **OutputType** = `Exe` (produces a runnable console app, not a library).
- **TargetFramework** = `net8.0`.
- **ImplicitUsings** = `enable` (auto-adds common `using` directives like
  `System`).
- **Nullable** = `enable` (turns on nullable-reference-type warnings).

## Q2. Do `#region`/`#endregion` change the compiled output? Why might you still use them?

No. `#region`/`#endregion` are purely a **compiler pre-processor / editor
hint** — the compiler strips them out before generating any IL, so they have
zero effect on the compiled assembly or the program's runtime behavior.
They're still worth using because they let you collapse large, related
blocks of code (e.g. "all the Part B demo code") in the IDE's outline view,
which makes long files easier to navigate and review without changing what
actually gets built or run.

## Q3. When would you reach for `///` XML doc comments instead of a plain `//`?

Use `///` XML doc comments on **public APIs** — public classes, methods,
properties, and parameters that other code (or other developers) will call
without necessarily reading the implementation. They get picked up by
IntelliSense/tooltips in the IDE and can be exported into generated API
documentation, so they're meant to explain *what a member does and how to
call it* from the outside. Plain `//` comments are better for explaining
*internal* implementation details, reasoning, or "why" notes that only
someone reading the method body needs to see.

## Q4. Why does C# have no true global variables, and what's the closest equivalent?

C# has no true global variables because every single member — fields
included — must be declared inside some type (a class, struct, etc.); the
language has no concept of a variable that exists completely outside of any
type or namespace. This is a deliberate design choice that keeps state
organized, encapsulated, and namespaced, avoiding the naming collisions and
uncontrolled mutation that language-level globals tend to cause in other
languages.

The closest equivalent is a `public static` field (or property) on a class,
often combined with `readonly`/`const` for values that shouldn't change.
Because it's `static`, exactly one copy of it exists for the whole program
and it can be reached from anywhere as `ClassName.FieldName` — functionally
global, but still attached to (and namespaced under) a real type.
