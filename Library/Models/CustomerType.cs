﻿namespace Library.Models
{
    public class CustomerType
    {
        public int CustomerTypeId { get; set; }

        public string? CustomerTypeName { get; set; }

        public ICollection <Customer>? Customer { get; set; }
    }
}
