# Stack & Heap — `Order` reference-type walkthrough

We trace this exact sequence:

```csharp
Order o1 = new Order { OrderId = 1, CustomerName = "Ali" };
Order o2 = o1;
o2.IsPaid = true;
```

---

## Diagram 1 — after `Order o1 = new Order { OrderId = 1, CustomerName = "Ali" };`

```
   STACK                      HEAP
 ┌───────────────┐          ┌────────────────────────────┐
 │ o1 : 0xA1F0 ───┼────────► │ 0xA1F0  [Order object]     │
 └───────────────┘          │   OrderId       = 1         │
                             │   CustomerName  = "Ali"     │
                             │   Quantity      = 0         │
                             │   UnitPrice     = 0.00      │
                             │   TotalPrice    = 0.00      │
                             │   IsPaid        = false     │
                             │   DiscountPercent = 0.0     │
                             │   ShippingCity  = ""        │
                             │   Priority      = '\0'      │
                             │   ItemCode      = 0         │
                             └────────────────────────────┘
```

`o1` itself lives on the stack and holds nothing but an address (`0xA1F0`
here, just a made-up example address). The real `Order` data — all ten
fields — was allocated on the heap by `new Order { ... }`.

---

## Diagram 2 — after `Order o2 = o1;`

```
   STACK                      HEAP
 ┌───────────────┐          ┌────────────────────────────┐
 │ o1 : 0xA1F0 ───┼───┐      │ 0xA1F0  [Order object]     │
 └───────────────┘   ├────► │   OrderId       = 1         │
 ┌───────────────┐   │      │   CustomerName  = "Ali"     │
 │ o2 : 0xA1F0 ───┼───┘      │   Quantity      = 0         │
 └───────────────┘          │   UnitPrice     = 0.00      │
                             │   TotalPrice    = 0.00      │
                             │   IsPaid        = false     │
                             │   DiscountPercent = 0.0     │
                             │   ShippingCity  = ""        │
                             │   Priority      = '\0'      │
                             │   ItemCode      = 0         │
                             └────────────────────────────┘
```

**What changed:** a brand new stack slot `o2` was created, but no new heap
object was created. `o2` was simply given a *copy of the address* that `o1`
holds, so both stack slots now point at the same single heap object.

---

## Diagram 3 — after `o2.IsPaid = true;`

```
   STACK                      HEAP
 ┌───────────────┐          ┌────────────────────────────┐
 │ o1 : 0xA1F0 ───┼───┐      │ 0xA1F0  [Order object]     │
 └───────────────┘   ├────► │   OrderId       = 1         │
 ┌───────────────┐   │      │   CustomerName  = "Ali"     │
 │ o2 : 0xA1F0 ───┼───┘      │   Quantity      = 0         │
 └───────────────┘          │   UnitPrice     = 0.00      │
                             │   TotalPrice    = 0.00      │
                             │   IsPaid        = true   ◄── changed
                             │   DiscountPercent = 0.0     │
                             │   ShippingCity  = ""        │
                             │   Priority      = '\0'      │
                             │   ItemCode      = 0         │
                             └────────────────────────────┘
```

**What changed:** only the single `IsPaid` field on the one shared heap
object flipped to `true`. Neither `o1` nor `o2` on the stack changed at all
— they still hold the exact same address as before. Because there is only
ever one `Order` object, reading `o1.IsPaid` right now would also report
`true`.

---

## What would be different with structs?

`Point` from Part C is a **struct** (value type), and the picture changes
completely if `Order` had been a struct too:

- `Order o1 = new Order { ... };` would put the **entire 10-field block of
  data directly on the stack** inside `o1` — no heap allocation, no arrow,
  no address at all.
- `Order o2 = o1;` would **copy all ten fields** into a second, completely
  independent block of stack memory for `o2`. There would be two full
  copies of the order sitting side by side on the stack instead of one
  shared object on the heap.
- `o2.IsPaid = true;` would then only flip the `IsPaid` field inside `o2`'s
  own copy. `o1.IsPaid` would remain `false`, because `o1` and `o2` would no
  longer refer to anything in common — exactly like `p1.X` and `p2.X` stayed
  independent for `Point` in Part C.

In short: classes share one heap object through copied addresses; structs
give every variable its own private copy of the data.
