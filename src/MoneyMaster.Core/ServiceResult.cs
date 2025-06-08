namespace MoneyMaster.Service
{
    /// <summary>
    /// Wraps service calls that need to display additional information on error.
    /// </summary>
    public class ServiceResult
    {
        private bool _success;

        /// <summary>
        /// Gets the state of the <c>ServiceResult</c>. If false, <c>Errors</c> may be populated.
        /// </summary>
        public virtual bool Success
        {
            get => _success;
            set => _success = value;
        }

        /// <summary>
        /// Gets the errors associated with this service request, if any.
        /// </summary>
        public List<string>? Errors { get; protected set; }

        /// <summary>
        /// Indicates if the service call has resulted in a 'Severe' error (data corruption, fatally invalid parameters etc.)
        /// and the endpoint should take appropriate steps to restore functionality.
        /// </summary>
        public bool Severe { get; set; }

        /// <summary>
        /// Indicates if the ServiceResult has had errors logged after construction.
        /// </summary>
        public bool HasErrors => Errors != null;

        /// <summary>
        /// Creates a failed service result with no errors. Set <c>Value</c> on success or <c>AddErrors(param string[])</c>
        /// on failure.
        /// </summary>
        public ServiceResult()
        {
            _success = true;
            Errors = null;
        }

        /// <summary>
        /// Adds supplied strings to error list and marks result as unsuccessful.
        /// </summary>
        public virtual void AddErrors(params string[] errors)
        {
            Errors ??= new List<string>();
            Errors.AddRange(errors);
            _success = false;
        }
    }

    /// <summary>
    /// Wraps service calls that need to display additional information on error and return a value.
    /// </summary>
    public class ServiceResult<T> : ServiceResult
    {
        private T _value;

        /// <summary>
        /// The result of the service query. If result is successful value may be accessed; else throws.
        /// When set, the operation is marked as successful.
        /// </summary>
        /// <exception cref="InvalidOperationException">If accessed when <c>Success</c> is false</exception>
        public T Value
        {
            get => _success ? _value : throw new InvalidOperationException(
                $"ServiceResult<{typeof(T)}>.Value accessed after failed operation.");
            set
            {
                _value = value;
                _success = true;
            }
        }

        private bool _success;

        /// <summary>
        /// Gets the state of the <c>ServiceResult</c>. If true, <c>Value</c> may be used; otherwise accessing
        /// <c>Value</c> will throw and <c>Errors</c> may be populated. Do <b>NOT</b> set this property directly,
        /// instead set <c>Value</c> on success; otherwise call <c>AddErrors(params string[])</c>
        /// </summary>
        public override bool Success
        {
            get => _success;
            set =>
                throw new InvalidOperationException(
                    $"ServiceResult<{typeof(T)}>.Success cannot be directly set on generic ServiceResult<T>.");
        }

        /// <summary>
        /// Creates a failed service result with no errors. Set <c>Value</c> on success or <c>AddErrors(params string[])</c>
        /// on failure.
        /// </summary>
        public ServiceResult() : base()
        {
            _value = default!;
        }

        /// <summary>
        /// Adds supplied strings to error list, marks result as unsuccessful and removes the set value.
        /// </summary>
        public override void AddErrors(params string[] errors)
        {
            Errors ??= new List<string>();
            Errors.AddRange(errors);
            _value = default!;
            _success = false;
        }
    }
}
