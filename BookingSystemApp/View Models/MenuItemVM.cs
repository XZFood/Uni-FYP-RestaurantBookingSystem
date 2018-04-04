﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookingSystemApp.View_Models
{
    public class MenuItemVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public string Types { get; set; }

        public string DietInfo { get; set; }
    }
}