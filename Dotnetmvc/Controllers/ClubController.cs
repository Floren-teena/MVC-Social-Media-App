using dotnetcoremorningclass.Data;
using dotnetcoremorningclass.Interfaces;
using dotnetcoremorningclass.Models;
using dotnetcoremorningclass.Repository;
using dotnetcoremorningclass.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoremorningclass.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubrepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClubController(IClubRepository clubrepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _clubrepository = clubrepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var club = await _clubrepository.GetAll();
            return View(club);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            //Linq queries
            var club = await _clubrepository.GetByIdAsync(id);
            return View(club);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User?.GetUserId();
            var createClubModel = new CreateClubViewModel { AppUserId = curUserId };
            return View(createClubModel);
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            //return View();
            //this means thta if the user did not input a required field,this will show the user
            //the error while still on that page depending on the field not filled in correctly
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);

                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    ClubCategory = clubVM.ClubCategory,
                    AppUserId = clubVM.AppUserId,
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State
                    }
                };


                _clubrepository.Add(club);
                return RedirectToAction("Index");

            }
            else
            {
                ModelState.AddModelError("", "Photo Upload Failed");
            }

            return View(clubVM);
        }


        //For edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubrepository.GetByIdAsync(id);
            if (club == null) return View("Error");
            var clubVM = new EditClubViewModel()
            {
                Title = club.Title,
                Description = club.Description,
                //AddressId = club.AddressId,
                Address = club.Address,
                ClubCategory = club.ClubCategory,
                // URL = club.Image

            };

            return View(clubVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed To Edit Club");
                View("Edit", clubVm);
            }

            var result = await _photoService.AddPhotoAsync(clubVm.Image);

            if (result.Error != null)
            {
                ModelState.AddModelError("Img", "Photo Failed To UPLOAD");
                View("Edit", clubVm);
            }

            var club = new Club
            {
                Id = id,
                Title = clubVm.Title,
                Description = clubVm.Description,
                Address = clubVm.Address,
                Image = result.Url.ToString(),
                AddressId = clubVm.AddressId,
                ClubCategory = clubVm.ClubCategory,

            };

            _clubrepository.Update(club);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _clubrepository.GetByIdAsync(id);
            if (clubDetails == null)
            {

                return View("Error");
            }

            return View(clubDetails);
        }


        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetails = await _clubrepository.GetByIdAsync(id);
            if (clubDetails == null)
            {

                return View("Error");
            }

            _clubrepository.Delete(clubDetails);
            return RedirectToAction("Index");
        }
    }
}