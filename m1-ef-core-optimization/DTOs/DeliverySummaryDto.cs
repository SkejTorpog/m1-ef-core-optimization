namespace m1_ef_core_optimization.DTOs;

public class DeliverySummaryDto
{
    public string CourierName { get; set; }

    public int Month { get; set; }

    public double TotalDistance { get; set; }

    public decimal TotalFare { get; set; }
}
