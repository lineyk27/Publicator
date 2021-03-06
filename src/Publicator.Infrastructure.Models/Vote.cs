﻿using System;

namespace Publicator.Infrastructure.Models
{
    public class Vote : BaseEntity
    {
        public DateTime CreationDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public bool Up { get; set; }
    }
}
