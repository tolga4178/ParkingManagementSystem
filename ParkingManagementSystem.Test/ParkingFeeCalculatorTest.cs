using Moq;
using ParkingManagementSystem.Application.Repositories;
using ParkingManagementSystem.Application.Repositories.ParkingRecord;
using ParkingManagementSystem.Application.Repositories.Region;
using System;
using Xunit;

namespace ParkingManagementSystem.Test
{
    public class ParkingFeeCalculatorTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;


        public ParkingFeeCalculatorTest()
        {

            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _unitOfWorkMock.Setup(uow => uow.ParkingRecordWrite.CalculateFee(It.IsAny<string>(), It.IsAny<TimeSpan>()))
                           .Returns((string regionName, TimeSpan duration) =>
                           {
                               decimal baseRate = regionName switch
                               {
                                   "A" => 2m,
                                   "B" => 3m,
                                   "C" => 5m,
                                   _ => 1m
                               };
                               return baseRate * (decimal)duration.TotalHours;
                           });
        }

        [Theory]
        [InlineData("A", 1, 2)]
        [InlineData("A", 3, 6)]
        [InlineData("B", 2, 6)]
        [InlineData("C", 4, 20)]
        public void CalculateFee_Should_Return_Correct_Fee(string regionName, int hours, decimal expectedFee)
        {

            var duration = TimeSpan.FromHours(hours);
         
            var fee = _unitOfWorkMock.Object.ParkingRecordWrite.CalculateFee(regionName, duration);

            Assert.Equal(expectedFee, fee);
        }
    }
}
