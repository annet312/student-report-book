using FluentValidation.Attributes;
using StudentReportBook.ViewModel.Validations;


namespace StudentReportBook.ViewModel
{
    [Validator(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
