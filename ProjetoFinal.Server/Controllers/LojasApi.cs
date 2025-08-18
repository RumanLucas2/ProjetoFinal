using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.MicrosoftExtensions;
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
        /// Valida se a loja existe com as credenciais enviadas.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha">deve ser criptografada na fonte</param>
        /// <returns></returns>
        [HttpGet("Validate")]
        public async Task<ActionResult<bool>> Validate(
        [FromHeader(Name = "login")] string login,
        [FromHeader(Name = "senha")] string senha)
        {
            // valida credenciais
            var loja = await VerifyAsync(login, senha);
            if (loja is null)
                return Unauthorized(new { error = "Credenciais inválidas." });

            return Ok(true);
        }


        // GET: api/<ValuesController>
        /// <summary>
        /// Deprecated: Use Validate instead.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// deprecated
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        /// <summary>
        /// deprecated
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        /// <summary>
        /// deprecated
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
