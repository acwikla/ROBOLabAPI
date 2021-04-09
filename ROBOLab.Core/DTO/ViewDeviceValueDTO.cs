using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ROBOLab.Core.Models;

namespace ROBOLab.Core.DTO
{
    public class ViewDeviceValueDTO
    {
        public int Id { get; set; }

        [Required]
        public string Val { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        public ViewPropertyDTO Property { get; set; }

        [Required]
        public int DeviceId { get; set; }

        [Required]
        public ViewDeviceDTO Device { get; set; }
    }
}
