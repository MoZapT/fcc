using System.ComponentModel.DataAnnotations;
using WAFcc.Interfaces.Common;

namespace WAFcc.Models
{
    public class BaseModel : IBaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsActive { get; set; }
        public bool IsNewOrEmpty { get { return Id == Guid.Empty; } }

        public BaseModel()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            IsActive = true;
        }
    }
}