using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEscolar.Models
{
    public class HistoricoAluno
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Ano")]
        public int AnoHistoricoAluno { get; set; }
        [DisplayName("Serie")]
        public string TurmaAnoHistorico { get; set; }
        [DisplayName("Data Criação Histórico")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataHistoricoAluno { get; set; }
        [DisplayName("Escola")]
        public string EscolaAnoAluno { get; set; }
        [DisplayName("Cidade onde cursou")]
        public string CidadeEscolaAnoAluno { get; set; }
        [DisplayName("UF")]
        public string UfEscolaAnoAluno { get; set; }
        [DisplayName("básica")]
        public string ChCurricularBasica { get; set; }
        [DisplayName("Artes")]
        public string ChCurricularArtes { get; set; }
        [DisplayName("Informática")]
        public string ChCurricularInformatica { get; set; }
        [DisplayName("Lit. Infanto Juvenil")]
        public string ChCurricularLitInfantoJuvenil { get; set; }
        [DisplayName("Projeto Tempo Integral")]
        public string ChCurricularProjetoTempoIntegral { get; set; }
        [DisplayName("Básica")]
        public string QtdFaltaHoraAnoBasica { get; set; }
        [DisplayName("Parte Diversificada")]
        public string QtdFaltaHoraAnoParte { get; set; }
        [DisplayName("Projeto Tempo Integral")]
        public string QtdFaltaHoraAnoProjeto { get; set; }

        [DisplayName("QTD dias letivos")]
        public int DiasLetivos { get; set; }
        [DisplayName("Português")]
        public string AprovPortugues { get; set; }
        [DisplayName("Matemática")]
        public string AprovMatematica { get; set; }
        [DisplayName("Geografia")]
        public string AprovGeografia { get; set; }
        [DisplayName("História")]
        public string AprovHistoria { get; set; }
        [DisplayName("Ciencia")]
        public string AprovCiencia { get; set; }
        [DisplayName("Ed. Fisica")]
        public string AprovEduFisica { get; set; }
        [DisplayName("Ensino Religioso")]
        public string AprovEnsReligioso { get; set; }
        [DisplayName("Artes")]
        public string AprovArte { get; set; }
        [DisplayName("Informática")]
        public string AprovInformatica { get; set; }
        [DisplayName("Lit. Infanto Juvenil")]
        public string AprovLitInfantilJuvenil { get; set; }
        [DisplayName("Atividade Linguagem Matematica")]
        public string AprovAtividadeLingMat { get; set; }
        [DisplayName("Atividade Esportiva Motora")]
        public string AprovAtividadesEspMot { get; set; }
        [DisplayName("Formação pessoal")]
        public string AprovFormacaoPessoal { get; set; }
        [DisplayName("Situação do Aluno")]
        public string SituacaoAluno { get; set; }
        public int AlunoId { get; set; }

        [ForeignKey("AlunoId")]
        public virtual Aluno Aluno { get; set; }



             

        

    }
}