﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Data.Models
{
    public class Url
    {
        public int Id { get; set; }

        public string SensorUrl { get; set; }

        public string SensorType { get; set; }

        public string Description { get; set; }

        public int PollingInterval { get; set; }

        public string ValueType { get; set; }
    }
}
