using Microsoft.AspNetCore.Mvc;

namespace BlMadre.C_.Models
{
    public class Carrinho
    {
        public List<Produto> Inner { get; set; }

        // Construtor padrão necessário para desserialização
        public Carrinho()
        {
            Inner = new List<Produto>();
        }

        public Carrinho(List<Produto>? produto)
        {
            Inner = produto ?? new List<Produto>();
        }


        /// <summary>
        /// Adiciona um produto ao carrinho.
        /// </summary>
        /// <param name="produto"><see cref="Produto"/> a ser adicionado</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Add(Produto produto)
        {
            if (produto.Quantidade == 0)
            {
                throw new InvalidOperationException("Produto esgotado.");
            }
            var existingProduct = Inner.FirstOrDefault(p => p.Nome == produto.Nome);
            if (existingProduct != null)
            {
                existingProduct.Quantidade += produto.Quantidade ?? 1;
            }
            else
            {
                Inner.Add(produto);
            }
        }
        public void Remove(string nome)
        {
            var produto = Inner.FirstOrDefault(p => p.Nome == nome);
            if (produto != null)
            {
                Inner.Remove(produto);
            }
            else
            {
                throw new KeyNotFoundException("Produto não encontrado no carrinho.");
            }
        }

        public void Clear()
        {
            Inner.Clear();
        }

        public void Edit(string nome, int newQuantity)
        {
            var produto = Inner.FirstOrDefault(p => p.Nome == nome);
            if (produto != null)
            {
                if (newQuantity <= 0)
                {
                    Inner.Remove(produto);
                }
                else
                {
                    produto.Quantidade = newQuantity;
                }
            }
            else
            {
                throw new KeyNotFoundException("Produto não encontrado no carrinho.");
            }
        }

        public decimal Total()
        {
            return Inner.Sum(p => p.Preco * (p.Quantidade ?? 1));
        }

        public override string ToString()
        {
            string aux = "";
            foreach (var item in Inner)
            {
                aux += $"{item.Nome} - {item.Preco:C} - Quantidade: {item.Quantidade}\n\t";
            }
            return aux;
        }

        public JsonResult ToJsonResult()
        {
            return new JsonResult(Inner.Select(p => new
            {
                p.Nome,
                p.Preco,
                Quantidade = p.Quantidade ?? -1
            }));
        }

        public Produto? FindByName(string nome)
        {
            return Inner.FirstOrDefault(c => c.Nome == nome);
        }

        public Produto? Find(Produto prod)
        {
            if (Inner.Count == 0)
            {
                throw new InvalidOperationException("Carrinho vazio.");
            }
            return Inner.FirstOrDefault(c => c.Nome == prod.Nome);

        }
    }
}
