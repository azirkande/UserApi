using FluentValidation;
using UserApi.Constants;

namespace UserApi.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginModelModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelModelValidator()
        {
            RuleFor(model => model.UserName).NotNull().NotEmpty().Matches(FieldConstants.EMAIL_PATTERN);
            RuleFor(model => model.Password).NotNull().NotEmpty().Length(4, FieldConstants.MAX_SECRET_LEN).Must(fv => !string.IsNullOrWhiteSpace(fv));
        }

    }
}
