using Legacy2Modern.Data;
using Legacy2Modern.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Legacy2Modern.Business.Services
{
    public class ServiceRequestService
    {
        private readonly ServiceRequestRepository
            _serviceRequestRepository;

        public ServiceRequestService()
        {
            _serviceRequestRepository =
                new ServiceRequestRepository();
        }

        public List<ServiceRequest> GetAll()
        {
            return _serviceRequestRepository.GetAll();
        }

        public ServiceRequest GetById(
            int serviceRequestId)
        {
            return _serviceRequestRepository
                .GetById(serviceRequestId);
        }

        public List<ServiceRequest> Search(
            string searchText,
            string status,
            string priority)
        {
            return _serviceRequestRepository.Search(
                searchText,
                status,
                priority);
        }
        public List<Customer> GetActiveCustomers()
        {
            return _serviceRequestRepository
                .GetActiveCustomers();
        }

        public List<CustomerProduct> GetCustomerProducts(
            int customerId)
        {
            return _serviceRequestRepository
                .GetCustomerProducts(customerId);
        }

        public List<Employee> GetActiveEmployees()
        {
            return _serviceRequestRepository
                .GetActiveEmployees();
        }

        public int CreateServiceRequest(
    int customerId,
    int? customerProductId,
    int? assignedToEmployeeId,
    string subject,
    string description,
    string requestType,
    string priority,
    int? createdByEmployeeId)
        {
            if (customerId <= 0)
                throw new ArgumentException(
                    "Customer is required.");

            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException(
                    "Subject is required.");

            if (string.IsNullOrWhiteSpace(priority))
                throw new ArgumentException(
                    "Priority is required.");

            var serviceRequest = new ServiceRequest
            {
                RequestNumber = GenerateRequestNumber(),
                CustomerId = customerId,
                CustomerProductId = customerProductId,
                AssignedToEmployeeId = assignedToEmployeeId,
                Subject = subject.Trim(),
                Description = string.IsNullOrWhiteSpace(description)
                    ? null
                    : description.Trim(),
                RequestType = requestType,
                Priority = priority,
                Status = "Open",
                CreatedDate = DateTime.Now,
                CreatedByEmployeeId = createdByEmployeeId
            };

            return _serviceRequestRepository
                .Create(serviceRequest);
        }

        private string GenerateRequestNumber()
        {
            return "SR-" +
                   DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        public void AssignEmployee( int serviceRequestId,  int? employeeId)
        {
            if (serviceRequestId <= 0)
            {
                throw new ArgumentException(
                    "Invalid service request.");
            }

            if (employeeId.HasValue &&
                employeeId.Value <= 0)
            {
                throw new ArgumentException(
                    "Invalid employee.");
            }

            _serviceRequestRepository
                .AssignEmployee(
                    serviceRequestId,
                    employeeId);
        }
    }
}