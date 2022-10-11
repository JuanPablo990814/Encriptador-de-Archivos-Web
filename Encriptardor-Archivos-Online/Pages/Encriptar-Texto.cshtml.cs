using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Encriptardor_Archivos_Online.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }
        //Variables
        public string accion;
        public string contenido;

        //Objeto
        Encriptador.DEncriptador algoritmo = new Encriptador.DEncriptador(); 
        
        //Variable Temporal
        [TempData]
        public string Mensaje { get; set; }

        [TempData]
        public string textoDE { get; set; }


        public void OnGet()
        {
        }

        public IActionResult OnPostEncriptar(string textocontenido,string textollave)
        {
            textocontenido = algoritmo.EncriptarTexto(textocontenido, textollave);
            Mensaje = "El texto ha sido encriptado";
            textoDE = textocontenido;
            return RedirectToPage("Encriptar-Texto");
        }

        public IActionResult OnPostDesencriptar(string textocontenido, string textollave)
        {
            textocontenido = algoritmo.DesencriptarTexto(textocontenido,textollave);
            Mensaje = "El texto ha sido desencriptado";
            textoDE = textocontenido;
            return RedirectToPage("Encriptar-Texto");
        }
    }
}
