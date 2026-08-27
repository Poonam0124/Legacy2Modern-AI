using Legacy2Modern.Data;
using Legacy2Modern.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Legacy2Modern.Business.Services
{
    public class ServiceRequestService
    {
        private const string StatusOpen =
    "Open";

        private const string StatusAssigned =
            "Assigned";

        private const string StatusInProgress =
            "In Progress";

        private const string StatusResolved =
            "Resolved";

        private const string StatusClosed =
            "Closed";

        private readonly ServiceRequestRepository
            _serviceRequestRepository;

        private readonly ServiceRequestCommentRepository
    _commentRepository;

        public ServiceRequestService()
        {
            _serviceRequestRepository = new ServiceRequestRepository();

            _commentRepository = new ServiceRequestCommentRepository();
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

        public void AssignEmployee(int serviceRequestId, int? employeeId)
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

        public void ChangeStatus(
      int serviceRequestId,
      string newStatus,
      int? changedByEmployeeId,
      string changeReason)
        {
            if (serviceRequestId <= 0)
            {
                throw new ArgumentException(
                    "Invalid service request.");
            }

            if (string.IsNullOrWhiteSpace(newStatus))
            {
                throw new ArgumentException(
                    "Status is required.");
            }

            var request =
                GetById(serviceRequestId);

            if (request == null)
            {
                throw new InvalidOperationException(
                    "Service request not found.");
            }

            string currentStatus =
                request.Status;

            if (string.Equals(
                currentStatus,
                newStatus,
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Service request is already in this status.");
            }

            ValidateStatusTransition(
                currentStatus,
                newStatus);

            _serviceRequestRepository
                .ChangeStatus(
                    serviceRequestId,
                    newStatus,
                    changedByEmployeeId,
                    currentStatus,
                    changeReason);
        }
        private void ValidateStatusTransition(
     string currentStatus,
     string newStatus)
        {
            bool isValid = false;

            switch (currentStatus)
            {
                case StatusOpen:
                    isValid =
                        newStatus == StatusAssigned;
                    break;

                case StatusAssigned:
                    isValid =
                        newStatus == StatusOpen ||
                        newStatus == StatusInProgress;
                    break;

                case StatusInProgress:
                    isValid =
                        newStatus == StatusAssigned ||
                        newStatus == StatusResolved;
                    break;

                case StatusResolved:
                    isValid =
                        newStatus == StatusInProgress ||
                        newStatus == StatusClosed;
                    break;

                case StatusClosed:
                    isValid = false;
                    break;
            }

            if (!isValid)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Invalid status transition: {0} → {1}.",
                        currentStatus,
                        newStatus));
            }
        }

        public List<ServiceRequestComment> GetComments(int serviceRequestId)
        {
            if (serviceRequestId <= 0)
            {
                throw new ArgumentException(
                    "Invalid service request.");
            }

            return _commentRepository
                .GetByServiceRequestId(
                    serviceRequestId);
        }
        public void AddComment(
    int serviceRequestId,
    int? employeeId,
    string commentText)
        {
            if (serviceRequestId <= 0)
            {
                throw new ArgumentException(
                    "Invalid service request.");
            }

            if (string.IsNullOrWhiteSpace(
                commentText))
            {
                throw new ArgumentException(
                    "Comment cannot be empty.");
            }

            if (commentText.Length > 5000)
            {
                throw new ArgumentException(
                    "Comment cannot exceed 5000 characters.");
            }

            var request =
                GetById(serviceRequestId);

            if (request == null)
            {
                throw new InvalidOperationException(
                    "Service request not found.");
            }

            _commentRepository.AddComment(
                serviceRequestId,
                employeeId,
                commentText.Trim());
        }

        public List<ServiceRequestHistory>
    GetStatusHistory(int serviceRequestId)
        {
            if (serviceRequestId <= 0)
            {
                throw new ArgumentException(
                    "Invalid service request.");
            }

            var request =
                GetById(serviceRequestId);

            if (request == null)
            {
                throw new InvalidOperationException(
                    "Service request not found.");
            }

            return _serviceRequestRepository
                .GetStatusHistory(serviceRequestId);
        }
    }
}