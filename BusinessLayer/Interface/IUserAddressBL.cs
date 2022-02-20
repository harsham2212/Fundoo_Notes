using CommonLayer.UserAddress;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUserAddressBL
    {
        public Task AddUserAddress(UserAddressPostModel userAddress, int userId);
        public Task<List<UserAddress>> GetUserAddresses(int userId);
        public Task UpdateUserAddress(UserAddressPostModel userAddress, int userId, int AddressId);
        public Task DeleteAddress(int AddressId);
    }
}
