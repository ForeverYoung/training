using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrenairTraining.Models
{
    public class TestToUserModel
    {
        public List<int> user_ids { get; set; }
        public int testconf_id { get; set; }
        public Nullable<System.DateTime> date_of_assignment { get; set; }
    }
}