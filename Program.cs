using System;
using System.Collections.Generic;

class Program
{
    static List<Produto> produtos = new List<Produto>
    {
        new Produto(1, "X-Burguer", 15.00),
        new Produto(2, "Refrigerante", 5.00),
        new Produto(3, "Sorvete", 8.00)
    };

    static List<ItemPedido> pedido = new List<ItemPedido>();

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Lanchonete Virtual");
            Console.WriteLine("1. Listar produtos");
            Console.WriteLine("2. Adicionar produto ao pedido");
            Console.WriteLine("3. Remover produto do pedido");
            Console.WriteLine("4. Visualizar pedido");
            Console.WriteLine("5. Finalizar pedido e sair");
            Console.Write("Escolha uma opção: ");
            
            string opcao = Console.ReadLine();
            switch (opcao)
            {
                case "1": ListarProdutos(); break;
                case "2": AdicionarProduto(); break;
                case "3": RemoverProduto(); break;
                case "4": VisualizarPedido(); break;
                case "5": FinalizarPedido(); return;
                default: Console.WriteLine("Opção inválida!"); break;
            }
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadLine();
        }
    }

    static void ListarProdutos()
    {
        Console.WriteLine("Produtos disponíveis:");
        foreach (var produto in produtos)
        {
            Console.WriteLine($"{produto.Id} - {produto.Nome} - R$ {produto.Preco:F2}");
        }
    }

    static void AdicionarProduto()
    {
        ListarProdutos();
        Console.Write("Digite o ID do produto: ");
        int id = int.Parse(Console.ReadLine());
        Produto produto = produtos.Find(p => p.Id == id);
        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado!");
            return;
        }
        Console.Write("Digite a quantidade: ");
        int quantidade = int.Parse(Console.ReadLine());
        
        ItemPedido item = pedido.Find(i => i.Produto.Id == id);
        if (item != null)
        {
            item.Quantidade += quantidade;
        }
        else
        {
            pedido.Add(new ItemPedido(produto, quantidade));
        }
        Console.WriteLine("Produto adicionado ao pedido!");
    }

    static void RemoverProduto()
    {
        VisualizarPedido();
        Console.Write("Digite o ID do produto para remover: ");
        int id = int.Parse(Console.ReadLine());
        ItemPedido item = pedido.Find(i => i.Produto.Id == id);
        if (item == null)
        {
            Console.WriteLine("Produto não encontrado no pedido!");
            return;
        }
        pedido.Remove(item);
        Console.WriteLine("Produto removido do pedido!");
    }

    static void VisualizarPedido()
    {
        Console.WriteLine("Pedido atual:");
        double total = 0;
        foreach (var item in pedido)
        {
            double subtotal = item.Produto.Preco * item.Quantidade;
            total += subtotal;
            Console.WriteLine($"{item.Produto.Nome} - {item.Quantidade}x - R$ {subtotal:F2}");
        }
        Console.WriteLine($"Total: R$ {total:F2}");
    }

    static void FinalizarPedido()
    {
        Console.WriteLine("Resumo do pedido:");
        double total = 0;
        int totalItens = 0;
        foreach (var item in pedido)
        {
            double subtotal = item.Produto.Preco * item.Quantidade;
            total += subtotal;
            totalItens += item.Quantidade;
            Console.WriteLine($"{item.Produto.Nome} - {item.Quantidade}x - R$ {subtotal:F2}");
        }
        double desconto = (total > 100) ? total * 0.10 : 0;
        double valorFinal = total - desconto;
        
        Console.WriteLine($"Total de itens: {totalItens}");
        Console.WriteLine($"Valor bruto: R$ {total:F2}");
        Console.WriteLine($"Desconto aplicado: R$ {desconto:F2}");
        Console.WriteLine($"Valor final a pagar: R$ {valorFinal:F2}");
        Console.WriteLine("Pedido finalizado!");
    }
}

class Produto
{
    public int Id { get; }
    public string Nome { get; }
    public double Preco { get; }
    
    public Produto(int id, string nome, double preco)
    {
        Id = id;
        Nome = nome;
        Preco = preco;
    }
}

class ItemPedido
{
    public Produto Produto { get; }
    public int Quantidade { get; set; }
    
    public ItemPedido(Produto produto, int quantidade)
    {
        Produto = produto;
        Quantidade = quantidade;
    }
}
