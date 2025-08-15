using Microsoft.AspNetCore.Mvc;

namespace BlMadre.C_.Models
{
    public class Carrinho
    {
        private List<Produto> Inner { get; set; }

        public Guid UserId 
        { 
            get;
            private set;
        }

        // Construtor padrão necessário para desserialização
        public Carrinho()
        {
            Inner = new List<Produto>();
        }

        public Carrinho(List<Produto>? produto)
        {
            Inner = produto ?? new List<Produto>();
        }

        #region summary
        /// <summary>
        /// Adiciona um produto ao carrinho.
        /// </summary>
        /// <param name="produto"><see cref="Produto"/> a ser adicionado</param>
        /// <exception cref="InvalidOperationException"></exception>
        #endregion
        public void Add(Produto produto, Client cliente)
        {
            if (produto.Quantidade <= 0) throw new AppException(ErrorCode.ItemQuantityInvalid);
            var existingProduct = Inner.FirstOrDefault(p => p.Nome == produto.Nome);
            if (existingProduct != null) existingProduct.Quantidade += produto.Quantidade ?? 1;
            if(cliente == null) throw new AppException(ErrorCode.NullReference, "Usuario Nulo no Carrinho");
            UserId = cliente.id;
            Inner.Add(produto);
        }

        #region summary
        /// <summary>
        /// remove um produto do carrinho pelo nome.
        /// </summary>
        /// <param name="nome"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        #endregion
        public void Remove(string nome)
        {
            var produto = Inner.FirstOrDefault(p => p.Nome == nome);
            if (produto != null)
            {
                Inner.Remove(produto);
            }
            else
            {
                throw new AppException(ErrorCode.ItemNotFound);
            }
        }

        #region summary
        /// <summary>
        /// Limpa o carrinho, removendo todos os produtos.
        /// </summary>
        #endregion
        public void Clear()
        {
            Inner.Clear();
        }

        #region summary
        /// <summary>
        /// Edita a quantidade de um produto no carrinho.
        /// </summary>
        /// <param name="nome">Nome do produto</param>
        /// <param name="newQuantity">Produto a ser mudado</param>
        /// <exception cref="KeyNotFoundException"></exception>
        #endregion
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
                throw new AppException(ErrorCode.ItemNotFound);
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
                throw new AppException(ErrorCode.EmptyCart);
            }
            return Inner.FirstOrDefault(c => c.Nome == prod.Nome);

        }
    }
}
