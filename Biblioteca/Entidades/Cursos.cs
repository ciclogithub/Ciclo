using Biblioteca.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Biblioteca.Entidades
{
    public class Cursos
    {
        public int idcurso { get; set; }
        public int idorganizador { get; set; }
        public string txcurso { get; set; }
        public int idtema { get; set; }
        public int idcategoria { get; set; }
        public int idlocal { get; set; }
        public string nrminimo { get; set; }
        public string nrmaximo { get; set; }
        public string txcargahoraria { get; set; }
        public bool flgratuito { get; set; }
        public string txlocal { get; set; }
        public string txdescritivo { get; set; }
        public string txfoto { get; set; }
        public Temas nome_tema { get; set; }
        public Categorias nome_categoria { get; set; }
        public Locais nome_local { get; set; }
        public List<Instrutores> instrutores { get; set; }
        public List<Alunos> alunos { get; set; }
        public int idcor { get; set; }
        public string txcor { get; set; }
        public string txidentificador { get; set; }
        public int total { get; set; }
        public int idespecialidade { get; set; }

        HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];

        public Cursos()
        {
            this.idcurso = 0;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txcurso = "";
            this.idtema = 0;
            this.idcategoria = 0;
            this.idlocal = 0;
            this.nrminimo = "";
            this.nrmaximo = "";
            this.txcargahoraria = "";
            this.flgratuito = false;
            this.txlocal = "";
            this.txdescritivo = "";
            this.txfoto = "";
            this.nome_tema = null;
            this.nome_categoria = null;
            this.nome_local = null;
            this.idcor = 0;
            this.txcor = "";
            this.txidentificador = "";
            this.total = 0;
            this.idespecialidade = 0;
        }

        public Cursos(int id, string curso, int tema, int categoria, int codlocal, string local, string minimo, string maximo, string cargahoraria, string descricao, bool gratuito, string foto, int cor, string identificador, int total, int especialidade)
        {
            this.idcurso = id;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txcurso = curso;
            this.idtema = tema;
            this.idcategoria = categoria;
            this.idlocal = codlocal;
            if (minimo == "0") { this.nrminimo = ""; } else { this.nrminimo = minimo; };
            if (maximo == "0") { this.nrmaximo = ""; } else { this.nrmaximo = maximo; };
            if (cargahoraria == "0") { this.txcargahoraria = ""; } else { this.txcargahoraria = cargahoraria; };
            this.flgratuito = gratuito;
            this.txlocal = local;
            this.txdescritivo = descricao;
            this.txfoto = foto;
            this.nome_tema = new TemasDB().Buscar(tema);
            this.nome_categoria = new CategoriasDB().Buscar(categoria);
            this.nome_local = new LocaisDB().Buscar(codlocal);
            this.instrutores = new InstrutoresDB().ListarDoCurso(id);
            this.alunos = new AlunosDB().ListarDoCurso(id);
            this.idcor = cor;
            this.txidentificador = identificador;
            this.txcor = new CoresDB().Buscar(cor);
            this.total = total;
            this.idespecialidade = especialidade;
        }

        public Cursos(int id, string curso)
        {
            this.idcurso = id;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txcurso = curso;
        }

        public Cursos(int id, string curso, int categoria, bool gratuito, int cor, int total)
        {
            this.idcurso = id;
            this.idorganizador = Convert.ToInt32(cookie.Value);
            this.txcurso = curso;
            this.flgratuito = gratuito;
            this.txcor = new CoresDB().Buscar(cor);
            this.nome_categoria = new CategoriasDB().Buscar(categoria);
            this.total = total;
        }

        public int Salvar()
        {
            int ident = new CursosDB().Salvar(this);
            return ident;
        }

        public void Alterar()
        {
            new CursosDB().Alterar(this);
        }

        public void Excluir(int ident)
        {
            new CursosDB().Excluir(ident);
        }

        public void AlterarFoto()
        {
            new CursosDB().AlterarFoto(this);
        }

        public void RemoverInstrutores(int id = 0)
        {
            new CursosDB().RemoverInstrutores(id);
        }

        public void SalvarInstrutor(int id = 0, int instrutor = 0)
        {
            if ((id > 0) && (instrutor > 0)) {
                new CursosDB().SalvarInstrutor(id, instrutor);
            }
        }

        public void RemoverAlunos(int id = 0)
        {
            new CursosDB().RemoverAlunos(id);
        }

        public void SalvarAlunos(int id = 0, int aluno = 0)
        {
            if ((id > 0) && (aluno > 0))
            {
                new CursosDB().SalvarAlunos(id, aluno);
            }
        }
    }
}
