namespace CSharpBasicsAssignment;

// Order is a CLASS (reference type). Instances live on the HEAP.
// Variables of type Order that live on the stack only store an ADDRESS
// (a reference) pointing to the object's data on the heap.
//
// Exactly 10 fields, all concrete types (no field of type object).
class Order
{
    public int OrderId;
    public string CustomerName = string.Empty;
    public int Quantity;
    public decimal UnitPrice;
    public decimal TotalPrice;
    public bool IsPaid;
    public double DiscountPercent;
    public string ShippingCity = string.Empty;
    public char Priority; // 'H', 'M', or 'L'
    public long ItemCode;

    // Method 1: computes TotalPrice from Quantity, UnitPrice and DiscountPercent
    // and stores the result back into the TotalPrice field.
    public void CalculateTotal()
    {
        decimal discountFactor = 1 - ((decimal)DiscountPercent / 100);
        TotalPrice = Quantity * UnitPrice * discountFactor;
    }

    // Method 2: prints a one-line summary of the order.
    public void PrintSummary()
    {
        Console.WriteLine(
            $"Order #{OrderId} | Customer: {CustomerName} | Total: {TotalPrice:C} | Paid: {IsPaid}");
    }
}
