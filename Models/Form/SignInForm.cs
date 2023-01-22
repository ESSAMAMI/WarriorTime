using Google.Protobuf.WellKnownTypes;

namespace MartialTime.Models.Form
{
    public class SignInForm
    {
        public string? email { get; set; }
        public string? password { get; set; }
        public string? userType { get; set; }

    }
}
