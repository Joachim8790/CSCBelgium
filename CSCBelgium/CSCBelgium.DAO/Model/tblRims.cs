//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CSCBelgium.DAO.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblRims
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblRims()
        {
            this.tblRimImages = new HashSet<tblRimImages>();
        }
    
        public int RimID { get; set; }
        public int RimBrandID { get; set; }
        public string RimModel { get; set; }
        public int RimPrice { get; set; }
        public byte Sold { get; set; }
    
        public virtual tblRimBrands tblRimBrands { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblRimImages> tblRimImages { get; set; }
    }
}
