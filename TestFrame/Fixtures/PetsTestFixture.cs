using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFrame.Base;

namespace TestFrame.Fixtures
{
    public class PetsTestFixture: RestClientFixture
    {
        public string Api { get; set; }
        public int PetID { get; set; }
    }
}
