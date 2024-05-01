// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace BOL
// {
//     public class Feedback
//     {
//         // Primary key
//         [Key]
//         public int FeedbackID { get; set; }

//         // Other properties
//         public string Username { get; set; }

//         // Foreign key
//         public int ServiceProviderID { get; set; }

//         public string FeedbackMessage { get; set; }
//         public float Rating { get; set; }

//         // Navigation property
//         [ForeignKey("ServiceProviderID")]
//         public ServiceProvider ServiceProvider { get; set; }
//     }
// }
