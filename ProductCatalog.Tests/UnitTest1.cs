// Деректива Xunit не подвязываеться к проекту хотя устоновлены вся обновлена в этом проекте , поэтому  сам файл тестов закомментирован.

// using Xunit;
// using ProductCatalog.Controllers;
// using Microsoft.AspNetCore.Mvc;

// namespace ProductCatalog.Tests
// {
//     public class ProductControllerTests
//     {
//         [Fact]
//         public void Get_ReturnsOk_WhenProductExists()
//         {
//             // Arrange
//             var controller = new ProductController();

//             // Act
//             var result = controller.Get(1);

//             // Assert
//             var okResult = Assert.IsType<OkObjectResult>(result);
//             Assert.NotNull(okResult.Value);
//         }

//         [Fact]
//         public void Get_ReturnsNotFound_WhenProductDoesNotExist()
//         {
//             // Arrange
//             var controller = new ProductController();

//             // Act
//             var result = controller.Get(999);

//             // Assert
//             Assert.IsType<NotFoundResult>(result);
//         }
//     }
// }