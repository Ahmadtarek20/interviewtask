using System.Collections.Generic;
using System.Threading.Tasks;
using ITI.Enterprise.InterviewTask.DomainClasses;

namespace ITI.Enterprise.InterviewTask.Repositories.Repositories
{
    public interface ICompanyRepository: IRepository<Company>
    {
        //Task<IEnumerable<Company>> GetAllAsync();

    }
}