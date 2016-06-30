using System;
using Umbraco.Web;

namespace Umbraco_Validation_Attributes
{
    public static class UmbracoValidationHelper
    {
        public static UmbracoHelper UmbracoHelper { get; set; }

        static UmbracoValidationHelper()
        {
            //Ensure we have a context
            if (UmbracoContext.Current == null)
            {
                throw new Exception("We have no Umbraco context, are you sure you are running this in Umbraco?");
            }

            //Setup Umbraco Helper for our inheriting classes to use as needed
            UmbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        public static string GetPropertyValue(string propertyAlias)
        {
            var currentPage = UmbracoHelper.TypedContent(UmbracoContext.Current.PageId);
            var errorText = currentPage.HasProperty(propertyAlias)?currentPage.GetPropertyValue<string>(propertyAlias):string.Empty;

            //Sanity checking it's not empty
            if (string.IsNullOrEmpty(errorText))
            {
                errorText = "Required";
            }
            return errorText;
        }



        public static string FormatErrorMessage(string name, string propertyAlias)
        {
            var currentPage = UmbracoHelper.TypedContent(UmbracoContext.Current.PageId);
            var errorText = currentPage.HasProperty(propertyAlias) ? currentPage.GetPropertyValue<string>(propertyAlias) : string.Empty;

            //Sanity checking it's not empty
            if (string.IsNullOrEmpty(errorText))
            {
                errorText = "Required";
            }

            // String replacment the token wiht our localised propertyname
            // The {{Field}} field is required
           // errorText = errorText.Replace("{{Field}}", name);

            //Return the value
            return errorText;
        }

        public static string FormatRangeErrorMessage(string name, string propertyAlias, object min,
            object max)
        {
            var errorText = UmbracoHelper.TypedContent(UmbracoContext.Current.PageId)
               .GetPropertyValue<string>(propertyAlias);

            //Sanity checking it's not empty
            if (string.IsNullOrEmpty(errorText))
            {
                errorText = "Range exceeded";
            }

            //Convert object to int's
            var minVal = Convert.ToInt32(min);
            var maxVal = Convert.ToInt32(max);

            // String replacment the token wiht our localised propertyname
            // The field {{Field}} must be between {0} and {1}
            errorText = errorText.Replace("{{Field}}", name);
            errorText = string.Format(errorText, minVal, maxVal);

            //Return the value
            return errorText;
        }

        public static string FormatCompareErrorMessage(string name, string propertyAlias,
            string otherProperty)
        {
            var currentPage = UmbracoHelper.TypedContent(UmbracoContext.Current.PageId);
            var errorText = currentPage.HasProperty(propertyAlias) ? currentPage.GetPropertyValue<string>(propertyAlias) : string.Empty;


            //Sanity checking it's not empty
            if (string.IsNullOrEmpty(errorText))
            {
                errorText = "Values does not match";
            }

            //TODO - Somehow figure out
            //Get other property display name, but from UmbracoDisplay as getting C# property name


            // String replacment the token with our localised propertyname
            //'{{Field}}' and '{0}' do not match.
            errorText = errorText.Replace("{{Field}}", name);
            errorText = string.Format(errorText, otherProperty);

            //Return the value
            return errorText;
        }

        public static string FormatLengthErrorMessage(string name, string propertyAlias, int maxLength,
            int minLength)
        {
            var currentPage = UmbracoHelper.TypedContent(UmbracoContext.Current.PageId);
            var errorText = currentPage.HasProperty(propertyAlias) ? currentPage.GetPropertyValue<string>(propertyAlias) : string.Empty;

            //Sanity checking it's not empty
            if (string.IsNullOrEmpty(errorText))
            {
                errorText = "Maximum length exceeded";
            }


            // it's ok to pass in the minLength even for the error message without a {2} param since String.Format will just
            // ignore extra arguments

            // String replacment the token wiht our localised propertyname
            // The field {{Field}} must be less than {0} (MaxLength)
            // The field {{Field}} must be less than {0} (MaxLength) & greater than {1} (MinLength)
            errorText = errorText.Replace("{{Field}}", name);
            errorText = string.Format(errorText, maxLength, minLength);

            //Return the value
            return errorText;
        }
    }
}
