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
    
    public partial class TimeBased
    {
        public int RuleID { get; set; }
        public int BookID { get; set; }
        public string Describe { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual TimeRule TimeRule { get; set; }
    }
}
