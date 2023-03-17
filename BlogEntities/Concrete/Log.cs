﻿using BlogShared.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEntities.Concrete
{
    public class Log : IEntity
    {
        public int Id { get; set; }

        public string MachineName { get; set; }

        public DateTime Logged { get; set; }

        public string Level { get; set; }

        public string Message { get; set; }

        public string Logger { get; set; }

        public string CallSite { get; set; }

        public string Exception { get; set; }
    }
}
