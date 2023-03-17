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
	public class DataResult<T> : IDataResult<T>
	{
		public DataResult(ResultStates resultStates ,T data)
		{
			Data = data;
			ResultStates = resultStates;
		}

        public DataResult(ResultStates resultStates, T data, IEnumerable<ValidationError> validationErrors)
        {
            Data = data;
            ResultStates = resultStates;
			ValidationErrors = validationErrors;
        }

        public DataResult(ResultStates resultStates,string message, T data)
		{
			Data = data;
			ResultStates = resultStates;
			Message = message;
		}

        public DataResult(ResultStates resultStates, string message, T data, IEnumerable<ValidationError> validationErrors)
        {
            Data = data;
            ResultStates = resultStates;
            Message = message;
            ValidationErrors = validationErrors;
        }

        public DataResult(ResultStates resultStates, string message, T data, Exception exception)
		{
			Data = data;
			ResultStates = resultStates;
			Message = message;
			Exception = exception;
		}

        public DataResult(ResultStates resultStates, string message, T data, Exception exception, IEnumerable<ValidationError> validationErrors)
        {
            Data = data;
            ResultStates = resultStates;
            Message = message;
            Exception = exception;
            ValidationErrors = validationErrors;
        }

        public T Data { get; }

		public ResultStates ResultStates { get; }

		public string Message { get; }

		public Exception Exception { get; }

        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}
