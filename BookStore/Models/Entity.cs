using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
