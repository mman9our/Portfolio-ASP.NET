using System;

namespace Core_Portfolio.Entities
{
    public class PortfolioItem : EntityBase
    {
        public String ProjectName { get; set; }
        public String Description { get; set; }
        public String ImageUrl { get; set; }
        public Address address { get; set; }
    }
}
