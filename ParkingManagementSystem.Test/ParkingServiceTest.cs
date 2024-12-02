using Moq;
using ParkingManagementSystem.Application.Repositories;
using ParkingManagementSystem.Application.Repositories.ParkingRecord;
using ParkingManagementSystem.Application.Repositories.Region;
using ParkingManagementSystem.Application.Services;
using ParkingManagementSystem.Domain;
using ParkingManagementSystem.Persistance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Test
{
    public class ParkingServiceTest
    {

        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ParkingService _parkingService;

        public ParkingServiceTest()
        {

            _unitOfWorkMock = new Mock<IUnitOfWork>();


            _parkingService = new ParkingService(_unitOfWorkMock.Object);
        }



        [Fact]
        public async Task ParkVehicleAsync_Should_Select_SuitableRegion()
        {

            var regions = TestData.GetMockRegions(); // Test verisi alınır
            var suitableRegion = regions.FirstOrDefault(r => r.AllowedVehicleSize == "Small" && r.OccupiedSpots < r.Capacity);


            _unitOfWorkMock.Setup(repo => repo.RegionRead.GetAllAsync(true))
                .ReturnsAsync(regions); // Region verisini döndürür

            // Update işlemi mocklanıyor ve OccupiedSpots++ işlemi simüle ediliyor
            _unitOfWorkMock.Setup(repo => repo.RegionWrite.UpdateAsync(It.IsAny<Region>()))
        
                .Returns(Task.CompletedTask); // Update işlemi simüle edilir

            _unitOfWorkMock.Setup(repo => repo.ParkingRecordWrite.AddAsync(It.IsAny<ParkingRecord>()))
                .Returns(Task.CompletedTask); // Add işlemi simüle edilir

            _unitOfWorkMock.Setup(repo => repo.SaveAsync())
                .ReturnsAsync(1); // Save işlemi simüle edilir

            // Act
            var result = await _parkingService.ParkVehicleAsync("Small");

            // Assert
            Assert.NotNull(result); // Sonuç null olmamalı
            Assert.Equal("A", result.RegionName); // Beklenen bölge ismi
            Assert.Equal(1, suitableRegion.OccupiedSpots); // OccupiedSpots 1 artmalı
        }



        [Fact]
        public async Task ParkVehicleAsync_Should_Return_Null_When_No_SuitableRegion()
        {

            _unitOfWorkMock.Setup(repo => repo.RegionRead.GetAllAsync(true)).ReturnsAsync(TestData.GetMockRegions());
            var result = await _parkingService.ParkVehicleAsync("Medium");

            Assert.Null(result); // "Medium" araçlar için uygun bir yer yok.
        }

        [Fact]
        public async Task ExitVehicleAsync_Should_Calculate_Fee_And_Free_Spot()
        {

            var mockParkingRecord = TestData.GetMockParkingRecord();
            var mockRegions = TestData.GetMockRegions();

            // Uygun bölgeyi bulur
            var suitableRegion = mockRegions.FirstOrDefault(r => r.Id == mockParkingRecord.RegionId);
            suitableRegion.OccupiedSpots = 1;

            _unitOfWorkMock.Setup(repo => repo.ParkingRecordRead.GetByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(mockParkingRecord);

            // Region verisini mocklarken, OccupiedSpots doğru bir şekilde arttırılır
            _unitOfWorkMock.Setup(repo => repo.RegionRead.GetByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(suitableRegion); // Doğru region döndürülür

            // Update işlemi mocklanıyor ve OccupiedSpots-- işlemi simüle ediliyor
            _unitOfWorkMock.Setup(repo => repo.RegionWrite.UpdateAsync(It.IsAny<Region>()))
                .Returns(Task.CompletedTask); // Update işlemi simüle edilir

            _unitOfWorkMock.Setup(fee => fee.ParkingRecordWrite.CalculateFee(It.IsAny<string>(), It.IsAny<TimeSpan>()))
                .Returns(6m); // Ücret hesaplanır

            _unitOfWorkMock.Setup(repo => repo.SaveAsync())
                .ReturnsAsync(1); // Save işlemi simüle edilir


            var result = await _parkingService.ExitVehicleAsync(mockParkingRecord.Id);


            Assert.NotNull(result); // Sonuç null olmamalı
            Assert.Equal(6m, result.Fee);  //Hesaplanan ücret 6m ye eşit olmalı 
            Assert.NotNull(result.ExitTime); //Çıkış zamanı boş olmamalı
            Assert.Equal(0, suitableRegion.OccupiedSpots); // Çıkışta işgal edilen yer boşaltılmalı
        }
    }
}
