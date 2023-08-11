using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Controllers;
using PersonalFinance.Data;
using PersonalFinance.RequestModels;
using PersonalFinance.Services;
using Xunit;
using Moq;
using PersonalFinance.Models;
using Microsoft.EntityFrameworkCore;

namespace PersonalFinance.Tests.Unit;

public class TransactionsControllerTests {    
    private Mock<ISpellCheckerService> _mockSpellCheckerService;
    private AppDb _context;
    private ISpellCheckerService _spellCheckerService;
    private TransactionsController _controller;

    public TransactionsControllerTests()
    {
        _context = GetDatabaseContext();

        _mockSpellCheckerService = new Mock<ISpellCheckerService>();               
        _spellCheckerService = _mockSpellCheckerService.Object;
        _controller = new TransactionsController(_context, _spellCheckerService);
    }

    AppDb GetDatabaseContext()
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
        var searchModel = new TransactionSearchModel();

        var transactions = new List<Transaction> {
            new Transaction {
                Description = "test",
                Date = DateTime.Now,
                TransactionType = new() { 
                    Description = "test",
                    TransactionTypeGroup = new() {
                        Description = "test"
                    },
                },
                Account = new() { 
                    Description = "test",
                    AccountType = new() {
                        Description= "test",
                    }, 
                    Bank = new() {
                        Name = "test",
                    }
                },                
            }
        };

        _context.AddRange(transactions);
        _context.SaveChanges();

        _mockSpellCheckerService.Setup(x => x.GetSpellCheckedSearchVectorString(It.IsAny<string>()))
            .Returns(string.Empty);

        var result = await _controller.GetTransactions(searchModel);   
        
        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(transactions, (result.Result as OkObjectResult).Value);
    }

    [Fact]
    public async Task GetTransactions_ReturnsNotFound_WhenTransactionsAreNull()
    {
        var searchModel = new TransactionSearchModel();

        _mockSpellCheckerService.Setup(x => x.GetSpellCheckedSearchVectorString(It.IsAny<string>()))
           .Returns(string.Empty);

        _context.Transactions = null;

        var result = await _controller.GetTransactions(searchModel);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetTransactions_ReturnsBadRequest_WhenExceptionIsThrown()
    {
        var searchModel = new TransactionSearchModel {
            SearchInfo = "\\"
        };

        var result = await _controller.GetTransactions(searchModel);

        Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Contains("exception was thrown", (result.Result as BadRequestObjectResult).Value.ToString());
    }
}
