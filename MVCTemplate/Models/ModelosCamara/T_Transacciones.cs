namespace MVCTemplate.Models.ModelosCamara
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Transacciones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDtransaccion { get; set; }

        public DateTime FechayHora { get; set; }

        public int Material_ID { get; set; }

        public int EmpleadoID { get; set; }

        public int Cantidad { get; set; }

        public virtual T_Empleados T_Empleados { get; set; }

        public virtual T_Materiales T_Materiales { get; set; }
    }
}
