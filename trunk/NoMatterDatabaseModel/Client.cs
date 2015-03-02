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
    
    public partial class Client
    {
        public Client()
        {
            this.Enquiries = new HashSet<Enquiry>();
            this.ClientPaymentTypes = new HashSet<ClientPaymentType>();
            this.Settings = new HashSet<Setting>();
            this.Discounts = new HashSet<Discount>();
            this.Sections = new HashSet<Section>();
            this.Users = new HashSet<User>();
            this.ClientDeliveryOptions = new HashSet<ClientDeliveryOption>();
        }
    
        public int ClientId { get; set; }
        public System.Guid ClientUUID { get; set; }
        public string ClientName { get; set; }
        public bool Enabled { get; set; }
    
        public virtual ICollection<Enquiry> Enquiries { get; set; }
        public virtual ICollection<ClientPaymentType> ClientPaymentTypes { get; set; }
        public virtual ICollection<Setting> Settings { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<ClientDeliveryOption> ClientDeliveryOptions { get; set; }
    }
}
