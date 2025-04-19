using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library_management.Models
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Book is required")]
        [Display(Name = "Book")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Member is required")]
        [Display(Name = "Member")]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Loan date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Loan Date")]
        public DateTime LoanDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Due date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(14);

        [DataType(DataType.Date)]
        [Display(Name = "Return Date")]
        public DateTime? ReturnDate { get; set; }

        [Display(Name = "Fine Amount")]
        [Range(0, 1000, ErrorMessage = "Fine must be between 0 and 1000")]
        public decimal? FineAmount { get; set; }

        // Navigation properties
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; } = null!; // Ensures non-nullability

        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; } = null!; // Ensures non-nullability
    }
}