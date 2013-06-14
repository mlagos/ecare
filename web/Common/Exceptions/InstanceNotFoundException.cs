using System;
using Nextgal.ECare.Common.Exceptions;

namespace Nextgal.ECare.Common.Exceptions
{
    /// <summary>
    /// Exception thrown when an instance is not found.
    /// </summary>
    [Serializable]
    public class InstanceNotFoundException : InstanceException
    {
        public InstanceNotFoundException(object key, string className) :
            base("Instance not found", key, className)
        {
        }

        public InstanceNotFoundException()
            :
                base("Instance not found", "", "")
        {
        }
    }
}