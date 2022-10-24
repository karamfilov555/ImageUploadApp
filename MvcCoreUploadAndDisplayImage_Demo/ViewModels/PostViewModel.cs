using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MvcCoreUploadAndDisplayImage_Demo.ViewModels
{
    public class PostViewModel
    {
        [Required(ErrorMessage = "Please enter title")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter description")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please choose Url for redirect when post clicked")]
        [Display(Name = "Url for Redirect")]
        public string UrlForRedirect { get; set; }

        [Required(ErrorMessage = "Please choose image")]
        [Display(Name = "Post Picture")]
        public IFormFile PostImage { get; set; }
    }
}
