using Contactlist.Model;

namespace ContactList.Business
{
    public interface IContactService
    {
        IEnumerable<Contact> GetAllContacts();
        Contact GetContactById(int id);
        void CreateContact(Contact contact);
        void UpdateContact(int id, Contact contact);
        void DeleteContact(int id);
        IEnumerable<Contact> SearchContacts(string name, string email, string phoneNumber);

    }
}
