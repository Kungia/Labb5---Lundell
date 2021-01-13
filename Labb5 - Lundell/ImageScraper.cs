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
        List<string> linkList = new List<string>();
        long img = 0;
        public ImageScraper()
        {
            InitializeComponent();
            ImagesBox.Text = $"Welcome to this Image Scraper In Search Of Image Domination. {Environment.NewLine}{Environment.NewLine}" +
                    $"This app only accepts links in the format of 'gp.se or 'https://www.gp.se/' {Environment.NewLine}" +
                    $"This app can not guarantee performance on any other page than GP.SE.{Environment.NewLine}{Environment.NewLine}" +
                    $"I hope you will enjoy using this this Image Scraper In Search Of Image Domination.";
            Linkbox.Select();
            Refresh();
        }

        private void ExtractBtn_Click(object sender, EventArgs e)
        {
            try
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
                            linkList.Add(link1);
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
                    SaveBtn.Select();
                }
            }
            catch (Exception)
            {
                ImagesBox.Text = $"There was something wrong with your link.{Environment.NewLine}" +
                    $"This app only accepts links in the format of 'gp.se or 'https://www.gp.se/'{Environment.NewLine}" +
                    $"Please, try again";
                Linkbox.Text = "";
                Linkbox.Select();
            }
        }

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            long downCount = 0;
            string path = "";
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    path = fbd.SelectedPath;
                }
            }

            if (!string.IsNullOrWhiteSpace(path))
            {
                foreach (var link in linkList)
                {
                    Task<byte[]> task = httpclient.GetByteArrayAsync(link);
                    taskList.Add(task);
                }

                while (taskList.Count > 0)
                {
                    Task<byte[]> finishedTask = (Task<byte[]>)await Task.WhenAny(taskList);
                    byte[] imgByteArr = finishedTask.Result;

                    if (finishedTask.Exception != null)
                    {
                        ImagesBox.Text = finishedTask.Exception.Message;
                    }
                    taskList.Remove(finishedTask);

                    try
                    {
                        if (imgByteArr.Length > 0 && Png(imgByteArr) == true)
                        {
                            FileStream f1;
                            f1 = new FileStream($@"{path}\image{downCount}.png", FileMode.Create, FileAccess.Write);
                            await f1.WriteAsync(imgByteArr, 0, imgByteArr.Length);
                            f1.Close();
                            downCount++;
                            downloadcount.Text = $"{downCount}out of {img} succesfully downloaded";
                        }
                        else if (imgByteArr.Length > 0)
                        {
                            FileStream f1;
                            f1 = new FileStream($@"{path}\image{downCount}.jpg", FileMode.Create, FileAccess.Write);
                            await f1.WriteAsync(imgByteArr, 0, imgByteArr.Length);
                            f1.Close();
                            downCount++;
                            downloadcount.Text = $"{downCount} out of {img} images succesfully downloaded";
                        }
                    }
                    catch (Exception)
                    {
                        ImagesBox.Text = "";
                        Linkbox.Select();
                        throw;
                    }
                    ImagesBox.Text = "";
                    Linkbox.Select();
                }
            }
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

