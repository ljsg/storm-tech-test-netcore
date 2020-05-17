using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Todo.Data;
using Todo.Data.Entities;
using Todo.Services;
using Xunit;

namespace Todo.Tests.Services
{
    public class ApplicationDbContextConvenienceTests
    {
        readonly ApplicationDbContext dbContext;
        readonly IdentityUser testIdentityAlice;
        readonly IdentityUser testIdentityBob;

        public ApplicationDbContextConvenienceTests()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite(connection);

            using (var tempContext = new ApplicationDbContext(optionsBuilder.Options))
            {
                tempContext.Database.EnsureCreated();
            }

            dbContext = new ApplicationDbContext(optionsBuilder.Options);

            testIdentityAlice = new IdentityUser("alice@example.com");
            TodoList aliceTodoList = new TestTodoListBuilder(testIdentityAlice, "Shopping")
                    .WithItem("Bread", Importance.High)
                    .Build();

            testIdentityBob = new IdentityUser("bob@example.com");
            TodoList bobTodoList = new TestTodoListBuilder(testIdentityBob, "Movies")
                    .WithItem("Spiderman", Importance.High)
                    .Build();

            bobTodoList.Items.Add(new TodoItem(bobTodoList.TodoListId, testIdentityAlice.Id, "Avengers", Importance.Medium));

            dbContext.Add(aliceTodoList);
            dbContext.Add(bobTodoList);
            dbContext.SaveChanges();
        }

        //Much more test cases could exist here to cover more permutaions of happy & sad paths
        [Fact]
        public void Return_only_lists_for_the_owner()
        {
            var userLists = dbContext.OwnedTodoList(testIdentityAlice.Id).ToList();
            Assert.Single(userLists.Where(l => l.Owner.Id == testIdentityAlice.Id));
        }

        [Fact]
        public void Return_lists_where_user_is_assigned_items()
        {
            var userLists = dbContext.AssignedItemsTodoList(testIdentityAlice.Id).ToList();
            Assert.Single(userLists.Where(l => l.Owner.Id == testIdentityBob.Id));
        }
    }
}
