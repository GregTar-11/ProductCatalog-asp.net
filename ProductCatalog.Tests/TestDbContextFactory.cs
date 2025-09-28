// using Microsoft.EntityFrameworkCore;
// using ProductCatalog.Data;
// using ProductCatalog.Models;

// namespace ProductCatalog.Tests
// {
//     public static class TestDbContextFactory
//     {
//         public static AppDbContext Create()
//         {
//             var options = new DbContextOptionsBuilder<AppDbContext>()
//                 .UseInMemoryDatabase(Guid.NewGuid().ToString()) // уникальная база для каждого теста
//                 .Options;

//             var context = new AppDbContext(options);

//             context.Products.AddRange(
//                 new Product { Id = 1, Name = "Phone", Price = 1000 },
//                 new Product { Id = 2, Name = "Laptop", Price = 2000 }
//             );
//             context.SaveChanges();

//             return context;
//         }
//     }
// }
