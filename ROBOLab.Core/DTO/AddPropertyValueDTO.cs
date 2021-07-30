using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ROBOLab.Core.DTO
{
    public class AddPropertyValueDTO
    {
        public int PropertyId { get; set; }

        public string PropertyName { get; set; }

        public int DeviceJobId { get; set;}

        [Required]
        public string Val { get; set; }
    }
}
