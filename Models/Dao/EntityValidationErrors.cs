using System;
using System.Runtime.Serialization;

namespace Project_Year_2.Models.Dao
{
    [Serializable]
    internal class EntityValidationErrors : Exception
    {
        public EntityValidationErrors()
        {
        }

        public EntityValidationErrors(string message) : base(message)
        {
        }

        public EntityValidationErrors(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityValidationErrors(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}