﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SibersTest.Models.Request
{
    public class AddEmployee
    {
        public string FullName { get; set; } = string.Empty;

        public string PositionTitle { get; set; } = string.Empty;
     

        public string Mobile { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
       
      
    }
}
