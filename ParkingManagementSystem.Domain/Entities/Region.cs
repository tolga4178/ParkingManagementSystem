using ParkingManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Domain
{
    public class Region: BaseEntity
    {
        public string? Name { get; set; } // Örn: "A", "B", "C"
        public int Capacity { get; set; }
        public string? AllowedVehicleSize { get; set; }
        public int? OccupiedSpots { get; set; } = 0;
        public ICollection<ParkingRecord>? ParkingRecords { get; set; }
    }
}
