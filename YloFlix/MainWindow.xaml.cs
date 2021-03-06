﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace YloFlix
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += MainWindow_DragEnter;
        }

        private void MainWindow_DragOver(object sender, DragEventArgs e)
        {
            //Utils.log("DragOver");
        }

        private void MainWindow_DragLeave(object sender, DragEventArgs e)
        {
            //Utils.log("DragLeave");
        }

        private void MainWindow_DragEnter(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                string fileName = file.Split(Path.DirectorySeparatorChar).Last();
                try
                {
                    Episode episode = Utils.ConvertStrToEpisode(fileName);
                    Utils.Log(episode.toAddictedUrl());
                    var rIO = new RemoteIO(episode.toAddictedUrl());
                    var tmp = rIO.Cache();
                    var fileReader = new FileReader(tmp);
                    fileReader.PutFileInMemory();
                    var parser = new Parser(fileReader, episode);
                    string url = parser.GetDownloadLink();
                    url = string.Concat("http://www.addic7ed.com", url);
                    Utils.Log(url);
                    string tmpSrt = rIO.Download(url);
                    string subtitlePath = Path.ChangeExtension(file, ".srt");
                    File.Move(tmpSrt, subtitlePath);
                }
                catch (Exception ex)
                {
                    Utils.Log(ex.Message);
                }
            }
        }
    }
}
