using System;
using System.Text.RegularExpressions;
using System.Windows;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace YT_DL_02
{
    public partial class MainWindow : Window
    {
        private readonly YoutubeClient youtube;

        public MainWindow()
        {
            InitializeComponent();

            // i made this =_= for the MR.blackWindow
            youtube = new YoutubeClient();
        }
        // when we click on the " FetchInfoButton " we do the following!
        private async void FetchInfoButton_Click(object sender, RoutedEventArgs e)
        {
            // i made them try-catchh cauuuse why NOT????
            // sometimes maybe good sometimes may be shit </3
            try
            {
                // Extract the video ID from the YouTube link using regular expressions
                // EtractVideoID function helps to get the id DUH
                string videoId = ExtractVideoId(YouTubeLinkTextBox.Text);
                // to download the video

                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoId);
                var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
                await youtube.Videos.Streams.DownloadAsync(streamInfo, $@"C:\Users\iiAka\Desktop\youtubePath\video.{streamInfo.Container}");

                // Retrieve video data
                Video video = await youtube.Videos.GetAsync(videoId);

                // Hellooo!! we put da data here
                VideoTitleLabel.Text = video.Title;
                VideoAuthorLabel.Text = video.Title.ToString();
                VideoDurationLabel.Text = video.Duration.ToString();
                VideoDescriptionTextBox.Text = video.Description;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // dis one MR.GPT did it =_= 
        private string ExtractVideoId(string youtubeLink)
        {
            // Extract the video ID using regular expressions
            string videoId = string.Empty;

            // Regular expression pattern to match YouTube URLs
            string pattern = @"(?:youtu\.be\/|youtube(?:-nocookie)?\.com\/(?:embed\/|v\/|watch\?v=|watch\?.+&v=|embed\/|v\/|shorts\/))([^?&""'>]+)";

            Regex regex = new Regex(pattern, RegexOptions.Compiled);
            Match match = regex.Match(youtubeLink);

            if (match.Success)
            {
                videoId = match.Groups[1].Value;
            }

            return videoId;
        }
    }
}
