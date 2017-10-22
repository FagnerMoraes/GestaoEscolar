using GestaoEscolar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestaoEscolar.DAO
{
    public class DisciplinaDAO
    {
        private Contexto contexto;

        public DisciplinaDAO(Contexto contexto)
        {
            this.contexto = contexto;
        }
        
        public IList<Disciplina> ListarDisciplina()
        {
            return contexto.Disciplinas.ToList();
        }

        public Disciplina BuscaPorId(int Id)
        {
            return contexto.Disciplinas.FirstOrDefault(arg => arg.Id == Id);
        }

        public void Salvar(Disciplina disciplina)
        {
            contexto.Disciplinas.Add(disciplina);
            contexto.SaveChanges();
        }

        public void Alterar(Disciplina disciplina)
        {
            contexto.Entry(disciplina).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Excluir(Disciplina disciplina)
        {
            contexto.Disciplinas.Remove(disciplina);
            contexto.SaveChanges();
        }

        public void VerificarDisciplinasCadastradas()
        {
            var lista = contexto;

            var disciplinas = new List<Disciplina>();


            if (!contexto.Disciplinas.Any(arg => arg.NomeDisciplina == "LINGUA PORTUGUESA"))
            {
                var disciplina = new Disciplina();

                disciplina.NomeDisciplina = "LINGUA PORTUGUESA";
                disciplina.MaximoPontos = "A";
                disciplina.MediaPontos = "B";
                disciplina.MinimoPonto = "C";

                disciplinas.Add(disciplina);

            }

            if (!contexto.Disciplinas.Any(arg => arg.NomeDisciplina == "ARTE"))
            {
                var disciplina = new Disciplina();

                disciplina.NomeDisciplina = "ARTE";
                disciplina.MaximoPontos = "A";
                disciplina.MediaPontos = "B";
                disciplina.MinimoPonto = "C";

                disciplinas.Add(disciplina);
            }

            if (!contexto.Disciplinas.Any(arg => arg.NomeDisciplina == "EDUCACAO FISICA"))
            {
                var disciplina = new Disciplina();

                disciplina.NomeDisciplina = "ED FISICA";
                disciplina.MaximoPontos = "A";
                disciplina.MediaPontos = "B";
                disciplina.MinimoPonto = "C";

                disciplinas.Add(disciplina);
            }

            if (!contexto.Disciplinas.Any(arg => arg.NomeDisciplina == "HISTORIA"))
            {
                var disciplina = new Disciplina();

                disciplina.NomeDisciplina = "HISTORIA";
                disciplina.MaximoPontos = "A";
                disciplina.MediaPontos = "B";
                disciplina.MinimoPonto = "C";

                disciplinas.Add(disciplina);
            }

            if (!contexto.Disciplinas.Any(arg => arg.NomeDisciplina == "GEOGRAFIA"))
            {
                var disciplina = new Disciplina();

                disciplina.NomeDisciplina = "GEOGRAFIA";
                disciplina.MaximoPontos = "A";
                disciplina.MediaPontos = "B";
                disciplina.MinimoPonto = "C";

                disciplinas.Add(disciplina);
            }

            if (!contexto.Disciplinas.Any(arg => arg.NomeDisciplina == "ENSINO RELIGIOSO"))
            {
                var disciplina = new Disciplina();

                disciplina.NomeDisciplina = "ENSINO RELIGIOSO";
                disciplina.MaximoPontos = "A";
                disciplina.MediaPontos = "B";
                disciplina.MinimoPonto = "C";

                disciplinas.Add(disciplina);
            }

            if (!contexto.Disciplinas.Any(arg => arg.NomeDisciplina == "CIENCIAS DA NATUREZA"))
            {
                var disciplina = new Disciplina();

                disciplina.NomeDisciplina = "CIENCIAS DA NATUREZA";
                disciplina.MaximoPontos = "A";
                disciplina.MediaPontos = "B";
                disciplina.MinimoPonto = "C";

                disciplinas.Add(disciplina);
            }

            if (!contexto.Disciplinas.Any(arg => arg.NomeDisciplina == "MATEMATICA"))
            {
                var disciplina = new Disciplina();

                disciplina.NomeDisciplina = "MATEMATICA";
                disciplina.MaximoPontos = "A";
                disciplina.MediaPontos = "B";
                disciplina.MinimoPonto = "C";

                disciplinas.Add(disciplina);
            }

            if (!contexto.Disciplinas.Any(arg => arg.NomeDisciplina == "INFORMATICA"))
            {
                var disciplina = new Disciplina();

                disciplina.NomeDisciplina = "INFORMATICA";
                disciplina.MaximoPontos = "A";
                disciplina.MediaPontos = "B";
                disciplina.MinimoPonto = "C";

                disciplinas.Add(disciplina);
            }

            if (!contexto.Disciplinas.Any(arg => arg.NomeDisciplina == "LITERATURA INFANTO JUVENIL"))
            {
                var disciplina = new Disciplina();

                disciplina.NomeDisciplina = "LITERATURA INFANTO JUVENIL";
                disciplina.MaximoPontos = "A";
                disciplina.MediaPontos = "B";
                disciplina.MinimoPonto = "C";

                disciplinas.Add(disciplina);
            }

            
            foreach(var novaDisciplina in disciplinas)
            {
                Salvar(novaDisciplina);
            }
            
        }


        }
    }