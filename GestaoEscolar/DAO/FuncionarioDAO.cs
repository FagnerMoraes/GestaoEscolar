using GestaoEscolar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestaoEscolar.DAO
{
    public class FuncionarioDAO
    {
        private Contexto contexto;

        public FuncionarioDAO(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public IList<Funcionario> ListaFuncionario()
        {
            return contexto.Funcionarios.Include("Escola").Include("TipoFuncionario").OrderBy(x => x.NomeFuncionario).ToList();
        }

        public IList<Escola> listarEscola()
        {
            return contexto.Escolas.ToList();
        }

        public IList<TipoFuncionario> listarTipoFuncionario()
        {
            return contexto.TipoFuncionarios.ToList();
        }

        public void Salvar(Funcionario funcionario)
        {
            contexto.Funcionarios.Add(funcionario);
            contexto.SaveChanges();
        }

        public Funcionario BuscaPorId(int id)
        {
            return contexto.Funcionarios.Include("Escola").Include("TipoFuncionario").FirstOrDefault(x => x.Id == id);
        }

        public void Alterar(Funcionario funcionario)
        {
            contexto.Entry(funcionario).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Excluir(Funcionario funcionario)
        {
            contexto.Funcionarios.Remove(funcionario);
            contexto.SaveChanges();
        }
    }
}