using System.Collections.Generic;
using System.Threading.Tasks;
using ITI.Enterprise.InterviewTask.DomainClasses;

namespace ITI.Enterprise.InterviewTask.Repositories.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        new Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetProductsAsync(int page = 1, int size = 10);

    }
}
