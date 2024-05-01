using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BOL
{
    public class BookingList
    {
        [Key]
        public int RequirementID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }

         [ForeignKey("ServiceProvider")]  // Define foreign key attribute
        public int ServiceProviderID { get; set; }  // Foreign key property
        public ServiceProvider ServiceProvider { get; set; }  // Navigation property

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string NameFirst { get; set; }

        [StringLength(50)]
        public string NameLast { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string Skills { get; set; }

        [StringLength(50)]
        public string Wages { get; set; }

        // Parameterless constructor
        public BookingList()
        {
            // Default constructor
        }

        // Constructor with parameters
        public BookingList(int requirementid, int userId, string username, string firstName, string lastName, string phoneNumber, string skills, string wages)
        {
            RequirementID = requirementid;
            UserID = userId;
            Username = username;
            NameFirst = firstName;
            NameLast = lastName;
            PhoneNumber = phoneNumber;
            Skills = skills;
            Wages = wages;
        }
    }
}
