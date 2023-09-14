using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PersonalFinance.Blazor.Controllers;
using PersonalFinance.Blazor.Data;
using PersonalFinance.Blazor.Models;
using PersonalFinance.Blazor.RequestModels;
using PersonalFinance.Blazor.Services;
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
        await _context.SaveChangesAsync();

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
    public async Task GetTotalOutgoingAmountForPeriod_TransactionsNull_ReturnsNotFound()
    {
        // Arrange
        int id = 1;
        _context.Transactions = null;

        // Act
        var result = await _controller.GetTotalOutgoingAmountForPeriod(new());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
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

    [Fact]
    public async Task GetTotalIncomingAmountForPeriod_TransactionsNull_ReturnsNotFound()
    {
        // Arrange
        int id = 1;
        _context.Transactions = null;

        // Act
        var result = await _controller.GetTotalIncomingAmountForPeriod(new());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetTransaction_ReturnsOkResult()
    {
        // Arrange
        int id = 1;
        var transactions = CreateTestTransactions();
        _context.AddRange(transactions);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetTransaction(id);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetTransaction_ReturnsNotFound()
    {
        // Arrange
        int id = 1;

        // Act
        var result = await _controller.GetTransaction(id);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetTransaction_ReturnsNotFound_WhenContextDbSetIsNull()
    {
        // Arrange
        int id = 1;
        _context.Transactions = null;

        // Act
        var result = await _controller.GetTransaction(id);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetBanks_ReturnsOk_WhenBanksExist()
    {
        // Arrange
        var banks = CreateTestBanks();
        _context.Banks.AddRange(banks);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetBanks();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedBanks = okResult.Value;
        Assert.Equal(returnedBanks, okResult.Value);
    }

    [Fact]
    public async Task GetBanks_ReturnsNotFound_WhenBanksAreNull()
    {
        // Arrange
        _context.Banks = null;

        // Act
        var result = await _controller.GetBanks();

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetActiveAccounts_ReturnsOk_WhenActiveAccountsExist()
    {
        // Arrange
        var activeAccounts = CreateTestAccounts();
        _context.Accounts.AddRange(activeAccounts);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetActiveAccounts();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedAccounts = okResult.Value as List<Account>;
        Assert.True(returnedAccounts.All(acc => acc.Status == true));
        Assert.Equal(returnedAccounts, okResult.Value);
    }

    [Fact]
    public async Task GetActiveAccounts_ReturnsNotFound_WhenActiveAccountsAreNull()
    {
        // Arrange
        _context.Accounts = null;

        // Act
        var result = await _controller.GetActiveAccounts();

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    private List<Bank> CreateTestBanks()
    {

        var bank1 = new Bank {
            Id = 1,
            Name = "Bank 1",
            Number = 12345,
            Accounts = new List<Account> {
                new Account
                {
                    Description = "Account 1",
                    AccountType = new AccountType
                    {
                        Description = "Account Type 1"
                    }
                },
                new Account
                {
                    Description = "Account 2",
                    AccountType = new AccountType
                    {
                        Description = "Account Type 2"
                    }
                }
            }
        };

        var bank2 = new Bank {
            Id = 2,
            Name = "Bank 2",
            Number = 67890,
            Accounts = new List<Account>
            {
                new Account
                {
                    Description = "Account 3",
                    AccountType = new AccountType
                    {
                        Description = "Account Type 3"
                    }
                }
            }
        };

        return new List<Bank> { bank1, bank2 };
    }

    private List<Account> CreateTestAccounts()
    {
        var account1 = new Account {
            Id = 1,
            Description = "Account 4",
            OpeningBalance = 1000.00,
            Status = true,
            BankId = 1,
            Bank = new Bank {
                Id = 1,
                Name = "Bank 1",
                Number = 12345
            },
            AccountTypeId = 1,
            AccountType = new AccountType {
                Id = 1,
                Description = "Account Type 1"
            }
        };

        var account2 = new Account {
            Id = 2,
            Description = "Account 5",
            OpeningBalance = 1500.00,
            Status = false,
            BankId = 2,
            Bank = new Bank {
                Id = 2,
                Name = "Bank 2",
                Number = 67890
            },
            AccountTypeId = 2,
            AccountType = new AccountType {
                Id = 2,
                Description = "Account Type 2"
            }
        };

        return new List<Account> { account1, account2 };
    }



    private List<Transaction> CreateTestTransactions()
    {
        return new List<Transaction>
        {
                new Transaction
                {
                    Id = 1,
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
