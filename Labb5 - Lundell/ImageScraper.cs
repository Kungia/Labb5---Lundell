using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labb5___Lundell
{
    public partial class ImageScraper : Form
    {
        WebClient client = new WebClient();
        HttpClient httpclient = new HttpClient();
        List<Task> taskList = new List<Task>();
        long img = 0;
        public ImageScraper()
        {
            InitializeComponent();
            Linkbox.Select();
        }

        private void ExtractBtn_Click(object sender, EventArgs e)
        {
            string pattern = "<img.*src=\"(.*?)\"";
            string pattern2 = @"(?<=src="")(.*?)(?="")";
            string httpCheck = "https://";
            var UserLink = Linkbox.Text;

            if (!UserLink.Contains(httpCheck))
            {
                UserLink = httpCheck + UserLink;
            }
            using (client)
            {
                string htmlCode = client.DownloadString(UserLink);
                Regex rgx = new Regex(pattern);
                Regex rgx2 = new Regex(pattern2);

                foreach (Match match in rgx.Matches(htmlCode))
                {
                    string match1 = match.ToString();
                    foreach (Match match2 in rgx2.Matches(match1))
                    {

                        string link1 = ($"{match2.Value}");

                        if (!link1.Contains(httpCheck))
                        {
                            link1 = UserLink + link1;
                        }
                        ImagesBox.AppendText($"{link1}{Environment.NewLine}{Environment.NewLine}");
                        Task < byte[]> task = httpclient.GetByteArrayAsync(link1);
                        taskList.Add(task);
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

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            long downCount = 0;
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath)) ;

                while (taskList.Count > 0)
                {
                    Task<byte[]> finishedTask = (Task<byte[]>)await Task.WhenAny(taskList);
                    byte[] imgByteArr = finishedTask.Result;

                    if (imgByteArr.Length > 0 && Png(imgByteArr) == true)
                    {
                        FileStream f1;
                        f1 = new FileStream($"{fbd.SelectedPath}image{downCount}.png", FileMode.Create, FileAccess.Write);
                        f1.WriteAsync(imgByteArr, 0, imgByteArr.Length);
                    }
                    else if (imgByteArr.Length > 0)
                    {
                        FileStream f1;
                        f1 = new FileStream($"{fbd.SelectedPath}image{downCount}.jpg", FileMode.Create, FileAccess.Write);
                        f1.WriteAsync(imgByteArr, 0, imgByteArr.Length);
                    }
                    downCount++;
                }
            }

            /*
              Använd WriteAsync() på FileStream objektet för att asynkront skriva data till fil.
              Om någon av dina metoder använder await, se till att använda await i anropande
              metoder hela vägen tillbaks till(inklusive) metoden som triggades av ett event.
              Event handlers metoder är vanligtvis av typ void. 
              Detta är egentligen det enda fall
              där det är okej att använda async void. (annars använder ni async Task eller async
              Task<T>).

              När en Task avslutats kan ni kolla om den kastat någon exception med
              task.Exception(t.ex.om en bild inte kunde laddas ner för att en länk var felaktig).*/

        }
        static bool Png(Byte[] imgByteArr)
        {
            string pngId = "";
            for (int i = 0; i < 8; i++)
            {
                pngId += imgByteArr[i].ToString("X2");
            }

            if (pngId == "89504E470D0A1A0A")
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
