//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrenairTraining.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class container
    {
        public container()
        {
            this.container1 = new HashSet<container>();
            this.containertodepartment = new HashSet<containertodepartment>();
            this.material = new HashSet<material>();
            this.question = new HashSet<question>();
        }
    
        public int container_id { get; set; }
        public string container_name { get; set; }
        public Nullable<int> ancestor_id { get; set; }
        public int type_id { get; set; }
        public Nullable<bool> deleted { get; set; }
    
        public virtual ICollection<container> container1 { get; set; }
        public virtual container container2 { get; set; }
        public virtual containertype containertype { get; set; }
        public virtual ICollection<containertodepartment> containertodepartment { get; set; }
        public virtual ICollection<material> material { get; set; }
        public virtual ICollection<question> question { get; set; }
    }
}
