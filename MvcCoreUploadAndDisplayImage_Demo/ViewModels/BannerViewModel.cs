using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MvcCoreUploadAndDisplayImage_Demo.ViewModels
{
    public class BannerViewModel
    {
        [Required(ErrorMessage = "Please choose Url for redirect when post clicked")]
        [Display(Name = "Url for Redirect")]
        public string UrlForRedirect { get; set; }

        [Required(ErrorMessage = "Please choose image")]
        [Display(Name = "Post Picture")]
        public IFormFile PostImage { get; set; }
    }
}
