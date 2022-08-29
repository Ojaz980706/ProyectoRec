namespace MVCTemplate.Models.ModelosCamara
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Empleados
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Empleados()
        {
            T_Transacciones = new HashSet<T_Transacciones>();
            T_Usuarios = new HashSet<T_Usuarios>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoEmpleado { get; set; }

        [Required]
        public string NombreCompleto { get; set; }

        public int AreaID { get; set; }

        [Required]
        public string Puesto { get; set; }

        public string Cara { get; set; }

        public virtual T_Areas T_Areas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Transacciones> T_Transacciones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Usuarios> T_Usuarios { get; set; }
    }
}
