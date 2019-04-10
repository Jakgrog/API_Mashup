using System.Collections.Generic;
using System.Linq;

namespace ApiMashup.Validation
{
    /// <summary>
    /// Validation list interface. Declares a IsValid property
    /// a Validation function and message property that
    /// can be used on a List with validation objects.
    /// </summary>
    public interface IValidationList
    {
        bool IsValid { get; }
        void Validate();
        IEnumerable<string> Messages { get; }
    }

    /// <summary>
    /// Implements IValidationList.
    /// </summary>
    public class ValidationList : List<IValidation>, IValidationList
    {
        public bool IsValid
        {
            get
            {
                return this.All(v => v.IsValid);
            }
        }

        public void Validate()
        {
            foreach (var validation in this)
            {
                validation.Validate();
            }
        }

        public IEnumerable<string> Messages
        {
            get
            {
                return this.Where(v => !v.IsValid).Select(v => v.Message);
            }
        }
    }
}