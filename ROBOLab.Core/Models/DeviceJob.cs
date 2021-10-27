using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.Models
{
    public class DeviceJob
    {
        public int Id { get; set; }
  
        public DateTime? ExecutionTime { get; set; }      // If date equal to null -> it means, job should be performed NOW (on the device).

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)] -> chyba nie dziala z sqlite, do sprawdzenia
        public DateTime CreatedDate { get; set; }          // Date, when job was added to database

        [DefaultValue(false)]
        public bool Done { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [EnumDataType(typeof(DeviceJobStatus)), DefaultValue(DeviceJobStatus.Created)]
        public DeviceJobStatus Status { get; set; }

        public DateTime? StatusChanged { get; set; }

        [Required]
        public int DeviceId { get; set; }

        [Required]
        public virtual Device Device { get; set; }

        [Required]
        public int JobId { get; set; }

        [Required]
        public virtual Job Job { get; set; }

    }

    public enum DeviceJobStatus
    {
        Created = 0,//utworzone
        Submitted = 32,//przyjete do realizacji
        InProgress = 128,//w trakcie realizacji
        Completed = 1024//zakonczone
    }
}
