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
    
    public partial class Thing
    {
        public Thing()
        {
            this.ThingFields = new HashSet<ThingField>();
        }
    
        public int ThingId { get; set; }
        public string ThingName { get; set; }
        public System.DateTime DateAdded { get; set; }
    
        public virtual ICollection<ThingField> ThingFields { get; set; }
    }
}
