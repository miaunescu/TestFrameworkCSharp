using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;
using static TestFrame.Attributes.TestCaseAttribute;

namespace TestFrame.Attributes
{
    public class TestCaseDiscoverer: ITraitDiscoverer
    {
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            string categoryName;
            string categoryValue;
            var attributeInfo = traitAttribute as ReflectionAttributeInfo;
            var testCaseAttribute = attributeInfo?.Attribute as TestCategoryAttribute;
            if (testCaseAttribute != null)
            {
                categoryName = testCaseAttribute.CategoryName;
                categoryValue = testCaseAttribute.CategoryValue;
            }
            else
            {
                var constructorArguments = traitAttribute.GetConstructorArguments().ToArray();
                categoryName = constructorArguments[0].ToString();
                categoryValue = constructorArguments[1].ToString();
            }
            yield return new KeyValuePair<string, string>(categoryName, categoryValue);
        }
    }
}

