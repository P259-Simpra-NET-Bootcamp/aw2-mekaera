using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SimApi.Data.Context;
using SimApi.Data.Domain;
using SimApi.Data.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimApi.Service.Validators
{
    public class StaffValidator : AbstractValidator<Staff>
    {

        public StaffValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please specify a first name");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Please specify a last name");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Please specify a phone number");
            RuleFor(x => x.AddressLine1).NotEmpty().WithMessage("Please specify an address");
            RuleFor(x => x.City).NotEmpty().WithMessage("Please specify a city");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Please specify a country");
            RuleFor(x => x.Province).NotEmpty().WithMessage("Please specify a province");
            RuleFor(x => x.DateOfBirth).Must(BeOlderThan18Years).WithMessage("Must be at least 18 years old");
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Please specify an email")
            .EmailAddress().WithMessage("Invalid email format");
            


        }

        private bool BeOlderThan18Years(DateTime dateOfBirth)
        {
            var today = DateTime.Today; 
            var age = today.Year - dateOfBirth.Year; 

            return age >= 18; 
        }

    }
}
