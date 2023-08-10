using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Controllers;
using PersonalFinance.Data;
using PersonalFinance.RequestModels;
using PersonalFinance.Services;
using Xunit;
using FakeItEasy;
using PersonalFinance.Queries;
using PersonalFinance.Models;

namespace PersonalFinance.Tests.Unit; 
public class TransactionsControllerTests {
    private AppDb _mockContext;
    private SpellCheckerService _mockSpellCheckerService;
    private TransactionsController _controller;

    public TransactionsControllerTests()
    {
        _mockContext = A.Fake<AppDb>();
        _mockSpellCheckerService = A.Fake<SpellCheckerService>();
        _controller = new TransactionsController(_mockContext, _mockSpellCheckerService);
    }

    [Fact]
    public async Task GetTransactions_ReturnsOk_WhenTransactionsExist()
    {
        TransactionSearchModel? searchModel = new TransactionSearchModel { SearchInfo = "test" };
        var transactions = new List<Transaction>
        {
            new Transaction {  },
        };        

        A.CallTo(() => _mockContext.Transactions.WhereDefaultFilters(searchModel, A<string>.Ignored, null)).Returns(transactions.AsQueryable());        
        A.CallTo(() => _mockSpellCheckerService.GetSpellCheckedSearchVectorString(A<string>.Ignored)).ReturnsLazily((string s) => s);

        var result = await _controller.GetTransactions(searchModel);

        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(transactions, (result.Result as OkObjectResult).Value);
    }

    [Fact]
    public async Task GetTransactions_ReturnsNotFound_WhenTransactionsAreNull()
    {
        var searchModel = new TransactionSearchModel { SearchInfo = "test" };
        A.CallTo(() => _mockContext.Transactions).Returns(null);

        var result = await _controller.GetTransactions(searchModel);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetTransactions_ReturnsBadRequest_WhenExceptionIsThrown()
    {
        var searchModel = new TransactionSearchModel { SearchInfo = "test" };
        A.CallTo(() => _mockContext.Transactions.WhereDefaultFilters(searchModel, A<string>.Ignored, null)).Throws(new Exception("Test exception"));

        var result = await _controller.GetTransactions(searchModel);

        Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Test exception", (result.Result as BadRequestObjectResult).Value);
    }
}
