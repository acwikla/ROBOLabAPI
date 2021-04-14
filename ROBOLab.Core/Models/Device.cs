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
        public virtual DeviceType DeviceType { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public virtual User User { get; set; }

        public virtual ICollection<DeviceJob> DeviceJob { get; set; }

        public virtual ICollection<Value> Values { get; set; }

        public virtual ICollection<Mode> Modes { get; set; }
    }
}
