using Xunit;
using ProductCatalog.Controllers;
using ProductCatalog.Data;
using ProductCatalog.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void ProduktIndex_ReturnsViewWithProducts()
        {
            // Arrange
            var context = TestDbContextFactory.Create();
            var controller = new ProductController(context);

            // Act
            var result = controller.ProduktIndex();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Details_ReturnsView_WhenProductExists()
        {
            var context = TestDbContextFactory.Create();
            var controller = new ProductController(context);

            var result = controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var product = Assert.IsType<Product>(viewResult.Model);
            Assert.Equal("Phone", product.Name);
        }

        [Fact]
        public void Details_ReturnsNotFound_WhenProductDoesNotExist()
        {
            var context = TestDbContextFactory.Create();
            var controller = new ProductController(context);

            var result = controller.Details(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_Post_ValidProduct_RedirectsToIndex()
        {
            var context = TestDbContextFactory.Create();
            var controller = new ProductController(context);
            var newProduct = new Product { Id = 3, Name = "Tablet", Price = 1500 };

            var result = controller.Create(newProduct);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ProduktIndex", redirect.ActionName);
            Assert.Equal(3, context.Products.Count());
        }

        [Fact]
        public void Edit_Post_ValidProduct_RedirectsToIndex()
        {
            var context = TestDbContextFactory.Create();
            var controller = new ProductController(context);
            var product = context.Products.First();
            product.Name = "Updated Phone";

            var result = controller.Edit(product);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ProduktIndex", redirect.ActionName);
            Assert.Equal("Updated Phone", context.Products.First().Name);
        }

        [Fact]
        public void DeleteConfirmed_RemovesProductAndRedirects()
        {
            var context = TestDbContextFactory.Create();
            var controller = new ProductController(context);

            var result = controller.DeleteConfirmed(1);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ProduktIndex", redirect.ActionName);
            Assert.Equal(1, context.Products.Count()); // один удалился
        }
    }
}
