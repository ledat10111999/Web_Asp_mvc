//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Do_an_Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class img
    {
        public int ID { get; set; }
        public string image { get; set; }
        public Nullable<int> IDpost { get; set; }
        public Nullable<int> IDimg { get; set; }
    
        public virtual post post { get; set; }
    }
}
