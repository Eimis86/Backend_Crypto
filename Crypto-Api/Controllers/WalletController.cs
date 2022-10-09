using AutoMapper;
using Cripto_Api.Contract;
using Cripto_Api.Data;
using Cripto_Api.Models.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Web;

namespace Cripto_Api.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,user")]
    public class WalletController : ControllerBase
    {

        public readonly IWalletContract _walletContract;
        public readonly IMapper _mapper;

        public WalletController (IMapper mapper, IWalletContract walletContract)
        {
            _walletContract = walletContract;
            _mapper = mapper;
        }

        [HttpGet("GetWallets")]
        public async Task<ActionResult<IEnumerable<GetWalletsDto>>> GetWalletById(int userid)
        {
            //Console.WriteLine(userid);
            var wallet = await _walletContract.GetWallets(userid);

            if(wallet == null)
            {
                return NotFound();
            }

            var mapp = _mapper.Map<List<GetWalletsDto>>(wallet);

            return Ok(mapp);
        }

        [HttpPost]
        //[Authorize(Roles = "admin,user")]
        public async Task<ActionResult<Wallet>> PostWallet(CreateWalletDto createWalletDto)
        {
            var wallet = _mapper.Map<Wallet>(createWalletDto);

            var count = await _walletContract.GetCount(createWalletDto.UserId);
                if(count==false){
                    await _walletContract.AddAsync(wallet); 

                    return CreatedAtAction("GetWalletById", new { id = wallet.Id }, wallet);
                }
            return Ok(null);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin,user")]
        public async Task<IActionResult> DeleteWallet(int id)
        {
            var wallet = await _walletContract.GetAsync(id);

            if(wallet == null)
            {
                return NotFound();
            }

            await _walletContract.DeleteAsync(id);

            return NoContent();
        }

    }
}
