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

namespace CSCBelgium.Controllers
{
    public class ConfiguratiepaneelController : Controller
    {
        // GET: Configuratiepaneel
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult EditPosts()
        {
            EditPostsViewModel vm = new EditPostsViewModel();
            tblPostsService service = new tblPostsService();
            vm.Posts = service.getAllPosts();
            return View(vm);
        }
        [Authorize]
        public ActionResult AddPost()
        {
            return View();
        }
        [Authorize]
        public ActionResult AddCar()
        {
            AddCarViewModel vm = new AddCarViewModel();
            tblCarsService cservice = new tblCarsService();
            tblColorsService colservice = new tblColorsService();
            tblBrandsService bservice = new tblBrandsService();
            vm.brandChoice = bservice.getBrands();
            vm.colorChoice = colservice.getColors();
            return View(vm);

        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCar(AddCarViewModel vm)
        {    
            tblCarsService cservice = new tblCarsService();
            tblColorsService colservice = new tblColorsService();
            tblBrandsService bservice = new tblBrandsService();
            tblCars car = new tblCars();
            car.BrandID = vm.selectedBrandId;
            car.ColorID = vm.selectedColorId;
            car.CarModel = vm.car.CarModel;
            car.CarDescription = vm.car.CarDescription;
            car.CarFuel = vm.fuelChoice.ToString();
            car.CarKilometers = vm.car.CarKilometers;
            car.CarPrice = vm.car.CarPrice;
            car.CarEquipment = vm.car.CarEquipment;
            car.CarYearOfConstruction = vm.car.CarYearOfConstruction;
            car.Sold = (byte)0;
            car.C02Emissions = vm.car.C02Emissions;
            car.PowerKW = vm.car.PowerKW;
            car.PowerPK = vm.car.PowerPK;
            car.CylinderCapacity = vm.car.CylinderCapacity;
            car.FirstRegistration = vm.car.FirstRegistration;
            car.Transmission = vm.transmissionChoice.ToString();
            car.Createdate = DateTime.Now.Date;
            cservice.addCar(car);
            List<HttpPostedFileBase> files = vm.files.ToList();
            for (int i = 0;i<files.Count();i++)
            {
                if (files.ElementAt(i) != null && files.ElementAt(i).ContentLength > 0)
                {
                    tblImages Image = new tblImages();
                    System.Diagnostics.Debug.WriteLine("image");
                    MemoryStream target = new MemoryStream();
                    files.ElementAt(i).InputStream.CopyTo(target);
                    byte[] image = target.ToArray();
                    Image.CarID = car.CarID;
                    Image.ImageOrder = i;
                    Image.Image = image;
                    Debug.Write("image: " + Image.CarID + "," + Image.ImageOrder + "," + Image.Image);
                    tblImagesService iservice = new tblImagesService();
                    iservice.AddImage(Image);

                }
                else
                {
                    Debug.WriteLine("file null");
                    
                }
               
            }
            return RedirectToAction("CarIndex");

           
        }
        [Authorize]
        public ActionResult AddColor()
        {
            tblColorsService service = new tblColorsService();
            AddColorViewModel vm = new AddColorViewModel();
            vm.colors = service.getColors();
            return View(vm);
        }
        [Authorize]
        public ActionResult ManageSliderImages()
        {
           tblSlidesService service = new tblSlidesService();
           ManageSliderImagesViewModel vm = new ManageSliderImagesViewModel();
           vm.slides = service.getSlides();
           vm.LinksEerst = Uitlijning.Links;
           vm.MiddenEerst = Uitlijning.Midden;
           vm.RechtsEerst = Uitlijning.Rechts;
           return View(vm);
        }
        [Authorize]
        public ActionResult addSliderImage()
        {
            
            AddSliderImageViewModel vm = new AddSliderImageViewModel();
           
            return View(vm);
        }
        [Authorize]
        public ActionResult ManageRims()
        {
            ManageRimsViewModel vm = new ManageRimsViewModel();
            tblRimsService service = new tblRimsService();
            vm.janee = JaNee.Ja;
            vm.neeja = JaNee.Nee;
            vm.rims = service.getRims();
            vm.RimsInStock = service.getRims().Where(a => a.Sold == 0).Count();
            vm.images = service.getRimImages().ToList();
            return View(vm);
        }
        [Authorize]
        public ActionResult AddRim()
        {
            AddRimViewModel vm = new AddRimViewModel();
            tblRimsService service = new tblRimsService();
            tblBrandsService bservice = new tblBrandsService();
            vm.brandChoice = bservice.getBrands();
            return View(vm);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRim(AddRimViewModel vm)
        {
            tblRimsService service = new tblRimsService();
            tblBrandsService bservice = new tblBrandsService();
            tblRims rim = new tblRims();
            rim.RimBrandID = vm.selectedBrandId;
            rim.RimModel = vm.rim.RimModel;
            rim.RimPrice = vm.rim.RimPrice;
            rim.Sold = (byte)0;
            service.addRim(rim);
            List<HttpPostedFileBase> files = vm.files.ToList();
            for (int i = 0; i < files.Count(); i++)
            {
                if (files.ElementAt(i) != null && files.ElementAt(i).ContentLength > 0)
                {
                    tblRimImages Image = new tblRimImages();
                    System.Diagnostics.Debug.WriteLine("image");
                    MemoryStream target = new MemoryStream();
                    files.ElementAt(i).InputStream.CopyTo(target);
                    byte[] image = target.ToArray();
                    Image.RimID = rim.RimID;
                    Image.Image = image;
                    service.addRimImage(Image);

                }
                else
                {
                    Debug.WriteLine("file null");

                }

            }
            return RedirectToAction("ManageRims");

        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addSliderImage(AddSliderImageViewModel vm)
        {
            tblSlidesService service = new tblSlidesService();
            tblSlides slide = new tblSlides();
            if (ModelState.IsValid)
            {
                Debug.WriteLine("test:"+vm.newSlide.CationColor);
                slide.CaptionText = vm.newSlide.CaptionText;
                slide.CaptionAlignment = vm.alignment.ToString();
                slide.CationColor = vm.newSlide.CationColor;


                HttpPostedFileBase file = Request.Files["txtAfbeelding"];

                if (file != null && file.ContentLength > 0)
                {

                    System.Diagnostics.Debug.WriteLine("image");
                    MemoryStream target = new MemoryStream();
                    file.InputStream.CopyTo(target);
                    byte[] image = target.ToArray();
                    slide.SlideImage = image;
                    service.addSlide(slide);
                    return RedirectToAction("ManageSliderImages");

                }
                else
                {
                    Debug.WriteLine("file null");
                    return View();
                }
            }
            else
            {
                Debug.WriteLine("modelstate invalid");
                return View();
            }

            
        }
        [Authorize]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddColor(AddColorViewModel vm)
        {
            tblColorsService service = new tblColorsService();

            service.addColor(vm.newColor);
            return RedirectToAction("CarIndex");
        }
        [Authorize]
        public ActionResult AddBrand()
        {
            tblBrandsService service = new tblBrandsService();
            AddBrandViewModel vm = new AddBrandViewModel();
            vm.brands = service.getBrands();
            return View(vm);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBrand(AddBrandViewModel vm)
        {
            tblBrandsService service = new tblBrandsService();
            service.addBrand(vm.newBrand);
            return RedirectToAction("CarIndex");
        }
        public ActionResult AddRimBrand()
        {
            tblRimBrandsService service = new tblRimBrandsService();
            AddRimBrandViewModel vm = new AddRimBrandViewModel();
            vm.brands = service.getBrands();
            return View(vm);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRimBrand(AddRimBrandViewModel vm)
        {
            tblRimBrandsService service = new tblRimBrandsService();
            service.addBrand(vm.newBrand);
            return RedirectToAction("ManageRims");
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPost(tblPosts Post)
        {
            tblPosts post = new tblPosts();
            if (ModelState.IsValid)
            {

                post.PostTitle = Post.PostTitle;
                post.PostDate = DateTime.Now;
                post.PostDescription = Post.PostDescription;
                tblPostsService service = new tblPostsService();


                HttpPostedFileBase file = Request.Files["txtAfbeelding"];

                if (file != null && file.ContentLength > 0)
                {

                    System.Diagnostics.Debug.WriteLine("image");
                    MemoryStream target = new MemoryStream();
                    file.InputStream.CopyTo(target);
                    byte[] image = target.ToArray();
                    post.PostImage = image;
                    Debug.Write("post: " + post.PostTitle + "," + post.PostDescription + "," + post.PostDate + "," + post.PostImage);
                    service.addNewPost(post);
                    return RedirectToAction("EditPosts");

                }
                else
                {
                    Debug.WriteLine("file null");
                    return View();
                }
            }
            else
            {
                Debug.WriteLine("modelstate invalid");
                return View();
            }
        }
        [Authorize]
        [HttpDelete]
        [ActionName("VerwijderLijn")]
        public void VerwijderLijn(int id)
        {
            System.Diagnostics.Debug.WriteLine(id);

            tblPostsService postservice = new tblPostsService();
            postservice.deletePost(id);

        }
        [Authorize]
        [HttpDelete]
        public void VerwijderAuto(int id)
        {
            System.Diagnostics.Debug.WriteLine(id);

            tblCarsService carservice = new tblCarsService();
            carservice.deleteCar(id);

        }
        [Authorize]
        [HttpDelete]
        public void VerwijderKleur(int id)
        {
            System.Diagnostics.Debug.WriteLine(id);

            tblColorsService colorService = new tblColorsService();
            colorService.deleteColor(id);

        }
        [Authorize]
        [HttpDelete]
        public void VerwijderSlide(int id)
        {
            System.Diagnostics.Debug.WriteLine(id);

            tblSlidesService slideService = new tblSlidesService();
            slideService.deleteSlide(id);

        }
        [Authorize]
        [HttpDelete]
        public void VerwijderRim(int id)
        {
            System.Diagnostics.Debug.WriteLine(id);

            tblRimsService rimsService = new tblRimsService();
            rimsService.deleteRim(id);
        }
        [Authorize]
        [HttpDelete]
        public void VerwijderMerk(int id)
        {
            System.Diagnostics.Debug.WriteLine(id);

            tblBrandsService brandService = new tblBrandsService();
            brandService.deleteBrand(id);

        }
        [Authorize]
        [HttpDelete]
        public void VerwijderVelgMerk(int id)
        {
            System.Diagnostics.Debug.WriteLine(id);

            tblRimBrandsService brandService = new tblRimBrandsService();
            brandService.deleteBrand(id);

        }
        [Authorize]
        [HttpPost]
        public void alignmentChanged(string parameter)
        {
            string[] parameters = parameter.Split('b');
            Debug.WriteLine(parameter);
            tblSlidesService service = new tblSlidesService();
            if (parameters[0].Equals("0"))
            {
                Debug.WriteLine("Links");
                service.updateAlignment(service.getSlides().ElementAt(Int32.Parse(parameters[1])), 0);

            }
            else
            {
                if (parameters[0].Equals("1"))
                {
                    Debug.WriteLine("Midden");
                    service.updateAlignment(service.getSlides().ElementAt(Int32.Parse(parameters[1])), 1);

                }
                else
                {
                    Debug.WriteLine("Rechts");
                    service.updateAlignment(service.getSlides().ElementAt(Int32.Parse(parameters[1])), 2);

                }
            }
        }
        [Authorize]
        [HttpPost]
        public void ddlChanged(string parameter)
        {
            string[] parameters = parameter.Split('b');
            Debug.WriteLine(parameter);
            tblCarsService service = new tblCarsService();
            if (parameters[0].Equals("0"))
            {
                Debug.WriteLine("Ja");
                service.SellCar(service.getAllCars().ElementAt(Int32.Parse(parameters[1])), true);


            }
            else
            {
                if (parameters[0].Equals("1"))
                {
                    Debug.WriteLine("Nee");
                    service.SellCar(service.getAllCars().ElementAt(Int32.Parse(parameters[1])), false);

                }
            }

        }
        [Authorize]
        [HttpPost]
        public void SoldChanged(string parameter)
        {
            string[] parameters = parameter.Split('b');
            Debug.WriteLine(parameter);
            tblRimsService service = new tblRimsService();
            if (parameters[0].Equals("0"))
            {
                Debug.WriteLine("Ja");
                service.SellRim(service.getRims().ElementAt(Int32.Parse(parameters[1])), true);


            }
            else
            {
                if (parameters[0].Equals("1"))
                {
                    Debug.WriteLine("Nee");
                    service.SellRim(service.getRims().ElementAt(Int32.Parse(parameters[1])), false);

                }
            }

        }
        [Authorize]
        public ActionResult CarIndex()
        {
            tblCarsService service = new tblCarsService();
            CarIndexViewModel vm = new CarIndexViewModel();
            vm.Cars = service.getAllCars();

            vm.CarsInStock = service.getAllCars().Where(a => a.Sold == 0).Count();
            vm.JaNee= JaNee.Ja;
            vm.NeeJa = JaNee.Nee;
            vm.Images = service.getAllImages();
            return View(vm);
        }
    }
}