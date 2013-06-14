using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nextgal.ECare.Common.Exceptions;

namespace Nextgal.ECare.Common.Exceptions
{
    /// <summary>
    /// Exception thrown when an instance is not found.
    /// </summary>
    [Serializable]
    public class NotAllowedException : InstanceException
    {
        public NotAllowedException(object key, string className):
            base("Not Allowed",key,className)
        {
        }

        public NotAllowedException() :
            base("Not Allowed", "", "")
        {
        }
    }
}