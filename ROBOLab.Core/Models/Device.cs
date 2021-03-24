using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.Models
{
    public class Device
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int DeviceTypeId { get; set; }

        [Required]
        public DeviceType DeviceType { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public User User { get; set; }

        public ICollection<Value> Values { get; set; }

        public ICollection<Mode> Modes { get; set; }
    }
}
