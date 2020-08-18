using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CrucisCo.MathsQuizEngine
{
    /// <summary>
    /// Enum to determine the kind of number the first number of the DecimalProblem might be
    /// </summary>
    public enum DecimalPlaces
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4
    }

    /// <summary>
    /// Enum to determine the type of number the second number of the DecimalProblem might be
    /// </summary>
    public enum DecimalOperator
    {
        [EnumMember(Value="-4")]
        TenThousandths,

        [EnumMember(Value = "-3")]
        Thousandths,

        [EnumMember(Value = "-2")]
        Hundredths,

        [EnumMember(Value = "-1")]
        Tenths,

        [EnumMember(Value = "0")]
        Units,

        [EnumMember(Value = "1")]
        Tens,

        [EnumMember(Value = "2")]
        Hundreds,

        [EnumMember(Value = "3")]
        Thousands,

        [EnumMember(Value = "4")]
        TenThousands
    }

    /// <summary>
    /// Enum to determine the type of operation
    /// </summary>
    public enum Operation
    {
        [EnumMember(Value = "0")]
        Multiply,

        [EnumMember(Value = "1")]
        Divide
    }
}
