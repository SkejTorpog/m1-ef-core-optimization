namespace m1_ef_core_optimization.Models;

public class Courier
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";

    public VehicleType VehicleType { get; set; }


    public ICollection<Delivery> Deliveries { get; set; }
}
