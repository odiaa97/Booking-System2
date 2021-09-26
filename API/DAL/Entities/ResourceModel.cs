using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ResourceModel
    {
        public int resourceId { get; set; }
        public int userId { get; set; }
        public string available { get; set; }

    }
}
