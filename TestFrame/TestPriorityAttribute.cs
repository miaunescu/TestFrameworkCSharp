using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestFrame
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TestPriorityAttribute : Attribute
    {
        public int Sequence { get; private set; }

        public TestPriorityAttribute(int sequenceNumber)
        {
            Sequence = sequenceNumber;
        }
    }
}
