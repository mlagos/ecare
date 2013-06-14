using System;
using System.Runtime.Serialization;

namespace Nextgal.ECare.Common.Exceptions
{
    /// <summary>
    /// Root of all the instance-related eceptions.
    /// </summary>
    [Serializable]
    public class InstanceException : ModelException
    {
        // additional fields
        private object m_Key;
        private string m_ClassName;

        public object Key
        {
            get
            {
                return m_Key;
            }

            set
            {
                m_Key = value;
            }
        }

        public string ClassName
        {
            get
            {
                return m_ClassName;
            }

            set
            {
                m_ClassName = value;
            }
        }

        // default constructor

        public InstanceException(string specificMessage, object key, string className)
            : base(specificMessage + " (key = '" + key.ToString() + "' - className = '" + className + "')")
        {
            m_Key = key;
            m_ClassName = className;
        }

        // protected constructor to de-seralize state data
        protected InstanceException(SerializationInfo info,
                                    StreamingContext context)
            : base(info, context)
        {
            // Initialize state 
            m_ClassName = info.GetString("m_ClassName");
            m_Key = info.GetValue("m_Key", typeof(object));
        }

        // override GetObjectData to serialize state data
        public override void GetObjectData(SerializationInfo info,
                                           StreamingContext context)
        {
            // Serialize this class' state and then call the base 
            // class GetObjectData
            info.AddValue("m_ClassName", m_ClassName, typeof(string));
            info.AddValue("m_Key", m_Key);
            base.GetObjectData(info, context);
        }


    }
}