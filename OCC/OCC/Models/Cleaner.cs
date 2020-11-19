﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCC.Models
{
    public class Cleaner 
    {
        public long CleanerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public long Phone { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Location { get; set; }

        public bool IsPontetialCleaner { get; set; }

        public string ExperienceLevel { get; set; }

        public bool IsCertificate { get; set; }

        public string certicate { get; set; }

    }
}
