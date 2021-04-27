using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Pinja.NetCore.WebApi.Util.ModelValidation;
using Xunit;

namespace Pinja.NetCore.WebApi.Util.Tests.ModelValidation
{
    public class ModelValidatorTests
    {
        public class ValidModel
        {
            public string NotChecked { get; set; }

            [Required]
            public string Required { get; set; } = "ABC VALUE HERE";

        }

        public class InvalidModel
        {
            [Required]
            public string RequiredExampleField { get; set; }
        }

        [Fact]
        public void WhenModelIsInvalid_ThenItErrorShouldBeReturned()
        {
            ModelValidator.TryFindErrorsFromModel(new InvalidModel(), out var error).Should().Be(true);
            error.Message.Should().Be("RequiredExampleField: The RequiredExampleField field is required.");
            error.Code.Should().Be("INVALID_MODEL");
        }

        [Fact]
        public void WhenValidModelIsUsed_ThenReturnFalseAndNoErrors()
        {
            ModelValidator.TryFindErrorsFromModel(new ValidModel(), out var error).Should().Be(false);
            error.Should().Be(null);
        }
    }
}
