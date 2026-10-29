using System;
using System.Collections.Generic;

class ProdutoCardapio
{
    public string Nome { get; private set; }
    public decimal Preco { get; private set; }
    public bool Disponivel { get; private set; }

    public ProdutoCardapio(string nome, decimal preco, bool disponivel)
    {
        if (preco < 0)
            throw new Exception("Preço inválido!");

        Nome = nome;
        Preco = preco;
        Disponivel = disponivel;
    }

    public virtual void Exibir()
    {
        Console.WriteLine($"{Nome} - R$ {Preco:F2}");
    }
}

class Lanche : ProdutoCardapio
{
    public string Ingredientes { get; private set; }

    public Lanche(string nome, decimal preco, bool disponivel,
        string ingredientes)
        : base(nome, preco, disponivel)
    {
        Ingredientes = ingredientes;
    }

    public override void Exibir()
    {
        base.Exibir();
        Console.WriteLine("Ingredientes: " + Ingredientes);
    }
}

class Bebida : ProdutoCardapio
{
    public string Tamanho { get; private set; }

    public Bebida(string nome, decimal preco, bool disponivel,
        string tamanho)
        : base(nome, preco, disponivel)
    {
        Tamanho = tamanho;
    }

    public override void Exibir()
    {
        base.Exibir();
        Console.WriteLine("Tamanho: " + Tamanho);
    }
}

class Sobremesa : ProdutoCardapio
{
    public string Sabor { get; private set; }

    public Sobremesa(string nome, decimal preco, bool disponivel,
        string sabor)
        : base(nome, preco, disponivel)
    {
        Sabor = sabor;
    }

    public override void Exibir()
    {
        base.Exibir();
        Console.WriteLine("Sabor: " + Sabor);
    }
}

class Pedido
{
    private List<ProdutoCardapio> produtos = new List<ProdutoCardapio>();
    private List<int> quantidades = new List<int>();

    public void Adicionar(ProdutoCardapio produto, int quantidade)
    {
        if (produto == null || !produto.Disponivel)
        {
            Console.WriteLine("Produto indisponível!");
            return;
        }

        if (quantidade <= 0)
        {
            Console.WriteLine("Quantidade inválida!");
            return;
        }

        produtos.Add(produto);
        quantidades.Add(quantidade);

        Console.WriteLine("Produto adicionado!");
    }

    public void Remover(int posicao)
    {
        if (posicao < 0 || posicao >= produtos.Count)
        {
            Console.WriteLine("Produto não encontrado!");
            return;
        }

        produtos.RemoveAt(posicao);
        quantidades.RemoveAt(posicao);

        Console.WriteLine("Produto removido!");
    }

    public decimal CalcularTotal(decimal desconto)
    {
        if (desconto < 0 || desconto > 100)
            throw new Exception("Desconto inválido!");

        decimal total = 0;

        for (int i = 0; i < produtos.Count; i++)
        {
            total += produtos[i].Preco * quantidades[i];
        }

        return total - (total * desconto / 100);
    }

    public void ExibirResumo(decimal desconto)
    {
        Console.WriteLine("\n===== RESUMO DO PEDIDO =====");

        for (int i = 0; i < produtos.Count; i++)
        {
            decimal subtotal = produtos[i].Preco * quantidades[i];

            Console.WriteLine(
                produtos[i].Nome + " | Quantidade: " +
                quantidades[i] + " | Subtotal: R$ " +
                subtotal.ToString("F2"));
        }

        Console.WriteLine("----------------------------");
        Console.WriteLine("Total com desconto: R$ " +
            CalcularTotal(desconto).ToString("F2"));
    }
}

class Program
{
    static void Main()
    {

        List<ProdutoCardapio> cardapio = new List<ProdutoCardapio>();

        cardapio.Add(new Lanche("X-Burger", 18, true, "Carne e queijo"));
        cardapio.Add(new Lanche("X-Salada", 20, true, "Carne, queijo e salada"));
        cardapio.Add(new Lanche("X-Bacon", 23, true, "Carne, queijo e bacon"));
        cardapio.Add(new Lanche("Cachorro-quente", 15, true, "Salsicha e molho"));
        cardapio.Add(new Lanche("Misto-quente", 12, true, "Queijo e presunto"));
        cardapio.Add(new Lanche("Coxinha", 8, true, "Frango"));
        cardapio.Add(new Lanche("Pastel de queijo", 9, true, "Queijo"));
        cardapio.Add(new Lanche("Pão de queijo", 6, true, "Queijo"));
        cardapio.Add(new Lanche("Batata frita", 16, true, "Batata e sal"));

        cardapio.Add(new Bebida("Coca-Cola", 7, true, "350 ml"));
        cardapio.Add(new Bebida("Guaraná", 7, true, "350 ml"));
        cardapio.Add(new Bebida("Suco de laranja", 9, true, "300 ml"));
        cardapio.Add(new Bebida("Suco de maracujá", 9, true, "300 ml"));
        cardapio.Add(new Bebida("Água mineral", 4, true, "500 ml"));
        cardapio.Add(new Bebida("Café", 4, true, "150 ml"));
        cardapio.Add(new Bebida("Achocolatado", 7, true, "300 ml"));

        cardapio.Add(new Sobremesa("Pudim", 8, true, "Leite condensado"));
        cardapio.Add(new Sobremesa("Bolo de chocolate", 9, true, "Chocolate"));
        cardapio.Add(new Sobremesa("Brownie", 10, true, "Chocolate"));
        cardapio.Add(new Sobremesa("Brigadeiro", 3, true, "Chocolate"));
        cardapio.Add(new Sobremesa("Mousse de maracujá", 8, true, "Maracujá"));
        cardapio.Add(new Sobremesa("Açaí", 15, true, "Açaí"));

        Console.WriteLine("===== CARDÁPIO DA LANCHONETE =====");

        foreach (ProdutoCardapio produto in cardapio)
        {
            produto.Exibir();
            Console.WriteLine();
        }

        Pedido pedido = new Pedido();

        pedido.Adicionar(cardapio[0], 2);
        pedido.Adicionar(cardapio[9], 2);
        pedido.Adicionar(cardapio[16], 1);

        pedido.ExibirResumo(10);
    }
}

