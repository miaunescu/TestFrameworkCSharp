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
        public int InvalidID { get; set; }
        public Int64 OrderId { get; set; }
        public Int64 Id { get; set; } 
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserNameList { get; set; }
        public string PasswordList { get; set; }
        public string InvalidUserName { get; set; }
        public string InvalidPassword { get; set; }

    }
}
