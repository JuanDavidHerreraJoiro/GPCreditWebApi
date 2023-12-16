using FluentValidation.Results;
using System.Runtime.Serialization;

namespace Application.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        public List<string>? Errors { get; set; }

        public ValidationException() : base("Se han producido uno o más errores de validación. ")
        {
            Errors = new List<string>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors!.Add(failure.ErrorMessage);
            }
        }

        protected ValidationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {

        }
    }
}
