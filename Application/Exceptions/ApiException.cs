using System.Globalization;
using System.Runtime.Serialization;

namespace Application.Exceptions
{
    [Serializable]
    public class ApiException : Exception
    {
        public ApiException() : base() { }

        public ApiException(string message) : base(message) { }

        public ApiException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }

        protected ApiException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}
