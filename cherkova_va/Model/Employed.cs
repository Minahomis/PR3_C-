//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cherkova_va.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employed
    {
        public int ID { get; set; }
        public int Company { get; set; }
        public int User { get; set; }
        public System.DateTime DataUs { get; set; }
        public Nullable<System.DateTime> DataUv { get; set; }
    
        public virtual CV CV { get; set; }
        public virtual Vacancy Vacancy { get; set; }
    }
}
