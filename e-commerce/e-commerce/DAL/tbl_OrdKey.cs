//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace e_commerce.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_OrdKey
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_OrdKey()
        {
            this.tbl_OrdHolder = new HashSet<tbl_OrdHolder>();
        }
    
        public long OrdID { get; set; }
        public Nullable<bool> IsDelivered { get; set; }
        public Nullable<System.DateTime> OrdDate { get; set; }
        public string CustName { get; set; }
        public string CustPhone { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_OrdHolder> tbl_OrdHolder { get; set; }
    }
}
