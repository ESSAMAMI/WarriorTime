namespace MartialTime.Models.Form
{
    public class UpdatePasswordForm
    {
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set;} = string.Empty;
    }
}
