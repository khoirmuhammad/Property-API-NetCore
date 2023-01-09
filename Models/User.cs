using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyAPI.Models
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public byte[] Password { get; set; } = default!;

        public byte[] PasswordKey { get; set; } = default!;

    }
}