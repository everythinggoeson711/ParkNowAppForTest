using AutoMapper;
using MediatR;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using Parking.FindingSlotManagement.Application.Features.Customer.Booking.Queries.GetAvailableSlot;
using Parking.FindingSlotManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Booking.Queries.GetAvailableSlots
{
    public class GetAvailableSlotsQueryHandler : IRequestHandler<GetAvailableSlotsQuery, ServiceResponse<IEnumerable<GetAvailableSlotsResponse>>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IParkingSlotRepository _parkingSlotRepository;
        private readonly IMapper _mapper;
        private readonly IFloorRepository _floorRepository;

        public GetAvailableSlotsQueryHandler(IBookingRepository bookingRepository,
            IParkingSlotRepository parkingSlotRepository,
            IMapper mapper,
            IFloorRepository floorRepository)
        {
            _bookingRepository = bookingRepository;
            _parkingSlotRepository = parkingSlotRepository;
            _mapper = mapper;
            _floorRepository = floorRepository;
        }

        public async Task<ServiceResponse<IEnumerable<GetAvailableSlotsResponse>>> Handle(GetAvailableSlotsQuery request, CancellationToken cancellationToken)
        {
            var startTimeBooking = request.StartTimeBooking;
            var endTimeBooking = request.StartTimeBooking.AddHours(request.DesireHour);
            var parkingId = request.ParkingId;

            try
            {
                // Get all parking slots for the parking
                var slotIncludes = new List<Expression<Func<Domain.Entities.ParkingSlot, object>>>
                {
                    x => x.Floor!,
                };

                var allParkingSlots = await _parkingSlotRepository
                    .GetAllItemWithCondition(x => x.Floor != null && x.Floor.ParkingId == parkingId, slotIncludes);

                if (allParkingSlots == null || !allParkingSlots.Any())
                {
                    return new ServiceResponse<IEnumerable<GetAvailableSlotsResponse>>
                    {
                        Data = new List<GetAvailableSlotsResponse>(),
                        Message = "Không tìm thấy slot nào trong bãi đỗ xe này",
                        StatusCode = 200,
                        Success = true,
                        Count = 0
                    };
                }

                // For now, return all slots (simplified version)
                // TODO: Filter out booked slots by checking TimeSlot and Booking tables
                var responses = _mapper.Map<IEnumerable<GetAvailableSlotsResponse>>(allParkingSlots);

                return new ServiceResponse<IEnumerable<GetAvailableSlotsResponse>>
                {
                    Data = responses,
                    Message = "Thành công",
                    StatusCode = 200,
                    Success = true,
                    Count = responses.Count(),
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<GetAvailableSlotsResponse>>
                {
                    Data = new List<GetAvailableSlotsResponse>(),
                    Message = $"Lỗi: {ex.Message}",
                    StatusCode = 500,
                    Success = false,
                    Count = 0
                };
            }
        }
    }
}

