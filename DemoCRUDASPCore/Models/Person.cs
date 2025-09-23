using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoCRUDASPCore.Models
{
    public class Person
    {
        [DisplayName("รหัสพนักงาน")]
        public int PersonID { get; set; }
        public string PersonName { get; set; }
        public string PersonAddress { get; set; }
        public string ImagName { get; set; }
        [NotMapped]
        [DisplayName("รูปภาพ")]
        public IFormFile ImagFile { get; set; }

    }
}
