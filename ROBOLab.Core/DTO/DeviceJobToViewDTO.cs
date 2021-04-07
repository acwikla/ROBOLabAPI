using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.DTO
{
    public class DeviceJobToViewDTO
    {
        public int Id { get; set; }

        [Required]
        public DateTime? ExecutionTime { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Done { get; set; }

        [Required]
        public string Body { get; set; }

        public DeviceToViewDTO Device { get; set; }

        public JobDTO Job { get; set; }
    }
}
