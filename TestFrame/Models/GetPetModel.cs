using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFrame.Models
{
    public class GetPetModel
    {
        public int id { get; set; }
        public List<CategoryModel> category { get; set; }
        public string name { get; set; }
        public string[] photoUrls { get; set; }
        public List<TagsModel>[] tags { get; set; }
        public string status { get; set; }
    }   
}
