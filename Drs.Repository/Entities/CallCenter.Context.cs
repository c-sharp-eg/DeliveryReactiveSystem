﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CallCenterEntities : DbContext
    {
        public CallCenterEntities()
            : base("name=CallCenterEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AddressHierarchy> AddressHierarchy { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<CatDeliveryStatus> CatDeliveryStatus { get; set; }
        public virtual DbSet<CatPayment> CatPayment { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientPhone> ClientPhone { get; set; }
        public virtual DbSet<ClientPhoneStatistics> ClientPhoneStatistics { get; set; }
        public virtual DbSet<ClientStatistics> ClientStatistics { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Control> Control { get; set; }
        public virtual DbSet<ControlTitle> ControlTitle { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Franchise> Franchise { get; set; }
        public virtual DbSet<FranchiseData> FranchiseData { get; set; }
        public virtual DbSet<FranchisePhone> FranchisePhone { get; set; }
        public virtual DbSet<FranchiseStore> FranchiseStore { get; set; }
        public virtual DbSet<FranchiseStoreOffLine> FranchiseStoreOffLine { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<MenuModule> MenuModule { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<OrderToStore> OrderToStore { get; set; }
        public virtual DbSet<OrderToStoreLog> OrderToStoreLog { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<PosOrder> PosOrder { get; set; }
        public virtual DbSet<PosOrderItem> PosOrderItem { get; set; }
        public virtual DbSet<Recurrence> Recurrence { get; set; }
        public virtual DbSet<RegionA> RegionA { get; set; }
        public virtual DbSet<RegionB> RegionB { get; set; }
        public virtual DbSet<RegionC> RegionC { get; set; }
        public virtual DbSet<RegionD> RegionD { get; set; }
        public virtual DbSet<Setting> Setting { get; set; }
        public virtual DbSet<StoreAddressDistribution> StoreAddressDistribution { get; set; }
        public virtual DbSet<StoreStatus> StoreStatus { get; set; }
        public virtual DbSet<UserDetail> UserDetail { get; set; }
        public virtual DbSet<ZipCode> ZipCode { get; set; }
        public virtual DbSet<ViewStoreInfo> ViewStoreInfo { get; set; }
        public virtual DbSet<ViewUserInfo> ViewUserInfo { get; set; }
        public virtual DbSet<FranchiseDataVersion> FranchiseDataVersion { get; set; }
        public virtual DbSet<FranchiseDataFile> FranchiseDataFile { get; set; }
        public virtual DbSet<InfoCallCenter> InfoCallCenter { get; set; }
        public virtual DbSet<InfoClientTerminal> InfoClientTerminal { get; set; }
        public virtual DbSet<InfoClientTerminalVersion> InfoClientTerminalVersion { get; set; }
        public virtual DbSet<InfoServer> InfoServer { get; set; }
        public virtual DbSet<FranchiseButton> FranchiseButton { get; set; }
        public virtual DbSet<Resource> Resource { get; set; }
    }
}
