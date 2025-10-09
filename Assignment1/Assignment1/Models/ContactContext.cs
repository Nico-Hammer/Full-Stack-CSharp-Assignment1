/* import EFCore functionality to be able to work with the database */
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Create the Models namespace so that the Contact and Category objects can be accessed elsewhere
/// </summary>
namespace Assignment1.Models
{
    /// <summary>
    /// Create the contact context class that inherits the DBContext attributes so it can access and modify the database
    /// </summary>
    public class ContactContext : DbContext
    {
            public ContactContext(DbContextOptions<ContactContext> options) : base(options) { }
            public DbSet<Contact> Contacts { get; set; }
            public DbSet<Category> Categories { get; set; }
            /// <summary>
            /// Do everything inside this function when the program is run and the database is created
            /// </summary>
            /// <param name="modelBuilder">Defines how the Contact and Category objects will interact with the database</param>
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Contact>().HasData(
                    /// <summary>
                    /// Create the seed data for the database
                    /// </summary>
                    new Contact
                    {
                        ContactID = 1,
                        FirstName = "Alice",
                        LastName = "A",
                        Phonenumber = "1115211243",
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
                        Phonenumber = "2223561234",
                        Email = "bob@mail.com",
                        CategoryID = 1,
                        Organization = "test",
                        CreatedDateTime = "01/01/0001 'at' 00:00:00",
                    }
                );
                /// <summary>
                /// Create the relationship category options and add them to the database context when it is created
                /// </summary>
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