using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoEscolar.Infra
{
    public class RelatorioInfra
    {
        public RelatorioInfra()
        {}

        public static Dictionary<string, string> listaRelatorio()
        {
            Dictionary<string, string> ListaRelatorios = new Dictionary<string, string>();

            ListaRelatorios.Add("Historico Aluno", "Relatorio/ModalHistorico");
            ListaRelatorios.Add("Declaracao de Matricula", "Relatorio/ModalDecMatricula");
            ListaRelatorios.Add("Ficha Individual", "Relatorio/ModalFichaIndividual");

            return ListaRelatorios; 
        }
    }
}