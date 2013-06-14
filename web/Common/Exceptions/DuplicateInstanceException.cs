using System;

namespace Nextgal.ECare.Common.Exceptions
{
    /// <summary>
    /// Exception thrown when an istance is duplicate.
    /// </summary>
    
    [Serializable]
    public class DuplicateInstanceException : InstanceException
    {
        public DuplicateInstanceException(object key, string className) :
            base("Duplicate instance", key, className)
        {
        }
    }
}