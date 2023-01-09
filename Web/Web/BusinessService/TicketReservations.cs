using System;
using Web.DAL;

namespace Web.BusinessService
{
    public class TicketReservations : ITicketReservations
    {
        private readonly ITicketReservationRepository _ticketReservationRepository;

        public TicketReservations(ITicketReservationRepository ticketReservationRepository)
        {
            _ticketReservationRepository = ticketReservationRepository;
        }

        public async Task<TicketReservation?> GetAsync(int id)
        {
            return await _ticketReservationRepository.GetAsync(id);
        }

        public Task SetAsync(TicketReservation ticketReservation)
        {
            return _ticketReservationRepository.SetAsync(ticketReservation);
        }

        public Task DeleteAsync(int id)
        {
            return _ticketReservationRepository.DeleteAsync(id);
        }
    }
}

