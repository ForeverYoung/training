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
    
    public partial class question
    {
        public question()
        {
            this.answer1 = new HashSet<answer>();
        }
    
        public int question_id { get; set; }
        public string text { get; set; }
        public byte[] image { get; set; }
        public string answer { get; set; }
        public int type_id { get; set; }
        public int container_id { get; set; }
        public Nullable<int> material_id { get; set; }
        public Nullable<System.TimeSpan> time { get; set; }
        public Nullable<bool> deleted { get; set; }
        public string explanation_text { get; set; }
        public byte[] explanation_img { get; set; }
    
        public virtual ICollection<answer> answer1 { get; set; }
        public virtual container container { get; set; }
        public virtual material material { get; set; }
        public virtual questiontype questiontype { get; set; }
    }
}
