namespace FlightPlanner.Services.Models.Requests
{
    public class AddFlightRequest
    {
        public AddAirportRequest From { get; set; }
        public AddAirportRequest To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
    }
}