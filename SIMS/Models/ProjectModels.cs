using System;
using System.ComponentModel.DataAnnotations;

namespace SIMS.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(100, ErrorMessage = "Username cannot exceed 100 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public int RoleID { get; set; }
    }

    public class Incident
    {
        public int IncidentID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }

        public string Description { get; set; }  // nvarchar(MAX)

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
        public string Category { get; set; }  // varchar(50)

        [Required(ErrorMessage = "Priority is required")]
        [StringLength(20, ErrorMessage = "Priority cannot exceed 20 characters")]
        public string Priority { get; set; }  // varchar(20)

        [Required(ErrorMessage = "Status is required")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string Status { get; set; }  // nvarchar(50)

        [StringLength(255, ErrorMessage = "Location cannot exceed 255 characters")]
        public string Location { get; set; }  // nvarchar(255)

        public string FilePath { get; set; }  // nvarchar(MAX)

        [Required]
        public int ReportedBy { get; set; }

        public int? AssignedTo { get; set; }  // Allows null

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? ArchivedAt { get; set; }  // Allows null

        // Navigation properties (not in database but useful for joins)
        public string ReporterName { get; set; }
        public string InvestigatorName { get; set; }
    }

    public class InternalMessage
    {
        public int MessageID { get; set; }

        [Required]
        public int IncidentID { get; set; }

        [Required]
        public int SenderID { get; set; }

        [Required(ErrorMessage = "Message cannot be empty")]
        public string MessageBody { get; set; }  // nvarchar(MAX)

        [Required]
        public DateTime SentAt { get; set; }

        // Optional: Add sender name for display
        public string SenderName { get; set; }
    }

}