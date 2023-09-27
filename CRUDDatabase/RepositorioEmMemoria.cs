namespace CRUDDatabase
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
            var existente = _entidades.FirstOrDefault(e => (e as Paciente).Id == (entidade as Paciente).Id);
            if (existente != null)
            {
                _entidades.Remove(existente);
                _entidades.Add(entidade);
            }
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
            return _entidades
                .Where(e => (e as Paciente).Nome.Contains(termo, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}