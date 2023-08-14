using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PersonalFinance.Controllers;
using PersonalFinance.Data;
using PersonalFinance.Models;
using PersonalFinance.RequestModels;
using PersonalFinance.Services;
using Xunit;

namespace PersonalFinance.Tests.Unit;

public class TransactionsControllerTests {
    private readonly Mock<ISpellCheckerService> _mockSpellCheckerService;
    private readonly AppDb _context;
    private readonly TransactionsController _controller;

    private const string DefaultDescription = "test";
    private const string InvalidString = "\\";

    public TransactionsControllerTests()
    {
        _context = GetDatabaseContext();
        _mockSpellCheckerService = new Mock<ISpellCheckerService>();
        _controller = new TransactionsController(_context, _mockSpellCheckerService.Object);
    }

    private AppDb GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<AppDb>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var databaseContext = new AppDb(options);
        databaseContext.Database.EnsureCreated();

        return databaseContext;
    }

    [Fact]
    public async Task GetTransactions_ReturnsOk_WhenTransactionsExist()
    {
        // Arrange
        var searchModel = new TransactionSearchModel();
        var transactions = CreateTestTransactions();
        _context.AddRange(transactions);
        _context.SaveChanges();

        _mockSpellCheckerService.Setup(x => x.GetSpellCheckedSearchVectorString(It.IsAny<string>()))
            .Returns(string.Empty);

        // Act
        var result = await _controller.GetTransactions(searchModel);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.Equal(transactions, okResult.Value);
    }

    [Fact]
    public async Task GetTransactions_ReturnsNotFound_WhenTransactionsAreNull()
    {
        // Arrange
        var searchModel = new TransactionSearchModel();
        _mockSpellCheckerService.Setup(x => x.GetSpellCheckedSearchVectorString(It.IsAny<string>()))
            .Returns(string.Empty);
        _context.Transactions = null;

        // Act
        var result = await _controller.GetTransactions(searchModel);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetTransactions_ReturnsBadRequest_WhenExceptionIsThrown()
    {
        // Arrange
        var searchModel = new TransactionSearchModel { SearchInfo = InvalidString };

        // Act
        var result = await _controller.GetTransactions(searchModel);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.Contains("exception was thrown", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task GetTotalOutgoingAmountForPeriod_ReturnsOkResult()
    {
        // Arrange
        var searchModel = new TransactionSearchModel();
        _mockSpellCheckerService.Setup(x => x.GetSpellCheckedSearchVectorString(It.IsAny<string>()))
            .Returns(string.Empty);

        // Act
        var result = await _controller.GetTotalOutgoingAmountForPeriod(searchModel);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetTotalOutgoingAmountForPeriod_ReturnsBadRequestResult_OnException()
    {
        // Arrange
        var searchModel = new TransactionSearchModel { SearchInfo = InvalidString };
        _mockSpellCheckerService.Setup(x => x.GetSpellCheckedSearchVectorString(It.IsAny<string>()))
            .Returns(string.Empty);

        // Act
        var result = await _controller.GetTotalOutgoingAmountForPeriod(searchModel);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Contains("exception was thrown", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task GetTotalIncomingAmountForPeriod_ReturnsOkResult()
    {
        // Arrange
        var searchModel = new TransactionSearchModel();
        _mockSpellCheckerService.Setup(x => x.GetSpellCheckedSearchVectorString(It.IsAny<string>()))
            .Returns(string.Empty);

        // Act
        var result = await _controller.GetTotalIncomingAmountForPeriod(searchModel);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetTotalIncomingAmountForPeriod_ReturnsBadRequestResult_OnException()
    {
        // Arrange
        var searchModel = new TransactionSearchModel { SearchInfo = InvalidString };
        _mockSpellCheckerService.Setup(x => x.GetSpellCheckedSearchVectorString(It.IsAny<string>()))
            .Returns(string.Empty);

        // Act
        var result = await _controller.GetTotalIncomingAmountForPeriod(searchModel);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Contains("exception was thrown", badRequestResult.Value.ToString());
    }

    private List<Transaction> CreateTestTransactions()
    {
        return new List<Transaction>
        {
                new Transaction
                {
                    Description = DefaultDescription,
                    Date = DateTime.Now,
                    TransactionType = new TransactionType
                    {
                        Description = DefaultDescription,
                        TransactionTypeGroup = new TransactionTypeGroup
                        {
                            Description = DefaultDescription
                        }
                    },
                    Account = new Account
                    {
                        Description = DefaultDescription,
                        AccountType = new AccountType
                        {
                            Description = DefaultDescription
                        },
                        Bank = new Bank
                        {
                            Name = DefaultDescription
                        }
                    }
                }
            };
    }
}
