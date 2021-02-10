using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbAbstractions {
    public abstract class DbIdentity : IDbIdentity<int> {
        public int id { get; set; }
    }
    public abstract class DbIdentity<TId> : IDbIdentity<TId> {
        public virtual TId id { get; set; }
        
        [NotMapped]
        public string StringId
        {
            get => GetStringId(id);
            set => id = GetTypedId(value);
        }

        /// <summary>
        /// Converts an outgoing typed resource identifier to string format.
        /// </summary>
        protected virtual string GetStringId(TId value)
        {
            return EqualityComparer<TId>.Default.Equals(value, default) ? null : value.ToString();
        }

        /// <summary>
        /// Converts an incoming 'id' element to the typed resource identifier.
        /// </summary>
        protected virtual TId GetTypedId(string value)
        {
            return value == null ? default : (TId)Convert.ChangeType(value, typeof(TId));
        }
    }
}