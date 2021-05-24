using FluentValidation;
using VM_ediaAPI.Data;
using VM_ediaAPI.Dtos;

namespace VM_ediaAPI.Validators
{
    public class AddPostDtoValidator : AbstractValidator<AddPostDto>
    {
        public AddPostDtoValidator(DataContext dataContext)
        {
            RuleFor(x => x.Description).MaximumLength(200);
            RuleFor(x => x.Photos).NotEmpty();
        }
    }
}