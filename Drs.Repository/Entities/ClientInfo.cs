//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Drs.Repository.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientInfo
    {
        public int ClientInfoId { get; set; }
        public int ServerInfoId { get; set; }
        public string ClientHost { get; set; }
        public string ClientCode { get; set; }
        public int CallCenterInfoId { get; set; }
    
        public virtual CallCenterInfo CallCenterInfo { get; set; }
    }
}
