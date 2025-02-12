﻿using dotnetcoremorningclass.Data.Enum;
using dotnetcoremorningclass.Models;

namespace dotnetcoremorningclass.ViewModels
{
    public class CreateRaceViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public Address Address { get; set; }
        public RaceCategory RaceCategory { get; set; }

        public string? AppUserId { get; set; }

    }

}
