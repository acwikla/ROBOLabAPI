using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.Models
{
    public class Mode
    {
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public ICollection<Property> Properties { get; set; }

        [Required]
        public int DeviceId { get; set; }

        [Required]
        public Device Device { get; set; }
    }
}
