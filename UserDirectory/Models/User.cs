using System.ComponentModel.DataAnnotations;

namespace UserDirectory.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Range(0, 120)]
        public int Age { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required, StringLength(10, MinimumLength = 4)]
        public string Pincode { get; set; }
    }
}

