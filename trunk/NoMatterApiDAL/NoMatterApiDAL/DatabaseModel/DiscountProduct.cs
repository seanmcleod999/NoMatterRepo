//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NoMatterApiDAL.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class DiscountProduct
    {
        public short DiscountProductId { get; set; }
        public short DiscountId { get; set; }
        public int ProductId { get; set; }
    
        public virtual Discount Discount { get; set; }
        public virtual Product Product { get; set; }
    }
}
