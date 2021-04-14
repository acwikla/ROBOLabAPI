using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.Models
{
    public class DeviceType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Device> Devices { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
