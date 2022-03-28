using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Constants;

namespace UserApi.Models
{
    public class AddUserModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }

    public class AddUserModelValidator : AbstractValidator<AddUserModel>
    {
        public AddUserModelValidator()
        {
            RuleFor(model => model.UserName).NotNull().NotEmpty().Matches(FieldConstants.EMAIL_PATTERN);
            RuleFor(model => model.FirstName).NotNull().NotEmpty().MaximumLength(FieldConstants.MAX_NAME_LEN).Must(fv => !string.IsNullOrWhiteSpace(fv));
            RuleFor(model => model.LastName).NotNull().NotEmpty().MaximumLength(FieldConstants.MAX_NAME_LEN).Must(fv => !string.IsNullOrWhiteSpace(fv));
            RuleFor(model => model.Password).NotNull().NotEmpty().Length(4, FieldConstants.MAX_SECRET_LEN).Must(fv => !string.IsNullOrWhiteSpace(fv));
        }

    }
}
