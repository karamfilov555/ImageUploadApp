using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MvcCoreUploadAndDisplayImage_Demo.Config;
using MvcCoreUploadAndDisplayImage_Demo.Helpers;
using MvcCoreUploadAndDisplayImage_Demo.Validators;
using MvcCoreUploadAndDisplayImage_Demo.ViewModels;
using NToastNotify;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MvcCoreUploadAndDisplayImage_Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly AzureStorageConfig storageConfig;
        private readonly IToastNotification toast;
        private static bool isLogged = false;


        public HomeController(
            IOptions<AzureStorageConfig> options,
            IToastNotification toast)
        {
            this.toast = toast;
            storageConfig = options.Value;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (loginViewModel is null || 
                string.IsNullOrWhiteSpace(loginViewModel.Username) || 
                string.IsNullOrWhiteSpace(loginViewModel.Password))
            {
                toast.AddErrorToastMessage("Please fill all required fields!");
                return View("Index");
            }
            if (!loginViewModel.Username.Equals("dr.adm1n"))
            {
                toast.AddErrorToastMessage("Invalid credentials!");
                return View("Index");
            }
            if (!loginViewModel.Password.Equals("p4$$word"))
            {
                toast.AddErrorToastMessage("Invalid credentials!");
                return View("Index");
            }

            isLogged = true;
            toast.AddSuccessToastMessage("Welcome back Admin!");
            return View("Upload");
        }

        public async Task<IActionResult> Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(PostViewModel model)
        {
            if (!isLogged)
            {
                toast.AddErrorToastMessage("Please login");
                return View("Index");
            }
            bool isUploaded = false;

            var vr = PostViewModelValidator.Validate(model);

            try
            {
                if (vr.Succeeded)
                {
                    if (storageConfig.AccountKey == string.Empty || storageConfig.AccountName == string.Empty)
                        return BadRequest("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

                    if (storageConfig.ImageContainer == string.Empty)
                        return BadRequest("Please provide a name for your image container in the azure blob storage");

                    if (StorageHelper.IsImage(model.PostImage))
                    {
                        if (model.PostImage.Length > 0)
                        {
                            using Stream stream = model.PostImage.OpenReadStream();
                            isUploaded = await StorageHelper.UploadFileToStorage(stream, model, storageConfig);
                        }
                        else
                        {
                            return new UnsupportedMediaTypeResult();
                        }
                    }
                    else
                    {
                        toast.AddErrorToastMessage($"Unsupported file format! Please provide one of the following file formats: \".jpg\", \".png\", \".gif\", \".jpeg\"");
                        return Redirect("Upload");
                    }
                }
                else
                {
                    toast.AddErrorToastMessage("Please fill all required fields!");
                    return View("Upload");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The specified blob already exists."))
                {
                    toast.AddErrorToastMessage("Image with the same name already exists in the database.");
                    return View("Upload");
                }
                toast.AddErrorToastMessage(ex.Message);
                return View("Upload");
            }
            if (isUploaded)
            {
                toast.AddSuccessToastMessage("Your post is uploaded succesfully!");
            }
            else
            {
                toast.AddErrorToastMessage("Something went wrong!");
            }
            return Redirect("Upload");
        }
    }
}
