namespace Web.BusinessService
{
    public interface ITicketReservations
    {
        Task<TicketReservation?> GetAsync(int id);
        Task SetAsync(TicketReservation ticketReservation);
        Task DeleteAsync(int id);
    }
}