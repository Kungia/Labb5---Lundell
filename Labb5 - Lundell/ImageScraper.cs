using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labb5___Lundell
{
    public partial class ImageScraper : Form
    {
        long img = 0;
        public ImageScraper()
        {
            InitializeComponent();
        }

        private void ExtractBtn_Click(object sender, EventArgs e)
        {
            string pattern = "<img.*src=\"(.*?)\"";
            string pattern2 = @"(?<=src="")(.*?)(?="")";
            /*När man trycker på knappen (eller enter) så laddar programmet
             ner HTML-koden från den URL man matat in, söker igenom denna efter länkar till
            bilder och visar alla länkar den hittar i en multiline TextBox. Det ska även finnas en
            Label som visar hur många bild-länkar som hittades på sidan.*/
            var UserLink = Linkbox.Text;
            using (WebClient client = new WebClient())
            {
                //< img class=”myclass” src=”/example-image.png”><img class=”myclass” src=”/example-image.png”> ---ExempelHTML
                string htmlCode = client.DownloadString(UserLink);
                Regex rgx = new Regex(pattern);
                Regex rgx2 = new Regex(pattern2);

                foreach (Match match in rgx.Matches(htmlCode))
                {
                    string match1 = match.ToString();
                    foreach (Match match2 in rgx.Matches(match1))
                    {
                        ImagesBox.AppendText($"{match2.Value}");
                    }                                     
                    img++;
                }
                     
                if (img == 0)
                {
                    ImgCount.Text = $"We could not find any images in your quest for image domination";
                }
                if (img == 1)
                {
                    ImgCount.Text = $"{img} image found in your quest for image domination";
                }
                else
                {
                    ImgCount.Text = $"{img} images found in your quest for image domination";
                }
               
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
          /*Sedan ska det finnas en knapp ”Save Images” som öppnar en
            -FolderBrowserDialog där man kan välja en mapp dit man vill spara alla bilderna.
            När man valt en mapp (och klickat ”OK”) så ska programmet asynkront hämta alla
            bilder via länkarna och spara ner till filer i den ordning nedladdningarna blir klara.
            Detta görs genom att skapa en ny Task för varje nedladdning och sedan använda
            Task.WhenAny() för att hantera varje Task när den blir klar. När en Task är klar tar
            ni det binärdata som den laddat ner från länken och skriver rakt av till en binär fil.
            Ni kan döpa filerna t.ex Image1, Image2 etc. men tänk på att filändelsen måste
            matcha datat för att filen ska gå att öppna. Så om länken pekar på t.ex en .png fil
            så måste filen heta t.ex Image5.png*/
            saveFileDialog1.ShowDialog();
            
        }
    }
}
