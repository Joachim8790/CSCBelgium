using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSCBelgium.Service;
using CSCBelgium.ViewModels;
using CSCBelgium.DAO.Model;
using System.IO;
using System.Diagnostics;
using System.Web.Mvc.Ajax;
using CSCBelgium.Views.Configuratiepaneel.CustomCode;
using System.Text.RegularExpressions;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using System.Drawing;

namespace CSCBelgium.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string element = "")
        {
            HomeViewModel vm = new HomeViewModel();
            tblCarsService service = new tblCarsService();
            tblSlidesService sservice = new tblSlidesService();
            vm.first8Cars = service.getAllCars().Take(8).ToList();
            vm.firstImages = service.GetFrontImages().ToList();
            vm.slides = sservice.getSlides();
            vm.scrollToElement = element;
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(HomeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                IdentityMessage message = new IdentityMessage();
                message.Subject = vm.subject;
                message.Body = "<h2>Nieuw bericht van " + vm.name + "</h2><br/> <b>Emailadres correspondent:</b> " + vm.emailaddress + "<br/><b>  Onderwerp: </b>" + vm.subject + "<br/><b> Verstuurd bericht: </b></br>" + vm.message;
                EmailService service = new EmailService();

                await service.SendAsync(message);
               
            }

            return RedirectToAction("Index");
        }
        public ActionResult Cars()
        {
            CarsViewModel vm = new CarsViewModel();
            tblCarsService service = new tblCarsService();
            tblColorsService colservice = new tblColorsService();
            tblBrandsService bservice = new tblBrandsService();
            
            vm.cars = service.getAllCars();
            if (vm.cars.Count() > 0)
            {
                vm.minKM = service.getAllCars().Min(a => a.CarKilometers);
                vm.maxKM = service.getAllCars().Max(a => a.CarKilometers);
                vm.minPrice = service.getAllCars().Min(b => b.CarPrice);
                vm.maxPrice = service.getAllCars().Max(b => b.CarPrice);
                vm.brandChoice = bservice.getBrands();
                vm.colorChoice = colservice.getColors();
                vm.images = service.GetFrontImages();
                vm.CarsInStock = service.getAllCars().Where(a => a.Sold == 0).Count();
            }

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cars(CarsViewModel vm)
        {
            Debug.WriteLine(vm.minKM);
            Debug.WriteLine(vm.maxKM);
            Debug.WriteLine(vm.minPrice);
            Debug.WriteLine(vm.maxPrice);

            List<tblCars> cars = new List<tblCars>();
            
            tblCarsService service = new tblCarsService();
            tblColorsService colservice = new tblColorsService();
            tblBrandsService bservice = new tblBrandsService();
            cars = service.getAllCars().ToList();
            if(vm.selectedBrandId != 0)
            {
                cars = service.FilterByBrand(cars, bservice.getBrands().ElementAt(vm.selectedBrandId - 2)).ToList();
                Debug.WriteLine(cars.Count());
            }
            if (vm.selectedColorId != 0)
            {
                cars = service.FilterByColor(cars, colservice.getColors().ElementAt(vm.selectedColorId - 1)).ToList();
                Debug.WriteLine(cars.Count());
            }
            if (vm.transmissionChoice== Transmission.Automaat || vm.transmissionChoice == Transmission.Manueel)
            {
                cars = cars.Where(a => a.Transmission == vm.transmissionChoice.ToString()).ToList();
                Debug.WriteLine(cars.Count());
            }
            if (vm.fuelChoice == Fuel.Benzine || vm.fuelChoice == Fuel.Diesel || vm.fuelChoice == Fuel.Elektrisch || vm.fuelChoice == Fuel.Hybride || vm.fuelChoice == Fuel.LPG )
            {
                cars = cars.Where(a => a.CarFuel == vm.fuelChoice.ToString()).ToList();
                Debug.WriteLine(cars.Count());
            }
            cars = service.FilterByKM(cars, vm.minKM, vm.maxKM).ToList();
            Debug.WriteLine(cars.Count());
            cars = service.FilterByPrice(cars, Convert.ToInt32(vm.minPrice), Convert.ToInt32(vm.maxPrice)).ToList();
            Debug.WriteLine(cars.Count());

            vm.cars = cars;
            vm.minKM = service.getAllCars().Min(a => a.CarKilometers);
            vm.maxKM = service.getAllCars().Max(a => a.CarKilometers);
            vm.minPrice = service.getAllCars().Min(b => b.CarPrice);
            vm.maxPrice = service.getAllCars().Max(b => b.CarPrice);
            vm.brandChoice = bservice.getBrands();
            vm.colorChoice = colservice.getColors();
            vm.images = service.getAllImages();
            vm.CarsInStock = service.getAllCars().Where(a => a.Sold == 0).Count();
            return View(vm);
        }
        //TEMP
        
        //private static byte[] ResizeImage(byte[] array, float resizePercentage,bool OnlyWhenGreaterThan800kB = false)
        //{
        //    if (OnlyWhenGreaterThan800kB)
        //    {
        //        if (array.Length < 800000)
        //        {
        //            Debug.WriteLine("Afbeelding is reeds kleiner dan 800 kB");
        //            return array;
        //        }
        //    }
        //    Bitmap image;
        //    using (var ms = new System.IO.MemoryStream(array))
        //    {
        //        image = new Bitmap(ms);
        //    }
        //    int currentWidth = image.Width;
        //    int currentHeight = image.Height;
        //    int width = (int)((float)currentWidth * resizePercentage);
        //    int height = (int)((float)currentHeight * resizePercentage);
        //    Bitmap resizedImage = new Bitmap(width, height);
        //    using (Graphics gfx = Graphics.FromImage(resizedImage))
        //    {
        //        gfx.DrawImage(image, new Rectangle(0, 0, width, height),
        //            new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
        //    }
        //    ImageConverter converter = new ImageConverter();
        //    return (byte[])converter.ConvertTo(resizedImage, typeof(byte[]));

        //}

        //END TEMP
        public ActionResult CarDetails(int carID)
        {
            
            CarDetailsViewModel vm = new CarDetailsViewModel();
            tblCarsService service = new tblCarsService();
            if (service.isSold(service.getCar(carID)))
            {
                return HttpNotFound();
            }
            else
            {
                tblBrandsService bservice = new tblBrandsService();
                vm.car = service.getCar(carID);
                vm.images = service.GetImagesByCar(vm.car);
                vm.car.CarEquipment = Regex.Replace(vm.car.CarEquipment, @"\r\n?|\n", ",");
                vm.carbrand = bservice.getBrand(vm.car.BrandID).BrandName;
                vm.carmodel = vm.car.CarModel;
                return View(vm);
            }
            

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CarDetails(CarDetailsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                tblBrandsService bservice = new tblBrandsService();
                IdentityMessage message = new IdentityMessage();
                message.Subject = "Interesse in volgende auto: " + vm.carbrand + " " + vm.carmodel;
                message.Body = "<h2>Nieuw bericht van " + vm.name + "</h2><br/> <b>Emailadres correspondent:</b> " + vm.emailadres + "<br/><b>  Onderwerp: </b> Interesse in volgende auto: " + vm.carbrand + " " + vm.carmodel + "<br/><b> Verstuurd bericht: </b></br>" + vm.message;
                EmailService service = new EmailService();

                await service.SendAsync(message);
            }

            return RedirectToAction("Index");

        }
        public ActionResult SellCar()
        {
            SellCarViewModel vm = new SellCarViewModel();
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> sellCar(SellCarViewModel vm)
        {
            if (vm.Comment == null)
            {
                vm.Comment = "";
            }
            if (vm.firstRegistration == null)
            {
                vm.firstRegistration = DateTime.Now.Date;
            }
            if (vm.CarDamage == null)
            {
                vm.CarDamage = "";
            }
            IdentityMessage message = new IdentityMessage();
            message.Subject = "Ik verkoop volgende auto: " + vm.CarBrand + " " + vm.CarModel;
            message.Body = "<h3>Nieuw bericht van " + vm.SurName + " " + vm.Name + "</h3><br/> <b>Emailadres correspondent:</b> " + vm.Emailaddress + "<br/> <b>Telefoonnummer correspondent:</b> " +vm.Phone+ "<br/><b>  Onderwerp: </b> Ik verkoop volgende auto: " + vm.CarBrand + " " + vm.CarModel + "<br/><b> Gegevens auto: </b></br><b>Merk: </b>" + vm.CarBrand + "<br/><b>Model: </b>" + vm.CarModel + "<br/><b>Kilometerstand: </b>" + vm.CarKilometers+ "<br/><b>Schade: </b>" + vm.CarDamage + "<br/><b>Kleur: </b>" +vm.Color+ "<br/><b>Bouwjaar: </b>" + vm.YearOfConstruction + "<br/><b>Beschrijving: </b>" + vm.CarDescription + "<br/><b>Brandstof: </b>" + vm.fuelChoice.ToString()+ "<br/><b>Transmissie: </b>"+vm.transmissionChoice.ToString()+ "<br/><b>Eerste inschrijving: </b>"+vm.firstRegistration.ToLongDateString()+ "<br/><b>Opmerking: </b>"+vm.Comment+"<br/><br/>";
            
            List<HttpPostedFileBase> files = vm.files.ToList();
            for (int i = 0; i < files.Count(); i++)
            {
                if (files.ElementAt(i) != null && files.ElementAt(i).ContentLength > 0)
                {
                    
                    MemoryStream target = new MemoryStream();
                    files.ElementAt(i).InputStream.CopyTo(target);
                    byte[] image = target.ToArray();
                    message.Body += "<img alt='' src='data:image;base64,"+Convert.ToBase64String(image)+"'style=' width:100%;max-width:600px;height:auto' />";
         

                }
                else
                {
                    Debug.WriteLine("file null");

                }
               
                
            }
            EmailService service = new EmailService();

            await service.SendAsync(message);
            return RedirectToAction("Index");
        }
        public ActionResult TestDrive()
        {
            return View();
        }
        public ActionResult Rims()
        {
            tblCarsService cservice = new tblCarsService();
            tblRimsService service = new tblRimsService();
            RimsViewModel vm = new RimsViewModel();
            vm.images = service.getRimImages();
            vm.rims = service.getRims();
            if (vm.rims.Count() > 0)
            {
                vm.CarsInStock = cservice.getAllCars().Where(a => a.Sold == 0).Count();
            }
            return View(vm);
        }
        public ActionResult RimDetails(int rimID)
        {
            tblRimsService service = new tblRimsService();
            if (service.isSold(service.getRim(rimID)))
            {
                return HttpNotFound();
            }
            else
            {
               
                RimDetailsViewModel vm = new RimDetailsViewModel();
                tblBrandsService bservice = new tblBrandsService();
                vm.rim = service.getRim(rimID);
                vm.images = service.getImagesOfRim(rimID);
                vm.rimbrand = bservice.getBrand(vm.rim.RimBrandID).BrandName;
                vm.rimmodel = vm.rim.RimModel;
                return View(vm);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RimDetails(RimDetailsViewModel vm)
        {

            if (ModelState.IsValid)
            {
                tblBrandsService bservice = new tblBrandsService();
                IdentityMessage message = new IdentityMessage();
                message.Subject = "Interesse in volgende velgen: " + vm.rimbrand + " " + vm.rimmodel;
                message.Body = "<h2>Nieuw bericht van " + vm.name + "</h2><br/> <b>Emailadres correspondent:</b> " + vm.emailadres + "<br/><b>  Onderwerp: </b> Interesse in volgende velgen: " + vm.rimbrand + " " + vm.rimmodel + "<br/><b> Verstuurd bericht: </b></br>" + vm.message;
                EmailService service = new EmailService();

                await service.SendAsync(message);
            }

            return RedirectToAction("Index");
           
        }
        public ActionResult Services()
        {
            
            return View();
        }

        public ActionResult Contact()
        {
            ContactViewModel vm = new ContactViewModel();



            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(ContactViewModel vm)
        {

            if (ModelState.IsValid)
            {
                IdentityMessage message = new IdentityMessage();
                message.Subject = vm.subject;
                message.Body = "<h2>Nieuw bericht van " + vm.name + "</h2><br/> <b>Emailadres correspondent:</b> " + vm.emailaddress + "<br/><b>  Onderwerp: </b>" + vm.subject + "<br/><b> Verstuurd bericht: </b></br>" + vm.message;
                EmailService service = new EmailService();

                await service.SendAsync(message);

            }

            return RedirectToAction("Index");
        }
        //public ActionResult DitIsEenTestPaginaEnHierKanJeNietsOpDoen()
        //{
        //    TestViewModel vm = new TestViewModel();
        //    tblCarsService cservice = new tblCarsService();
        //    int carid = cservice.getAllCars().Take(1).Select(a => a.CarID).First();
        //    vm.afbeeldingen = cservice.getXImagesOfCarY(2, carid);
        //    vm.afbeeldingen.ElementAt(0).Image = ResizeImage(vm.afbeeldingen.ElementAt(0).Image, 0.40f,true);
        //    vm.afbeeldingen.ElementAt(1).Image = ResizeImage(vm.afbeeldingen.ElementAt(1).Image, 0.40f,true);
        //    Debug.WriteLine(carid);
        //    Debug.WriteLine(vm.afbeeldingen.Count());
        //    return View(vm);


        //}

    }
    
   
}