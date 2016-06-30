using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Umbraco_Validation_Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class UmbracoRequired : RequiredAttribute, IClientValidatable
    {
        private readonly string _propertyAlias;

        public UmbracoRequired(string propertyAlias)
        {
            _propertyAlias = propertyAlias;
        }
        
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoValidationHelper.GetPropertyValue(_propertyAlias);

            var error = FormatErrorMessage(metadata.DisplayName);
            var rule = new ModelClientValidationRequiredRule(error);

            yield return rule;
        }
    }
}
