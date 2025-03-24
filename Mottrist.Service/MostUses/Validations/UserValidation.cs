
using Mottrist.Domain.Global;
using Mottrist.Service.DTOS.Admin;

namespace Mottrist.Service.MostUses.Validations
{
    public static class UserValidation
    {
        public static Result ValidateRequiredFields(UserDto userDto)
        {
            var result = new Result();

            if (string.IsNullOrWhiteSpace(userDto.FirstName))
                result.AddError("الاسم الأول مطلوب");

            if (string.IsNullOrWhiteSpace(userDto.LastName))
                result.AddError("اسم العائلة مطلوب");

            if (string.IsNullOrWhiteSpace(userDto.Email))
                result.AddError("البريد الإلكتروني مطلوب");

            if (string.IsNullOrWhiteSpace(userDto.Password))
                result.AddError("كلمة المرور مطلوبة");

            return result;
        }

    }
}
