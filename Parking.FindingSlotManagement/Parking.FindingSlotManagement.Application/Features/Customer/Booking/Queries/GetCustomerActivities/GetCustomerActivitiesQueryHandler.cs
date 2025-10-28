using AutoMapper;
using MediatR;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using Parking.FindingSlotManagement.Application.Features.Customer.Booking.Queries.GetUpcommingBooking;
using Parking.FindingSlotManagement.Application.Features.Keeper.Queries.SearchRequestBooking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Booking.Queries.GetCustomerActivities
{
    public class GetCustomerActivitiesQueryHandler : IRequestHandler<GetCustomerActivitiesQuery, ServiceResponse<IEnumerable<GetCustomerActivitiesResponse>>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetCustomerActivitiesQueryHandler(IBookingRepository bookingRepository, IUserRepository userRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<IEnumerable<GetCustomerActivitiesResponse>>> Handle(GetCustomerActivitiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var checkUserExist = await _userRepository.GetById(request.UserId);
                if (checkUserExist == null)
                {
                    return new ServiceResponse<IEnumerable<GetCustomerActivitiesResponse>>
                    {
                        Message = "Không tìm thấy tài khoản.",
                        Success = false,
                        StatusCode = 404
                    };
                }
                var lstBooking = await _bookingRepository.GetCustomerActivitiesByUserIdMethod(request.UserId);
                if (lstBooking == null)
                {
                    return new ServiceResponse<IEnumerable<GetCustomerActivitiesResponse>>
                    {
                        Message = "Không tìm thấy đơn đặt.",
                        Success = false,
                        StatusCode = 404
                    };
                }
                List<GetCustomerActivitiesResponse> lstReturn = new();
                foreach (var booking in lstBooking)
                {
                    // Add null checks to prevent NullReferenceException
                    var bookingDetail = booking.BookingDetails?.FirstOrDefault();
                    var timeSlot = bookingDetail?.TimeSlot;
                    var parkingSlot = timeSlot?.Parkingslot;
                    var floor = parkingSlot?.Floor;
                    var parking = floor?.Parking;
                    
                    // Create response even if some data is missing (will be null in response)
                    var eachEntity = new GetCustomerActivitiesResponse
                    {
                        BookingSearchResult = _mapper.Map<BookingSearchResult>(booking),
                        VehicleInforSearchResult = _mapper.Map<VehicleInforSearchResult>(booking.VehicleInfor),
                        ParkingSearchResult = parking != null ? _mapper.Map<ParkingSearchResult>(parking) : null,
                        ParkingSlotSearchResult = parkingSlot != null ? _mapper.Map<ParkingSlotSearchResult>(parkingSlot) : null
                    };
                    lstReturn.Add(eachEntity);
                }
                return new ServiceResponse<IEnumerable<GetCustomerActivitiesResponse>>
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
