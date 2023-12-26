using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnergyHub.Domain.Entities.Environment
{
    public class Environment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [Required]
        [MaxLength(-1)] // -1 represents MAX in SQL Server for NVARCHAR(MAX)
        public string EMSConnectionString { get; set; }

        [Required]
        [MaxLength(-1)] // -1 represents MAX in SQL Server for NVARCHAR(MAX)
        public string OptimiserConnectionString { get; set; }
    }
}
