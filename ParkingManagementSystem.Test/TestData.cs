using ParkingManagementSystem.Domain;

namespace ParkingManagementSystem.Test
{
    public class TestData
    {
        public static List<Region> GetMockRegions()
        {
            return new List<Region>
        {
            new Region { Id = Guid.Parse("42d61e27-4716-4d55-be27-f0db34495b45"), Name = "A", Capacity = 15, OccupiedSpots = 0, AllowedVehicleSize = "Small" },
            new Region { Id = Guid.Parse("750cded2-9850-49d1-99ff-8654b0c22086"), Name = "B", Capacity = 10, OccupiedSpots = 10, AllowedVehicleSize = "Medium" },
            new Region { Id = Guid.Parse("d71380e3-df89-4a4e-a9d8-53ab78563b43"), Name = "C", Capacity = 8, OccupiedSpots = 5, AllowedVehicleSize = "Large" }
        };
        }

        public static ParkingRecord GetMockParkingRecord()
        {
            return new ParkingRecord
            {
                Id = Guid.NewGuid(),
                VehicleSize = "Small",
                RegionId = GetMockRegions().First().Id,
                EntryTime = DateTime.UtcNow.AddHours(-3), // 3 saat önce giriþ yapýldý
            };
        }
    }
}