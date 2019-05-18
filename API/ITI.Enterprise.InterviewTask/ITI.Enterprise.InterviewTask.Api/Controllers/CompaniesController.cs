using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ITI.Enterprise.InterviewTask.Api.DTO;
using ITI.Enterprise.InterviewTask.DomainClasses;
using Microsoft.AspNetCore.Mvc;
using ITI.Enterprise.InterviewTask.Repositories.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;

namespace ITI.Enterprise.InterviewTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    [Produces("application/json","application/xml")]
    //[Authorize]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyRepository"></param>
        /// <param name="mapper"></param>
        public CompaniesController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            return Ok(await _companyRepository.GetAllAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public  ActionResult<Company> PostCompany([FromBody] CompanyDto company)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var companyObject = new Company();
            _mapper.Map(company, companyObject);
         _companyRepository.Add(companyObject);
         return Ok(companyObject);
        }
      
    }
}
