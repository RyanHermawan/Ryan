//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Business.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class outcome
    {
        public int id { get; set; }
        public int project_id { get; set; }
        public System.DateTime date { get; set; }
        public double value { get; set; }
        public string currency { get; set; }
    
        public virtual project project { get; set; }
    }
}