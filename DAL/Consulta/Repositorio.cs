using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Consulta
{
    public class Repositorio<T> : IDisposable where T : class
    {
        public string erro { get; set; }
        public IQueryable<T> query { get; set; }

        private readonly Contexto _contexto;

        public Contexto PegaContexto()
        {
            return this._contexto;
        }

        public Repositorio(Contexto contexto)
        {
            this._contexto = contexto;
        }

        public virtual bool Adicionar(T item)
        {
            try
            {
                _contexto.Set<T>().Add(item);
                _contexto.SaveChanges();

                return true;
            }catch(Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }

        public virtual IQueryable<T> PesquisarPor(Expression<Func<T,bool>> predicate)
        {
            try
            {
                query = _contexto.Set<T>().Where(predicate);

                return query;
            }catch (Exception ex)
            {
                erro = ex.Message;
                return null;
            }
        }


        public virtual bool RemoverFilhos(Expression<Func<T,bool>> predicate)
        {
            try
            {
                foreach (var m in _contexto.Set<T>().Where(predicate))
                {
                    _contexto.Set<T>().Remove(m);
                }

                _contexto.SaveChanges();

                return true;
            }catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }


        public virtual bool Remover(T item)
        {
            try
            {
                _contexto.Set<T>().Remove(item);
                _contexto.SaveChanges();

                return true;
            }catch(DbUpdateException ex)
            {
                var ent = ex.Entries.FirstOrDefault();

                if (ent.State == System.Data.Entity.EntityState.Deleted)
                {
                    UpdateException inner = (ex.InnerException as UpdateException);
                    var primeiro = inner.StateEntries.FirstOrDefault();
                    erro = "Falha ao remover a entidade pois existem registros relacionados na tabela \n " + primeiro.EntitySet.Name;

                    return false;
                }
                else
                {
                    erro = ex.Message;
                    return false;
                }
            }
        }


        public virtual bool Editar(T item)
        {
            try
            {
                _contexto.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _contexto.SaveChanges();

                return true;
            }catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }

        public virtual T obtemPorId(object id)
        {
            return _contexto.Set<T>().Find(id);
        }

        public virtual IQueryable<T> Tudo()
        {
            return _contexto.Set<T>();
        }
        
        public void Dispose()
        {
            _contexto.Dispose();
        }

        public bool Commit()
        {
            try
            {
                _contexto.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }

        public virtual bool AdicionarSemSalvar(T item)
        {
            try
            {
                _contexto.Set<T>().Add(item);

                return true;
            }catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }

        public virtual bool RemoverSemSalvar(T item)
        {
            try
            {
                _contexto.Set<T>().Remove(item);
                return true;
            }catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }

        public virtual bool EditarSemSalvar(T item)
        {
            try
            {
                _contexto.Entry(item).State = System.Data.Entity.EntityState.Modified;
                return true;
            }catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }

        public virtual bool AttachEntidade(T item)
        {
            try
            {
                _contexto.Set<T>().Attach(item);
                return true;
            }catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }

        public virtual bool DettachEntidade(T item)
        {
            try
            {
                _contexto.Entry(item).State = System.Data.Entity.EntityState.Detached;
                return true;
            }catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }

        public virtual bool RemoverRange(IList<T> lista)
        {
            try
            {
                _contexto.Set<T>().RemoveRange(lista);
                _contexto.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }
        public virtual bool RemoverRangeSemSalvar(IList<T> lista)
        {
            try
            {
                _contexto.Set<T>().RemoveRange(lista);
                return true;
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }


    }
}
