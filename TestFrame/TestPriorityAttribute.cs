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
