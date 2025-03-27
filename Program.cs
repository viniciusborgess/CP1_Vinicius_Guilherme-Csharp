using System;
using System.Collections.Generic;

class Program
{
    // Lista de produtos disponíveis na lanchonete.
    static List<Produto> produtos = new List<Produto>
    {
        new Produto(1, "X-Burguer", 15.00),    
        new Produto(2, "Refrigerante", 5.00),   
        new Produto(3, "Sorvete", 8.00)         
    };

    // Lista de itens no pedido do cliente.
    static List<ItemPedido> pedido = new List<ItemPedido>();

    static void Main()
    {
        // Laço infinito que exibe o menu principal do sistema.
        while (true)
        {
            // Limpa a tela e exibe o menu.
            Console.Clear();
            Console.WriteLine("Lanchonete Virtual");
            Console.WriteLine("1. Listar produtos");
            Console.WriteLine("2. Adicionar produto ao pedido");
            Console.WriteLine("3. Remover produto do pedido");
            Console.WriteLine("4. Visualizar pedido");
            Console.WriteLine("5. Finalizar pedido e sair");
            Console.Write("Escolha uma opção: ");
            
            // Lê a opção digitada pelo usuário.
            string opcao = Console.ReadLine();
            
            // Realiza uma ação dependendo da opção escolhida.
            switch (opcao)
            {
                case "1": ListarProdutos(); break; // Exibe a lista de produtos.
                case "2": AdicionarProduto(); break; // Adiciona um produto ao pedido.
                case "3": RemoverProduto(); break; // Remove um produto do pedido.
                case "4": VisualizarPedido(); break; // Exibe o conteúdo do pedido.
                case "5": FinalizarPedido(); return; // Finaliza o pedido e sai do programa.
                default: Console.WriteLine("Opção inválida!"); break; // Caso a opção seja inválida.
            }

            // Solicita ao usuário pressionar qualquer tecla para continuar.
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadLine();
        }
    }

    // Método para listar todos os produtos disponíveis.
    static void ListarProdutos()
    {
        Console.WriteLine("Produtos disponíveis:");
        foreach (var produto in produtos)
        {
            // Exibe os produtos com seu ID, nome e preço formatado.
            Console.WriteLine($"{produto.Id} - {produto.Nome} - R$ {produto.Preco:F2}");
        }
    }

    // Método para adicionar um produto ao pedido.
    static void AdicionarProduto()
    {
        // Exibe a lista de produtos disponíveis.
        ListarProdutos();

        // Solicita ao usuário o ID do produto a ser adicionado.
        Console.Write("Digite o ID do produto: ");
        int id = int.Parse(Console.ReadLine());
        
        // Procura o produto na lista de produtos com o ID informado.
        Produto produto = produtos.Find(p => p.Id == id);
        
        // Verifica se o produto foi encontrado.
        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado!");
            return;
        }

        // Solicita a quantidade do produto.
        Console.Write("Digite a quantidade: ");
        int quantidade = int.Parse(Console.ReadLine());
        
        // Verifica se o produto já está no pedido.
        ItemPedido item = pedido.Find(i => i.Produto.Id == id);
        
        // Se o produto já estiver no pedido, apenas incrementa a quantidade.
        if (item != null)
        {
            item.Quantidade += quantidade;
        }
        else
        {
            // Caso o produto não esteja no pedido, adiciona um novo item.
            pedido.Add(new ItemPedido(produto, quantidade));
        }

        // Confirma que o produto foi adicionado ao pedido.
        Console.WriteLine("Produto adicionado ao pedido!");
    }

    // Método para remover um produto do pedido.
    static void RemoverProduto()
    {
        // Exibe o pedido atual.
        VisualizarPedido();
        
        // Solicita ao usuário o ID do produto a ser removido.
        Console.Write("Digite o ID do produto para remover: ");
        int id = int.Parse(Console.ReadLine());
        
        // Encontra o item no pedido usando o ID do produto.
        ItemPedido item = pedido.Find(i => i.Produto.Id == id);
        
        // Verifica se o produto está no pedido.
        if (item == null)
        {
            Console.WriteLine("Produto não encontrado no pedido!");
            return;
        }

        // Remove o item do pedido.
        pedido.Remove(item);
        
        // Confirma que o produto foi removido do pedido.
        Console.WriteLine("Produto removido do pedido!");
    }

    // Método para visualizar o conteúdo do pedido.
    static void VisualizarPedido()
    {
        Console.WriteLine("Pedido atual:");
        double total = 0;
        
        // Exibe cada item do pedido, calculando o subtotal de cada um.
        foreach (var item in pedido)
        {
            double subtotal = item.Produto.Preco * item.Quantidade;
            total += subtotal;
            Console.WriteLine($"{item.Produto.Nome} - {item.Quantidade}x - R$ {subtotal:F2}");
        }

        // Exibe o total do pedido.
        Console.WriteLine($"Total: R$ {total:F2}");
    }

    // Método para finalizar o pedido e calcular o valor total com possíveis descontos.
    static void FinalizarPedido()
    {
        Console.WriteLine("Resumo do pedido:");
        double total = 0;
        int totalItens = 0;

        // Calcula o total de itens e o valor total do pedido.
        foreach (var item in pedido)
        {
            double subtotal = item.Produto.Preco * item.Quantidade;
            total += subtotal;
            totalItens += item.Quantidade;
            Console.WriteLine($"{item.Produto.Nome} - {item.Quantidade}x - R$ {subtotal:F2}");
        }

        // Aplica um desconto de 10% se o total for superior a R$ 100.
        double desconto = (total > 100) ? total * 0.10 : 0;
        double valorFinal = total - desconto;

        // Exibe o resumo final com o total de itens, valor bruto, desconto e valor final a pagar.
        Console.WriteLine($"Total de itens: {totalItens}");
        Console.WriteLine($"Valor bruto: R$ {total:F2}");
        Console.WriteLine($"Desconto aplicado: R$ {desconto:F2}");
        Console.WriteLine($"Valor final a pagar: R$ {valorFinal:F2}");
        
        // Finaliza o pedido.
        Console.WriteLine("Pedido finalizado!");
    }
}

// Classe que representa um produto.
class Produto
{
    public int Id { get; }  // ID do produto
    public string Nome { get; }  // Nome do produto
    public double Preco { get; }  // Preço do produto
    
    // Construtor para inicializar as propriedades do produto.
    public Produto(int id, string nome, double preco)
    {
        Id = id;
        Nome = nome;
        Preco = preco;
    }
}

// Classe que representa um item no pedido.
class ItemPedido
{
    public Produto Produto { get; }  // Produto do item
    public int Quantidade { get; set; }  // Quantidade do produto no pedido
    
    // Construtor para inicializar o produto e a quantidade.
    public ItemPedido(Produto produto, int quantidade)
    {
        Produto = produto;
        Quantidade = quantidade;
    }
}
