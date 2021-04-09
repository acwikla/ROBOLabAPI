using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.DTO
{
    public class AddDeviceJobDTO
    {
        public int Id { get; set; }

        [Required]
        public DateTime? ExecutionTime { get; set; }      

        [Required]
        public string Body { get; set; }
    }
}
