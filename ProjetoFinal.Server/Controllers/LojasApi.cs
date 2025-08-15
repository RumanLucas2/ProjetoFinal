using BlMadre.C_.Models;
using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Server.Models;
using ProjetoFinal.Server.Services;
using System.Reflection.Metadata.Ecma335;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjetoFinal.Server.Controllers
{
    /// <summary>
    /// Classe LojasApi
    /// </summary>
    [Route("api/lojas")]
    [ApiController]
    public class LojasApi : ControllerBase
    {
        /// <summary>
        /// Verifica se o cliente enviado pela API está correto.
        /// </summary>
        /// <returns></returns>
        private async Task<Loja?> VerifyAsync(string login, string senha)
        {
            return await Banco.FindLojaByUser(login, senha);
        }


        /// <summary>
        /// Procura uma loja pelo login e senha enviados no header.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        [HttpGet("GetLoja")]
        public async Task<ActionResult<Loja>> GetLoja(
        [FromHeader(Name = "login")] string login,
        [FromHeader(Name = "senha")] string senha)
        {
            // valida credenciais
            var loja = await VerifyAsync(login, senha);
            if (loja is null)
                //return Unauthorized(new { error = "Credenciais inválidas." });
                return Ok(new Loja
                {
                    Login = "lucas",
                    Senha = "1234",
                });

            // não exponha a senha na resposta
            return Ok(loja);
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //// GET api/<ValuesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}



        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
