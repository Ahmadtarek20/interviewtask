using System.ComponentModel.DataAnnotations;

namespace ITI.Enterprise.InterviewTask.Api.DTO
{
    /// <summary>
    /// The Company object with Name field.
    /// </summary>
    public class CompanyDto
    {
       /// <summary>
       /// The name of the company.
       /// </summary>
       [Required]
        public string Name { get; set; }
    }
}
