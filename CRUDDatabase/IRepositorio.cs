using System;
using System.Collections.Generic;

namespace CRUDDatabase
{
    public interface IRepositorio<T>
    {
        void Incluir(T entidade);
        void Alterar(T entidade);
        void Excluir(Guid id);
        T ObterPorId(Guid id);
        List<T> Pesquisar(string termo);
    }
}
