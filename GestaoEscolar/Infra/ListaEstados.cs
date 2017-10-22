using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoEscolar.Infra
{
    static class ListaEstados
    {

        static List<Estado> estados;

        static ListaEstados()
        {
            estados = new List<Estado>(50);
            estados.Add(new Estado("AL", "Alabama"));
            estados.Add(new Estado("AK", "Alaska"));
            estados.Add(new Estado("AZ", "Arizona"));
            estados.Add(new Estado("AR", "Arkansas"));
            estados.Add(new Estado("CA", "California"));
            estados.Add(new Estado("CO", "Colorado"));
            estados.Add(new Estado("CT", "Connecticut"));
            estados.Add(new Estado("DE", "Delaware"));
            estados.Add(new Estado("DC", "District Of Columbia"));
            estados.Add(new Estado("FL", "Florida"));
            estados.Add(new Estado("GA", "Georgia"));
            estados.Add(new Estado("HI", "Hawaii"));
            estados.Add(new Estado("ID", "Idaho"));
            estados.Add(new Estado("IL", "Illinois"));
            estados.Add(new Estado("IN", "Indiana"));
            estados.Add(new Estado("IA", "Iowa"));
            estados.Add(new Estado("KS", "Kansas"));
            estados.Add(new Estado("KY", "Kentucky"));
            estados.Add(new Estado("LA", "Louisiana"));
            estados.Add(new Estado("ME", "Maine"));
            estados.Add(new Estado("MD", "Maryland"));
            estados.Add(new Estado("MA", "Massachusetts"));
            estados.Add(new Estado("MI", "Michigan"));
            estados.Add(new Estado("MN", "Minnesota"));
            estados.Add(new Estado("MS", "Mississippi"));
            estados.Add(new Estado("MO", "Missouri"));
            estados.Add(new Estado("MT", "Montana"));
            estados.Add(new Estado("NE", "Nebraska"));
            estados.Add(new Estado("NV", "Nevada"));
            estados.Add(new Estado("NH", "New Hampshire"));
            estados.Add(new Estado("NJ", "New Jersey"));
            estados.Add(new Estado("NM", "New Mexico"));
            estados.Add(new Estado("NY", "New York"));
            estados.Add(new Estado("NC", "North Carolina"));
            estados.Add(new Estado("ND", "North Dakota"));
            estados.Add(new Estado("OH", "Ohio"));
            estados.Add(new Estado("OK", "Oklahoma"));
            estados.Add(new Estado("OR", "Oregon"));
            estados.Add(new Estado("PA", "Pennsylvania"));
            estados.Add(new Estado("RI", "Rhode Island"));
            estados.Add(new Estado("SC", "South Carolina"));
            estados.Add(new Estado("SD", "South Dakota"));
            estados.Add(new Estado("TN", "Tennessee"));
            estados.Add(new Estado("TX", "Texas"));
            estados.Add(new Estado("UT", "Utah"));
            estados.Add(new Estado("VT", "Vermont"));
            estados.Add(new Estado("VA", "Virginia"));
            estados.Add(new Estado("WA", "Washington"));
            estados.Add(new Estado("WV", "West Virginia"));
            estados.Add(new Estado("WI", "Wisconsin"));
            estados.Add(new Estado("WY", "Wyoming"));
        }

        public static string[] Abreviaturas()
        {
            List<string> abbrevList = new List<string>(estados.Count);
            foreach (var estado in estados)
            {
                abbrevList.Add(estado.Abreviatura);
            }
            return abbrevList.ToArray();
        }

        public static string[] Nomes()
        {
            List<string> nameList = new List<string>(estados.Count);
            foreach (var estado in estados)
            {
                nameList.Add(estado.Nome);
            }
            return nameList.ToArray();
        }

        public static Estado[] Estados()
        {
            return estados.ToArray();
        }

    }
    class Estado
    {

        public Estado()
        {
            Nome = null;
            Abreviatura = null;
        }

        public Estado(string ab, string name)
        {
            Nome = name;
            Abreviatura = ab;
        }

        public string Nome { get; set; }

        public string Abreviatura { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Abreviatura, Nome);
        }

    }
}