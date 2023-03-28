using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestFrame
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class FactSequenceAttribute : FactAttribute
    {
        public int Sequence { get; private set; }

        public FactSequenceAttribute(int sequenceNumber)
        {
            Sequence = sequenceNumber;
        }
    }
}
