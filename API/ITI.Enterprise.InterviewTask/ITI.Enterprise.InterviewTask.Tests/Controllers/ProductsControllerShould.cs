using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ITI.Enterprise.InterviewTask.Api.Controllers;
using ITI.Enterprise.InterviewTask.DomainClasses;
using ITI.Enterprise.InterviewTask.Api.DTO;
using ITI.Enterprise.InterviewTask.Repositories.Repositories;
using Microsoft.AspNetCore.Hosting;
using Moq;

namespace ITI.Enterprise.InterviewTask.Tests.Controllers
{
    public class ProductsControllerShould
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<ICompanyRepository> _mockCompanyRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IHostingEnvironment> _mockHostingEnv;
        private ProductsController _sut;

        public ProductsControllerShould()
        {
            _mockCompanyRepository = new Mock<ICompanyRepository>();
            _mockProductRepository = new Mock<IProductRepository>();

        // _sut = new ProductsController(_mockProductRepository, _mockCompanyRepository, _mockMapper, _mockHostingEnv);
        }
    }
}
