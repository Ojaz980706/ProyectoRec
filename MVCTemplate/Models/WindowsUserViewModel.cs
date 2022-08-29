
namespace MVCTemplate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public class WindowsUserViewModel
    {
        [Key]
        public string WindowsUser { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string UserType { get; set; }
        public string Department { get; set; }
    }
}