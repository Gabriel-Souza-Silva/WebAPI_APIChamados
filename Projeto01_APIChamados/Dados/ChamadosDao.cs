using Projeto01_APIChamados.Enumeracao;
using Projeto01_APIChamados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Projeto01_APIChamados.Dados
{
    public class ChamadosDao
    {
        public IEnumerable<Chamado> BuscarTodos()
        {
            using (var ctx = new ChamadosEntities())
            {
                return ctx.Chamados.ToList();
            }
        }

        public Chamado BuscarChamado(int id)
        {
            using (var ctx = new ChamadosEntities())
            {
                return ctx.Chamados.FirstOrDefault(p => p.ChamadoId == id);
            }
        }

        public void IncluirChamado(Chamado chamado)
        {
            using (var ctx = new ChamadosEntities())
            {
                ctx.Chamados.Add(chamado);
                ctx.SaveChanges();
            }
        }
               
        public ResultadoDelete DeletarChamado(int id)
        {
            using (var ctx = new ChamadosEntities())
            {
                var chamado = ctx.Chamados.FirstOrDefault(p => p.ChamadoId == id);
                
                if(chamado.Resposta == null)
                {
                    ctx.Entry(chamado).State = System.Data.Entity.EntityState.Deleted;
                    ctx.SaveChanges();
                    return ResultadoDelete.SUCESSO;
                }
                else
                {
                    return ResultadoDelete.JA_RESPONDIDO;
                }
            }
        }
                
    }
}