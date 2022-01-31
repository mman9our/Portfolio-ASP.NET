using Core_Portfolio.Entities;
using System;

namespace Infrastructure_Portfolio
{
    public class Personal : EntityBase
    {
        public String FullName { get; set; }
       
        public String Profile { get; set; }

        public String Avatar { get; set; }

    }
   
}