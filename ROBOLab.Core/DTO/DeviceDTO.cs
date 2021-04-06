using ROBOLab.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.DTO
{
    public class DeviceDTO
    {
        public int Id { get; set; }

        [Required]
        public string DeviceName { get; set; }
    }
}
