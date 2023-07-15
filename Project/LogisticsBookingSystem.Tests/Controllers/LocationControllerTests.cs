using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LogisticBookingSystem.Application.Commands;
using LogisticBookingSystem.Application.Queries;
using LogisticBookingSystem.Controllers;
using LogisticBookingSystem.Models.Requests;
using LogisticBookingSystem.Models.Responses;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace LogisticBookingSystem.Tests.Controllers
{
    [TestClass]
    public class LocationControllerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly LocationController _controller;

        public LocationControllerTests()
        {
            // Create a mock for IMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LocationModel, MakeLocationCommand>();
                cfg.CreateMap<LocationModel, UpdateLocationCommand>();
                cfg.CreateMap<Location, LocationModel>();
            });
            _mapper = mapperConfig.CreateMapper();

            // Create a mock for IMediator
            _mediatorMock = new Mock<IMediator>();

            // Create the LocationController instance
            _controller = new LocationController(_mapper, _mediatorMock.Object);
        }


        [TestMethod]
        public async Task GetLocationById_ExistingLocationId_ReturnsOkResultWithLocationModel()
        {
            // Arrange
            var locationId = Guid.NewGuid();
            var location = new Location { Id = locationId, Name = "Location 1", Address = "Address 1", Capacity = 10 };
            var locationModel = _mapper.Map<LocationModel>(location);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetLocationQuery>(), It.IsAny<CancellationToken>()));

            // Act
            var result = await _controller.GetLocationById(locationId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [TestMethod]
        public async Task GetLocationById_NonExistingLocationId_ReturnsNotFoundResult()
        {
            // Arrange
            var locationId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetLocationQuery>(), It.IsAny<CancellationToken>()));

            // Act
            var result = await _controller.GetLocationById(locationId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [TestMethod]
        public async Task CreateLocation_ValidLocationModel_ReturnsOkResult()
        {
            // Arrange
            var locationModel = new LocationModel { Name = "Location 1", Address = "Address 1", Capacity = 10 };
            var command = _mapper.Map<MakeLocationCommand>(locationModel);

            // Act
            var result = await _controller.CreateLocation(locationModel);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [TestMethod]
        public async Task UpdateLocation_ExistingLocationIdAndValidLocationModel_ReturnsOkResult()
        {
            // Arrange
            var locationId = Guid.NewGuid();
            var locationModel = new LocationModel { Name = "Location 1", Address = "Address 1", Capacity = 10 };
            var command = _mapper.Map<UpdateLocationCommand>(locationModel);
            command.Id = locationId;

            // Act
            var result = await _controller.UpdateLocation(locationId, locationModel);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [TestMethod]
        public async Task UpdateLocation_NonExistingLocationId_ReturnsNotFoundResult()
        {
            // Arrange
            var locationId = Guid.NewGuid();
            var locationModel = new LocationModel { Name = "Location 1", Address = "Address 1", Capacity = 10 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateLocationCommand>(), It.IsAny<CancellationToken>())).ThrowsAsync(new NotFoundException("Location not found."));

            // Act
            var result = await _controller.UpdateLocation(locationId, locationModel);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Location not found.", notFoundResult.Value);
        }

        [TestMethod]
        public async Task UpdateLocation_ConflictingData_ReturnsConflictResult()
        {
            // Arrange
            var locationId = Guid.NewGuid();
            var locationModel = new LocationModel { Name = "Location 1", Address = "Address 1", Capacity = 10 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateLocationCommand>(), It.IsAny<CancellationToken>())).ThrowsAsync(new ConflictException("Conflict detected."));

            // Act
            var result = await _controller.UpdateLocation(locationId, locationModel);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal("Conflict detected.", conflictResult.Value);
        }

        [TestMethod]
        public async Task DeleteLocation_ExistingLocationId_ReturnsOkResult()
        {
            // Arrange
            var locationId = Guid.NewGuid();

            // Act
            var result = await _controller.DeleteLocation(locationId);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [TestMethod]
        public async Task DeleteLocation_NonExistingLocationId_ReturnsNotFoundResult()
        {
            // Arrange
            var locationId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteLocationCommand>(), It.IsAny<CancellationToken>())).ThrowsAsync(new NotFoundException("Location not found."));

            // Act
            var result = await _controller.DeleteLocation(locationId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Location not found.", notFoundResult.Value);
        }
    }
}
