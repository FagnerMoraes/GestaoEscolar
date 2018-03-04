using GestaoEscolar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoEscolar.DAO
{
    public class TipoFuncionarioDAO
    {
        private Contexto contexto;

        public TipoFuncionarioDAO(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public void VerificarCargos()
        {
            var lista = contexto;
            var tipofuncionarios = new List<TipoFuncionario>();


            if (!contexto.TipoFuncionarios.Any(arg => arg.DescricaoFuncionario == "PROFESSOR"))
            {
                var tipofuncionario = new TipoFuncionario();
                tipofuncionario.DescricaoFuncionario = "PROFESSOR";
                tipofuncionarios.Add(tipofuncionario);
            }
            if (!contexto.TipoFuncionarios.Any(arg => arg.DescricaoFuncionario == "SECRETARIO"))
            {
                var tipofuncionario = new TipoFuncionario();
                tipofuncionario.DescricaoFuncionario = "SECRETARIO";
                tipofuncionarios.Add(tipofuncionario);
            }
            if (!contexto.TipoFuncionarios.Any(arg => arg.DescricaoFuncionario == "DIRETOR"))
            {
                var tipofuncionario = new TipoFuncionario();
                tipofuncionario.DescricaoFuncionario = "DIRETOR";
                tipofuncionarios.Add(tipofuncionario);
            }

        }


        }
    }