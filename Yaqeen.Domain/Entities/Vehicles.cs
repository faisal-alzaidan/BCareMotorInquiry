using System;
using System.Collections.Generic;
using System.Text;


namespace Yaqeen.Domain.Entities
{
    public class Vehicles
    {
        public string SequenceNumber { get; set; } = string.Empty;
        public string PlateNumber {  get; set; } = string.Empty;
        public string PlateLetters { get; set; } = string.Empty;

        public int MakeId { get; set; }
        public VehicleMake Make { get; set; } = null!;

        public int ModelId { get; set; }
        public VehicleModel Model { get; set; } = null!;

        public int ModelYear { get; set; }
        public string Color {  get; set; } = string.Empty;
        public string ChassisNumber {  get; set; } = string.Empty;
        public string OwnerNationalId {  get; set; } = string.Empty;

    }
}
