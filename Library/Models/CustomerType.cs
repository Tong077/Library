namespace Library.Models
{
    public class CustomerType
    {
        public int CustomerTypeId { get; set; }

        public string? CustomerTypeName { get; set; }

        public Customer? Customer { get; set; }
    }
}
