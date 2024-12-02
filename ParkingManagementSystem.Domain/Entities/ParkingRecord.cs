using ParkingManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Domain
{
    public class ParkingRecord: BaseEntity
    {
        public string? VehicleSize { get; set; } // Örn: "Small", "Medium", "Large"
        public Guid RegionId { get; set;} 
        public Region? Region { get; set;} 
        public DateTime EntryTime { get; set;} 
        public DateTime? ExitTime { get; set; }
    }
}
