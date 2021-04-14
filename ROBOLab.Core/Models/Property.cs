using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.Models
{
    public class Property
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public int DeviceTypeId { get; set; }

        [Required]
        public virtual DeviceType DeviceType { get; set; }

        [DefaultValue(false)]
        public bool IsMode { get; set; }

        public int ModeId { get; set; }

        public virtual Mode Mode { get; set; }

        public virtual ICollection<Value> Values { get; set; }
    }
}
