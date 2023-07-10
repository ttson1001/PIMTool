
using FluentValidation;
using PIMTool.Core.Dtos.ProjectDtos.Request;

namespace PIMTool.Validations
{
    public class ProjectDtoValidator : AbstractValidator<AddProjectDto>
    {
        public ProjectDtoValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty();
            RuleFor(dto => dto.ProjectNumber)
                .Must(x => x.ToString().Length < 4)
                .WithMessage("Project");
            RuleFor(dto => dto.Status).NotEmpty()
                .MaximumLength(3)
                .WithMessage("Status must 3 letter no than more");
            RuleFor(dto => dto.Customer).NotEmpty();
            RuleFor(dto => dto.GroupId).NotEmpty();
            RuleFor(dto => dto.Members).NotEmpty();

        }
    }
}
