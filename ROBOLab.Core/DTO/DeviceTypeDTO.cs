    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.DTO
{
    public class AddDeviceTypeDTO
    {
        [Required]
        public string Name { get; set; }
    }

    public class DeviceTypeDTO : AddDeviceTypeDTO
    {
        public int Id { get; set; }
    }
}