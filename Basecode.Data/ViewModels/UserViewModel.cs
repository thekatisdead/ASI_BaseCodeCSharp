using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Basecode.Data.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [JsonProperty(PropertyName = "uname")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [JsonProperty(PropertyName = "confirm_pass")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Email")]
        [JsonProperty(PropertyName = "email")]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; } = string.Empty;
        [Required]
        [JsonProperty(PropertyName = "role")]
        public string RoleName { get; set; } = string.Empty;
    }
}
