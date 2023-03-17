using BlogShared.Entities.Concrete;
using BlogShared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogShared.Utilities.Results.Abstract
{
    public interface IResult
	{
		public ResultStates ResultStates { get; }

		public string Message { get;}

		public Exception Exception { get;}

		public IEnumerable<ValidationError> ValidationErrors { get; set; }
	}
}
