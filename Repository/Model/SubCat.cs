//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repository.Model
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class SubCat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubCat()
        {
            this.bookSubCat = new ObservableCollection<bookSubCat>();
        }
    
        public int ID { get; set; }
        public int CatID { get; set; }
        public string Name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<bookSubCat> bookSubCat { get; set; }
        public virtual Cat Cat { get; set; }
    }
}