//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wagenpark.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ONDERHOUD
    {
        public System.DateTime OnderhoudsDatum { get; set; }
        public decimal Kosten { get; set; }
        public string Kenteken { get; set; }
        public int WerkplaatsNr { get; set; }
    
        public virtual CAR CAR { get; set; }
        public virtual WERKPLAATS WERKPLAATS { get; set; }
    }
}
