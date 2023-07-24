using PersonalFinance.Models;

namespace PersonalFinance.Data.AutoRegisterLists;
public class Banks
{
    public static List<Bank> GetAll()
    {
        return new()
        {
            new Bank { Name = "Stone Pagamentos", Number = 197 },
            new Bank { Name = "Banco Cooperativo Sicredi S.A.", Number = 748 },
            new Bank { Name = "Advanced Cc Ltda", Number = 117 },
            new Bank { Name = "Banco Inter", Number = 77 },
            new Bank { Name = "Neon Pagamentos", Number = 735 },
            new Bank { Name = "Superdigital", Number = 340 },
            new Bank { Name = "PagBank", Number = 290 },
            new Bank { Name = "Banco Agibank S.A.", Number = 121 },
            new Bank { Name = "Next", Number = 237 },
            new Bank { Name = "Banco Original", Number = 212 },
            new Bank { Name = "Nubank", Number = 260 },
            new Bank { Name = "Banco Willbank", Number = 280 },
            new Bank { Name = "Albatross Ccv S.A", Number = 172 },
            new Bank { Name = "Mercado Pago", Number = 323 },
            new Bank { Name = "Ativa Investimentos S.A", Number = 188 },
            new Bank { Name = "PicPay", Number = 380 },
            new Bank { Name = "Avista S.A. Crédito, Financiamento e Investimento", Number = 280 },
            new Bank { Name = "B&T Cc Ltda", Number = 80 },
            new Bank { Name = "Banco A.J.Renner S.A.", Number = 654 },
            new Bank { Name = "Banco ABC Brasil S.A.", Number = 246 },
            new Bank { Name = "Banco ABN AMRO S.A", Number = 75 },
            new Bank { Name = "Banco Alfa S.A.", Number = 25 },
            new Bank { Name = "Banco Alvorada S.A.", Number = 641 },
            new Bank { Name = "Banco Andbank (Brasil) S.A.", Number = 65 },
            new Bank { Name = "Banco Arbi S.A.", Number = 213 },
            new Bank { Name = "Banco B3 S.A.", Number = 96 },
            new Bank { Name = "Banco SANTANDER", Number = 33 },
            new Bank { Name = "Banco BMG S.A.", Number = 318 },
            new Bank { Name = "Banco BNP Paribas Brasil S.A.", Number = 752 },
            new Bank { Name = "Banco BOCOM BBM S.A.", Number = 107 },
            new Bank { Name = "Banco Bradescard S.A.", Number = 63 },
            new Bank { Name = "Código Banco Beg S.A.", Number = 31 },
            new Bank { Name = "Banco Bradesco BERJ S.A.", Number = 122 },
            new Bank { Name = "Banco Bradesco Cartões S.A.", Number = 204 },
            new Bank { Name = "Banco Bradesco Financiamentos S.A.", Number = 394 },
            new Bank { Name = "Banco Bradesco S.A.", Number = 237 },
            new Bank { Name = "Banco BS2 S.A.", Number = 218 },
            new Bank { Name = "Banco BTG Pactual S.A.", Number = 208 },
            new Bank { Name = "Banco C6 S.A – C6 Bank", Number = 336 },
            new Bank { Name = "Banco Caixa Geral – Brasil S.A.", Number = 473 },
            new Bank { Name = "Banco Caixa Econômica Federal", Number = 104 },
            new Bank { Name = "Banco Capital S.A.", Number = 412 },
            new Bank { Name = "Banco Cargill S.A.", Number = 40 }
        };
    }
}
