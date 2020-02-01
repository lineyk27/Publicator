using System;

namespace Publicator.ApplicationCore.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public DateTime JoidDate { get; set; }
        public string ImageUrl { get; set; }
        public RoleDTO Role { get; set; }
    }
}
