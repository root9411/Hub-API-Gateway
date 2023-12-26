using System;
using MediatR;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnergyHub.Application.Commands.Customer
{
    public class CreateCustomerRequest : IRequest<Domain.Entities.Customer.Customer>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string EnvironmentDatabaseName { get; set; }
        public string AccessToken { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        //public CreateCustomerRequest(string firstName, string lastName, string email, string password, int environmentId, string accessToken, bool isActive, DateTime createdOn, DateTime updatedOn)
        //{
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Email = email;
        //    Password = password;
        //    EnvironmentId = environmentId;
        //    AccessToken = accessToken;
        //    IsActive = isActive;
        //    CreatedOn = createdOn;
        //    UpdatedOn = updatedOn;
        //}
    }
}
