<<<<<<< HEAD
# AhmedMoharam74-Assignment3
Assignment repo for assignment/1-3 (Assignment3)
=======
# CSharpBasicsAssignment

Assignment 4 — Console Apps, Types & Memory Model (Lecture 01).

## How to run

```bash
dotnet restore
dotnet run
```

Running the project prints every part in order, with a clear `=== PART X ===`
header before each section, exactly matching the structure described below.

## Project layout

| File               | Contents                                                                 |
|--------------------|---------------------------------------------------------------------------|
| `CSharpBasicsAssignment.csproj` | Project file (OutputType, TargetFramework, ImplicitUsings, Nullable). |
| `Program.cs`       | Top-level statements entry point; Parts A, B, C, D, and F.                |
| `Order.cs`         | The `Order` class used by Part C's reference-type experiment.             |
| `STACK_HEAP.md`    | Part E — hand-drawn stack/heap diagrams for the `Order` example.          |
| `README.md`        | This file.                                                                 |
| `ANSWERS.md`       | Part G — short written answers.                                           |

## Part-by-part map

- **Part A** — explained in the comment block at the top of `Program.cs`
  (roles of `.csproj`/`Program.cs`/`obj`/`bin`, the namespace note, and the
  `.sln` vs `.slnx` note).
- **Part B** — `RunTypesDemo()` in `Program.cs`.
- **Part C** — `RunValueVsReferenceDemo()` in `Program.cs`, plus `Order.cs`
  and the `Point` struct.
- **Part D** — `RunScopeAndOperatorsDemo()` in `Program.cs`, plus the
  `partial class Program` field-scope demo (`FieldScopeMethodA`/`B`).
- **Part E** — `STACK_HEAP.md`.
- **Part F** — `FindSingleNumber()` / `RunSingleNumberDemo()` in
  `Program.cs`.
- **Part G** — `ANSWERS.md`.
>>>>>>> 35ee33a (assignment_3)
