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
        public int Id { get; set; }

        [Required]
        public string Val { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateTime { get; set; }

        [Required]
        public PropertyNameDTO Property { get; set; }


    }
}
