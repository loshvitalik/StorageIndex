//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StorageIndex
{
    using System;
    using System.Collections.Generic;
    
    public partial class storage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public storage()
        {
            this.folders = new HashSet<folders>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public int type { get; set; }
        public int capacityMb { get; set; }
    
        public virtual mediaTypes mediaTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<folders> folders { get; set; }
    }
}
