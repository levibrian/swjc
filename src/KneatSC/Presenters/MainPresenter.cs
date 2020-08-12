using KneatSC.Model;
using KneatSC.Resources;
using KneatSC.Services;
using System;
using System.Linq;
using System.Resources;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace KneatSC.Presenters
{
    public class MainPresenter : IMainPresenter
    {
        private readonly IStarshipService starshipService;
        private readonly ResourceManager headerResource;
        private readonly ResourceManager appResource;
        
        public MainPresenter(IStarshipService starshipService)
        {
            this.starshipService = starshipService;
            this.headerResource = new ResourceManager(typeof(HeaderResource));
            this.appResource = new ResourceManager(typeof(ApplicationResource));
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            await InitializeApp();
        }

        private async Task InitializeApp(bool displayValidationError = false)
        {
            Console.Clear();
            DisplayHeader();
            await DisplayJumpCalculator(displayValidationError);
        }

        private void DisplayHeader()
        {
            Console.Clear();
            Console.WriteLine(headerResource.GetString("Title"));
            Console.WriteLine("\r\n");
            Console.WriteLine(headerResource.GetString("Purpose"));
            Console.WriteLine("\r\n");
            Console.WriteLine(headerResource.GetString("Author"));
            Console.WriteLine(headerResource.GetString("Version"));
            Console.WriteLine("\r\n");
        }

        private async Task DisplayJumpCalculator(bool displayValidationError = false)
        {
            int distance;
            Console.WriteLine(appResource.GetString("StartMessage"));

            if (displayValidationError)
            {
                Console.WriteLine(appResource.GetString("ValidationMessage"));
            }

            Console.WriteLine(appResource.GetString("InputMessage"));

            var distanceInputted = Console.ReadLine();
            
            if(!int.TryParse(distanceInputted, out distance) || 
                    string.IsNullOrEmpty(distanceInputted) || 
                    distanceInputted.Equals("0"))
            {
                Console.Clear();
                await InitializeApp(true);
            }

            if (distance > 0)
            {
                Console.WriteLine("\r\n");
                Console.WriteLine(appResource.GetString("FetchDataMessage"));
                Console.WriteLine("\r\n");

                await FetchAndDisplayData(distance);

                Console.WriteLine(appResource.GetString("PressAnyKeyMessage"));

                switch (Console.ReadLine())
                {
                    case "c":
                    case "C":
                        break;
                    default:
                        await InitializeApp();
                        break;
                }
            }
        }

        private async Task FetchAndDisplayData(long distance)
        {
            var starshipCollection = await starshipService.GetAll(Convert.ToInt64(distance));

            foreach (var starship in starshipCollection)
            {
                DisplayStarship(starship);
            }
        }

        private void DisplayStarship(StarshipDTO starship)
        {
            Console.WriteLine($"Starship: {starship.Name}");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"Starship Model: {starship.Model}");
            Console.WriteLine($"Starship Class: {starship.StarshipClass}");
            Console.WriteLine($"Starship Max. Distance: {starship.MGLT}");
            Console.WriteLine($"Amount of Jumps: {starship.JumpCount}");
            Console.WriteLine("\r\n");
        }
    }
}