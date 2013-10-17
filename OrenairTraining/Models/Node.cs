using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrenairTraining.Models
{
    public class Node
    {

        public bool Deleted { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public int? ParrentId { get; set; }
        public List<Node> Childrens { get; set; }

        public Node(int id, string name, int? parrent_id, int type_id, bool isDeleted)
        {
            Id = id;
            Name = name;
            TypeId = type_id;
            Deleted = isDeleted;
            ParrentId = parrent_id;
            Childrens = new List<Node>();
        }
    }
}
