using AutoMapper;
using LogisticBookingSystem.Application.Commands;
using LogisticBookingSystem.Application.Queries;
using LogisticBookingSystem.Models.Requests;
using LogisticBookingSystem.Models.Responses;
using LogisticsBookingSystem.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LogisticBookingSystem.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public LocationController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            var query = new GetLocationQuery
            {
            };

            var locationas = await _mediator.Send(query);
            IEnumerable<LocationDto> locationModels = _mapper.Map<IEnumerable<LocationDto>>(locationas);
            return Ok(locationModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(Guid id)
        {
            var query = new GetLocationByLocationIDQuery
            {
                LocationId = id
            };

            var locations = await _mediator.Send(query);
            LocationDto locationModels = _mapper.Map<LocationDto>(locations);
            return Ok(locationModels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation(LocationModel request)
        {
            MakeLocationCommand command = new()
            {
                Address = request.Address,
                Capacity = request.Capacity,
                Name = request.Name
            };

            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(Guid id, LocationModel locationModel)
        {
            try
            {
                UpdateLocationCommand command = new()
                {
                    Id = id,
                    Address = locationModel.Address,
                    Capacity = locationModel.Capacity,
                    Name = locationModel.Name
                };

                await _mediator.Send(command);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the location: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(Guid id)
        {
            try
            {
                DeleteLocationCommand command = new()
                {
                    Id = id
                };
                await _mediator.Send(command);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the location: {ex.Message}");
            }
        }
    }
}
