using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Entidades
{
    public class Cursos_Lista
    {
        public string curso { get; set; }
        public int instrutor { get; set; }
        public int aluno { get; set; }
        public int maximo { get; set; }
        public List<Cursos_Datas> datas { get; set; }

        public Cursos_Lista()
        {
            this.curso = "";
            this.instrutor = 0;
            this.aluno = 0;
            this.maximo = 0;
        }

        public Cursos_Lista(string curso, int instrutor, int aluno, int maximo, int idcurso)
        {
            this.curso = curso;
            this.instrutor = instrutor;
            this.aluno = aluno;
            this.maximo = maximo;
            this.datas = new Cursos_DatasDB().Listar(idcurso);
        }
    }
}
