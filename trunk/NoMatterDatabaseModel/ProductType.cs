//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NoMatterDatabaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductType
    {
        public ProductType()
        {
            this.Products = new HashSet<Product>();
        }
    
        public short ProductTypeId { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Product> Products { get; set; }
    }
}
