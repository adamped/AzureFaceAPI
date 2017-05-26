using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFaceAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var azureFaceAPIKey = "XXXXXXXXXXXXXXX"; // Your API Key
            var url = "https://westus.api.cognitive.microsoft.com/face/v1.0";

            Task.Run(async () =>
            {
                var client = new Microsoft.ProjectOxford.Face.FaceServiceClient(azureFaceAPIKey, url);
              
                var stream = new FileStream(@"D:\sample\images\dj1.jpg", FileMode.Open, FileAccess.Read);

                var face1 = await client.DetectAsync(stream);

                stream = new FileStream(@"D:\sample\images\dj2.jpg", FileMode.Open, FileAccess.Read);

                var face2 = await client.DetectAsync(stream);
                               
                var result = await client.VerifyAsync(face1[0].FaceId, face2[0].FaceId);

                Console.WriteLine(result.Confidence);

            }).ContinueWith((t) =>
            {
                Console.WriteLine(t.Exception.Message);
            }, TaskContinuationOptions.OnlyOnFaulted);

            Console.ReadLine();
        }
    }
}
