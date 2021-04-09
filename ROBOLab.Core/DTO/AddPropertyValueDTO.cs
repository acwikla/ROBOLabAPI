using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ROBOLab.Core.DTO
{
    public class AddPropertyValueDTO
    {
        public int Id { get; set; }

        [Required]
        public string Val { get; set; }
    }
}
