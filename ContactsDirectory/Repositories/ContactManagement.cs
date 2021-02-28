using ContactsDirectory.DataContext;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsDirectory.Repositories
{
    public class ContactManagement : IContactManagement
    {

        private readonly ApplicationDbContext _DbContext;

        public ContactManagement(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }
       

        public async Task<ContactInformation> AddContact(ContactInformation contactInformation)
        {
            var result = await _DbContext.contactInformation.AddAsync(contactInformation);
            contactInformation.Status = Enums.ContactStatus.Active.ToString();
            await _DbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ContactInformation> DeleteContact(int Id)
        {
            var result = await _DbContext.contactInformation.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (result != null)
            {
                _DbContext.contactInformation.Remove(result);
                await _DbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<ContactInformation>> GetAllContactsData()
        {
            return await _DbContext.contactInformation.ToListAsync();
        }

        public async Task<ContactInformation> GetContact(int Id)
        {
            return await _DbContext.contactInformation.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<ContactInformation> UpdateContact(ContactInformation contactInformation)
        {
            var result = await _DbContext.contactInformation.FirstOrDefaultAsync(x => x.Id == contactInformation.Id);
            if (result != null)
            {
                result.FirstName = contactInformation.FirstName;
                result.LastName = contactInformation.LastName;
                result.ContactNumber = contactInformation.ContactNumber;
                result.Email = contactInformation.Email;
                result.Status = contactInformation.Status;
                await _DbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
