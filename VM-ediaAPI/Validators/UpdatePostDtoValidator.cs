using FluentValidation;
using VM_ediaAPI.Data;
using VM_ediaAPI.Dtos;

namespace VM_ediaAPI.Validators
{
    public class UpdatePostDtoValidator : AbstractValidator<UpdatePostDto>
    {
        public UpdatePostDtoValidator(DataContext dataContext)
        {
            RuleFor(x => x.Description).NotEmpty().MaximumLength(200);
        }
    }
}