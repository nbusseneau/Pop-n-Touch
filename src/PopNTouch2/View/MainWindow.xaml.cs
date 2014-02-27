using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;

namespace PopNTouch2.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : SurfaceWindow
    {
        public MainWindow()
        {
            //InitializeComponent();
        }

        public void Button_AddTab_Click(object sender, EventArgs e) { }
        public void Button_ChooseSong_Click(object sender, EventArgs e) { }
        public void Button_EraseAll_Click(object sender, EventArgs e) { }
        public void Button_Start_Click(object sender, EventArgs e) { }
        public void Button_PlayStop_Unchecked(object sender, EventArgs e) { }
        public void Button_PlayStop_Checked(object sender, EventArgs e) { }
    }
}
