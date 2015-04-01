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
    
    public partial class Client
    {
        public Client()
        {
            this.ClientStatistics = new HashSet<ClientStatistics>();
            this.OrderToStore = new HashSet<OrderToStore>();
            this.ClientPhone = new HashSet<ClientPhone>();
        }
    
        public int ClientId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual ICollection<ClientStatistics> ClientStatistics { get; set; }
        public virtual ICollection<OrderToStore> OrderToStore { get; set; }
        public virtual ICollection<ClientPhone> ClientPhone { get; set; }
    }
}
