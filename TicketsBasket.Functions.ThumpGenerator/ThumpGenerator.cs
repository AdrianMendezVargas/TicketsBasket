using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace TicketsBasket.Functions.ThumpGenerator
{
    public static class ThumpGenerator
    {
        [FunctionName("ThumbGenerator")]

        //Los parámetros de esta función son proveídos automáticamente. Sirven para capturar el Stream del Blob que se esta insertando en este caso 
        public static void Run([BlobTrigger("images/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, //Representa un input
                               [Blob("images-thumbs/{name}", FileAccess.Write)] Stream thumpStream, //Representa un output. Con este parametro puedo crear un nuevo Stream en el contenedor seleccionado
                               string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            //Check extension
            var allowedExtensions = new[] { ".jpg" , ".png" , ".bmp" };
            string extension = Path.GetExtension(name);
            if (!allowedExtensions.Contains(extension)) {
                log.LogError($"{name} blob is not a valid image");
                return;
            }
            
            //Getting the image uploaded from the stream
            var image = Image.FromStream(myBlob);

            //Calculating the width and height
            int newWidth = 200;
            double ratio = Convert.ToDouble(image.Width) / Convert.ToDouble(image.Height);
            int newHeight = Convert.ToInt32(Math.Round(newWidth / ratio, 0));

            //Creating the thump
            var bitmap = new Bitmap(image);
            var thumpImage = bitmap.GetThumbnailImage(newWidth , newHeight , null , IntPtr.Zero);

            //Saving the image in the output stream with the same format than the input image
            thumpImage.Save(thumpStream , image.RawFormat);

            log.LogInformation($"Thump for {name} has been created with the dimensions {newWidth} x {newHeight}");

        }
    }
}
