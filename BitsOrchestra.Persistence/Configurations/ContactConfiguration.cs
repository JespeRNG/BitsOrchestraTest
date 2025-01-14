using BitsOrchestraTest.Domains.Contacts;
using System.Data.Entity.ModelConfiguration;

namespace BitsOrchestra.Persistence.Configurations
{
    public class ContactConfiguration : EntityTypeConfiguration<Contact>
    {
        public ContactConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.Name)
                .IsRequired();

            Property(c => c.DateOfBirth)
                .IsRequired();

            Property(c => c.Married)
                .IsRequired();

            Property(c => c.Phone)
                .IsRequired()
                .HasMaxLength(13);

            HasIndex(c => c.Phone).IsUnique();

            Property(c => c.Salary)
                .IsRequired()
                .HasPrecision(18, 2);
        }
    }
}
