using System;

namespace ITI.Enterprise.InterviewTask.DomainClasses
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public virtual Company Company { get; set; }
        public Guid CompanyId { get; set; }
        public string ImageUrl { get; set; }
        public Product()
        {
            Id = Guid.NewGuid();
        }
    }
}
