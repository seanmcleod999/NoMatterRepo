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
    
    public partial class ClientPage
    {
        public int ClientPageId { get; set; }
        public int ClientId { get; set; }
        public string PageText { get; set; }
        public string PageName { get; set; }
    
        public virtual Client Client { get; set; }
    }
}
