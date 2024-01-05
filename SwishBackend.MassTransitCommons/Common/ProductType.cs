using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SwishBackend.MassTransitCommons.Common
{
    public enum ProductType
    {
        [EnumMember(Value = "Painting")]
        Painting,
        [EnumMember(Value = "Car")]
        Car
    }
}
