using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class UserRoleModel
    {
        // this class made to assign roles to specific user
        public int UserId { get; set; }
        public string[] Roles { get; set; }
    }
}
