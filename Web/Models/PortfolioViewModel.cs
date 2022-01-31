using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class PortfolioViewModel
    {
        public Guid Id { get; set; }
        public String ProjectName { get; set; }
        public String Description { get; set; }
        public String ImageUrl { get; set; }
        public IFormFile  File { get; set; }

    }

}
