namespace MVCTemplate.Models.ModelosCamara
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_Usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDusuario { get; set; }

        public int EmpleadoID { get; set; }

        public int Rol { get; set; }

        [Required]
        public string Contraseña { get; set; }

        public string contraseñaRostro { get; set; }

        public virtual T_Empleados T_Empleados { get; set; }

        public virtual T_Roles T_Roles { get; set; }
    }
}
