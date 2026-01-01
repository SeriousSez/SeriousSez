using AutoMapper;
using SeriousSez.ApplicationService.Interfaces;
using SeriousSez.Domain.Entities;
using SeriousSez.Domain.Entities.Fridge;
using SeriousSez.Domain.Models;
using SeriousSez.Domain.Responses;
using SeriousSez.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriousSez.ApplicationService.Services
{
    public class FridgeService : IFridgeService
    {
        private readonly IMapper _mapper;
        private readonly IFridgeRepository _fridgeRepository;
        private readonly IFridgeGroceryRepository _fridgeIngredientRepository;

        public FridgeService(IMapper mapper, IFridgeRepository fridgeRepository, IFridgeGroceryRepository fridgeIngredientRepository)
        {
            _mapper = mapper;
            _fridgeRepository = fridgeRepository;
            _fridgeIngredientRepository = fridgeIngredientRepository;
        }

        public async Task<ICollection<FridgeResponse>> Get(Guid userId)
        {
            var fridges = await _fridgeRepository.GetAllByUserId(userId);

            return _mapper.Map<ICollection<FridgeResponse>>(fridges);
        }

        public async Task Add(FridgeModel model)
        {
            var entity = _mapper.Map<Domain.Entities.Fridge.Fridge>(model);

            await _fridgeRepository.Create(entity);
        }

        public async Task AddHomeFridge(User user)
        {
            var fridge = new Fridge
            {
                Id = Guid.NewGuid(),
                Name = "Home",
                Purchased = DateTime.Now,
                Groceries = new List<FridgeGrocery>(),
                User = user
            };

            await _fridgeRepository.Create(fridge);
        }

        public async Task Retire(Guid fridgeId)
        {
            var fridge = await _fridgeRepository.Get(fridgeId);
            fridge.Retired = DateTime.Now;

            await _fridgeRepository.Update(fridge);
        }

        public async Task UnRetire(Guid fridgeId)
        {
            var fridge = await _fridgeRepository.Get(fridgeId);
            fridge.Retired = DateTime.MinValue;

            await _fridgeRepository.Update(fridge);
        }

        public async Task<ICollection<FridgeGroceryResponse>> GetGroceries(Guid fridgeId)
        {
            var fridges = await _fridgeIngredientRepository.GetByFridgeId(fridgeId);

            return _mapper.Map<ICollection<FridgeGroceryResponse>>(fridges);
        }

        public async Task AddGrocery(FridgeGroceryModel model)
        {
            var entity = _mapper.Map<Domain.Entities.Fridge.FridgeGrocery>(model);
            entity.FridgeId = model.FridgeId;

            await _fridgeIngredientRepository.Create(entity);
        }

        public async Task RemoveGrocery(Guid id)
        {
            await _fridgeIngredientRepository.Delete(id);
        }
    }
}
