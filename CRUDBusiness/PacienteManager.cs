using System;
using System.Collections.Generic;
using CRUDDatabase;

namespace CRUDBusiness
{
    public class PacienteManager
    {
        public IRepositorio<Paciente> _repositorio;

        public PacienteManager(IRepositorio<Paciente> repositorio)
        {
            _repositorio = repositorio;
        }

        public void IncluirPaciente(Paciente paciente)
        {
            _repositorio.Incluir(paciente);
        }

        public void AlterarPaciente(Paciente paciente)
        {
            _repositorio.Alterar(paciente);
        }

        public void ExcluirPaciente(Guid id)
        {
            _repositorio.Excluir(id);
        }

        public Paciente ObterPacientePorId(Guid id)
        {
            return _repositorio.ObterPorId(id);
        }

        public List<Paciente> PesquisarPacientes(string termo)
        {
            return _repositorio.Pesquisar(termo);
        }
    }
}
