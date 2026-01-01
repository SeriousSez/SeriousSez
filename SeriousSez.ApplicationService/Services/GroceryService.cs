using AutoMapper;
using Microsoft.Extensions.Logging;
using SeriousSez.ApplicationService.Interfaces;
using SeriousSez.Domain.Entities.Grocery;
using SeriousSez.Domain.Responses;
using SeriousSez.Infrastructure.Interfaces;
using SeriousSez.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriousSez.ApplicationService.Services
{
    public class GroceryService : IGroceryService
    {
        private readonly ILogger<GroceryService> _logger;
        private readonly IGroceryRepository _groceryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GroceryService(ILogger<GroceryService> logger, IGroceryRepository groceryRepository, IUserRepository userRepository, IMapper mapper)
        {
            _logger = logger;
            _groceryRepository = groceryRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GroceryListResponse> GetGroceryList(string userId)
        {
            var groceryList = await _groceryRepository.GetByUserId(userId);

            return _mapper.Map<GroceryListResponse>(groceryList);
        }

        public async Task Create(Guid userId)
        {
            var user = await _userRepository.GetByUserId(userId);
            await _groceryRepository.Create(new GroceryList { User = user, Ingredients = new List<GroceryIngredient>() });

            _logger.LogTrace("GroceryList created!");
        }
    }
}
