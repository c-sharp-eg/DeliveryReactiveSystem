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
    
    public partial class AspNetRoles
    {
        public AspNetRoles()
        {
            this.Permission = new HashSet<Permission>();
            this.AspNetUsers = new HashSet<AspNetUsers>();
        }
    
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Permission> Permission { get; set; }
        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
    }
}
