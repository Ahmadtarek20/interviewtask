using System.ComponentModel.DataAnnotations;

namespace ITI.Enterprise.InterviewTask.Api.DTO
{
    /// <summary>
    /// A user with email and password fields for registration and issuing a token.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// The email of the user.
        /// </summary>
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// The password of the user.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
