using Domain.Contracts;
using Domain.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {

        private readonly IDatabase _database = connection.GetDatabase();
        public Task<bool> DeleteBasketAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            throw new NotImplementedException();
        }
    }
}
