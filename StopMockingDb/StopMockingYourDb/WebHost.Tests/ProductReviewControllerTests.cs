using System.Net;
using System.Net.Http.Json;
using Dapper;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebHost.DataAccess;
using WebHost.Models;
using WebHost.Tests.TestInfrastructure;

namespace WebHost.Tests
{
    [TestFixture]
    public class ProductReviewControllerTests
    {
        private static HttpClient? _testClient;
        private ProductDbContext _context;

        [SetUp]
        public async Task Setup()
        {
            var testServer = TestHostCreator.CreateTestServer();
            _testClient = testServer.CreateClient();
            _context = testServer.Services.GetService(typeof(ProductDbContext)) as ProductDbContext;
            await _context.SetupDatabaseAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await _context.TeardownDatabaseAsync();
        }

        [Test]
        public async Task GetProductReviews_ValidProductId_ReturnsAllReviewsInDb()
        {
            // Arrange
            var product1 = await CreateProduct();
            await CreateProductReviewData(product1.Id);// Create productReview we don't care about

            var product2 = await CreateProduct();
            await CreateProductReviewData(product2.Id);

            // Act
            var response = await _testClient.GetAsync($"productreviews?productId={product2.Id}");

            response.EnsureSuccessStatusCode();

            // Assert
            var result = await response.Content.ReadFromJsonAsync<ProductReview[]>();
            Assert.That(result?.Length, Is.EqualTo(1));
        }

        [Test]
        public async Task CreateProductReview_ValidProductId_ReturnsNewlyCreatedProductReview()
        {
            // Arrange                        
            var product = await CreateProduct();

            // Act
            var reviewRequest = new ProductReview
            {
                ProductId = product.Id,
                Comments = "I can see clearly now!",
                Rating = 4.0,
                UserId = Guid.NewGuid()
            };
            var response = await _testClient.PostAsJsonAsync("productreviews", reviewRequest);

            response.EnsureSuccessStatusCode();

            // Assert

            var createdReview =
                _context.ProductReviews.SingleOrDefault(x => x.ProductId == product.Id && x.UserId == reviewRequest.UserId);
            
            Assert.That(createdReview, Is.Not.Null);
            Assert.That(createdReview.Id, Is.GreaterThan(0));
            Assert.That(createdReview.Comments, Is.EqualTo(reviewRequest.Comments));
            Assert.That(createdReview.Rating, Is.EqualTo(reviewRequest.Rating));
        }

        [Test]
        public async Task CreateProductReview_ValidProductId_FailsDueToReviewForUserAlreadyExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var product = await CreateProduct();
            await CreateProductReviewData(product.Id, userId); // Creating a product review for the user

            // Act
            var reviewRequest = new ProductReview
            {
                ProductId = product.Id,
                Comments = "I can see clearly now!",
                Rating = 4.0,
                UserId = userId
            };
            
            var response = await _testClient.PostAsJsonAsync("productreviews", reviewRequest);
            
            Assert.That(response.StatusCode, Is.Not.EqualTo(HttpStatusCode.Created));
        }

        private async Task<Product> CreateProduct()
        {
            var product = new Product()
            {
                Name = "10' HDMI Cable",
                Price = 10.99
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        private async Task CreateProductReviewData(int productId, Guid? userId = null)
        {
            var productReview = new ProductReview()
            {
                ProductId = productId,
                Rating = 5.0,
                UserId = userId ?? Guid.NewGuid(),
                Comments = "Does the job!"
            };
            _context.ProductReviews.Add(productReview);
            await _context.SaveChangesAsync();
        }
    }
}