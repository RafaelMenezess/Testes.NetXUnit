using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System.Numerics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using Xunit;

namespace Alura.Estacionamento.Testes
{
    public class PatioTestes
    {
        [Fact]
        public void ValidaFaturamento()
        {
            //Arrange
            Patio estacionamento = new Patio();
            Veiculo veiculo = new Veiculo();
            veiculo.Proprietario = "Rafael";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = "Prata";
            veiculo.Modelo = "Celta";
            veiculo.Placa = "AAA-9999";

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
        public void ValidaFaturamentoComVariosVeiculos(string proprietario,
            string placa, string cor, string modelo)
        {
            //Arrange
            Patio estacionamento = new Patio();
            Veiculo veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Placa = placa;

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("José", "BBB-1111", "preto", "Gol")]
        public void LocalizaVeiculoNoPatio(string proprietario,
            string placa, string cor, string modelo)
        {
            //Arrange
            Patio estacionamento = new Patio();
            Veiculo veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Placa = placa;

            estacionamento.RegistrarEntradaVeiculo(veiculo);

            //Act
            var consultado = estacionamento.PesquisaVeiculo(placa);

            //Assert
            Assert.Equal(placa, consultado.Placa);
        }

        [Fact]
        public void AlterarDadosVeiculos()
        {
            //Arrange
            Patio estacionamento = new Patio();
            Veiculo veiculo = new Veiculo();
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
    }
}
