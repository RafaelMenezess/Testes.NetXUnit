using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class PatioTestes : IDisposable
    {
        private Veiculo veiculo;
        private Patio estacionamento;
        private Operador operador;
        public ITestOutputHelper saidaConsoleTeste;

        public PatioTestes(ITestOutputHelper _saidaConsoleTeste)
        {
            saidaConsoleTeste = _saidaConsoleTeste;
            saidaConsoleTeste.WriteLine("Construtor invocado.");
            veiculo = new Veiculo();
            estacionamento = new Patio();

            operador = new Operador();
            operador.Nome = "Adão";
        }

        [Fact]
        public void ValidaFaturamentoDoEstaciomentoComVeiculo()
        {
            //Arrange
            //Patio estacionamento = new Patio();
            //Veiculo veiculo = new Veiculo();
            veiculo.Proprietario = "Rafael";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = "Prata";
            veiculo.Modelo = "Celta";
            veiculo.Placa = "AAA-9999";

            estacionamento.OperadorPatio = operador;
            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);

        }

        [Theory]
        [InlineData("José", "BBB-1111", "preto", "Gol")]
        [InlineData("Maria", "CCC-2222", "branco", "Palio")]
        [InlineData("Carlos", "DDD-3333", "cinza", "Vectra")]
        public void ValidaFaturamentoDoEstacionamentoComVariosVeiculos(string proprietario,
            string placa, string cor, string modelo)
        {
            //Arrange
            //Patio estacionamento = new Patio();
            //Veiculo veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Placa = placa;

            estacionamento.OperadorPatio = operador;

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("José", "BBB-1111", "preto", "Gol")]
        public void LocalizaVeiculoNoPatioComBaseIdTicket(string proprietario,
            string placa, string cor, string modelo)
        {
            //Arrange
            //Patio estacionamento = new Patio();
            //Veiculo veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Placa = placa;

            estacionamento.OperadorPatio = operador;

            estacionamento.RegistrarEntradaVeiculo(veiculo);

            //Act
            var consultado = estacionamento.PesquisaVeiculo(veiculo.IdTicket);

            //Assert
            Assert.Contains("### Ticket Estacionamento ###", consultado.Ticket);
        }

        [Fact]
        public void AlterarDadosDoProprioVeiculo()
        {
            //Arrange
            //Patio estacionamento = new Patio();
            //Veiculo veiculo = new Veiculo();
            estacionamento.OperadorPatio = operador;

            veiculo.Proprietario = "José Silva";
            veiculo.Cor = "Cinza";
            veiculo.Modelo = "Omega";
            veiculo.Placa = "ABC-1234";
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            Veiculo veiculoAlterado = new Veiculo();
            veiculoAlterado.Proprietario = "José Silva";
            veiculoAlterado.Cor = "Preto";//Alterado
            veiculoAlterado.Modelo = "Omega";
            veiculoAlterado.Placa = "ABC-1234";


            //Act
            var alterado = estacionamento.AlterarDadosVeiculo(veiculoAlterado);

            //Assert
            Assert.Equal(alterado.Cor, veiculoAlterado.Cor);
        }

        public void Dispose()
        {
            saidaConsoleTeste.WriteLine("Dispose invocado.");

        }
    }
}
