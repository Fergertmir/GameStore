using GameStore.Domain.Entities;
using System.Collections.Generic;


namespace GameStore.WebUI.Models
{
    // Класс для передачи группы данных из контекста в представление.
    public class GamesListViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}