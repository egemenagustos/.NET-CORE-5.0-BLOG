using BlogEntities.Concrete;
using BlogShared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEntities.Dtos
{
    public class UserDto : DtoGetBase
    {
        public User User { get; set; }
    }
}
