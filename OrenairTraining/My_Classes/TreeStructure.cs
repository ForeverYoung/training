using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrenairTraining.Models;

namespace OrenairTraining.My_Classes
{
    public static class TreeStructure
    {

        /// <summary>
        /// Создание списка узлов дерева из таблицы сontainers
        /// </summary>
        /// <param name="list">Пустой список, тип Node, предназначенный для заполнения</param>
        public static List<Node> GenerateTreeToList()
        {
            List<Node> list = new List<Node>();
            using(OrenairTrainingEntities _db=new OrenairTrainingEntities())
            {
                foreach (var container in _db.container)
                {
                    if (list.Any(item => item.Id == container.ancestor_id))
                    {
                        list.Find(item => item.Id == container.ancestor_id).Childrens.Add(new Node(container.container_id, container.container_name, container.ancestor_id, container.type_id, (bool)container.deleted));
                    }
                    else                   
                        list.Add(new Node(container.container_id, container.container_name, container.ancestor_id, container.type_id, (bool)container.deleted));
                }
            }
            return list;
        }
    }
}