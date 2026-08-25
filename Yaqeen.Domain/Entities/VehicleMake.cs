namespace Yaqeen.Domain.Entities
{
    public class VehicleMake
    {
        public int Id { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;

        // Navigation property
        public ICollection<VehicleModel> Models { get; set; } = new List<VehicleModel>();
    }
}