using System.Linq;
using FluentValidation;
using VM_ediaAPI.Data;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Validators
{
    public class ReactionValidator :  AbstractValidator<Reaction>
    {
        public ReactionValidator(DataContext dataContext)
        {
            RuleFor(x => x).Custom((value, context) => {
                var reactionExist = dataContext.Reactions.Any(x => x.PostId == value.PostId && x.UserId == value.UserId);
                if(reactionExist)
                {
                    context.AddFailure("UserId", "Ten użytwkonik już reagował na ten post");
                }
            });
        }

    }
}