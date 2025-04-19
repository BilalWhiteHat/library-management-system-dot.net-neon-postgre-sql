using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace library_management.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        [StringLength(500, ErrorMessage = "Biography cannot exceed 500 characters")]
        public string Biography { get; set; }

        // Navigation property
        public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}