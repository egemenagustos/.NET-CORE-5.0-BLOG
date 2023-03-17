using BlogShared.Entities.Concrete;
using BlogShared.Utilities.Results.Abstract;
using BlogShared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogShared.Utilities.Results.Concrete
{
	public class Result : IResult
	{
		public Result(ResultStates resultStates)
		{
			ResultStates= resultStates;
		}

        public Result(ResultStates resultStates, IEnumerable<ValidationError> validationErrors)
        {
            ResultStates = resultStates;
			ValidationErrors = validationErrors;
        }

        public Result(ResultStates resultStates,string message)
		{
			ResultStates = resultStates;
			Message = message;
		}

        public Result(ResultStates resultStates, string message, IEnumerable<ValidationError> validationErrors)
        {
            ResultStates = resultStates;
            Message = message;
            ValidationErrors = validationErrors;
        }

        public Result(ResultStates resultStates, string message, Exception exception)
		{
			ResultStates = resultStates;
			Message = message;
			Exception = exception;
		}

        public Result(ResultStates resultStates, string message, Exception exception, IEnumerable<ValidationError> validationErrors)
        {
            ResultStates = resultStates;
            Message = message;
            Exception = exception;
            ValidationErrors = validationErrors;
        }

        public ResultStates ResultStates { get; }

		public string Message { get; }

		public Exception Exception { get; }

        public IEnumerable<ValidationError> ValidationErrors { get; set ; }
    }
}
