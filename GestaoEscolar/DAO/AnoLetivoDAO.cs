using GestaoEscolar.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GestaoEscolar.DAO
{
    public class AnoLetivoDAO
    {
        private Contexto contexto;

        public AnoLetivoDAO(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public void Salvar(AnoLetivo novoAnoLetivo)
        {
            contexto.AnoLetivos.Add(novoAnoLetivo);
            contexto.SaveChanges();
        }

        public void Alterar(AnoLetivo anoLetivo)
        {
            contexto.Entry(anoLetivo).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Excluir(AnoLetivo anoLetivo)
        {
            contexto.AnoLetivos.Remove(anoLetivo);
            contexto.SaveChanges();
        }

        public AnoLetivo BuscaAnoLetivoPorId(int Id)
        {
            return contexto.AnoLetivos.First(x => x.Id == Id);
        }

        public IList<AnoLetivo> ListaAnoLetivo()
        {
            return contexto.AnoLetivos.ToList();
        }

    }
}