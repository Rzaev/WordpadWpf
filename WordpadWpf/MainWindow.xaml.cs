using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Windows.Shapes;

namespace WordpadWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        public string FileName { get; set; }

        private string SetText()
        {
            return txtEditor.Text;
           

        }
        private int SetStart()
        {
            return txtEditor.SelectionStart;
        }

        private int SetiLength()
        {
            return txtEditor.SelectionLength; ;
        }

        private void CursorPosition()
        {
            txtEditor.Text = SetText();
            txtEditor.SelectionStart = SetStart();
            txtEditor.SelectionLength = SetiLength();
            txtEditor.Focus();
        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Width = 1100;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                txtEditor.Text = File.ReadAllText(openFileDialog.FileName);


            FileName = openFileDialog.FileName;
            try
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                PathTxt.Text = System.IO.Path.GetFullPath(FileName);
            }
            catch (System.Exception)
            {
            }
        }

        private void CopyBtn_Click(object sender, RoutedEventArgs e)
        {
            txtEditor.Copy();
            CursorPosition();
        }

        private void PasteBtn_Click(object sender, RoutedEventArgs e)
        {
            txtEditor.Paste();

            CursorPosition();
        }


        private void LoadSave()
        {
            SaveFileDialog save = new SaveFileDialog();
            if (save.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(save.FileName))
                {
                    writer.Write(txtEditor.Text);
                }
            }
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadSave();
        }

        private void CutBtn_Click(object sender, RoutedEventArgs e)
        {
            txtEditor.Cut();
            CursorPosition();
       
        }

        private void SelectAllBtn_Click(object sender, RoutedEventArgs e)
        {
            txtEditor.SelectAll();
            CursorPosition();
        }

        private void AutoSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AutoSave_Checked(object sender, RoutedEventArgs e)
        {
           
            SaveFileDialog save = new SaveFileDialog();

            if (save.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(save.FileName))
                {
                    writer.Write(txtEditor.Text);
                }
            }
        }

        private void txtEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AutoSave.IsChecked == true)
                File.WriteAllText(FileName, txtEditor.Text);

         
        }
    }
}
