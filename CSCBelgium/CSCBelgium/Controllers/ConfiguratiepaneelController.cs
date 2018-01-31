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
using System.Drawing;
using System.Drawing.Imaging;

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
            //tblPostsService service = new tblPostsService();
            //ICollection<tblPosts> posts = service.getAllPosts();

            //for (int i = 0; i < posts.Count(); i++)
            //{
            //    tblPosts temp = posts.ElementAt(i);
            //    temp.PostImage = ResizeImage(temp.PostImage, 0.70f);
            //    service.UpdatePost(temp);
            //}
            return View();
        }
        private void addCarImagesToFileSystem(tblImages image, byte[] ImageArray)
        {
            string filename = "Image" + image.ImageID + ".jpg";
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"Content\Filesystem");
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"Content\Filesystem\Cars\Car" + image.CarID);

            var fs = new BinaryWriter(new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"Content\Filesystem\Cars\Car" + image.CarID + @"\" + filename, FileMode.Append, FileAccess.Write));
            fs.Write(ImageArray);
            fs.Close();
        }
        private void addSlideImagesToFileSystem(tblSlides slide, byte[] ImageArray)
        {
            string filename = "slide.jpg";
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"Content\Filesystem");
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"Content\Filesystem\Slides\Slide" + slide.SlideID);

            var fs = new BinaryWriter(new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"Content\Filesystem\Slides\Slide" + slide.SlideID + @"\" + filename, FileMode.Append, FileAccess.Write));
            fs.Write(ImageArray);
            fs.Close();
        }
        private void deleteSlideImagesFromFileSystem(int slideid)
        {
            System.IO.Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + @"Content\FileSystem\Slides\Slide" + slideid, true);
        }
        private void deleteCarImagesFromFileSystem(int carid)
        {
            System.IO.Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + @"Content\FileSystem\Cars\Car"+carid, true);
        }
        [Authorize]
        public ActionResult EditCar(int carID)
        {
            EditCarViewModel vm = new EditCarViewModel();
            tblCarsService cservice = new tblCarsService();
            tblColorsService colservice = new tblColorsService();
            tblBrandsService bservice = new tblBrandsService();
            vm.car = cservice.getCar(carID);
            vm.selectedBrandId = vm.car.BrandID;
            vm.selectedColorId = vm.car.ColorID;
            vm.brandChoice = bservice.getBrands();
            vm.colorChoice = colservice.getColors();
            vm.fuelChoice = (Fuel)Enum.Parse(typeof(Fuel), vm.car.CarFuel, true);
            vm.transmissionChoice = (Transmission)Enum.Parse(typeof(Transmission), vm.car.Transmission, true);

            return View(vm);

        }
        //private static byte[] ResizeImage(byte[] array, float resizePercentage, bool OnlyWhenGreaterThan800kB = false)
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCar(AddCarViewModel vm)
        {

            tblCarsService cservice = new tblCarsService();
            tblColorsService colservice = new tblColorsService();
            tblBrandsService bservice = new tblBrandsService();
            tblImagesService iservice = new tblImagesService();
            tblCars car = cservice.getCar(vm.car.CarID);
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
            cservice.editCar(car);
            List<HttpPostedFileBase> files = vm.files.ToList();
            //ICollection<tblImages> images = cservice.GetImagesByCar(car);
            //for (int i = 0; i < images.Count() ; i++)
            //{
            //    tblImages temp = images.ElementAt(i);
            //    temp.Image = ResizeImage(temp.Image, 0.70f);
            //    iservice.UpdateImage(temp);
            //}
            for (int i = 0; i < files.Count(); i++)
            {
                if (files.ElementAt(i) != null && files.ElementAt(i).ContentLength > 0)
                {
                    if (i == 0)
                    {
                        iservice.DeleteAllImagesOfCar(vm.car);
                    }
                    tblImages Image = new tblImages();
                    System.Diagnostics.Debug.WriteLine("image");
                    MemoryStream target = new MemoryStream();
                    files.ElementAt(i).InputStream.CopyTo(target);
                    byte[] image = target.ToArray();
                    Image.CarID = car.CarID;
                    Image.ImageOrder = i;
                    Image.ImagePath = "Temp";
                    iservice.AddImage(Image);
                    Image.ImagePath = @"Cars/Car" + car.CarID + @"/Image" + Image.ImageID + ".jpg";
                    iservice.UpdateImage(Image);
                    addCarImagesToFileSystem(Image, image);

                }
                else
                {
                    Debug.WriteLine("file null");
                    return RedirectToAction("CarIndex");

                }

            }
            return RedirectToAction("CarIndex");


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
            for (int i = 0; i < files.Count(); i++)
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
                    Image.ImagePath =@"Cars\Car"+car.CarID+@"\Image";
                    tblImagesService iservice = new tblImagesService();
                    iservice.AddImage(Image);
                    Image.ImagePath = @"Cars/Car" + car.CarID + @"/Image" + Image.ImageID+".jpg" ;
                    iservice.UpdateImage(Image);
                    addCarImagesToFileSystem(Image, image);

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
            vm.order = service.getSlides().Select(a => a.SlideOrder).ToList();



            return View(vm);
        }
        [Authorize]
        public ActionResult addSliderImage()
        {

            AddSliderImageViewModel vm = new AddSliderImageViewModel();
            //tblCarsService sservice = new tblCarsService();
            //ICollection<tblCars> cars = sservice.getAllCars();
            //for (int i = 0; i < cars.Count; i++)
            //{
            //    ICollection<tblImages> images = sservice.GetImagesByCar(cars.ElementAt(i));
            //    for (int j = 0; j < images.Count; j++)
            //    {
            //        string filename = "Image" + images.ElementAt(j).ImageID + ".jpg";
            //        System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"Content\Filesystem");
            //        System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"Content\Filesystem\Cars\Car" + cars.ElementAt(i).CarID);

            //        var fs = new BinaryWriter(new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"Content\Filesystem\Cars\Car" + cars.ElementAt(i).CarID + @"\" + filename, FileMode.Append, FileAccess.Write));
            //        fs.Write(images.ElementAt(j).Image);
            //        fs.Close();

            //    }
            //}



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
            if (vm.newSlide.CaptionText == null)
            {
                vm.newSlide.CaptionText = " ";
            }
            if (ModelState.IsValid)
            {
                Debug.WriteLine("test:" + vm.newSlide.CationColor);
                slide.CaptionText = vm.newSlide.CaptionText;
                slide.CaptionAlignment = vm.alignment.ToString();
                slide.CationColor = vm.newSlide.CationColor;
                if (service.getSlides().Count() != 0)
                {
                    slide.SlideOrder = service.getSlides().Select(a => a.SlideOrder).Max() + 1;
                }
                else
                {
                    slide.SlideOrder = 1;
                }




                HttpPostedFileBase file = Request.Files["txtAfbeelding"];

                if (file != null && file.ContentLength > 0)
                {

                    System.Diagnostics.Debug.WriteLine("image");
                    MemoryStream target = new MemoryStream();
                    file.InputStream.CopyTo(target);
                    byte[] image = target.ToArray();
                    slide.ImagePath = @"Slides/Slide";
                    service.addSlide(slide);
                    slide.ImagePath = @"Slides/Slide" + slide.SlideID + @"/slide.jpg";
                    service.UpdateSlide(slide);
                    addSlideImagesToFileSystem(slide, image);
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
            deleteCarImagesFromFileSystem(id);

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
            deleteSlideImagesFromFileSystem(id);

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
        public void OrderChanged(string parameter)
        {
            string[] parameters = parameter.Split('b');
            string s = parameters[0];
            int result;
            if (int.TryParse(s, out result))
            {
                Debug.WriteLine(parameter);
                tblSlidesService service = new tblSlidesService();
                Debug.WriteLine(parameters[0]);
                Debug.WriteLine(parameters[1]);

                service.UpdateOrder(service.getSlides().ElementAt(Int32.Parse(parameters[1])), Int32.Parse(parameters[0]));
            }
            else
            {

            }


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
            vm.JaNee = JaNee.Ja;
            vm.NeeJa = JaNee.Nee;

            return View(vm);
        }
    }
}