using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.ModelBinding;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;
using ModelMetadata = System.Web.Mvc.ModelMetadata;

namespace Umbraco_Validation_Attributes
{
    public class UmbracoCompare : CompareAttribute, IClientValidatable
    {
        private readonly string _otherProperty;
        private readonly string _propertyAlias;


        public UmbracoCompare(string propertyAlias, string otherProperty)
            : base(otherProperty)
        {
            _propertyAlias = propertyAlias;
            _otherProperty = otherProperty;
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoValidationHelper.GetPropertyValue(_propertyAlias);

            var error = FormatErrorMessage(metadata.DisplayName);
            var rule = new ModelClientValidationEqualToRule(error, _otherProperty);

            yield return rule;
        }

      }
}
