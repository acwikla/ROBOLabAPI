using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.DTO
{
    public class DeviceTypeDTO
    {
        public int Id { get; set; }

        [Required]
        public string DeviceTypeName { get; set; }
    }
}
