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
    
    public partial class task_recovery
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public task_recovery()
        {
            this.task_recovery_schedule = new HashSet<task_recovery_schedule>();
        }
    
        public int id { get; set; }
        public int task_id { get; set; }
        public System.DateTime start_date { get; set; }
        public System.DateTime finish_date { get; set; }
    
        public virtual task task { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<task_recovery_schedule> task_recovery_schedule { get; set; }
    }
}
