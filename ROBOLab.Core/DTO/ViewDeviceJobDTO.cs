using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ROBOLab.Core.DTO
{
    public class ViewDeviceJobDTO
    {
        public int Id { get; set; }

        public DateTime? ExecutionTime { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Done { get; set; }

        [Required]
        public string Body { get; set; }

        //public ViewDeviceDTO Device { get; set; } dane urzadzenia nie sa konieczne do wykonania zadania, wiec mozna nie udostepniac tych danych tutaj

        public JobDTO Job { get; set; }
    }
}
