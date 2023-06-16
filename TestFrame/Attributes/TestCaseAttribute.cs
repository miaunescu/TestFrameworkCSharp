using Xunit.Sdk;

namespace TestFrame.Attributes
{
    public class TestCaseAttribute
    {
        //Custom Attribute used to differentiate tests purpose or filter them taking into consideration specific categories
        [TraitDiscoverer("TestFrame.Atrributes.TestCaseDiscoverer", "TestFrame")]
        [AttributeUsage(AttributeTargets.Method)]
        public class TestCategoryAttribute : Attribute, ITraitAttribute
        {
            public string CategoryName { get; set; }
            public string CategoryValue { get; set; }
            public TestCategoryAttribute(string categoryName, string categoryValue)
            {
                CategoryName = categoryName;
                CategoryValue = categoryValue;
            }
        }
    }
}
