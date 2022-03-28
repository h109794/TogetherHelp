using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Entity
{
    [Table("evaluation")]
    public class Evaluation : BaseEntity
    {
        public int UserId { get; set; }
        public bool IsAgree { get; set; }
    }
}
