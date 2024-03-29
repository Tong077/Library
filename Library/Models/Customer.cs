using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public bool IsHidden { get; set; }
        public string? CustomerCode {  get; set; }
       
        public int CustomerTypeId { get; set; }
        public string? CustomerName { get; set; }
        public string? Sex { get; set; }
        public DateTime Dob { get; set; }
        public string? Pob {  get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public ICollection<CustomerType>? CustomerTypes { get; set; }
    }
}
