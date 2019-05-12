using System;
using ITI.Enterprise.InterviewTask.Api.Controllers;
using Xunit;
using ITI.Enterprise.InterviewTask.DomainClasses;
using ITI.Enterprise.InterviewTask.Api.DTO;
using ITI.Enterprise.InterviewTask.Repositories.Repositories;
using Moq;

namespace ITI.Enterprise.InterviewTask.Tests.Controllers
{
    public class CompaniesControllerShould
    {
        private readonly Mock<ICompanyRepository> _mock;
        private readonly CompaniesController _sut;
        public CompaniesControllerShould()
        {
            _mock = new Mock<ICompanyRepository>();
           // _sut = new CompaniesController(_mock);
        }
        [Fact]
        public void GetMethodS()
        {
            
        }
    }
}
