using System;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Web.DAL
{
    public class TicketReservationRepository : ITicketReservationRepository
    {
        private readonly IDatabase _cache;

        public TicketReservationRepository(IDatabase cache)
        {
            _cache = cache;
        }

        public async Task<TicketReservation?> GetAsync(int id)
        {
            string serializedReservation = await _cache.StringGetAsync(id.ToString());
            if (!string.IsNullOrEmpty(serializedReservation))
            {

                return JsonConvert.DeserializeObject<TicketReservation>(serializedReservation);
            }

            return null;
        }

        public Task SetAsync(TicketReservation ticketReservation)
        {
            var serialized = JsonConvert.SerializeObject(ticketReservation, Newtonsoft.Json.Formatting.Indented);
            _ = _cache.StringSetAsync($"{ticketReservation.Id}", serialized);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
             _ = _cache.KeyDeleteAsync(id.ToString());
        }
    }
}

