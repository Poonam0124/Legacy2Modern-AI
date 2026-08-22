using System;
using System.Collections.Generic;
using System.Linq;

namespace Legacy2Modern.Data.Repositories
{
    public class CustomerContactRepository
    {
        public List<CustomerContact> GetByCustomerId(int customerId)
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                return context.CustomerContacts
                    .Where(x => x.CustomerId == customerId)
                    .OrderByDescending(x => x.IsPrimary)
                    .ThenBy(x => x.ContactType)
                    .ToList();
            }
        }

        public CustomerContact GetById(int customerContactId)
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                return context.CustomerContacts
                    .FirstOrDefault(x =>
                        x.CustomerContactId == customerContactId);
            }
        }

        public void Add(CustomerContact contact)
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                contact.CreatedDate = DateTime.Now;

                context.CustomerContacts.Add(contact);

                context.SaveChanges();
            }
        }

        public void Update(CustomerContact contact)
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                var existing =
                    context.CustomerContacts
                        .FirstOrDefault(x =>
                            x.CustomerContactId ==
                            contact.CustomerContactId);

                if (existing == null)
                {
                    return;
                }

                existing.ContactType = contact.ContactType;
                existing.ContactValue = contact.ContactValue;
                existing.IsPrimary = contact.IsPrimary;

                context.SaveChanges();
            }
        }
    }
}