using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CRUDDatabase
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
            List<T> entidades = ObterEntidadesDoArquivo();

            
            if (typeof(T) == typeof(Paciente))
            {
                var pacienteExistente = entidades.Cast<Paciente>().FirstOrDefault(p => p.Id == (entidade as Paciente).Id);
                if (pacienteExistente != null)
                {
                    
                    pacienteExistente.Nome = (entidade as Paciente).Nome;
                    pacienteExistente.DataNascimento = (entidade as Paciente).DataNascimento;
                    pacienteExistente.Idade = (entidade as Paciente).Idade;
                    pacienteExistente.Ativo = (entidade as Paciente).Ativo;
                    pacienteExistente.Peso = (entidade as Paciente).Peso;

                    
                    SalvarEntidadesNoArquivo(entidades);
                }
            }

            
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

            if (!string.IsNullOrWhiteSpace(termo))
            {
                
                if (typeof(T) == typeof(Paciente))
                {
                    var pacientes = entidades.Cast<Paciente>().ToList();
                    return pacientes
                        .Where(p => p.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase))
                        .Cast<T>()
                        .ToList();
                }

                
            }

            return entidades;
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
