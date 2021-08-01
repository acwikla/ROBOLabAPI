using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.DTO
{
    public class AddPropertyDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Body { get; set; }

        [DefaultValue(false)]
        public bool IsMode { get; set; }
    }
}
