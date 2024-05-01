using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

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

        [StringLength(100)]
        public string Address { get; set; }
          
        // public int ServiceProviderID { get; set; }
        public ServiceProvider ServiceProvider { get; set; }

        public ICollection<UserRequirement> UserRequirements { get; set; }
        public ICollection<BookingList> BookingLists { get; set; }
        public ICollection<Status> Statuses { get; set; }

        // Parameterless constructor
        public User()
        {
            // Default constructor
        }

        // Constructor without ID
        public User(string nameFirst, string nameLast, string username, string password, string phoneNumber, string address)
        {
            NameFirst = nameFirst;
            NameLast = nameLast;
            Username = username;
            Password = password;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        // Constructor with ID
        public User(int id, string nameFirst, string nameLast, string username, string password, string phoneNumber, string address)
            : this(nameFirst, nameLast, username, password, phoneNumber, address)
        {
            UserID = id;
        }
    }}