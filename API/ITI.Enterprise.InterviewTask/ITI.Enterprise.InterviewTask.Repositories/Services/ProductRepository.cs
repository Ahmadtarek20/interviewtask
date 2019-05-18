using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.Enterprise.InterviewTask.DataModel;
using ITI.Enterprise.InterviewTask.DomainClasses;
using ITI.Enterprise.InterviewTask.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITI.Enterprise.InterviewTask.Repositories.Services
{
   public class ProductRepository : Repository<Product,AppDbContext>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await Set.Include(p=>p.Company).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsAsync(int page = 1, int size = 10)
        {
            var entries = await Set.Include(p => p.Company).Skip(((page - 1) * size)).Take(size).ToListAsync();
            return entries;
        }
    }
}
