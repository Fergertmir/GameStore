﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    public class GameController : Controller
    {
        private IGameRepository repository;
        public int pageSize = 4;

        // Внедрение зависимости Ninject при инициализации контроллера.
        public GameController(IGameRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            GamesListViewModel model = new GamesListViewModel
            {
                Games = repository.Games
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(game => game.GameId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ? 
                        repository.Games.Count() : 
                        repository.Games.Where(p => p.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public FilePathResult GetImage(int gameId)
        {
            Game game = repository.Games
                .FirstOrDefault(g => g.GameId == gameId);

            if (game != null)
            {
                if (game.ImagePath != null)
                    return File(game.ImagePath, game.ImageMimeType);
                else
                    return null;
            }
            else
            {
                return null;
            }
        }
    }
}