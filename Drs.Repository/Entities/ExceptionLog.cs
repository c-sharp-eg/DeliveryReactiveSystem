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
    
    public partial class ExceptionLog
    {
        public System.Guid ExceptionLogUid { get; set; }
        public string Username { get; set; }
        public string MsgException { get; set; }
        public string InnerException { get; set; }
        public string StackTrace { get; set; }
        public string ParamsValues { get; set; }
        public System.DateTime Timestamp { get; set; }
    }
}
