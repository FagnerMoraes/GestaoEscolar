using GestaoEscolar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestaoEscolar.DAO
{
    public class EscolaDAO
    {
        private Contexto contexto;

        public EscolaDAO(Contexto contexto)
        {
            this.contexto = contexto;
        }
        
        public void salva(Escola novaEscola)
        {
            contexto.Escolas.Add(novaEscola);
            contexto.SaveChanges();
        }

        public void SalvarMudanca(Escola escola)
        {
            contexto.Entry(escola).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public IList<Escola> ListaEscolas()
        {
            return contexto.Escolas.ToList();
        }

        public Escola BuscaPorId(long Id)
        {
            return contexto.Escolas.Find(Id);
        }

        public void Excluir (long Id)
        {
            var escola = contexto.Escolas.First(x => x.Id == Id);
            contexto.Escolas.Remove(escola);
            contexto.SaveChanges();
        }
    }
}