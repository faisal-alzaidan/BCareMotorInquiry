namespace Yaqeen.Domain.Entities
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;

        // Foreign Key
        public int MakeId { get; set; }
        public VehicleMake Make { get; set; } = null!;
    }
}