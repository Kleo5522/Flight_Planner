using FlightPlanner.Services.Models.Requests;

namespace FlightPlanner.Services.Interfaces
{
    public interface IValidation
    {
        bool IsAddFlightRequestAnyPropertyContainsNullOrEmptyValue(AddFlightRequest flight);
        bool IsAirportAnyPropertyNullOrEmpty(AddAirportRequest airport);
        bool IsAirportToAndAirportFromEqual(string to, string from);
        bool IsAddFlightRequestArrivalDateTimeLessOrEqualToDepartureDateTime(string departureTime,
            string arrivalTime);
        bool IsSearchFlightRequestAnyPropertyContainsNullValue(SearchFlightRequest flight);
    }
}