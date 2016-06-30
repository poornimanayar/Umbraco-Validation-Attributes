using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Umbraco_Validation_Attributes
{
    public class UmbracoStringLength : StringLengthAttribute, IClientValidatable
    {
        private readonly string _propertyAlias;
        private readonly int _minimumLength;
        private readonly int _maximumLength;

        public UmbracoStringLength(string propertyAlias, int minimumLength, int maximumLength)
            : base(maximumLength)
        {
            _propertyAlias = propertyAlias;
            _minimumLength = minimumLength;
            _maximumLength = maximumLength;
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoValidationHelper.GetPropertyValue(_propertyAlias);

            var error = FormatErrorMessage(metadata.DisplayName);
            var rule = new ModelClientValidationStringLengthRule(error, _minimumLength, _maximumLength);

            yield return rule;
        }
    }
}