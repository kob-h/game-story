namespace Web.DAL
{
    public interface ITicketReservationRepository
    {
        Task<TicketReservation?> GetAsync(int id);
        Task SetAsync(TicketReservation ticketReservation);
        Task DeleteAsync(int id);
    }
}