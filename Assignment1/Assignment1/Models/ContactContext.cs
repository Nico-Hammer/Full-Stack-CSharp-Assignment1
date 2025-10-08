using Microsoft.EntityFrameworkCore;

namespace Assignment1.Models
{

    public class ContactContext : DbContext
    {
            public ContactContext(DbContextOptions<ContactContext> options) : base(options) { }

            
            public DbSet<Contact> Contacts { get; set; }
            public DbSet<Category> Categories { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Contact>().HasData(
                    new Contact
                    {
                        ContactID = 1,
                        FirstName = "Alice",
                        LastName = "A",
                        Phonenumber = "521-1243",
                        Email = "alice@mail.com",
                        CategoryID = 2,
                        Organization = "test",
                        CreatedDateTime = "01/01/0001 'at' 00:00:00",
                    },
                    new Contact
                    {
                        ContactID = 2,
                        FirstName = "Bob",
                        LastName = "B",
                        Phonenumber = "356-1234",
                        Email = "bob@mail.com",
                        CategoryID = 1,
                        Organization = "test",
                        CreatedDateTime = "01/01/0001 'at' 00:00:00",
                    }
                );
                modelBuilder.Entity<Category>().HasData(
                    new Category
                    {
                        CategoryID = 1,
                        CategoryName = "Family",
                    },
                    new Category
                    {
                        CategoryID = 2,
                        CategoryName = "Friend",
                    },
                    new Category
                    {
                        CategoryID = 3,
                        CategoryName = "Work",
                    });
            }
    }
}