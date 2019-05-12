using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.Enterprise.InterviewTask.Api.DTO
{
    /// <summary>
    /// A product with Id, Name, ImageUrl, Price and CompanyName fields.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// The id of the product.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The name of the product.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The ImageUrl of the product.
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// The Price of the product.
        /// </summary>
        public float Price { get; set; }
        /// <summary>
        /// The Company Name of the product.
        /// </summary>
        public string CompanyName { get; set; }
    }
}
