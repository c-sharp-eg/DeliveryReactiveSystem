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
    
    public partial class AddressHierarchy
    {
        public int AddressHierarchyId { get; set; }
        public string RegionParent { get; set; }
        public string RegionChild { get; set; }
        public string PropertyParent { get; set; }
        public string PropertyChild { get; set; }
    }
}
