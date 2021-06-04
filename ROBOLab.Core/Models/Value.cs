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
        public DateTime? DateTime { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        public virtual Property Property { get; set; }

        [Required]
        public int DeviceId { get; set; }

        [Required]
        public virtual Device Device { get; set; }

        //DB: zamiast Job ma byc DeviceJob (zobacz na tą pikną fotę ze schematem bazy)
        // Chodzo o to ze chcesz znac konkretny DeviceJob w ramach ktorego zapisano te statystyki

        public int? JobId { get; set; }

        public virtual Job Job { get; set; }
    }
}
