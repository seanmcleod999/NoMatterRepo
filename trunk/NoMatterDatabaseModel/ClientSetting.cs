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
    
    public partial class ClientSetting
    {
        public int ClientSettingId { get; set; }
        public int ClientId { get; set; }
        public string StringValue { get; set; }
        public Nullable<int> IntValue { get; set; }
        public short SettingId { get; set; }
    
        public virtual Setting Setting { get; set; }
        public virtual Client Client { get; set; }
    }
}
