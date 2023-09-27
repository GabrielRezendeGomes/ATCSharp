using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CamadaDeDados.Dados;

namespace CamadaDeDados
{
    internal class RepositorioEmArquivo
    {
        public class RepositorioEmArquivo<T> : IRepositorio<T>
        {
            private string _nomeArquivo;

            public RepositorioEmArquivo(string nomeArquivo)
            {
                _nomeArquivo = nomeArquivo;
            }

            public void Incluir(T entidade)
            {
                List<T> entidades = ObterEntidadesDoArquivo();
                entidades.Add(entidade);
                SalvarEntidadesNoArquivo(entidades);
            }

            public void Alterar(T entidade)
            {
                // Implementar a lógica de alteração em arquivo JSON
            }

            public void Excluir(Guid id)
            {
                List<T> entidades = ObterEntidadesDoArquivo();
                entidades.RemoveAll(e => (e as Paciente).Id == id);
                SalvarEntidadesNoArquivo(entidades);
            }

            public T ObterPorId(Guid id)
            {
                List<T> entidades = ObterEntidadesDoArquivo();
                return entidades.Find(e => (e as Paciente).Id == id);
            }

            public List<T> Pesquisar(string termo)
            {
                List<T> entidades = ObterEntidadesDoArquivo();
                // Implementar a lógica de pesquisa em arquivo JSON
                return null;
            }

            private List<T> ObterEntidadesDoArquivo()
            {
                if (File.Exists(_nomeArquivo))
                {
                    string json = File.ReadAllText(_nomeArquivo);
                    return JsonConvert.DeserializeObject<List<T>>(json);
                }
                return new List<T>();
            }

            private void SalvarEntidadesNoArquivo(List<T> entidades)
            {
                string json = JsonConvert.SerializeObject(entidades);
                File.WriteAllText(_nomeArquivo, json);
            }
        }
    }
}
