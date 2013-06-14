using System;
using System.Runtime.Serialization;

namespace Nextgal.ECare.Common.Exceptions
{
    /// <summary>
    /// Exception representing an error with parameter values. Encapsulation of
    /// another exception is allowed.
    /// </summary>
    [Serializable]
    public class DuplicatePositionException : Exception
    {
        // default constructor
        public DuplicatePositionException()
        {
        }

        // constructor with exception message
        public DuplicatePositionException(string message, string param) : base(message + " " + param)
        {
        }

        // constructor with message and inner exception
        public DuplicatePositionException(string message, Exception inner): base(message, inner)
        {
        }

        // protected constructor to de-seralize state data
        protected DuplicatePositionException(SerializationInfo info, StreamingContext context): base(info, context)
        {
        }
    }
}