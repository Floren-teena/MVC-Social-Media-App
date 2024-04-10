using dotnetcoremorningclass.Data;
using dotnetcoremorningclass.Interfaces;
using dotnetcoremorningclass.Models;
using dotnetcoremorningclass.Repository;
using dotnetcoremorningclass.Services;
using dotnetcoremorningclass.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoremorningclass.Controllers
{
    //do the correct way and see why u cant use async 
    public class RaceController : Controller
    {
        private readonly IRaceRepository _racerepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public RaceController(IRaceRepository racerepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _racerepository = racerepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var race = await _racerepository.GetAll();
            return View(race);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            //Linq queries
            var race = await _racerepository.GetByIdAsync(id);
            return View(race);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User?.GetUserId();
            var createRaceModel = new CreateRaceViewModel { AppUserId = curUserId };
            return View(createRaceModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            //return View();
            //this means that if the user did not input a required field,this will show the user
            //the error while still on that page depending on the field not filled in correctly

            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);

                var race = new Race()
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    RaceCategory = raceVM.RaceCategory,
                    AppUserId = raceVM.AppUserId,
                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.State
                    }
                };


                _racerepository.Add(race);
                return RedirectToAction("Index");

            }
            else
            {
                ModelState.AddModelError("", "Photo Upload Failed");
            }

            return View(raceVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var race = await _racerepository.GetByIdAsync(id);
            if (race == null) return View("Error");
            var raceVM = new EditRaceViewModel()
            {
                Title = race.Title,
                Description = race.Description,
                //AddressId = club.AddressId,
                Address = race.Address,
                RaceCategory = race.RaceCategory,
                // URL = club.Image

            };

            return View(raceVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel raceVm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed To Edit Club");
                View("Edit", raceVm);
            }

            var result = await _photoService.AddPhotoAsync(raceVm.Image);

            if (result.Error != null)
            {
                ModelState.AddModelError("Img", "Photo Failed To UPLOAD");
                View("Edit", raceVm);
            }

            var race = new Race
            {
                Id = id,
                Title = raceVm.Title,
                Description = raceVm.Description,
                Address = raceVm.Address,
                Image = result.Url.ToString(),
               // AddressId = raceVm.AddressId,
                RaceCategory = raceVm.RaceCategory,

            };

            _racerepository.Update(race);
            return RedirectToAction("Index");

        }


    }

}




