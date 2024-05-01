using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL
{
     public class ServiceProvider
    {
        [Key]
        public int ServiceProviderID { get; set; }

        [Required]
        [StringLength(50)]
        public string NameFirst { get; set; }

        [Required]
        [StringLength(50)]
        public string NameLast { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(30)]
        public string Skills { get; set; }

        [StringLength(50)]
        public string Wages { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        //    public int UserID { get; set; }  // Foreign key to User
        //   public User User { get; set; }  // Navigation property to User

        // public ICollection<BookingList> BookingLists { get; set; }

        // Parameterless constructor
        public ServiceProvider()
        {
            // Default constructor
        }

        // Constructor without ID
        public ServiceProvider(string nameFirst, string nameLast, string username, string password, string phoneNumber, string address)
        {
            NameFirst = nameFirst;
            NameLast = nameLast;
            Username = username;
            Password = password;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        // Constructor with ID
        public ServiceProvider(int id, string nameFirst, string nameLast, string username, string password, string phoneNumber, string address)
            : this(nameFirst, nameLast, username, password, phoneNumber, address)
        {
            ServiceProviderID = id;
        }
    }
    }

