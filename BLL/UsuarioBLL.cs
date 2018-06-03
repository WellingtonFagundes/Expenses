using DAL;
using DAL.Consulta;
using MLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace BLL
{
    public class UsuarioBLL : CriptografiaBLL
    {

        public Contexto db;

        public UsuarioBLL()
        {
            db = new Contexto();
        }

        public bool Login(Usuario usuario)
        {
            //JObject obj = new JObject();
            bool status = false;

            UsuarioDAL usuDAL = new UsuarioDAL(db);

            //Criptografar senha
            MLL.MD5Crypt cript = new MD5Crypt();
            string senhacript;
            senhacript = cript.Criptografar(usuario.Email + ";" + usuario.Senha);


            var v = db.Usuario.Where(u => u.Email.Equals(usuario.Email) && u.Senha.Equals(senhacript)).FirstOrDefault();

            if (v != null)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            return status;
        }

        public JObject InsereUsuario(MLL.Usuario usu)
        {
            JObject obj = new JObject();

            UsuarioDAL usuDAL = new UsuarioDAL(db);
            usu.Email = usu.Email.ToLower(); 

            if (usuDAL.ObterPorEmail(usu.Email) != null)
            {
                obj.Add(new JProperty("erro", "Email já existe na base"));
                return obj;
            }

            //Criptografar Senha
            MD5Crypt cript = new MD5Crypt();
            usu.Senha = cript.Criptografar(usu.Email + ";" + usu.Senha);

            if (usuDAL.Adicionar(usu))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", usuDAL.erro));
            }

            return obj;
        }

        public JObject CadastrarUsuario(HttpPostedFileBase arquivo,MLL.Usuario usu,FormCollection form,string caminho,string caminhoRelativo)
        {
            JObject obj = new JObject();

            UsuarioDAL usuDAL = new UsuarioDAL(db);
            usu.Email = usu.Email.ToLower();

            if (usuDAL.ObterPorEmail(usu.Email) != null)
            {
                obj.Add(new JProperty("erro", "Email já existe na base"));
                return obj;
            }

            //Caminho com o endereço do Web Server para exibir a foto imagem
            var pathSalvarNaBase = Path.Combine("http:\\NOTE\\EXPENSES" + caminhoRelativo);
            //Caminho mapeado no servidor fisicamente
            arquivo.SaveAs(caminho);

            usu.Path_Image = pathSalvarNaBase;

            if (form["chkAdministrador"] == "on")
            {
                usu.Administrador = true;
            }
            else if (form["chkAdministrador"] == null)
            {
                usu.Administrador = false;
            }


            //Criptografar Senha
            MD5Crypt cript = new MD5Crypt();
            usu.Senha = cript.Criptografar(usu.Email + ";" + usu.Senha);

            if (usuDAL.Adicionar(usu))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", usuDAL.erro));
            }

            return obj;
        }
        public Usuario ObtemDadosUsuarioLogado(string email)
        {
            UsuarioDAL usuDAL = new UsuarioDAL(db);

            Usuario user = usuDAL.ObterDadosUsuarioLogado(email);

            return user;
        }

        public List<MLL.Usuario> UsuariosCadastrados()
        {
            UsuarioDAL usuDAL = new UsuarioDAL(db);

            List<MLL.Usuario> listaUsu = usuDAL.Tudo()
                                               .Where(u => u.Excluido == null)    
                                               .OrderBy(u => u.Nome_Usuario).ToList();

            foreach(MLL.Usuario usu in listaUsu)
            {
                usu.idcriptografado = CriptografaID(usu.Codigo_Usuario);
            }

            return listaUsu;
        }

        public string ExcluirUsuario(MLL.Usuario usu)
        {
            string result = "";

            UsuarioDAL usuDAL = new UsuarioDAL(db);

            usu.Excluido = DateTime.Now;

            if(usuDAL.Editar(usu) == true)
            {
                result = "ok";
            }
            else
            {
                result = "Erro - " + usuDAL.erro;
            }

            return result;
        }

        public MLL.Usuario ObterPorID(int codigo)
        {
            UsuarioDAL usuDAL = new UsuarioDAL(db);

            MLL.Usuario usu = usuDAL.obtemPorId(codigo);

            return usu;
        }


        public JObject EditarUsuario(MLL.Usuario usu)
        {
            JObject obj = new JObject();
            UsuarioDAL usuDAL = new UsuarioDAL(db);

            if (usuDAL.Editar(usu))
            {
                obj.Add(new JProperty("ok", "ok"));
            }
            else
            {
                obj.Add(new JProperty("erro", usuDAL.erro));
            }

            return obj;
        }
    }
}
