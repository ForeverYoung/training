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
    
    public partial class session
    {
        public int session_id { get; set; }
        public System.DateTime datetime { get; set; }
        public int user_id { get; set; }
        public int testconfig_id { get; set; }
        public string ipaddress { get; set; }
        public Nullable<bool> deleted { get; set; }
    }
}
