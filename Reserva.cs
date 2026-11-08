
using System;

namespace AppHotel
{
    public class Reserva
    {
        public string Nome { get; set; }
        public string Quarto { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public int Hospedes { get; set; }

        public string Periodo
        {
            get
            {
                return $"{DataEntrada:dd/MM/yyyy} até {DataSaida:dd/MM/yyyy}";
            }
        }

        public Reserva(string nome, string quarto,
            DateTime dataEntrada, DateTime dataSaida, int hospedes)
        {
            Nome = nome;
            Quarto = quarto;
            DataEntrada = dataEntrada;
            DataSaida = dataSaida;
            Hospedes = hospedes;
        }
    }
}
