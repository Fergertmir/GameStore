using System.Linq;
using System.Web.Mvc;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System.Web;
using System;
using System.IO;
using static GameStore.WebUI.Models.ImagesDirectoryEnumModel;

namespace GameStore.WebUI.Controllers
{
    [Authorize]
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
            //SaveImage(1, null);
            return View(repository.Games);
        }

        [HttpPost]
        public ActionResult Edit(Game game, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    if (!image.ContentType.Contains("image/"))
                    {
                        ModelState.AddModelError("image", "Некорректное изображение");
                        return View(game);
                    }
                    game.ImagePath = SaveFile(game.GameId, image);
                    game.ImageMimeType = image.ContentType;
                }

                if (ModelState.IsValid)
                    // Слишком много картинок с одинаковым названием
                    return View(game);

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

        /// <summary>
        /// Saves the file in directory (BaseDirectory)/[ImageDirectories]/[Id]/ and return the full path to the file.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="image"></param>
        /// <param name="ImageDirectory"></param>
        /// <param name="FileName"></param>
        /// <returns>The path to the file in file system. If there is an error in the incoming parameters, it will return zero.</returns>
        private string SaveFile(int Id, HttpPostedFileBase File, 
            ImageDirectories ImageDirectory = ImageDirectories.GameLogo, string FileName = null)
        {
            if (File == null)
                return null;

            string FilePath = AppDomain.CurrentDomain.BaseDirectory +
                "Images\\" + ImageDirectory + "\\" + Id.ToString() + "\\";

            System.Diagnostics.Debug.WriteLine(FilePath);

            if (!Directory.Exists(FilePath))
                Directory.CreateDirectory(FilePath);

            switch (ImageDirectory)
            {
                case ImageDirectories.GameLogo:
                    FilePath += "Logo" + Path.GetExtension(File.FileName);
                    break;
                default:
                    if (FileName != null)
                        FilePath += FileName.ToString() + Path.GetExtension(File.FileName);
                    else
                    {
                        FilePath += File.FileName;
                        int i = 0;
                        if (System.IO.File.Exists(FilePath))
                        {
                            FilePath = FilePath.Insert(FilePath.LastIndexOf(Path.GetExtension(FilePath)),
                                "_" + i.ToString());
                            while (System.IO.File.Exists(FilePath) && i <= 100)
                            {
                                i++;
                                FilePath = FilePath.Remove(FilePath.LastIndexOf((i - 1).ToString() +
                                    Path.GetExtension(FilePath)), (i - 1).ToString().Length)
                                    .Insert(FilePath.LastIndexOf((i - 1).ToString() + Path.GetExtension(FilePath)), i.ToString());
                            }
                        }
                        

                        if (i == 101)
                        {
                            ModelState.AddModelError("image", "Существует больше 100 файлов с подобным названием.");
                            return null;
                        }

                    }

                    break;
            }
            File.SaveAs(FilePath);

            return FilePath;

        }


    }
}