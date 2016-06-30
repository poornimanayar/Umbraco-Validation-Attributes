using System;
using System.ComponentModel;

namespace Umbraco_Validation_Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class UmbracoDisplayName : DisplayNameAttribute
    {
        private readonly string _propertyAlias;

        // This is a positional argument
        public UmbracoDisplayName(string propertyAlias)
        {
            _propertyAlias = propertyAlias;
        }

        public override string DisplayName
        {
            get { return UmbracoValidationHelper.GetPropertyValue(_propertyAlias); }
        }
    }
}
