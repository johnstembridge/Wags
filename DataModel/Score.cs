//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wags.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Score:IEntity
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public int Shots { get; set; }
        public int Points { get; set; }
        public int PlayerId { get; set; }
        public int RoundId { get; set; }
    
        public virtual Player Player { get; set; }
        public virtual Round Round { get; set; }
    }
}
