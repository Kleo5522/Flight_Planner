using FlightPlanner.Core.Models;

namespace FlightPlanner.Services.Models.Data_Format
{
    public class PageResult<T> where T : Entity
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public T[] Items { get; set; }
    }
}