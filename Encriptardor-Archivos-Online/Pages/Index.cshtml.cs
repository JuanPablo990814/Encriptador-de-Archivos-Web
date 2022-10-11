using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Encriptardor_Archivos_Online.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _he;

        public IndexModel(IWebHostEnvironment he)
        {
            _he = he;
        }

        [TempData]
        public string Mensaje { get; set; }

        public void OnGet()
        {

        }

        [BindProperty]
        public IFormFile Uploadfiles { get; set; }

        public async Task OnPostAsync()
        {
            var Fileupload = Path.Combine(_he.ContentRootPath, "Files", Uploadfiles.FileName);
            using (var Fs = new FileStream(Fileupload, FileMode.Create))
            {
                await Uploadfiles.CopyToAsync(Fs);
                ViewData["Message"] = "The Selected File " + Uploadfiles.FileName + " Is Uploaded Succesfully";
            }


        }

        //string archivo = null;
        //string extension = ".?";
        //string nombre = "sin_archivo";

        byte[] archivoBytes;

        Encriptador.DEncriptador algoritmo = new Encriptador.DEncriptador();

        [BindProperty]
        public IFormFile Upload { get; set; }

        //<RedirectToPageResult>
        //Task<RedirectToActionResult>
        public async Task<ActionResult> OnPostEncriptar(IFormFile file, string textollave)
        {
            if (file != null && file.Length > 0)
                try
                {
                    var Fileupload = Path.Combine(_he.ContentRootPath, "Files", file.FileName);
                    using (var Fs = new FileStream(Fileupload, FileMode.Create))
                    {
                        await file.CopyToAsync(Fs);
                        ViewData["NotiArch"] = "The Selected File " + file.FileName + " Is Uploaded Succesfully";
                    }

                    try
                    {
                        //ENCRIPTADO DE ARCHIVO
                        archivoBytes = algoritmo.EncriptarArchivo(Fileupload, textollave);
                        try
                        {
                            Mensaje = "Encriptado Correctamente";
                            ViewData["NotiArch"] = "Encriptado Correctamente";
                            //OnPostDownload(Fileupload);

                            var fileName = file.FileName;
                            var filePath = Path.Combine(_he.ContentRootPath, "Files", fileName);
                            var fileExists = System.IO.File.Exists(filePath);
                            var fs = System.IO.File.OpenRead(filePath);
                            return File(fs, "application/force-download", fileName);

                        }
                        catch (Exception ex)
                        {
                            Mensaje = "Error al descargar: " + ex;
                        }

                    }
                    catch (Exception ex)
                    {
                        Mensaje = "Error al Encriptar: " + ex.Message.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Mensaje = "Error al subir archivo: " + ex.Message.ToString();
                }
            else
            {
                Mensaje = "You have not specified a file.";
            }

            return RedirectToPage();
            //return RedirectToAction("");
        }

        /*public FileResult Download(string path)
        {
            string filename = Path.GetFileName(path);
            return File(path, "application/force-download", filename);
        }*/

        /*public FileResult OnPostDownload(string path)
        {
            var fileName = Path.GetFileName(path);
            var filePath = Path.Combine(_he.ContentRootPath, "Files", fileName);
            var fileExists = System.IO.File.Exists(filePath);
            var fs = System.IO.File.OpenRead(filePath);

            return File(fs, "application/force-download", fileName);
        }

        */
        /*
        public FileResult OnPostDownload()
        {
            var fileName = "hola.txt";
            var filePath = Path.Combine(_he.ContentRootPath, "Files", "hola.txt");
            var fileExists = System.IO.File.Exists(filePath);
            var fs = System.IO.File.OpenRead(filePath);
            return File(fs, "application/force-download", fileName);
        }*/
        public async Task<ActionResult> OnPostDesencriptar(IFormFile file, string textollave)
        {
            if (file != null && file.Length > 0)
                try
                {
                    var Fileupload = Path.Combine(_he.ContentRootPath, "Files", file.FileName);
                    using (var Fs = new FileStream(Fileupload, FileMode.Create))
                    {
                        await file.CopyToAsync(Fs);
                        ViewData["NotiArch"] = "The Selected File " + file.FileName + " Is Uploaded Succesfully";
                    }

                    try
                    {
                        //ENCRIPTADO DE ARCHIVO
                        archivoBytes = algoritmo.DesencriptarArchivo(Fileupload, textollave);
                        try
                        {
                            Mensaje = "Desencriptado Correctamente";
                            ViewData["NotiArch"] = "Desencriptado Correctamente";
                            //OnPostDownload(Fileupload);

                            var fileName = file.FileName;
                            var filePath = Path.Combine(_he.ContentRootPath, "Files", fileName);
                            var fileExists = System.IO.File.Exists(filePath);
                            var fs = System.IO.File.OpenRead(filePath);
                            return File(fs, "application/force-download", fileName);

                        }
                        catch (Exception ex)
                        {
                            Mensaje = "Error al descargar: " + ex;
                        }

                    }
                    catch (Exception ex)
                    {
                        Mensaje = "Error al Encriptar: " + ex.Message.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Mensaje = "Error al subir archivo: " + ex.Message.ToString();
                }
            else
            {
                Mensaje = "You have not specified a file.";
            }

            return RedirectToPage();
            //return RedirectToAction("");
        }
    }
}
