using OdezhdaDlyaRuchek.AppData;
using System;
using System.Collections.Generic;
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

namespace OdezhdaDlyaRuchek.Pages
{
    /// <summary>
    /// Interaction logic for PageMaterialsTable.xaml
    /// </summary>
    public partial class PageMaterialsTable : Page
    {
        public PageMaterialsTable()
        {
            InitializeComponent();

            RefreshTable();
        }

        public void RefreshTable()
        {
            DataGridMaterials.ItemsSource = ConnectOdb.conObj.Materials.ToList();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.frameMain.Navigate(new PageMaterialsEdit(sender, this));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.frameMain.Navigate(new PageMaterialsAdd(this));
        }
    }
}
