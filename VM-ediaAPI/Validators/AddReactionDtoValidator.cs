using System.Linq;
using FluentValidation;
using VM_ediaAPI.Data;
using VM_ediaAPI.Dtos;

namespace VM_ediaAPI.Validators
{
    public class AddReactionDtoValidator : AbstractValidator<AddReactionDto>
    {
        public AddReactionDtoValidator(DataContext dataContext)
        {
            RuleFor(x => x.PostId).NotEmpty().Custom((value, context) => {
                var postIsExist = dataContext.Posts.Any(x => x.Id == value);
                if(!postIsExist)
                {
                    context.AddFailure("postId", "Podany post nie istnieje");
                }
            });


        }

       
    }
}