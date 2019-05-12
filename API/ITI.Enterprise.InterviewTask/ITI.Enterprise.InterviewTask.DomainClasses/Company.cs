using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.Enterprise.InterviewTask.DomainClasses
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public Company()
        {
          Id = Guid.NewGuid();
            Products = new HashSet<Product>();
        }
    }
}
