using System.Linq;
using System.Web.Mvc;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;

namespace GameStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IGameRepository repository;

        public AdminController(IGameRepository repo)
        {
            repository = repo;
        }

        public ViewResult Edit(int gameId)
        {
            Game game = repository.Games
                .FirstOrDefault(g => g.GameId == gameId);
            return View(game);
        }

        public ViewResult Index()
        {
            return View(repository.Games);
        }

        [HttpPost]
        public ActionResult Edit(Game game)
        {
            if (ModelState.IsValid)
            {
                repository.SaveGame(game);
                if (game.GameId == 0)
                {
                    TempData["message"] = string.Format("Добавлен товар \"{0}\".", game.Name);
                }
                else
                {
                    TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены.", game.Name);
                }

                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(game);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Game());
        }

        [HttpPost]
        public ActionResult Delete(int gameId)
        {
            Game deletedGame = repository.DeleteGame(gameId);
            if (deletedGame != null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" был удалена.",
                    deletedGame.Name);
            }
            return RedirectToAction("Index");
        }


    }
}