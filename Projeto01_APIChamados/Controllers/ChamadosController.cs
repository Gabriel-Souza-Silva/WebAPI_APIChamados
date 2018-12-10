using Projeto01_APIChamados.Dados;
using Projeto01_APIChamados.Enumeracao;
using Projeto01_APIChamados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Projeto01_APIChamados.Controllers
{
    public class ChamadosController : ApiController
    {
        /*ESTE ARQUIVO ESTA SENDO USADO POR UM PROJETO CHAAMDO *Projeto03_Chamados* no topico_clienteWebApi no modulo 8*/

        static readonly ChamadosDao dao = new ChamadosDao();

        public IEnumerable<Chamado> GetChamados()
        {
            return dao.BuscarTodos();
        }

        [Route ("api/chamados/{id}")]
        public Chamado GetChamado(int id)
        {
            return dao.BuscarChamado(id);
        }

        public HttpResponseMessage PostChamado(Chamado chamado)
        {
            dao.IncluirChamado(chamado);

            var response = Request.CreateResponse<Chamado>(HttpStatusCode.Created, chamado);
            string uri = Url.Link("DefaultApi", new { id = chamado.ChamadoId });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [AcceptVerbs("Get", "Delete")]
        [Route("api/chamados/deletar/{id}")]
        public HttpResponseMessage DeleteChamado(int id)
        {
            Chamado chamado = dao.BuscarChamado(id);
            ResultadoDelete resultado = dao.DeletarChamado(id);

            if (resultado == ResultadoDelete.SUCESSO)
            {
                var response = Request.CreateResponse<Chamado>(HttpStatusCode.Created, chamado);
                string uri = Url.Link("DefaultApi", new { id });
                response.Headers.Location = new Uri(uri);

                return response;
            }
            else
            {
                var erro = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Erro no servidor"),
                    ReasonPhrase = "Irmão, já foi respondido esse chamado"
                };

                throw new HttpResponseException(erro);
            }

        }
    }
}
