using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using Parking.FindingSlotManagement.Application.Features.Keeper.Queries.SearchRequestBooking;
using Parking.FindingSlotManagement.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Booking.Queries.GetUpcommingBooking
{
    public class GetUpcommingBookingQueryHandler : IRequestHandler<GetUpcommingBookingQuery, ServiceResponse<IEnumerable<GetUpcommingBookingResponse>>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUpcommingBookingQueryHandler> _logger;

        public GetUpcommingBookingQueryHandler(
            IBookingRepository bookingRepository, 
            IUserRepository userRepository, 
            IMapper mapper,
            ILogger<GetUpcommingBookingQueryHandler> logger)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ServiceResponse<IEnumerable<GetUpcommingBookingResponse>>> Handle(GetUpcommingBookingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var checkUserExist = await _userRepository.GetById(request.UserId);
                if (checkUserExist == null)
                {
                    return new ServiceResponse<IEnumerable<GetUpcommingBookingResponse>>
                    {
                        Message = "Không tìm thấy tài khoản.",
                        Success = false,
                        StatusCode = 404
                    };
                }
                var lstBooking = await _bookingRepository.GetUpcommingBookingByUserIdMethod(request.UserId);
                if(lstBooking == null)
                {
                    return new ServiceResponse<IEnumerable<GetUpcommingBookingResponse>>
                    {
                        Message = "Không tìm thấy đơn đặt.",
                        Success = false,
                        StatusCode = 404
                    };
                }
                List<GetUpcommingBookingResponse> lstReturn = new();
                
                foreach (var booking in lstBooking)
                {
                    // Add null checks to prevent NullReferenceException
                    var bookingDetail = booking.BookingDetails?.FirstOrDefault();
                    var timeSlot = bookingDetail?.TimeSlot;
                    var parkingSlot = timeSlot?.Parkingslot;
                    var floor = parkingSlot?.Floor;
                    var parking = floor?.Parking;
                    
                    // Skip this booking if any required data is missing (may be newly created and still processing)
                    if (bookingDetail == null || timeSlot == null || parkingSlot == null || floor == null || parking == null)
                    {
                        _logger.LogWarning(
                            "Booking {BookingId} for user {UserId} is missing required data - skipping. " +
                            "BookingDetails: {HasDetails}, TimeSlot: {HasTimeSlot}, ParkingSlot: {HasParkingSlot}, Floor: {HasFloor}, Parking: {HasParking}. " +
                            "This may be normal for newly created bookings.",
                            booking.BookingId, 
                            request.UserId,
                            bookingDetail != null,
                            timeSlot != null,
                            parkingSlot != null,
                            floor != null,
                            parking != null);
                        
                        // Simply skip - do NOT auto-cancel to avoid race conditions with booking creation
                        continue;
                    }
                    
                    var eachEntity = new GetUpcommingBookingResponse
                    {
                        BookingSearchResult = _mapper.Map<BookingSearchResult>(booking),
                        VehicleInforSearchResult = _mapper.Map<VehicleInforSearchResult>(booking.VehicleInfor),
                        ParkingSearchResult = _mapper.Map<ParkingSearchResult>(parking),
                        ParkingSlotSearchResult = _mapper.Map<ParkingSlotSearchResult>(parkingSlot)
                    };
                    lstReturn.Add(eachEntity);
                }
                
                return new ServiceResponse<IEnumerable<GetUpcommingBookingResponse>>
                {
                    Data = lstReturn.OrderByDescending(x => x.BookingSearchResult.BookingId),
                    Success = true,
                    StatusCode = 200,
                    Message = "Thành công"
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
