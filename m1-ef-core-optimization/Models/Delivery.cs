namespace m1_ef_core_optimization.Models;

public class Delivery
{
    public Guid Id { get; set; }

    public Guid CourierId { get; set; }

    public double Distance { get; set; }

    public int Duration { get; set; }

    public decimal Fare { get; set; }

    public DeliveryStatus Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }


    public Courier? Courier { get; set; }
}
