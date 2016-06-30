using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Umbraco_Validation_Attributes
{
   public class UmbracoRegex : RegularExpressionAttribute, IClientValidatable
    {
        private readonly string _propertyAlias;


        public UmbracoRegex(string propertyAlias, string pattern)
            : base(pattern)
        {
            _propertyAlias = propertyAlias;
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoValidationHelper.GetPropertyValue(_propertyAlias);

            var error           = FormatErrorMessage(metadata.DisplayName);
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = error,
                ValidationType = "regex"
            };

            rule.ValidationParameters.Add("pattern", Pattern);

            yield return rule;
        }
    }
}
