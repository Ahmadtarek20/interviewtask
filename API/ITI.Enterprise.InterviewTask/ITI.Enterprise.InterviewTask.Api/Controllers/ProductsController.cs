using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ITI.Enterprise.InterviewTask.Api.DTO;
using ITI.Enterprise.InterviewTask.Api.Filters;
using ITI.Enterprise.InterviewTask.DomainClasses;
using ITI.Enterprise.InterviewTask.Repositories.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Enterprise.InterviewTask.Api.Controllers
{

    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    [ApiController]
    //[Authorize]

    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        private readonly ICompanyRepository _companyRepository;
        private readonly IHostingEnvironment _environment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productRepository"></param>
        /// <param name="companyRepository"></param>
        /// <param name="environment"></param>
        public ProductsController(IProductRepository productRepository, ICompanyRepository companyRepository, IHostingEnvironment environment)
        {
            _productRepository = productRepository;
            _environment = environment;
            _companyRepository = companyRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProductsResultFilter]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products.ToList());
        }

       /// <summary>
       /// Post Product method is used to make post requests products.
       /// </summary>
       /// <param name="productDto">Product Object</param>
       /// <returns>An ActionResult of type - Ok or BadRequest - depending on the sent product object.</returns>
       /// <remarks></remarks>
       [HttpPost]
       [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProductResultFilter]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDto>> PostProduct([FromForm]ProductCreationDto productDto)
       {
           var company = await _companyRepository.FindAsync(c => c.Name.ToLower() == productDto.CompanyName);
           if (company == null)
               return BadRequest();
           var imagePath = "default.png";
           if (productDto.Photo != null)
           {
               imagePath = GetSavedImagePath(productDto.Photo);
           }
           var product = new Product
           {
               Name = productDto.Name,
               Company = company,
               CompanyId = company.Id,
               ImageUrl = imagePath,
               Price = productDto.Price,
               
           };
        
           ActionResult result = Ok(_productRepository.Add(product));

           return result;
       }
        /// <summary>
        /// Get a product by its id
        /// </summary>
        /// <param name="id">The id of the product you want to retrieve </param>
        /// <returns>An ActionResult of type Ok with product object or NotFound or BadRequest depending on the sent id.</returns>
        [HttpGet("{id}")]
        [ProductResultFilter]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            ActionResult result;
           
            if (id == new Guid())
                result = BadRequest();
            else
            {
                var product = await _productRepository.FindAsync(p => p.Id == id);
                if (product != null)
                    result = Ok(product);
                else
                    result = NotFound();

            }
            return result;
        }

     

        private string GetSavedImagePath(IFormFile image)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
            var uniqueFileName = Guid.NewGuid() + "_" + image.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            image.CopyTo(new FileStream(filePath, FileMode.Create));

            return uniqueFileName;
        }
        /// <summary>
        /// Modifies a given product
        /// </summary>
        /// <param name="id">Product Id through routing</param>
        /// <param name="product">The product object you want to modify</param>
        /// <returns>An ActionResult of type Ok with the modified product or BadRequest or NotFound depending on the sent object.</returns>
        /// <remarks>
        /// Sample request\
        /// PUT /products/id \
        ///{\
        /// "id": "4e6a5d54-0d5f-456f-a5dd-02b43fa5b6b9",\
        ///"name" : "NewProduct", // The name of the new product\
        ///"price" : 2546,\
        ///"companyname": "Selected Company Name",\
        ///"oldphotopath": "ac29d90f-f2a4-4376-8fa7-f6c1f31cb664_Token.png", // The old ImageUrl of the product\
        ///"photo": "Photo Object (Input type=file)"\
        ///}
        /// </remarks>
        [HttpPut("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProductResultFilter]
        public async Task<ActionResult<ProductDto>> PutProduct([FromRoute]Guid id, [FromForm]ProductModificationDto product)
        {

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            if (id != product.Id)
            {
                return BadRequest();
            }
            var pro = await _productRepository.FindAsync(p => p.Id == id);

            if (pro == null)
                return NotFound();

            if (pro.ImageUrl != product.OldPhotoPath)
            {
                return BadRequest();
            }
          
           var uniqueFileName = product.Photo != null ? GetSavedImagePath(product.Photo) : product.OldPhotoPath;
            
            var company =
                await _companyRepository.FindAsync(c => c.Name.ToLower() == product.CompanyName.ToLower());
            if (company == null)
                return NoContent();
            pro.Name = product.Name;
            pro.Price = product.Price;
            pro.Company = company;
            pro.CompanyId = company.Id;
            pro.ImageUrl = uniqueFileName;
            var updated = _productRepository.Update(pro, id);
            if (updated != null)
            {
                return Ok(updated);
            }

            return NoContent();
        }
        /// <summary>
        /// Deletes the product that its id was sent through uri.
        /// </summary>
        /// <param name="id">TProduct id</param>
        /// <returns>An ActionResult with type Ok and the delete product or not found if the id was invalid</returns>
        [AllowAnonymous]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            IActionResult result;

            var product = await _productRepository.FindAsync(p=>p.Id == id);
            if (product == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(product);
            }

            return result;
        }
    }
}