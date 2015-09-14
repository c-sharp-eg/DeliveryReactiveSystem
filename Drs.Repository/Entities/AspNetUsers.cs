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
    
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            this.AspNetUserClaims = new HashSet<AspNetUserClaims>();
            this.AspNetUserLogins = new HashSet<AspNetUserLogins>();
            this.ClientPhone = new HashSet<ClientPhone>();
            this.ClientPhone1 = new HashSet<ClientPhone>();
            this.Franchise = new HashSet<Franchise>();
            this.Franchise1 = new HashSet<Franchise>();
            this.FranchisePhone = new HashSet<FranchisePhone>();
            this.FranchisePhone1 = new HashSet<FranchisePhone>();
            this.AspNetRoles = new HashSet<AspNetRoles>();
            this.PosOrder = new HashSet<PosOrder>();
            this.OrderToStore = new HashSet<OrderToStore>();
            this.FranchiseStore = new HashSet<FranchiseStore>();
            this.FranchiseStore1 = new HashSet<FranchiseStore>();
            this.FranchiseStore2 = new HashSet<FranchiseStore>();
            this.FranchiseStoreOffLine = new HashSet<FranchiseStoreOffLine>();
            this.FranchiseStoreOffLine1 = new HashSet<FranchiseStoreOffLine>();
        }
    
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
    
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<ClientPhone> ClientPhone { get; set; }
        public virtual ICollection<ClientPhone> ClientPhone1 { get; set; }
        public virtual ICollection<Franchise> Franchise { get; set; }
        public virtual ICollection<Franchise> Franchise1 { get; set; }
        public virtual ICollection<FranchisePhone> FranchisePhone { get; set; }
        public virtual ICollection<FranchisePhone> FranchisePhone1 { get; set; }
        public virtual ICollection<AspNetRoles> AspNetRoles { get; set; }
        public virtual UserDetail UserDetail { get; set; }
        public virtual ICollection<PosOrder> PosOrder { get; set; }
        public virtual ICollection<OrderToStore> OrderToStore { get; set; }
        public virtual ICollection<FranchiseStore> FranchiseStore { get; set; }
        public virtual ICollection<FranchiseStore> FranchiseStore1 { get; set; }
        public virtual ICollection<FranchiseStore> FranchiseStore2 { get; set; }
        public virtual ICollection<FranchiseStoreOffLine> FranchiseStoreOffLine { get; set; }
        public virtual ICollection<FranchiseStoreOffLine> FranchiseStoreOffLine1 { get; set; }
    }
}
