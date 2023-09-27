
using System;
using System.Collections.Generic;
using CRUDBusiness;
using CRUDDatabase;

namespace CRUDConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IRepositorio<Paciente> repositorio = null;

            bool sair = false;
            while (!sair)
            {
                Console.WriteLine("\nEscolha uma opção:");
                Console.WriteLine("1. Usar RepositorioEmArquivo");
                Console.WriteLine("2. Usar RepositorioEmMemoria");
                Console.WriteLine("3. Sair");

                string escolhaRepositorio = Console.ReadLine();

                switch (escolhaRepositorio)
                {
                    case "1":
                        repositorio = new RepositorioEmArquivo<Paciente>("pacientes.json");
                        break;
                    case "2":
                        repositorio = new RepositorioEmMemoria<Paciente>();
                        break;
                    case "3":
                        sair = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                if (repositorio != null)
                {
                    PacienteManager pacienteManager = new PacienteManager(repositorio);
                    List<Paciente> ultimosPacientes = pacienteManager.PesquisarPacientes("");

                    if (ultimosPacientes != null)
                    {
                        if (ultimosPacientes.Count > 0)
                        {
                            Console.WriteLine("Últimos 5 pacientes cadastrados:");
                            int count = 0;
                            foreach (var paciente in ultimosPacientes)
                            {
                                Console.WriteLine($"{++count}. Nome: {paciente.Nome}, Data de Nascimento: {paciente.DataNascimento.ToShortDateString()} id:{paciente.Id}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Não existem pacientes cadastrados.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ocorreu um erro ao recuperar a lista de pacientes.");
                    }

                    bool sair2 = false;
                    while (!sair2)
                    {
                        Console.WriteLine("\nEscolha uma opção:");
                        Console.WriteLine("1. Incluir Paciente");
                        Console.WriteLine("2. Alterar Paciente");
                        Console.WriteLine("3. Excluir Paciente");
                        Console.WriteLine("4. Pesquisar Paciente");
                        Console.WriteLine("5. Sair");

                        string escolha = Console.ReadLine();

                        switch (escolha)
                        {
                            case "1":
                                IncluirPaciente(pacienteManager);
                                break;
                            case "2":
                                AlterarPaciente(pacienteManager);
                                break;
                            case "3":
                                ExcluirPaciente(pacienteManager);
                                break;
                            case "4":
                                PesquisarPaciente(pacienteManager);
                                break;
                            case "5":
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Opção inválida. Tente novamente.");
                                break;
                        }
                    }
                }
            }
        }

        static void IncluirPaciente(PacienteManager pacienteManager)
        {

            Guid id = Guid.NewGuid();

            Console.WriteLine("ID gerado para o paciente: " + id);
            Console.WriteLine("Digite o nome do paciente:");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite a data de nascimento do paciente (dd/mm/yyyy):");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dataNascimento))
            {
                Console.WriteLine("Digite a idade do paciente:");
                if (int.TryParse(Console.ReadLine(), out int idade))
                {
                    Console.WriteLine("O paciente está ativo? (S/N):");
                    bool ativo = Console.ReadLine().Trim().ToUpper() == "S";

                    Console.WriteLine("Digite o peso do paciente:");
                    if (double.TryParse(Console.ReadLine(), out double peso))
                    {
                        Paciente paciente = new Paciente
                        {
                            Id = id,
                            Nome = nome,
                            DataNascimento = dataNascimento,
                            Idade = idade,
                            Ativo = ativo,
                            Peso = peso
                        };

                        pacienteManager.IncluirPaciente(paciente);
                        Console.WriteLine("Paciente incluído com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Peso inválido. Tente novamente.");
                    }
                }
                else
                {
                    Console.WriteLine("Idade inválida. Tente novamente.");
                }
            }
            else
            {
                Console.WriteLine("Data de nascimento inválida. Tente novamente.");
            }
        }

        static void AlterarPaciente(PacienteManager pacienteManager)
        {
            Console.WriteLine("Digite o ID do paciente que deseja alterar:");
            if (Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                Paciente pacienteExistente = pacienteManager.ObterPacientePorId(id);
                if (pacienteExistente != null)
                {
                    Console.WriteLine($"Paciente encontrado: {pacienteExistente.Nome}");
                    Console.WriteLine("Digite o novo nome do paciente (ou pressione Enter para manter o mesmo):");
                    string novoNome = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(novoNome))
                    {
                        pacienteExistente.Nome = novoNome;
                    }

                    Console.WriteLine("Digite a nova data de nascimento do paciente (ou pressione Enter para manter a mesma):");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime novaDataNascimento))
                    {
                        pacienteExistente.DataNascimento = novaDataNascimento;
                    }

                    Console.WriteLine("Digite a nova idade do paciente (ou pressione Enter para manter a mesma):");
                    if (int.TryParse(Console.ReadLine(), out int novaIdade))
                    {
                        pacienteExistente.Idade = novaIdade;
                    }

                    Console.WriteLine("O paciente está ativo? (S/N, ou pressione Enter para manter o mesmo):");
                    string ativoInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(ativoInput))
                    {
                        pacienteExistente.Ativo = ativoInput.Trim().ToUpper() == "S";
                    }

                    Console.WriteLine("Digite o novo peso do paciente (ou pressione Enter para manter o mesmo):");
                    if (double.TryParse(Console.ReadLine(), out double novoPeso))
                    {
                        pacienteExistente.Peso = novoPeso;
                    }

                    pacienteManager.AlterarPaciente(pacienteExistente);
                    Console.WriteLine("Paciente alterado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Paciente não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido. Tente novamente.");
            }
        }

        static void ExcluirPaciente(PacienteManager pacienteManager)
        {
            Console.WriteLine("Digite o ID do paciente que deseja excluir:");
            if (Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                Paciente pacienteExistente = pacienteManager.ObterPacientePorId(id);
                if (pacienteExistente != null)
                {
                    Console.WriteLine($"Paciente encontrado: {pacienteExistente.Nome}");
                    Console.WriteLine("Tem certeza de que deseja excluir este paciente? (S/N):");
                    string confirmacao = Console.ReadLine().Trim().ToUpper();
                    if (confirmacao == "S")
                    {
                        pacienteManager.ExcluirPaciente(id);
                        Console.WriteLine("Paciente excluído com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Exclusão cancelada.");
                    }
                }
                else
                {
                    Console.WriteLine("Paciente não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido. Tente novamente.");
            }
        }

        static void PesquisarPaciente(PacienteManager pacienteManager)
        {
            Console.WriteLine("Digite o termo de pesquisa ou o ID do paciente:");
            string termo = Console.ReadLine();


            if (Guid.TryParse(termo, out Guid idPesquisa))
            {
                Paciente pacienteEncontrado = pacienteManager.ObterPacientePorId(idPesquisa);
                if (pacienteEncontrado != null)
                {
                    Console.WriteLine($"Detalhes do Paciente {pacienteEncontrado.Nome}:");
                    Console.WriteLine($"ID: {pacienteEncontrado.Id}");
                    Console.WriteLine($"Data de Nascimento: {pacienteEncontrado.DataNascimento.ToShortDateString()}");
                    Console.WriteLine($"Idade: {pacienteEncontrado.Idade}");
                    Console.WriteLine($"Ativo: {pacienteEncontrado.Ativo}");
                    Console.WriteLine($"Peso: {pacienteEncontrado.Peso}");
                    return;
                }
            }


            List<Paciente> resultados = pacienteManager.PesquisarPacientes(termo);

            if (resultados != null)
            {
                if (resultados.Count > 0)
                {
                    Console.WriteLine("Resultados da pesquisa:");
                    int count = 0;
                    foreach (var paciente in resultados)
                    {
                        Console.Write($"{++count}. Nome: {paciente.Nome}\n Data de Nascimento: {paciente.DataNascimento.ToShortDateString()}\n peso:{paciente.Peso}\n idade:{paciente.Idade}\n Id:{paciente.Id}");
                    }
                }
                else
                {
                    Console.WriteLine("Nenhum paciente correspondente encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Ocorreu um erro ao realizar a pesquisa.");
            }
        }
    }
}

    
