using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIClient
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
       /* public override string ToString()
        {
            return this.name + ", " + this.age + " years old";
        }*/
    }
}
