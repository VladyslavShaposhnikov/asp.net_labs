using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using aspdotnetLabs.Models;

namespace Data.Entities;

[Table("books")]
public class BookEntity
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Please enter the title of the book")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Please enter the author of the book")]
    public string Author { get; set; }
    [Required(ErrorMessage = "Please enter how many pages the book has")]
    [Range(1, int.MaxValue, ErrorMessage = "The number of pages must be greater than 0")]
    public int Pages { get; set; }
    [Required(ErrorMessage = "Please enter the ISBN of the book")]
    [RegularExpression("^(97[89])-\\d{1,5}-\\d{1,7}-\\d{1,7}-\\d$", 
        ErrorMessage = "The ISBN must follow the format 978-x-x-x-x or 979-x-x-x-x")]
    public string ISBN { get; set; }
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Please enter the publish date")]
    public DateTime PublishDate { get; set; }
    [Required(ErrorMessage = "Please enter publisher")]
    public string Publisher { get; set; }
    [Display(Name = "Book category")]
    public Category Category { get; set; }
    public DateTime Created { get; set; }
}