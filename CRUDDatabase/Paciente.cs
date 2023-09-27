using System;

namespace CRUDDatabase
{
    public class Paciente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; }
        public bool Ativo { get; set; }
        public double Peso { get; set; }
    }
}