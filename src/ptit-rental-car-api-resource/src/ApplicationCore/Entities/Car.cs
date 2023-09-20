using NGOT.ApplicationCore.ValueObjects;
using NGOT.Common.Enums;
using NGOT.Common.Models;

namespace NGOT.ApplicationCore.Entities;

public class Car : Entity<Guid>
{
    public string Name { get; set; } = null!;
    public CarSpecificity Specificity { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid BrandId { get; set; }
    public Guid CarTypeId { get; set; }
    public decimal Price { get; set; }
    public string Rule { get; set; } = null!;
    public CarStatusEnum Status { get; set; }

    // Navigation properties
    public virtual Brand Brand { get; set; } = null!;
    public virtual CarType CarType { get; set; } = null!;
    public virtual List<CarImage> CarImages { get; set; } = new();
    public virtual List<CarFeature> CarFeatures { get; set; } = new();
    public virtual List<CarAdditionalFees> CarAdditionalFees { get; set; } = new();
    public virtual List<RentalRequest> RentalRequests { get; set; } = new();
    public virtual List<CarRentalDocuments> CarRentalDocuments { get; set; } = new();
}