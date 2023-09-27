using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CamadaDeDados.Dados;

namespace CamadaDeDados
{
    internal class RepositorioEmMemoria
    {
        public class RepositorioEmMemoria<T> : IRepositorio<T>
        {
            private List<T> _entidades = new List<T>();

            public void Incluir(T entidade)
            {
                _entidades.Add(entidade);
            }

            public void Alterar(T entidade)
            {
                // Implementar a lógica de alteração aqui
            }

            public void Excluir(Guid id)
            {
                _entidades.RemoveAll(e => (e as Paciente).Id == id);
            }

            public T ObterPorId(Guid id)
            {
                return _entidades.FirstOrDefault(e => (e as Paciente).Id == id);
            }

            public List<T> Pesquisar(string termo)
            {
                // Implementar a lógica de pesquisa aqui
                return _entidades.Where(e => (e as Paciente).Nome.Contains(termo)).ToList();
            }
        }

    }
}
