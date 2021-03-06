﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgriExchange.Models
{
    public class Address
    {
            [Key]
            public int ID { get; set; }
            [Display(Name = "Address")]
            public string addressLine { get; set; }
            public float Lattitude { get; set; }
            public float Longitude { get; set; }
            public string Zone { get; set; }
            public Cities City { get; set; }
            public ZipCode Zip { get; set; }

    }
    public class Cities
    {
            [Key]
            public int ID { get; set; }
            [Display(Name = "City")]
            public string City { get; set; }
            public States State { get; set; }

    }
        public class States
        {
            public int ID { get; set; }
            [Display(Name = "State")]
            public string State { get; set; }
            public string abbreviation { get; set; }
        }
        public class ZipCode
        {
            public int ID { get; set; }
            [Display(Name = "Zip")]
            public int zip { get; set; }
        }
}