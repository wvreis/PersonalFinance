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
    // Cria um objeto de teste para o contexto do banco de dados
    private AppDb _mockContext;
    // Cria um objeto de teste para o serviço de verificação ortográfica
    private SpellCheckerService _mockSpellCheckerService;
    // Cria uma instância da controller a ser testada, passando os objetos de teste como parâmetros
    private TransactionsController _controller;

    public TransactionsControllerTests()
    {
        // Usa a biblioteca Fake do Xunit para criar os objetos de teste
        _mockContext = A.Fake<AppDb>();
        _mockSpellCheckerService = A.Fake<SpellCheckerService>();
        _controller = new TransactionsController(_mockContext, _mockSpellCheckerService);
    }

    [Fact]
    public async Task GetTransactions_ReturnsOk_WhenTransactionsExist()
    {
        // Cria um modelo de pesquisa com alguns valores
        TransactionSearchModel? searchModel = new TransactionSearchModel { SearchInfo = "test" };
        // Cria uma lista de transações para simular o retorno do contexto do banco de dados
        var transactions = new List<Transaction>
        {
            new Transaction {  },
        };        

        // Configura o objeto de teste do contexto do banco de dados para retornar a lista de transações quando o método WhereDefaultFilters for chamado
        A.CallTo(() => _mockContext.Transactions.WhereDefaultFilters(searchModel, A<string>.Ignored, null)).Returns(transactions.AsQueryable());        
        // Configura o objeto de teste do serviço de verificação ortográfica para retornar a mesma string que foi passada como parâmetro quando o método GetSpellCheckedSearchVectorString for chamado
        A.CallTo(() => _mockSpellCheckerService.GetSpellCheckedSearchVectorString(A<string>.Ignored)).ReturnsLazily((string s) => s);

        // Chama o método da controller que queremos testar
        var result = await _controller.GetTransactions(searchModel);

        // Verifica se o resultado é do tipo OkObjectResult
        Assert.IsType<OkObjectResult>(result.Result);
        // Verifica se o valor do resultado é igual à lista de transações que criamos
        Assert.Equal(transactions, (result.Result as OkObjectResult).Value);
    }

    [Fact]
    public async Task GetTransactions_ReturnsNotFound_WhenTransactionsAreNull()
    {
        // Cria um modelo de pesquisa com alguns valores
        var searchModel = new TransactionSearchModel { SearchInfo = "test" };
        // Configura o objeto de teste do contexto do banco de dados para retornar null quando a propriedade Transactions for acessada
        A.CallTo(() => _mockContext.Transactions).Returns(null);

        // Chama o método da controller que queremos testar
        var result = await _controller.GetTransactions(searchModel);

        // Verifica se o resultado é do tipo NotFoundResult
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetTransactions_ReturnsBadRequest_WhenExceptionIsThrown()
    {
        // Cria um modelo de pesquisa com alguns valores
        var searchModel = new TransactionSearchModel { SearchInfo = "test" };
        // Configura o objeto de teste do contexto do banco de dados para lançar uma exceção quando o método WhereDefaultFilters for chamado
        A.CallTo(() => _mockContext.Transactions.WhereDefaultFilters(searchModel, A<string>.Ignored, null)).Throws(new Exception("Test exception"));

        // Chama o método da controller que queremos testar
        var result = await _controller.GetTransactions(searchModel);

        // Verifica se o resultado é do tipo BadRequestObjectResult
        Assert.IsType<BadRequestObjectResult>(result.Result);
        // Verifica se o valor do resultado é igual à mensagem da exceção que lançamos
        Assert.Equal("Test exception", (result.Result as BadRequestObjectResult).Value);
    }
}
