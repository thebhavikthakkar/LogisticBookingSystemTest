using AutoMapper;
using Azure.Core;
using LogisticBookingSystem.Application.Commands;
using LogisticBookingSystem.Application.Queries;
using LogisticBookingSystem.Models.Requests;
using LogisticBookingSystem.Models.Responses;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Enum;
using LogisticsBookingSystem.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LogisticBookingSystem.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var query = new GetAllBookingsQuery();
            var bookings = await _mediator.Send(query);
            return Ok(_mapper.Map<IEnumerable<BookingDto>>(bookings));
        }

        [HttpGet("dateRange")]
        public async Task<IActionResult> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
        {
            var query = new GetBookingsByDateRangeQuery(startDate, endDate);
            var bookings = await _mediator.Send(query);
            return Ok(_mapper.Map<IEnumerable<BookingDto>>(bookings));
        }

        [HttpGet("location/{locationId}")]
        public async Task<IActionResult> GetBookingsByLocationId(Guid locationId)
        {
            var query = new GetBookingByLocationIdQuery(locationId);
            var bookings = await _mediator.Send(query);
            return Ok(_mapper.Map<IEnumerable<BookingDto>>(bookings));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingModel request)
        {
            MakeBookingCommand command = new()
            {
                LocationId = request.LocationId,
                Date = request.Date,
                Goods = request.Goods,
                Carrier = request.Carrier,
                Time = request.Time,
                State = (int)request.State
            };

            await _mediator.Send(command);

            return Ok();

        }

        [HttpPut("{bookingId}")]
        public async Task<IActionResult> UpdateBooking(Guid bookingId, BookingModel model)
        {
            try
            {
                UpdateBookingCommand command = new()
                {
                    Id = bookingId,
                    LocationId = model.LocationId,
                    Date = model.Date,
                    Goods = model.Goods,
                    Carrier = model.Carrier,
                    State = model.State,
                    Time = model.Time
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
                return StatusCode(500, $"An error occurred while updating the booking: {ex.Message}");
            }
        }

        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> DeleteBooking(Guid bookingId)
        {
            try
            {
                DeleteBookingCommand command = new()
                {
                    Id = bookingId
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
                return StatusCode(500, $"An error occurred while deleting the booking: {ex.Message}");
            }
        }

        [HttpPost("{bookingId}/process")]
        public async Task<IActionResult> ProcessBooking(Guid bookingId)
        {
            var command = new ProcessBookingCommand { BookingId = bookingId };

            try
            {
                await _mediator.Send(command);
                return Ok("Booking processed successfully.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the booking.");
            }
        }

    }
}
