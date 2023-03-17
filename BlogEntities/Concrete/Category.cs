using BlogShared.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEntities.Concrete
{
    public class Category : EntityBase, IEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
