using Contactlist.Model;

namespace ContactList.Repository
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetContacts();
        Contact GetContactById(int id);
        void AddContact(Contact contact);
        void UpdateContact(Contact contact);
        void SoftDeleteContact(Contact contact);
        bool ContactExists(int id);

    }
}
