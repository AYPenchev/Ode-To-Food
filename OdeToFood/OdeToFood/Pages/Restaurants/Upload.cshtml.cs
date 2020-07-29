using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OdeToFood.Pages.Restaurants
{
    public class UploadModel : PageModel
    {
        private IHostingEnvironment _environment;
        public UploadModel(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public async Task OnPostAsync()
        {
            var file = Path.Combine(_environment.ContentRootPath, "uploads", Upload.FileName);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;

            using (var fileStream = System.IO.File.OpenText("restaurant.xml"))
            using (XmlReader reader = XmlReader.Create(fileStream, settings))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            Console.WriteLine($"Start Element: {reader.Name}. Has Attributes? : {reader.HasAttributes}");
                            break;
                        case XmlNodeType.Text:
                            Console.WriteLine($"Inner Text: {reader.Value}");
                            break;
                        case XmlNodeType.EndElement:
                            Console.WriteLine($"End Element: {reader.Name}");
                            break;
                        default:
                            Console.WriteLine($"Unknown: {reader.NodeType}");
                            break;
                    }
                }
            }
        }
        public void OnGet()
        {

        }
    }
}