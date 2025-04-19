using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library_management.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }

        [Required]
        [Display(Name = "Book")]
        public int BookId { get; set; }

        [Required]
        [Display(Name = "Member")]
        public int MemberId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Loan Date")]
        public DateTime LoanDate { get; set; } = DateTime.Now;

        [Required]
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
        public virtual Book Book { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }
    }
}