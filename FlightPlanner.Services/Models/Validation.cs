using System;
using FlightPlanner.Services.Interfaces;
using FlightPlanner.Services.Models.Requests;

namespace FlightPlanner.Services.Models
{
    public class Validation : IValidation
    {
        public bool IsAddFlightRequestAnyPropertyContainsNullOrEmptyValue(AddFlightRequest flight)
        {
            {
                return string.IsNullOrEmpty(flight.DepartureTime) ||
                       string.IsNullOrEmpty(flight.ArrivalTime) ||
                       string.IsNullOrEmpty(flight.Carrier) ||
                       flight.To == null ||
                       flight.From == null ||
                       IsAirportAnyPropertyNullOrEmpty(flight.From) ||
                       IsAirportAnyPropertyNullOrEmpty(flight.To);
            }
        }

        public bool IsAirportAnyPropertyNullOrEmpty(AddAirportRequest airport)
        {
            return string.IsNullOrEmpty(airport.Airport) ||
                   string.IsNullOrEmpty(airport.City) ||
                   string.IsNullOrEmpty(airport.Country);
        }

        public bool IsAirportToAndAirportFromEqual(string to, string @from)
        {
            return to.ToUpper().Trim() == from.ToUpper().Trim();
        }

        public bool IsAddFlightRequestArrivalDateTimeLessOrEqualToDepartureDateTime(string departureTime,
            string arrivalTime)
        {
            var flightDepartureDateTime = DateTime.Parse(departureTime);
            var flightArrivalDateTime = DateTime.Parse(arrivalTime);
            return flightArrivalDateTime <= flightDepartureDateTime;
        }

        public bool IsSearchFlightRequestAnyPropertyContainsNullValue(SearchFlightRequest flight)
        {
            return flight?.From == null || flight.To == null || flight.DepartureDate == null;
        }
    }
}