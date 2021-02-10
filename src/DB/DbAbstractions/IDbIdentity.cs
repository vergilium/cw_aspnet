namespace DbAbstractions {
    public interface IDbIdentity {
        /// <summary>
        ///  The value for element 'id' in a JSON:API request or response.
        /// </summary>
        string StringId { get; set; }
    }

    public interface IDbIdentity<TId> {
        /// <summary>
        /// The typed identifier as used by the underlying data store (usually numeric).
        /// </summary>
        TId id { get; set; }
    }
}