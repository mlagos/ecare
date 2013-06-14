using System;
using System.Runtime.Serialization;

namespace Nextgal.ECare.Common.Exceptions
{
    /// <summary>
    /// Exception representing an error with parameter values. Encapsulation of
    /// another exception is allowed.
    /// </summary>
    [Serializable]
    public class WrongParameterValueException : Exception
    {
        // default constructor
        public WrongParameterValueException(): base()
        {
        }

        // constructor with exception message
        public WrongParameterValueException(string message, string param): base(message + ","+ param){}

        // constructor with message and inner exception
        public WrongParameterValueException(string message, Exception inner): base(message, inner)
        {
        }

        // protected constructor to de-seralize state data
        protected WrongParameterValueException(SerializationInfo info, StreamingContext context):base(info, context)
        {
        }
    }
}