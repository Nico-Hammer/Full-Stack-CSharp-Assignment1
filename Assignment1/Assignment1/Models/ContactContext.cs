using Microsoft.EntityFrameworkCore;

namespace Assignment1.Models
{

    public class ContactContext : DbContext
    {
            public ContactContext(DbContextOptions<ContactContext> options) : base(options) { }

            public DbSet<Contact> Contacts { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Contact>().HasData(
                    new Contact
                    {
                        ContactID = 1,
                        FirstName = "Alice",
                        LastName = "A",
                        Phonenumber = 521-1243,
                        Email = "alice@mail.com",
                        Category = "Friend",
                        Organization = "test"
                    },
                    new Contact
                    {
                        ContactID = 2,
                        FirstName = "Bob",
                        LastName = "B",
                        Phonenumber = 356-1234,
                        Email = "bob@mail.com",
                        Category = "Friend",
                        Organization = "test"
                    }
                );
            }
    }
}