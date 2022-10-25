using MvcCoreUploadAndDisplayImage_Demo.ViewModels;

namespace MvcCoreUploadAndDisplayImage_Demo.Validators
{
    public static class PostViewModelValidator
    {
        public static ValidationResult Validate(PostViewModel postViewModel)
        {
            var vr = new ValidationResult();

            if (postViewModel is null)
            {
                vr.Succeeded = false;
                vr.Errors.Add("Post view model cannot be null");
            }
            if (string.IsNullOrWhiteSpace(postViewModel.Title))
            {
                vr.Succeeded = false;
                vr.Errors.Add("Title cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(postViewModel.Description))
            {
                vr.Succeeded = false;
                vr.Errors.Add("Description cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(postViewModel.UrlForRedirect))
            {
                vr.Succeeded = false;
                vr.Errors.Add("Redirect url cannot be empty.");
            }
            if (postViewModel.PostImage is null)
            {
                vr.Succeeded = false;
                vr.Errors.Add("Image filed cannot be empty.");
            }

            return vr;
        }
    }
}
