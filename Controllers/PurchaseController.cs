using GamesStoreWebApi.Exceptions;
using GamesStoreWebApi.Models.Entities;
using GamesStoreWebApi.Models.Persistence.Abstractions;
using GamesStoreWebApi.Models.Persistence.Implementations;
using GamesStoreWebApi.Models.ViewModels.FromView;
using GamesStoreWebApi.Models.ViewModels.ToView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace GamesStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private IUnitOfWork UnitOfWork { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }
        public PurchaseController(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager) 
        {
            UnitOfWork = unitOfWork;
            _userManager = userManager;
        }
        
        [HttpGet("Get"), Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public async Task<IActionResult> Get(int pageSize, int pageNumber = 1) 
        {
            //проверяем, что такой пользователь существует
            var user = await _userManager.FindByEmailAsync(this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value) 
                ?? throw new ItemNotFoundException("Invalud user id.");
            //получаем информацию о количестве покупок пользователя и формируем информацию о пагинации
            var count = await (from purchase in UnitOfWork.PurchaseRepository.Get() where purchase.Purchaser.Id == user.Id select purchase).CountAsync();
            var pageInfo = new PageInfoViewModel(count, pageSize, pageNumber);
            //получаем постранично покупки
            var purchs = await 
            (
                from purchase in UnitOfWork.PurchaseRepository.Get()
                where purchase.Purchaser.Id == user.Id
                orderby purchase.PurchaseTime descending
                select purchase
            )
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            var purchaseViewModels = new List<DetailedPurchaseViewModel>();
            //связываем информацию о покупках, ключах и играх во ViewModel
            foreach(var purch in purchs) 
            {
                //связываем ключи из покупки с играми (группируем)
                var gameKeys = (from key in purch.Keys where key.KeyPurchase != null group key by key.KeyGame);
                var games = new List<PurchaseGamePriceViewModel>();
                foreach (var gameKey in gameKeys)
                {
                    var game = await UnitOfWork.GameRepository.GetById(gameKey.Key.Id);                  
                    games.Add(PurchaseGamePriceViewModel.FromGame(game, gameKey.ToList()));
                }
                //формируем ViewModel покупки
                var purchaseViewModel = new DetailedPurchaseViewModel(purch.Id, games, purch.PurchaseTime, purch.BankCard);
                //добавлём ViewModel в список покупок
                purchaseViewModels.Add(purchaseViewModel);
            }
            //отправляем ответ
            return Ok(new GenericPageViewModel<DetailedPurchaseViewModel>(purchaseViewModels, pageInfo));

        }
        
        [HttpGet("GetById"), Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public async Task<IActionResult> GetById(Guid id)
        {
            //проверяем, что такой пользователь существует
            var user = await _userManager.FindByEmailAsync(this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value)
               ?? throw new ItemNotFoundException("Invalud user id.");
            //проверяем валидность id и получаем покупку по нему
            var purchase = await UnitOfWork.PurchaseRepository.GetById(id);
            //связываем ключи из покупки с играми (группируем)
            var gameKeys = (from key in purchase.Keys where key.KeyPurchase != null group key by key.KeyGame);

            var games = new List<PurchaseGamePriceViewModel>();
            //достаём подробную информацию об играх
            foreach (var gameKey in gameKeys)
            {
                var game = await UnitOfWork.GameRepository.GetById(gameKey.Key.Id);
                games.Add(PurchaseGamePriceViewModel.FromGame(game, gameKey.ToList()));
            }
            //формируем ViewModel
            var purchaseViewModel = new DetailedPurchaseViewModel(purchase.Id, games, purchase.PurchaseTime, purchase.BankCard);
            //отправляем ответ
            return Ok(purchaseViewModel);
        }
        
        [HttpPost("Create"), Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public async Task<IActionResult> Create(CreatePurchaseViewModel createPurchase)
        {
            //проверка на то, что email указанный в claim валиден
            var user = await _userManager.FindByEmailAsync(this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value) 
                ?? throw new ItemNotFoundException("Invalud user id.");

            //создание объекта покупки
            var id = new Guid();
            var purchase = new Purchase
            {
                Id = id,
                BankCard = createPurchase.Bankcard,
                PurchaseTime = DateTime.UtcNow,
                Purchaser = user,
                Keys = new List<Key>()
            };

            //потом передастся во ViewModel
            var games = new List<PurchaseGamePriceViewModel>();

            foreach (var gameGuid in createPurchase.GameKeyCountDict.Keys)
            {
                //получение связанной с покупкой игры
                var game = await UnitOfWork.GameRepository.GetById(gameGuid);

                //проверка на то, что свободных ключей в продаже хватит
                var keyCount = (from key in game.Keys where key.KeyPurchase is not null select key).Count();
                if (keyCount > createPurchase.GameKeyCountDict[gameGuid])
                    throw new WebApiException("Not enough keys to sell.");

                //получение и связывание ключей с покупкой
                var keys = (from key in game.Keys where key.KeyPurchase == null && key.KeyGame.Id == game.Id select key)
                    .Take(createPurchase.GameKeyCountDict[gameGuid]).ToList();
                foreach (var key in keys)
                    key.KeyPurchase = purchase;
                purchase.Keys.AddRange(keys);

                //потом передастся во ViewModel
                games.Add(PurchaseGamePriceViewModel.FromGame(game, keys)); 
            }

            //сохранение в БД
            UnitOfWork.PurchaseRepository.Create(purchase);
            await UnitOfWork.Save();

            //отправка ответа
            var purchaseViewModel = new DetailedPurchaseViewModel(
                Id: purchase.Id,
                Games: games,
                PurchaseTime: purchase.PurchaseTime,
                BankCard: purchase.BankCard
            );
            return Ok(purchaseViewModel);
        }

        
        
        [HttpPut("Update"), Authorize(AuthenticationSchemes = "Bearer", Roles = "Root")]
        public async Task<IActionResult> Update(UpdatePurchaseViewModel updatePurchaseViewModel)
        {
            //проверяем, что такой пользователь существует
            var user = await _userManager.FindByEmailAsync(this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value)
               ?? throw new ItemNotFoundException("Invalud user id.");
            //проверяем валидность id и получаем покупку по нему
            var purchase = await UnitOfWork.PurchaseRepository.GetById(updatePurchaseViewModel.Id);
            //связываем ключи из покупки с играми (группируем)
            var gameKeys = (from key in purchase.Keys where key.KeyPurchase != null group key by key.KeyGame);

            var games = new List<PurchaseGamePriceViewModel>();
            //достаём подробную информацию об играх
            foreach (var gameKey in gameKeys)
            {
                var game = await UnitOfWork.GameRepository.GetById(gameKey.Key.Id);
                games.Add(PurchaseGamePriceViewModel.FromGame(game, gameKey.ToList()));
            }
            purchase.BankCard = updatePurchaseViewModel.Bankcard;
            purchase.PurchaseTime = updatePurchaseViewModel.PurchaseTime;
            UnitOfWork.PurchaseRepository.Update(purchase);
            await UnitOfWork.Save();
            //формируем ViewModel
            var purchaseViewModel = new DetailedPurchaseViewModel(purchase.Id, games, purchase.PurchaseTime, purchase.BankCard);
            return Ok(purchaseViewModel);
        }
        [HttpDelete("Delete"), Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //проверяем, что такой пользователь существует
            var user = await _userManager.FindByEmailAsync(this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value)
               ?? throw new ItemNotFoundException("Invalud user id.");
            //проверяем валидность id и получаем покупку по нему
            var purchase = await UnitOfWork.PurchaseRepository.GetById(id);
            //связываем ключи из покупки с играми (группируем)
            var gameKeys = (from key in purchase.Keys where key.KeyPurchase != null group key by key.KeyGame);

            var games = new List<PurchaseGamePriceViewModel>();
            //достаём подробную информацию об играх
            foreach (var gameKey in gameKeys)
            {
                var game = await UnitOfWork.GameRepository.GetById(gameKey.Key.Id);
                games.Add(PurchaseGamePriceViewModel.FromGame(game, gameKey.ToList()));
            }
            //формируем ViewModel
            var purchaseViewModel = new DetailedPurchaseViewModel(purchase.Id, games, purchase.PurchaseTime, purchase.BankCard);
            //удаляем из репозитория
            UnitOfWork.PurchaseRepository.Delete(purchase);
            await UnitOfWork.Save();
            //отправляем ответ
            return Ok(purchaseViewModel);
        }
    }
}