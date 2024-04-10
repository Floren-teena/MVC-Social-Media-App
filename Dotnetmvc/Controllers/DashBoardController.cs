using dotnetcoremorningclass.Interfaces;
using dotnetcoremorningclass.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoremorningclass.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IDashBoardRepository _dashBoardRepository;
        //implement repo for club,use the i int for club,then if its available,add club and put d value for club
        public DashBoardController(IDashBoardRepository dashBoardRepository)
        {
            _dashBoardRepository = dashBoardRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userRace = await _dashBoardRepository.GetAllUserRacesAsync();
            var userClub = await _dashBoardRepository.GetAllUserClubsAsync();

            var dashboardViewModel = new DashBoardViewModel
            {
                Races = userRace,
                Clubs = userClub

            };
            return View(dashboardViewModel);
        }
    }
}
