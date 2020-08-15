using System;

namespace Publicator.Core.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public DateTime JoinDate { get; set; }
        public string ImageUrl { get; set; }
        public RoleDTO Role { get; set; }
    }
}
