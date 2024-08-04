using Contactlist.Model;
using ContactList.Repository;

namespace ContactList.Business
{
   
        public class ContactService : IContactService
        {
            private readonly IContactRepository _contactRepository;

            public ContactService(IContactRepository contactRepository)
            {
                _contactRepository = contactRepository;
            }

            public IEnumerable<Contact> GetAllContacts()
            {
                return _contactRepository.GetContacts();
            }

            public Contact GetContactById(int id)
            {
                return _contactRepository.GetContactById(id);
            }

            public void CreateContact(Contact contact)
            {
                contact.IsDeleted = false;
                _contactRepository.AddContact(contact);
            }

            public void UpdateContact(int id, Contact contact)
            {
                var existingContact = _contactRepository.GetContactById(id);
                if (existingContact == null)
                {
                    throw new KeyNotFoundException("Contact not found");
                }
                existingContact.Name = contact.Name;
                existingContact.Email = contact.Email;
                existingContact.PhoneNumber = contact.PhoneNumber;
                existingContact.BirthDate = contact.BirthDate;
                existingContact.Category = contact.Category;
                _contactRepository.UpdateContact(existingContact);
            }

            public void DeleteContact(int id)
            {
                var contact = _contactRepository.GetContactById(id);
                if (contact == null)
                {
                    throw new KeyNotFoundException("Contact not found");
                }
                _contactRepository.SoftDeleteContact(contact);
            }

            public IEnumerable<Contact> SearchContacts(string name, string email, string phoneNumber)
            {
                var contacts = _contactRepository.GetContacts().AsQueryable();
                if (!string.IsNullOrEmpty(name))
                {
                    contacts = contacts.Where(c => c.Name.Contains(name));
                }
                if (!string.IsNullOrEmpty(email))
                {
                    contacts = contacts.Where(c => c.Email.Contains(email));
                }
                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    contacts = contacts.Where(c => c.PhoneNumber.Contains(phoneNumber));
                }
                return contacts.ToList();
            }
        }
    }


