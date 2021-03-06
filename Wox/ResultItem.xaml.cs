﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wox.Annotations;
using Wox.Helper;
using Wox.Plugin;
using Brush = System.Windows.Media.Brush;

namespace Wox
{
    public partial class ResultItem : UserControl, INotifyPropertyChanged
    {

        private bool selected;

        public Result Result { get; private set; }

        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                if (selected)
                {
                    img.Visibility = Visibility.Visible;
                    img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Images\\enter.png"));
                }
                else
                {
                    img.Visibility = Visibility.Hidden;
                }
                OnPropertyChanged("Selected");
            }
        }

        public void SetIndex(int index)
        {
            tbIndex.Text = index.ToString();
        }

        public ResultItem(Result result)
        {
            InitializeComponent();
            Result = result;

            tbTitle.Text = result.Title;
            tbSubTitle.Text = result.SubTitle;
            string path = string.Empty;
            if (!string.IsNullOrEmpty(result.IcoPath) && result.IcoPath.Contains(":\\") && File.Exists(result.IcoPath))
            {
                path = result.IcoPath;
            }
            else if (!string.IsNullOrEmpty(result.IcoPath) && File.Exists(result.PluginDirectory + result.IcoPath))
            {
                path = result.PluginDirectory + result.IcoPath;
            }

            if (!string.IsNullOrEmpty(path))
            {
                if (path.ToLower().EndsWith(".exe") || path.ToLower().EndsWith(".lnk"))
                {
                    imgIco.Source = GetIcon(path);
                }
                else
                {
                    imgIco.Source = new BitmapImage(new Uri(path));
                }
            }

            AddHandler(MouseLeftButtonUpEvent, new RoutedEventHandler((o, e) =>
            {
                Result.Action();
                CommonStorage.Instance.UserSelectedRecords.Add(result);
                if (!result.DontHideWoxAfterSelect)
                {
                    App.Window.HideApp();
                }
                e.Handled = true;
            }));
        }

        private static ImageSource GetIcon(string fileName)
        {
            Icon icon = Icon.ExtractAssociatedIcon(fileName);
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                        icon.Handle,
                        new Int32Rect(0, 0, icon.Width, icon.Height),
                        BitmapSizeOptions.FromEmptyOptions());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
