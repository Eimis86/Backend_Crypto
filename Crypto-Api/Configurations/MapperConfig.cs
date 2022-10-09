using AutoMapper;
using Cripto_Api.Data;
using Cripto_Api.Models.Coin;
using Cripto_Api.Models.User;
using Cripto_Api.Models.Wallet;

namespace Cripto_Api.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, UserResponseTokenDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap(); 
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, LoginUserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();

            CreateMap<Wallet, CreateWalletDto>().ReverseMap();
            CreateMap<Wallet, WalletDto>().ReverseMap();
            CreateMap<Wallet, GetWalletsDto>().ReverseMap();

            CreateMap<Coin, CoinDto>().ReverseMap();
            CreateMap<Coin, CreateCoinDto>().ReverseMap();
            CreateMap<Coin, GetCoinDto>().ReverseMap();
        }

    }
}
