using System;
using System.Runtime.Serialization;

namespace Nextgal.ECare.Common.Exceptions
{
    /// Root of all the model exceptions.
    /// </summary>
    [Serializable]
    public class ModelException : Exception
    {
        // default constructor
        public ModelException()
            : base()
        {
        }

        // constructor with exception message
        public ModelException(string message)
            : base(message)
        {
        }

        // protected constructor to de-seralize state data
        protected ModelException(SerializationInfo info,
                                 StreamingContext context)
            : base(info, context)
        {
        }
    }
}