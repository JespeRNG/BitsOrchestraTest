using System;
using System.Linq;
using Mehdime.Entity;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using BitsOrchestraTest.Core.Helpers;
using BitsOrchestraTest.Contracts.DTO;
using BitsOrchestraTest.Core.Interfaces;
using BitsOrchestraTest.Domains.Contacts;
using BitsOrchestraTest.Persistence.Interfaces;

namespace BitsOrchestraTest.Services
{
    public class ContactService : IContactService
    {
        private readonly IRepository _repository;
        private readonly IDbContextScopeFactory _scopeFactory;

        public ContactService(IRepository repository, IDbContextScopeFactory scopeFactory)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        }

        public async Task<IEnumerable<ContactAccessDto>> GetContactsAsync()
        {
            using (var dbScope = _scopeFactory.Create())
            {
                var contacts = await _repository.GetAll<Contact>().ToListAsync();

                if (contacts == null) throw new NullReferenceException(nameof(contacts));

                var contactDtos = contacts.Select(contact => new ContactAccessDto 
                { 
                    Id = DataProtectionHelper.EncryptId(contact.Id),
                    Name = contact.Name, 
                    DateOfBirth = contact.DateOfBirth,
                    Married = contact.Married,
                    Phone = contact.Phone,
                    Salary = contact.Salary
                }).ToList();

                return contactDtos;
            }
        }

        public async Task AddContactsAsync(List<ContactCreateDto> contactDtos)
        {
            if (contactDtos == null) throw new ArgumentNullException(nameof(contactDtos));

            using (var dbScope = _scopeFactory.Create())
            {
                foreach (var contactDto in contactDtos)
                {
                    var contact = new Contact()
                    {
                        Name = contactDto.Name,
                        DateOfBirth = contactDto.DateOfBirth,
                        Married = contactDto.Married,
                        Phone = contactDto.Phone,
                        Salary = contactDto.Salary
                    };
                    _repository.Add(contact);
                }
                await dbScope.SaveChangesAsync();
            }
        }

        public async Task DeleteContactAsync(string encryptedId) 
        {
            if (encryptedId == null) throw new ArgumentNullException(nameof(encryptedId));

            var decryptedId = DataProtectionHelper.DecryptId(encryptedId);

            using (var dbScope = _scopeFactory.Create())
            {
                var contact = await _repository.FindAsync<Contact, Guid>(decryptedId);
                if (contact == null) throw new NullReferenceException(nameof(contact));

                _repository.Remove(contact);
                await dbScope.SaveChangesAsync(); 
            } 
        }

        public async Task UpdateContactAsync(ContactUpdateDto contactUpdateDto, string encryptedId)
        {
            if (contactUpdateDto == null) throw new ArgumentNullException(nameof(contactUpdateDto));
            if (encryptedId == null) throw new ArgumentNullException(nameof(encryptedId));

            var decryptedId = DataProtectionHelper.DecryptId(encryptedId);

            using (var dbScope = _scopeFactory.Create())
            {
                var contact = await _repository.FindAsync<Contact, Guid>(decryptedId);
                if (contact == null) throw new NullReferenceException(nameof(contact));

                contact.Married = contactUpdateDto.Married;
                contact.Phone = contactUpdateDto.Phone;
                contact.Salary = contactUpdateDto.Salary;

                _repository.Update(contact);
                await dbScope.SaveChangesAsync();
            }
        }
    }
}
