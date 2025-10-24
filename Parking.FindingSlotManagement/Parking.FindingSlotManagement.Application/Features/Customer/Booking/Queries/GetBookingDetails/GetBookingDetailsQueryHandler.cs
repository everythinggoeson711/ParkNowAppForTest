using AutoMapper;
using MediatR;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using Parking.FindingSlotManagement.Application.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Booking.Queries.GetBookingDetails
{
    public class GetBookingDetailsQueryHandler : IRequestHandler<GetBookingDetailsQuery, ServiceResponse<GetBookingDetailsResponse>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public GetBookingDetailsQueryHandler(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetBookingDetailsResponse>> Handle(GetBookingDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var booking = await _bookingRepository
                    .GetBookingDetailsByBookingIdMethod(request.BookingId);

                if (booking == null)
                {
                    return new ServiceResponse<GetBookingDetailsResponse>
                    {
                        Message = "Đơn đặt không tồn tại",
                        StatusCode = 200,
                        Success = true,
                    };
                }

                // Defensive null checks for navigation properties
                var bookingDetail = booking.BookingDetails?.FirstOrDefault();
                var timeSlot = bookingDetail?.TimeSlot;
                var parkingSlot = timeSlot?.Parkingslot;
                var floor = parkingSlot?.Floor;
                var parking = floor?.Parking;

                // Check if essential data is available
                if (bookingDetail == null || timeSlot == null || parkingSlot == null || 
                    floor == null || parking == null)
                {
                    return new ServiceResponse<GetBookingDetailsResponse>
                    {
                        Message = "Thông tin booking chưa đầy đủ",
                        StatusCode = 200,
                        Success = false,
                    };
                }

                var response = new GetBookingDetailsResponse
                {
                    BookingDetails = _mapper.Map<BookingDetailsDto>(booking),
                    User = _mapper.Map<UserBookingDto>(booking.User),
                    VehicleInfor = _mapper.Map<VehicleInforDtoos>(booking.VehicleInfor),
                    ParkingSlotWithBookingDetailDto = _mapper.Map<ParkingSlotWithBookingDetailDto>(parkingSlot),
                    FloorWithBookingDetailDto = _mapper.Map<FloorWithBookingDetailDto>(floor),
                    ParkingWithBookingDetailDto = _mapper.Map<ParkingWithBookingDetailDto>(parking),
                    TransactionWithBookingDetailDtos = _mapper.Map<List<TransactionWithBookingDetailDto>>(booking.Transactions)
                };

                return new ServiceResponse<GetBookingDetailsResponse>
                {
                    Data = response,
                    Message = "Thành công",
                    StatusCode = 200,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

