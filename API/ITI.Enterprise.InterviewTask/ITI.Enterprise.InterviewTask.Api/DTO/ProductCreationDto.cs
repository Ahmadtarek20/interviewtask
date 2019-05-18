using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ITI.Enterprise.InterviewTask.Api.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductCreationDto
    {
        /// <summary>
        ///    A product for creation with Name, Photo object (Input type="file"), Price and CompanyName fields.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// The company name of the product.
        /// </summary>
        [Required]
        public string CompanyName { get; set; }
        /// <summary>
        ///The product photo (Input type="file")
        /// </summary>
        public IFormFile Photo { get; set; }
        /// <summary>
        /// The price of the product.
        /// </summary>
        [Required]
        public float  Price { get; set; }
    }
}
