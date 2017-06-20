using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GestaoEscolar.Controllers;

namespace GestaoEscolar.Models
{
    public class Aluno
    {
        //Basico
        [Key]
        public int Id { get; set; }
        [DisplayName("ID INEP")]
        public string IdentificacaoUnica { get; set; } // Identifi cação única (código gerado pelo Inep
        

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(200, MinimumLength = 8, ErrorMessage = "Nome deve ser valido")]
        [RegularExpression("^([a-zA-Z .&'-]+)$", ErrorMessage = "Nome deve conter apenas Letras")]
        [DisplayName("Aluno")]
        public string Nome { get; set; } // nome completo do aluno, sem abreviações, de acordo com a certidão de nascimento

        [DisplayName("NIS")]
        public string Nis { get; set; } // Número de Identifi cação Social (NIS)

        [DisplayName("Nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; } // formato dd/mm/aaaa

        // [Required(ErrorMessage = "Sexo é obrigatório")]
        [DisplayName("Sexo")]
        public string Sexo { get; set; } // masculino ou feminino

        [DisplayName("Estado Civil")]
        public string EstadoCivilAluno { get; set; }

        [DisplayName("Profissão")]
        public string ProfissaoAluno { get; set; }

        [DisplayName("Religião")]
        public string ReligiaoAluno { get; set; }

        [DisplayName("Raça/Cor")]
        public string RacaCor { get; set; } //branca, preta, parda, amarela, indígena ou não declarada

        [DisplayName("Situação")]
        public string Situacao { get; set; }// Ativo, Transferido, Abandono, Expulsao, Saiu Por Outro motivo

        
        //filiacao Nome completo sem abreviação de acordo com a Certidao de nascimento
        [DisplayName("Responsavel pelo aluno")]
        public string Responsavel { get; set; }// pai ou mãe ou familiar

        [DisplayName("Parentesco do Responsavel")]
        public string ParentescoResponsavel { get; set; } // grau de parentesco

        [DisplayName("Pai")]
        public string NomePai { get; set; }

        [DisplayName("Profissão do Pai")]
        public string ProfPai { get; set; }

        [DisplayName("Telefone do Pai")]
        public string TelPai { get; set; }

        [DisplayName("Mãe")]
        public string NomeMae { get; set; }

        [DisplayName("Profissão do Mãe")]
        public string ProfMae { get; set; }

        [DisplayName("Telefone do Mãe")]
        public string TelMae { get; set; }

        //Continuacao
        [DisplayName("Nacionalidade")]
        public string Nacionalidade { get; set; } //Brasileira ; Brasileira – nascido no exterior ou naturalizado ; Estrangeira

        [DisplayName("Pais de origem")]
        public string PaisOrigem { get; set; } // Ver lista de paises

        [DisplayName("UF")]
        public string UfNaturalidade { get; set; }

        [DisplayName("Cidade Natal")]
        public string Naturalidade { get; set; }

        // se o aluno possui ou não defi ciência, transtorno global do desenvolvimento 
        // Opções: Deficiência física, Deficiência auditiva, Surdez, Deficiência visual, Cegueira, Baixa visão,
        // Deficiência intelectual, Deficiência múltipla, Surdocegueira, Autismo, Síndrome de Rett, Síndrome de Asperger, Transtorno Desintegrativo da Infância

        [DisplayName("Pessoa autorizada a buscar aluno")]
        public string PessoaAutoBuscarAluno { get; set; }

        [DisplayName("Disponibilidade dos pais para reuniões")]
        public string HorDispReunioesPaisAluno { get; set; }

        [DisplayName("Possui problemas de saúde?")]
        public string ProblemaSaudeAluno { get; set; }

        [DisplayName("Descrição do problema de saúde")]
        public string DescProblemaSaudeAluno { get; set; }

        [DisplayName("Medicamentos que o aluno faz uso")]
        public string MedicamentoUsadoAluno { get; set; }

        [DisplayName("Alergias do aluno")]
        public string AlergiasAluno { get; set; }

        [DisplayName("Faz acompanhamento com Fonoaudiologo?")]
        public string AcompFonoaudiologoAluno { get; set; }

        [DisplayName("Faz acompanhamento com Pscicologo?")]
        public string AcompPscicologoAluno { get; set; }

        [DisplayName("Faz acompanhamento com APAE?")]
        public string AcompApaeAluno { get; set; }

        [DisplayName("Possui deficiência fisica?")]
        public string DefFisicaAluno { get; set; }

        [DisplayName("Descrição da deficiência fisica?")]
        public string DescDefFisicaAluno { get; set; }

        [DisplayName("Possui deficiência visual?")]
        public string DefVisualAluno { get; set; }

        [DisplayName("Descrição da deficiência visual?")]
        public string DescDefVisualAluno { get; set; }

        [DisplayName("Possui deficiência auditiva?")]
        public string DefAuditivaAluno { get; set; }

        [DisplayName("Descrição da deficiência auditiva?")]
        public string DescDefAuditivaAluno { get; set; }

        [DisplayName("Possui altas habilidaedes?")]
        public string AltaHabilidade { get; set; }// se o aluno possui ou não altas habilidades/superdotação

        [DisplayName("Descrição das altas habilidades")]
        public string DescAltaHabilidade { get; set; } // Descricao habilidade



        [DisplayName("Recurso para realização de provas?")]
        public string RecursoParticipacaoProvas { get; set; } //tipo de recurso e/ou serviço necessário para a participação do aluno em avaliações do Inep
        // dados Auxílio ledor, Auxílio transcrição, Guia-intérprete, Intérprete de Libras, Leitura labial, Prova em Braille,
        //Prova ampliada (fonte tamanho 16), Prova ampliada (fonte tamanho 20), Prova ampliada (fonte tamanho 24), Nenhum.


        //Documentos
        [DisplayName("Nº Identidade")]
        public string Rg { get; set; }

        [DisplayName("Orgão Expedidor")]
        public string OrgaoExpRg { get; set; }

        [DisplayName("UF RG")]
        public string UfRg { get; set; }

        [DisplayName("Dt. Expedição")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataExpRg { get; set; }

        [DisplayName("Certidão Civil")]
        public string CertidaoCivil { get; set; } //modelo Antigo ou Novo

        [DisplayName("Tipo certidão")]
        public string TipoCertidaoCivil { get; set; } // Certidão de nascimento, Certidão de casamento

        [DisplayName("Nova certidão civil")]
        public string NumeroNovaCertidaoCivil { get; set; } // numero das novas certidões

        [DisplayName("Numero do Termo")]
        public string NumeroDoTermo { get; set; }

        [DisplayName("Folha")]
        public string Folha { get; set; }

        [DisplayName("Livro")]
        public string Livro { get; set; }


        [DisplayName("Data de Emissão")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataEmissaoCertidao { get; set; }

        [DisplayName("Nome do Cartório")]
        public string NomeCartorioCertidao { get; set; }

        [DisplayName("CPF")]
        public string Cpf { get; set; }

        //Endereco Residencial

        [DisplayName("Distrito")]
        public string LocalZonaResidencia { get; set; }

        [DisplayName("CEP")]
        public string CepResidencia { get; set; }

        [DisplayName("Logradouro")]
        public string EnderecoResidencia { get; set; }

        [DisplayName("Numero")]
        public string NumeroResidencia { get; set; }

        [DisplayName("Complemento")]
        public string ComplementoResidencia { get; set; }

        [DisplayName("Bairro")]
        public string BairroResidencia { get; set; }

        [DisplayName("UF")]
        public string UfResidencia { get; set; }

        [DisplayName("Cidade")]
        public string CidadeResidencia { get; set; }


        public virtual ICollection<Matricula> Matriculas { get; set; }
        public virtual ICollection<HistoricoAluno> HistoricoAlunos { get; set; }



    }
}