using MvcCoreUploadAndDisplayImage_Demo.ViewModels;

namespace MvcCoreUploadAndDisplayImage_Demo.Validators
{
    public static class BannerViewModelValidator
    {
        public static ValidationResult Validate(BannerViewModel bannerViewModel)
        {
            var vr = new ValidationResult();

            if (bannerViewModel is null)
            {
                vr.Succeeded = false;
                vr.Errors.Add("Post view model cannot be null");
            }
            if (string.IsNullOrWhiteSpace(bannerViewModel.UrlForRedirect))
            {
                vr.Succeeded = false;
                vr.Errors.Add("Redirect url cannot be empty.");
            }
            if (bannerViewModel.PostImage is null)
            {
                vr.Succeeded = false;
                vr.Errors.Add("Image filed cannot be empty.");
            }

            return vr;
        }
    }
}
