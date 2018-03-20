using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.Domain.Entities;

namespace GameStore.WebUI.Models
{
    public class GameViewModel
    {
        public Game Game { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}