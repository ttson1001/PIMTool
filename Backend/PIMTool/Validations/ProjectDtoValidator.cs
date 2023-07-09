
using FluentValidation;
using PIMTool.Dtos.ProjectDtos.Request;

namespace PIMTool.Validations
{
    public class ProjectDtoValidator : AbstractValidator<AddProjectDto>
    {
        public ProjectDtoValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty();
            RuleFor(dto => dto.ProjectNumber)
                .Must(x => x.ToString().Length < 4)
                .WithMessage("Lỗi rồi");
            

        }
    }
}
