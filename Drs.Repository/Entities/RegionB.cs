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
    
    public partial class RegionB
    {
        public RegionB()
        {
            this.Address = new HashSet<Address>();
            this.RegionC = new HashSet<RegionC>();
            this.RegionD = new HashSet<RegionD>();
        }
    
        public int RegionId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public Nullable<int> RegionArId { get; set; }
        public Nullable<int> ZipCodeId { get; set; }
    
        public virtual ICollection<Address> Address { get; set; }
        public virtual Country Country { get; set; }
        public virtual RegionA RegionA { get; set; }
        public virtual ZipCode ZipCode { get; set; }
        public virtual ICollection<RegionC> RegionC { get; set; }
        public virtual ICollection<RegionD> RegionD { get; set; }
    }
}
