﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florentina_Infinion_Assessment.Application.DTOs
{
    public class EmailDto
    {
        public string? To { get; set; }
        public string? From { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? FirstName { get; set; }
    }
}
