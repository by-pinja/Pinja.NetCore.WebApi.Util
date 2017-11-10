using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Protacon.NetCore.WebApi.Util.ModelValidation
{
    public static class ModelValidator
    {
        public static bool TryFindErrorsFromModel(object model, out ApiError error)
        {
            var vc = new ValidationContext(model);
            var errors = new List<ValidationResult>();

            Validator.TryValidateObject(model, vc, errors);

            if (errors.Count == 0)
            {
                error = null;
                return false;
            }

            error = new ApiError("INVALID_MODEL", ParseMessage(errors));
            return true;
        }

        private static string ParseMessage(List<ValidationResult> validationResult)
        {
            return validationResult
                .Aggregate("", (a, b) => $"{a}, {ParseMemberNames(b)}: {b}").Trim(',').Trim();
        }

        private static object ParseMemberNames(ValidationResult validationResult)
        {
            return validationResult.MemberNames
                .Aggregate("", (a, b) => $"{a}, {b}").Trim(',').Trim();
        }
    }
}
