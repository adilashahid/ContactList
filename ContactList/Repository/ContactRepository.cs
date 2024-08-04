using Contactlist.Model;
using Microsoft.EntityFrameworkCore;

namespace ContactList.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly Dbcontact _dbContext;

        public ContactRepository(Dbcontact dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Contact> GetContacts()
        {
            return _dbContext.Contacts.ToList();
        }

        public Contact GetContactById(int id)
        {
            return _dbContext.Contacts.Find(id);
        }

        public void AddContact(Contact contact)
        {
            _dbContext.Contacts.Add(contact);
            _dbContext.SaveChanges();
        }

        public void UpdateContact(Contact contact)
        {
            _dbContext.Entry(contact).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void SoftDeleteContact(Contact contact)
        {
            contact.IsDeleted = true;
            _dbContext.Entry(contact).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public bool ContactExists(int id)
        {
            return _dbContext.Contacts.Any(c => c.Id == id);
        }
    }
}