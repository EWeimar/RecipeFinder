using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Models
{
    public enum MeasureUnit
    { 
       [EnumMember(Value = "-")]
       None,
       [EnumMember(Value = "ml")]
       Ml,
       [EnumMember(Value = "cl")]
       Cl,
       [EnumMember(Value = "dl")]
       Dl,
       [EnumMember(Value = "l")]
       L,
       [EnumMember(Value = "g")]
       G,
       [EnumMember(Value = "kg")]
       Kg,
       [EnumMember(Value = "tsk")]
       Tsk,
       [EnumMember(Value = "spsk")]
       Spsk,
       [EnumMember(Value = "knsp")]
       Knsp,
       [EnumMember(Value = "stk")]
       Stk,

    }
}
