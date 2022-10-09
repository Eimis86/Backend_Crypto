using AutoMapper;
using Cripto_Api.Contract;
using Cripto_Api.Data;
using Cripto_Api.Models.Coin;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace Cripto_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "user, admin")]
    public class CoinController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICoinContract _coinContract;

        public CoinController(IMapper mapper, ICoinContract coinContract)
        {
            _mapper = mapper;
            _coinContract = coinContract;
        }

        [HttpGet("GetCoins")]
        public async Task<ActionResult<IEnumerable<GetCoinDto>>> getCoins(int walletId)
        {
            var getAllCoins = await _coinContract.FindAllCoinsInWallet(walletId);

            if( getAllCoins == null )
            {
                return NotFound();
            }

            var mapp = _mapper.Map<List<GetCoinDto>>(getAllCoins);

            return Ok(mapp);

        }

        [HttpPut("Buy")]
        //[Authorized(Roles="user, admin")]
        public async Task<ActionResult> AddCoin(CreateCoinDto createCoinDto)
        {
            var getCoin = await _coinContract.GetCoinId(createCoinDto);

            if (getCoin == null)
            {
                var coinAdd = _mapper.Map<Coin>(createCoinDto);
                await _coinContract.InsertCoin(coinAdd);

                return NoContent();
            }

            var coinModified = await _coinContract.AddPrice(createCoinDto);

            _mapper.Map(coinModified, createCoinDto);

            await _coinContract.UpdateAsync(coinModified);

            return NoContent();

        }

        [HttpPut("Sell")]
        public async Task<ActionResult> RemoveCoin(SellCoinDto sellCoinDto)
        {
            var removeCoin = await _coinContract.RemoveCoin(sellCoinDto);

            if (removeCoin == null)
            {
                //delete completely
                return NotFound("doesnt exist");
            }

            //sell some part of it
            _mapper.Map(removeCoin, sellCoinDto);
            await _coinContract.UpdateAsync(removeCoin);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoinWhenZero(int id)
        {
            var user = await _coinContract.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await _coinContract.DeleteAsync(id);

            return NoContent();
        }

    }
}
