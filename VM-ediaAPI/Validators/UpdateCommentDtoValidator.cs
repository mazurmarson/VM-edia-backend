using FluentValidation;
using VM_ediaAPI.Data;
using VM_ediaAPI.Dtos;

namespace VM_ediaAPI.Validators
{
    public class UpdateCommentDtoValidator : AbstractValidator<UpdateCommentDto>
    {
        public UpdateCommentDtoValidator(DataContext dataContext)
        {
            RuleFor(x => x.Content).NotEmpty().MaximumLength(200).MinimumLength(2);
        }
    }
}