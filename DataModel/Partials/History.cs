using System;
using System.Linq.Expressions;

namespace Wags.DataModel
{
    public partial class History:IEntity
    {
        public EntityState EntityState { get; set; }
    }
}
