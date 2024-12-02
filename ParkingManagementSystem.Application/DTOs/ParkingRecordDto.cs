using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Application.DTOs
{
    public class ParkingRecordDto
    {
        public Guid Id { get; set; }
        public string VehicleSize { get; set; }
        public string RegionName { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public decimal? Fee { get; set; }
    }
}
