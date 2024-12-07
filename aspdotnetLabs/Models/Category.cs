using System.ComponentModel.DataAnnotations;

namespace aspdotnetLabs.Models;
public enum Category 
{ 
    [Display(Name = "Fantasy books")]Fantesy = 1, 
    [Display(Name = "Science books")]Science = 2, 
    [Display(Name = "Historical books")]Historical = 3, 
    [Display(Name = "Psychology books")]Psychology = 4,
    [Display(Name = "Thriller books")]Thriller = 5,
    [Display(Name = "Drama books")]Drama = 6
}