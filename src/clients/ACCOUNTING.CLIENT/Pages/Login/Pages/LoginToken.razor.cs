using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace ACCOUNTING.CLIENT.Pages.Login.Pages
{
    partial class LoginToken
    {
        private MudForm form;
        private FileModel model = new();
        private FileModelFluentValidator ValidationRules = new();
        private bool SuppressOnChangeWhenInvalid;
        private MudFileUpload<IBrowserFile> _fileUpload;
        private string textInput = "Upload Token";
        private void UploadFiles(InputFileChangeEventArgs e)
        {
            //If SuppressOnChangeWhenInvalid is false, perform your validations here
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            textInput = model.File.Name.Split(".").First();
            Snackbar.Add($"Upload file named {model.File.Name.Split(".").First()}", MudBlazor.Severity.Info);

            //TODO upload the files to the server
        }

        private async Task Submit()
        {
            await form.Validate();

            if (form.IsValid)
            {
                Snackbar.Add("Submitted!");
            }
        }

        private async Task ClearAsync() {
            _fileUpload?.ClearAsync();
            textInput = "Upload Token";
        }

        public class FileModel
        {
            public string Name { get; set; }
            public IBrowserFile File { get; set; }
        }

        /// <summary>
        /// A standard AbstractValidator which contains multiple rules and can be shared with the back end API
        /// </summary>
        /// <typeparam name="OrderModel"></typeparam>
        public class FileModelFluentValidator : AbstractValidator<FileModel>
        {
            public FileModelFluentValidator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .Length(1, 100);
                RuleFor(x => x.File)
                .NotEmpty();
                When(x => x.File != null, () =>
                {
                    RuleFor(x => x.File.Size).LessThanOrEqualTo(10485760).WithMessage("The maximum file size is 10 MB");
                });
            }

            public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var result = await ValidateAsync(ValidationContext<FileModel>.CreateWithOptions((FileModel)model, x => x.IncludeProperties(propertyName)));
                if (result.IsValid)
                    return Array.Empty<string>();
                return result.Errors.Select(e => e.ErrorMessage);
            };
        }
    }
}
