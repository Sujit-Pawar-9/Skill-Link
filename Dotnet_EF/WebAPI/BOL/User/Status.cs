using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL
{
    public class Status
    {
        // Primary key
        [Key]
        public int StatusID { get; set; }

        // Other properties
        public string ServiceProviderUserName { get; set; }

        // Foreign key
        [ForeignKey("User")]
        public int RequirementID { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public bool IsSelected { get; set; }
    }
}
