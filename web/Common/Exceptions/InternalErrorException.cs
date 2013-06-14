using System;
using System.Runtime.Serialization;

namespace Nextgal.ECare.Common.Exceptions
{
    /// <summary>
    /// Exception representing an internal error. Encapsulation of
    /// another exception is allowed.
    /// </summary>
    [Serializable]
    public class InternalErrorException : Exception
    {
        // default constructor

        public InternalErrorException(): base()
        {
        }

        // constructor with exception message
        public InternalErrorException(string message): base(message)
        {
        }

        // constructor with message and inner exception
        public InternalErrorException(string message, Exception inner): base(message, inner)
        {
        }

        // protected constructor to de-seralize state data

        protected InternalErrorException(SerializationInfo info,StreamingContext context): base(info, context)
        {
        }
    }
}