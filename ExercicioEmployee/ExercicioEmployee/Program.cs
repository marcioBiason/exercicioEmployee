using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using ExercicioEmployee.Entities;

namespace ExercicioEmployee
{
    class Program
    {
        static void Main(string[] args)
        {
            //Lendo o caminho digitado pelo usuario indicando o arquivo;
            Console.WriteLine("Enter full file path: ");

            //String onde fica armazenado o caminho digitado pelo usuario;
            string path = Console.ReadLine();

            //Lista onde será armazenada os atributos do Funcionario
            List<Employee> list = new List<Employee>();

            try
            {
                //Crinado logica para leitura de arquivos
                using (StreamReader sr = File.OpenText(path))
                {
                    //enquanto nao chego o fim do arquivo;
                    while (!sr.EndOfStream)
                    {
                        //Splitando os arquivo da planilha por ',';
                        string[] fields = sr.ReadLine().Split(',');

                        //Preenchendo o campo nome;
                        string name = fields[0];

                        //Preenchendo o campo email;
                        string email = fields[1];

                        //Preenchendo o campo salario;
                        double salario = double.Parse(fields[2], CultureInfo.InvariantCulture);

                        //Inserindo um novo funcionario na lista;
                        list.Add(new Employee(name, email, salario));
                    }
                }

                //Pedindo para o usuario digitar um valor;
                Console.Write("Enter salary: ");
                //Armazenando o valor digitado na variavel salarioDigitado;
                double salarioDigitado = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                //Pulando uma linha
                Console.WriteLine("");

                //Selecionando os email dos funcionarios que ganham mais que o valor digitado pelo usuario;
                var emails = list.Where(e => e.Salary > salarioDigitado)
                    .OrderBy(e => e.Email)
                    .Select(e => e.Email);

                //Imprimindo os email abaixo dos funcionarios que tem o salario maior que o numero didgitado pelo ususario;
                Console.WriteLine("Email of people whose salary is more than " + salarioDigitado.ToString("F2", CultureInfo.InvariantCulture));
                foreach (string email in emails)
                {
                    Console.WriteLine(email);
                }
                Console.WriteLine("");
                //Somando e imprimido a soma dos salrios dos funcionario que começam seus nomes com a letra 'M';
                var somaSalario = list.Where(e => e.Name[0] == 'M').Sum(e => e.Salary);
                Console.WriteLine("Sum of salary of people whose name starts with 'M' : " + somaSalario.ToString("F2", CultureInfo.InvariantCulture));
            }
            catch (IOException e)
            {
                //Caso ocorra alguma erro, isso será impresso na tela;
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }
        }
    }
}
