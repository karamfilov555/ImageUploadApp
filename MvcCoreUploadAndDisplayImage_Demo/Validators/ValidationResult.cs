using System.Collections.Generic;

namespace MvcCoreUploadAndDisplayImage_Demo.Validators
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Succeeded = true;
            Errors = new List<string>();
        }

        public ValidationResult(bool isSucceeded, List<string> errors)
        {
            Succeeded = isSucceeded;
            Errors = errors;
        }

        public bool Succeeded { get; set; }

        public List<string> Errors { get; set; }
    }
}
