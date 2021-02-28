using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsDirectory.Repositories
{
    public  interface IContactManagement
    {
        Task<IEnumerable<ContactInformation>> GetAllContactsData();
        Task<ContactInformation> GetContact(int Id);

        Task<ContactInformation> AddContact(ContactInformation contactInformation);

        Task<ContactInformation> UpdateContact(ContactInformation contactInformation);

        Task<ContactInformation> DeleteContact(int Id);
    }
}
