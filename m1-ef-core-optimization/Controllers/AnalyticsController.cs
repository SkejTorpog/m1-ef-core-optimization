using m1_ef_core_optimization.DTOs;
using m1_ef_core_optimization.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace m1_ef_core_optimization.Controllers;

[ApiController]
[Produces("application/json")]
public class AnalyticsController : ControllerBase
{
    [HttpGet("api/analytics/deliveries/summary")]
    public async Task<ActionResult<List<DeliverySummaryDto>>> GetBookingStatus(
        [FromServices] AppDbContext context
    )
    {
        var deliveriesSummary = await context
            .Deliveries.AsNoTracking()
            .Where(d => d.Status == DeliveryStatus.Completed)
            .GroupBy(d => new
            {
                d.CourierId,
                d.Courier.Name,
                d.CreatedAt.Month,
            })
            .Select(d => new DeliverySummaryDto
            {
                CourierName = d.Key.Name,
                Month = d.Key.Month,
                TotalDistance = d.Sum(o => o.Distance),
                TotalFare = d.Sum(o => o.Fare),
            })
            .ToListAsync();

        return Ok(deliveriesSummary);
    }
}
