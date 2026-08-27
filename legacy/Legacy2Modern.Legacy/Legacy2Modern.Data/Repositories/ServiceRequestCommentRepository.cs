using System;
using System.Linq;
using System.Collections.Generic;

namespace Legacy2Modern.Data.Repositories
{
    public class ServiceRequestCommentRepository
    {
        public List<ServiceRequestComment> GetByServiceRequestId(
            int serviceRequestId)
        {
            using (var context =
                new Legacy2ModernDBEntities())
            {
                return context.ServiceRequestComments
                    .Include("Employee")
                    .Where(x =>
                        x.ServiceRequestId ==
                        serviceRequestId)
                    .OrderBy(x => x.CreatedDate)
                    .ToList();
            }
        }

        public void AddComment(
            int serviceRequestId,
            int? employeeId,
            string commentText)
        {
            using (var context =
                new Legacy2ModernDBEntities())
            {
                var comment =
                    new ServiceRequestComment
                    {
                        ServiceRequestId =
                            serviceRequestId,

                        EmployeeId =
                            employeeId,

                        CommentText =
                            commentText,

                        CreatedDate =
                            DateTime.Now
                    };

                context.ServiceRequestComments
                    .Add(comment);

                context.SaveChanges();
            }
        }
    }
}