using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge.Mutants.Infrastructure.Data
{
    public abstract class EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
    }

    public abstract class AuditEntityBase : EntityBase
    {
        public DateTime? CreatedAt { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedByName { get; set; }
    }
}
