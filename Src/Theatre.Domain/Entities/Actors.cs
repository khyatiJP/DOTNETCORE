using System.ComponentModel.DataAnnotations;

namespace Theatre.Domain.Entities
{
    public class Actors
    {
        [Key]
        public int ActorId { get; set; }
        public string Name { get; set; }


    }
}
