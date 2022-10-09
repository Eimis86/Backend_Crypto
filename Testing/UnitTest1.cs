using AutoMapper;
using Cripto_Api.Contract;
using Cripto_Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace Testing
{
    public class UnitTest1
    {
        [Fact]
        public async Task Coin_get_function()
        {
            //Arrange
            int walletId = 5;
            var controller = new CoinController();
            //Act
            var actionResult = await controller.getCoins(walletId);

            //Assert
            var result = actionResult.Result as OkObjectResult;
            Assert.Equal(result, );
        }

        /*public void ValidateMappingConfigurationTest()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            IMapper mapper = new Mapper(mapperConfig);

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }*/

    }
}