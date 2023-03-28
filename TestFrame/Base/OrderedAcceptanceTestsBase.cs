using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace TestFrame.Base
{
    [TestCaseOrderer(ordererTypeName: "TestFrame.SequenceOrder", ordererAssemblyName: "TestFrame")]
    public class OrderedAcceptanceTestsBase<TFixture> : AcceptanceTestsBase<TFixture>, IClassFixture<TFixture> where TFixture : class
    {
        public OrderedAcceptanceTestsBase(string pathToConfigDirectory, ITestOutputHelper outputHelper) : base(pathToConfigDirectory, outputHelper)
        {
        }

        public OrderedAcceptanceTestsBase(TFixture testFixture, ITestOutputHelper outputHelper) : base(testFixture, outputHelper)
        {
        }
    }
}
