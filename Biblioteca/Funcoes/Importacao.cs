using Biblioteca.DB;
using Biblioteca.Entidades;
using ExcelDataReader;
using System;
using System.IO;
using System.Text;
using System.Web;

namespace Biblioteca.Funcoes
{
    public static class Importacao
    {
        public static string ImportaAluno(string path)
        {
            int i = 0;
            Boolean err = false;
            string line = "";
            string nl = "\n";// System.Environment.NewLine;

            string nome = "";
            string cpf = "";
            string empresa = "";
            string pais = "";
            string estado = "";
            string cidade = "";
            string observacao = "";
            string email1 = "";
            string email2 = "";
            string telefone1 = "";
            string telefone2 = "";
            string whatsapp = "";

            int p = 0;
            int e = 0;
            int c = 0;
            int emp = 0;
            int alu = 0;

            HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
            int org = Convert.ToInt32(cookie.Value);

            var stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);
            while (reader.Read())
            {
                nome = Convert.ToString(reader.GetValue(0));
                cpf = Convert.ToString(reader.GetValue(1));
                empresa = Convert.ToString(reader.GetValue(2));
                pais = Convert.ToString(reader.GetValue(3));
                estado = Convert.ToString(reader.GetValue(4));
                cidade = Convert.ToString(reader.GetValue(5));
                observacao = Convert.ToString(reader.GetValue(6));
                email1 = Convert.ToString(reader.GetValue(7));
                email2 = Convert.ToString(reader.GetValue(8));
                telefone1 = Convert.ToString(reader.GetValue(9));
                telefone2 = Convert.ToString(reader.GetValue(10));
                whatsapp = Convert.ToString(reader.GetValue(11));

                if (i == 0)
                {
                    if (nome.ToUpper() != "NOME") { err = true; }
                    if (cpf.ToUpper() != "CPF") { err = true; }
                    if (empresa.ToUpper() != "EMPRESA") { err = true; }
                    if (pais.ToUpper() != "PAIS") { err = true; }
                    if (estado.ToUpper() != "ESTADO") { err = true; }
                    if (cidade.ToUpper() != "CIDADE") { err = true; }
                    if (observacao.ToUpper() != "OBSERVACAO") { err = true; }
                    if (email1.ToUpper() != "EMAIL1") { err = true; }
                    if (email2.ToUpper() != "EMAIL2") { err = true; }
                    if (telefone1.ToUpper() != "TELEFONE1") { err = true; }
                    if (telefone2.ToUpper() != "TELEFONE2") { err = true; }
                    if (whatsapp.ToUpper() != "WHATSAPP") { err = true; }

                    if (err)
                    {
                        line = "ERRO: Os nomes das colunas não correspondem ao modelo";
                        break;
                    }
                }
                else
                {
                    line = line + "Linha " + i + " - ";

                    // Verifica se o cpf foi preenchido
                    if ((cpf == "") || (cpf.Length != 14))
                    {
                        line = line + "NÃO IMPORTADO - CPF não informado ou em formato incorreto! " + nl;
                    }
                    else
                    {
                        // Verifica se já existe o cpf
                        if (new AlunosDB().ExisteCPF(cpf))
                        {
                            line = line + "NÃO IMPORTADO - CPF já cadastrado! " + nl;
                        }
                        else
                        {
                            // Verifica os demais obrigatórios
                            if ((nome == "") || (nome.Length > 200)) {
                                err = true;
                                line = line + "NÃO IMPORTADO - NOME não informado ou em formato incorreto! " + nl;
                            }
                            if ((email1 == "") || (email1.Length > 200)) {
                                err = true;
                                line = line + "NÃO IMPORTADO - EMAIL1 não informado ou em formato incorreto! " + nl;
                            }

                            // Grava aluno
                            if (!err)
                            {
                                // Busca pais
                                p = 0;
                                p = new PaisesDB().Buscar(pais);

                                // Busca estado
                                e = 0;
                                e = new EstadosDB().Buscar(estado);

                                // Busca cidade
                                c = 0;
                                if (e > 0)
                                {
                                    c = new CidadesDB().Buscar(e, cidade);
                                }

                                // Não encontrou a cidade
                                if (c == 0)
                                {
                                    err = true;
                                    line = line + "IMPORTADO PARCIALMENTE - PAÍS/ESTADO/CIDADE não informado(s) ou não encontrado(s) na nossa base de dados, verifique a digitação! " + nl;
                                }

                                // Busca a empresa
                                emp = 0;
                                emp = new EmpresasDB().ExisteEmpresa(empresa);
                                if (emp == 0)
                                {
                                    Empresas empresas = new Empresas();
                                    empresas.txempresa = empresa;
                                    empresas.idorganizador = org;
                                    emp = empresas.Salvar();
                                }

                                // Objeto aluno
                                Alunos aluno = new Alunos();
                                aluno.txaluno = nome;
                                aluno.txcpf = cpf;
                                aluno.txobs = observacao;
                                aluno.idorganizador = org;
                                aluno.idempresa = emp;
                                aluno.idcidade = c;
                                alu = aluno.Salvar();

                                // Grava e-mails
                                new AlunosDB().SalvarEmail(alu, email1);
                                if (email2 != "") { new AlunosDB().SalvarEmail(alu, email2); }

                                // Grava telefones
                                if (telefone1 != "") { new AlunosDB().SalvarTelefone(alu, telefone1, 0); }
                                if (telefone2 != "") { new AlunosDB().SalvarTelefone(alu, telefone2, 0); }
                                if (whatsapp != "") { new AlunosDB().SalvarTelefone(alu, whatsapp, 1); }

                                if (!err)
                                {
                                    line = line + "IMPORTADO COM SUCESSO!" + nl;
                                }
                            }
                        }
                    }

                }

                i = i + 1;
            }
            reader.Close();
            reader = null;

            return line;

            //string path_txt = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Images/importacao"), org + ".txt");
            //if (File.Exists(path_txt))
            //{
            //    File.Delete(path_txt);
            //}
            //using (FileStream fs = File.Create(path_txt))
            //{
            //    Byte[] info = new UTF8Encoding(true).GetBytes(line);
            //    fs.Write(info, 0, info.Length);
            //}
        }

        public static string ImportaEmpresa(string path)
        {
            int i = 0;
            Boolean err = false;
            string line = "";
            string nl = "\n";// System.Environment.NewLine;

            string codigo = "";
            string nome = "";
            string cnpj = "";
            string pais = "";
            string estado = "";
            string cidade = "";
            string cep = "";
            string bairro = "";
            string endereco = "";
            string numero = "";
            string complemento = "";
            string email1 = "";
            string email2 = "";
            string telefone1 = "";
            string telefone2 = "";
            string whatsapp = "";

            int p = 0;
            int e = 0;
            int c = 0;
            int emp = 0;

            HttpCookie cookie = HttpContext.Current.Request.Cookies["ciclo_usuario"];
            int org = Convert.ToInt32(cookie.Value);

            var stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);
            while (reader.Read())
            {
                codigo = Convert.ToString(reader.GetValue(0));
                nome = Convert.ToString(reader.GetValue(1));
                cnpj = Convert.ToString(reader.GetValue(2));
                pais = Convert.ToString(reader.GetValue(3));
                estado = Convert.ToString(reader.GetValue(4));
                cidade = Convert.ToString(reader.GetValue(5));
                cep = Convert.ToString(reader.GetValue(6));
                bairro = Convert.ToString(reader.GetValue(7));
                endereco = Convert.ToString(reader.GetValue(8));
                numero = Convert.ToString(reader.GetValue(9));
                complemento = Convert.ToString(reader.GetValue(10));
                email1 = Convert.ToString(reader.GetValue(11));
                email2 = Convert.ToString(reader.GetValue(12));
                telefone1 = Convert.ToString(reader.GetValue(13));
                telefone2 = Convert.ToString(reader.GetValue(14));
                whatsapp = Convert.ToString(reader.GetValue(15));

                if (i == 0)
                {
                    if (codigo.ToUpper() != "CODIGO") { err = true; }
                    if (nome.ToUpper() != "NOME") { err = true; }
                    if (cnpj.ToUpper() != "CNPJ") { err = true; }
                    if (pais.ToUpper() != "PAIS") { err = true; }
                    if (estado.ToUpper() != "ESTADO") { err = true; }
                    if (cidade.ToUpper() != "CIDADE") { err = true; }
                    if (cep.ToUpper() != "CEP") { err = true; }
                    if (bairro.ToUpper() != "BAIRRO") { err = true; }
                    if (endereco.ToUpper() != "ENDERECO") { err = true; }
                    if (numero.ToUpper() != "NUMERO") { err = true; }
                    if (complemento.ToUpper() != "COMPLEMENTO") { err = true; }
                    if (email1.ToUpper() != "EMAIL1") { err = true; }
                    if (email2.ToUpper() != "EMAIL2") { err = true; }
                    if (telefone1.ToUpper() != "TELEFONE1") { err = true; }
                    if (telefone2.ToUpper() != "TELEFONE2") { err = true; }
                    if (whatsapp.ToUpper() != "WHATSAPP") { err = true; }

                    if (err)
                    {
                        line = "ERRO: Os nomes das colunas não correspondem ao modelo";
                        break;
                    }
                }
                else
                {
                    line = line + "Linha " + i + " - ";

                    // Verifica se o cnpj foi preenchido
                    if ((cnpj == "") || (cnpj.Length != 18))
                    {
                        line = line + "NÃO IMPORTADO - CNPJ não informado ou em formato incorreto! " + nl;
                    }
                    else
                    {
                        // Verifica se já existe o cnpj
                        if (new EmpresasDB().ExisteCNPJ(cnpj))
                        {
                            line = line + "NÃO IMPORTADO - CNPJ já cadastrado! " + nl;
                        }
                        else
                        {
                            // Verifica os demais obrigatórios
                            if ((nome == "") || (nome.Length > 200))
                            {
                                err = true;
                                line = line + "NÃO IMPORTADO - NOME não informado ou em formato incorreto! " + nl;
                            }
                            if ((email1 == "") || (email1.Length > 200))
                            {
                                err = true;
                                line = line + "NÃO IMPORTADO - EMAIL1 não informado ou em formato incorreto! " + nl;
                            }

                            // Grava empresa
                            if (!err)
                            {
                                // Busca pais
                                p = 0;
                                p = new PaisesDB().Buscar(pais);

                                // Busca estado
                                e = 0;
                                e = new EstadosDB().Buscar(estado);

                                // Busca cidade
                                c = 0;
                                if (e > 0)
                                {
                                    c = new CidadesDB().Buscar(e, cidade);
                                }

                                // Não encontrou a cidade
                                if (c == 0)
                                {
                                    err = true;
                                    line = line + "IMPORTADO PARCIALMENTE - PAÍS/ESTADO/CIDADE não informado(s) ou não encontrado(s) na nossa base de dados, verifique a digitação! " + nl;
                                }

                                // Objeto emprsa
                                Empresas empresa = new Empresas();
                                empresa.idorganizador = org;
                                empresa.txempresa = nome;
                                empresa.txcnpj = cnpj;
                                empresa.txcodigo = codigo;
                                empresa.nrcep = cep;
                                empresa.idcidade = c;
                                empresa.txnumero = numero;
                                empresa.txlogradouro = endereco;
                                empresa.txcomplemento = complemento;
                                empresa.txbairro = bairro;
                                emp = empresa.Salvar();

                                // Grava e-mails
                                new EmpresasDB().SalvarEmail(emp, email1);
                                if (email2 != "") { new EmpresasDB().SalvarEmail(emp, email2); }

                                // Grava telefones
                                if (telefone1 != "") { new EmpresasDB().SalvarTelefone(emp, telefone1, 0); }
                                if (telefone2 != "") { new EmpresasDB().SalvarTelefone(emp, telefone2, 0); }
                                if (whatsapp != "") { new EmpresasDB().SalvarTelefone(emp, whatsapp, 1); }

                                if (!err)
                                {
                                    line = line + "IMPORTADO COM SUCESSO!" + nl;
                                }
                            }
                        }
                    }

                }

                i = i + 1;
            }
            reader.Close();
            reader = null;

            return line;

            //string path_txt = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Images/importacao"), org + ".txt");
            //if (File.Exists(path_txt))
            //{
            //    File.Delete(path_txt);
            //}
            //using (FileStream fs = File.Create(path_txt))
            //{
            //    Byte[] info = new UTF8Encoding(true).GetBytes(line);
            //    fs.Write(info, 0, info.Length);
            //}
        }
    }
}
