using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.Models
{
    public class Value
    {
        public int Id { get; set; }

        [Required]
        public string Val { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateTime { get; set; }

        [Required]
        public virtual ICollection<Property> Properties { get; set; }

        [Required]
        public int DeviceId { get; set; }

        [Required]
        public Device Device { get; set; }

        public int JobId { get; set; }

        public Job Job { get; set; }
}
}
