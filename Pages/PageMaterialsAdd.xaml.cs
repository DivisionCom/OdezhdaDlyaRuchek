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
    /// Interaction logic for PageMaterialsAdd.xaml
    /// </summary>
    public partial class PageMaterialsAdd : Page
    {
        private PageMaterialsTable _page;
        public PageMaterialsAdd(PageMaterialsTable pageMaterialsTable)
        {
            InitializeComponent();

            _page = pageMaterialsTable;

            cmbMaterialType.ItemsSource = ConnectOdb.conObj.MaterialType.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ConnectOdb.conObj.Materials.Add(new Materials()
            {
                Name = tboxName.Text,
                MaterialTypeID = cmbMaterialType.SelectedIndex + 1,
                WarehouseCount = Convert.ToInt32(tboxWarehouseCount.Text),
                Unit = tboxUnit.Text,
                BoxCount = Convert.ToInt32(tboxBoxCount.Text),
                MinimumCount = Convert.ToInt32(tboxMinimumCount.Text),
                Price = Convert.ToInt32(tboxPrice.Text),
                Image = tboxImage.Text,
                Description = tboxDescription.Text
            });
            ConnectOdb.conObj.SaveChanges();
            _page.RefreshTable();

            MessageBox.Show("Данные успешно добавлены!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

            FrameObj.frameMain.GoBack();
        }
    }
}
