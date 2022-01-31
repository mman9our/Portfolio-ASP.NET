
using Core_Portfolio;
using Core_Portfolio.Entities;
using Infrastructure_Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class HomeViewModel
    {
        public Personal personal { get; set; }

        public List<PortfolioItem> PortfolioItem { get; set; }

    }
}
