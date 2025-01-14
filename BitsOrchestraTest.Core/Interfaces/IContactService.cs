using System.Threading.Tasks;
using System.Collections.Generic;
using BitsOrchestraTest.Contracts.DTO;

namespace BitsOrchestraTest.Core.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactAccessDto>> GetContactsAsync();

        Task AddContactsAsync(List<ContactCreateDto> contactDtos);

        Task DeleteContactAsync(string encryptedId);

        Task UpdateContactAsync(ContactUpdateDto contactUpdateDto, string encryptedId);
    }
}
