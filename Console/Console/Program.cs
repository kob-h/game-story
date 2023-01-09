
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;

const string BaseURL = "http://localhost:5000";
const string TicketReservationRelativePath = "TicketReservations";
const string MediaType = "application/json";

HttpClient client = new HttpClient();
ConfigureHttpClient(client);


await AddReservation(1, 30, "John", "Smith");
var res1 = await GetTicketReservation(1);
await DeleteTicketReservation(1);
var res2 = await GetTicketReservation(1);

async Task AddReservation(int id, int age, string name, string order)
{
    var newTicketReservation = new TicketReservation()
    {
        Id = id,
        Age = age,
        Name = name,
        Order = order
    };

    var serializedNewTicketReservation = JsonConvert.SerializeObject(newTicketReservation);
    var content = CreateStringContent(serializedNewTicketReservation);

    var res = await client.PostAsync(TicketReservationRelativePath, content);
}

async Task<TicketReservation> GetTicketReservation(int id)
{
    var result = await client.GetAsync($"{TicketReservationRelativePath}/{id}");
    if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
    {
        return null;
    }

    var reswait = await result.Content.ReadAsStringAsync();
    return JsonConvert.DeserializeObject<TicketReservation>(reswait);
}

async Task DeleteTicketReservation(int id)
{
    _ = await client.DeleteAsync($"{TicketReservationRelativePath}/{id}");
}


Console.ReadLine();

StringContent CreateStringContent(string serializedObject)
{
    return new StringContent(serializedObject, Encoding.UTF8, MediaType);
}

void ConfigureHttpClient(HttpClient client)
{
    client.BaseAddress = new Uri(BaseURL);
    client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue(MediaType));
}

public class TicketReservation
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Order { get; set; }
    public int Age { get; set; }
}


