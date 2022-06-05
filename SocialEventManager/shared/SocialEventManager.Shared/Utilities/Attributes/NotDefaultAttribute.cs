using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants.Validations;

namespace SocialEventManager.Shared.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class NotDefaultAttribute : ValidationAttribute
    {
        public NotDefaultAttribute()
            : base(ValidationConstants.TheFieldMustNotHaveTheDefaultValue)
        {
        }

        public override bool IsValid(object value)
        {
            if (value is null)
            {
                return true;
            }

            Type type = value.GetType();
            if (type.IsValueType)
            {
                object defaultValue = Activator.CreateInstance(type);
                return !value.Equals(defaultValue);
            }

            return true;
        }
    }
}
