using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFrame.Builder
{
    public class RestFactory
    {
        private RestBuilder restBuilder;
        public RestFactory(RestBuilder restBuilder)
        {
            this.restBuilder = restBuilder;
        }

        public RestBuilder Create()
        {
            return restBuilder;
        }

    }
}
