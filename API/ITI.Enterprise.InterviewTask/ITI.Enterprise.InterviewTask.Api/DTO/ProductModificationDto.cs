using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ITI.Enterprise.InterviewTask.Api.DTO
{
    /// <summary>
    /// Product modification 
    /// </summary>
    public class ProductModificationDto
    {
        /// <summary>
        /// The id of the product to be modified
        /// </summary>
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IFormFile Photo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public float Price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string CompanyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string OldPhotoPath { get; set; }
    }
}
