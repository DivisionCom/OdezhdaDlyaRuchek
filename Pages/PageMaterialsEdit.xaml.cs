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
    /// Interaction logic for PageMaterialsEdit.xaml
    /// </summary>
    public partial class PageMaterialsEdit : Page
    {
        private PageMaterialsTable _page;
        private Materials _materials;
        public PageMaterialsEdit(object o, PageMaterialsTable pageMaterialsTable)
        {
            InitializeComponent();
            _page = pageMaterialsTable;
            _materials = (o as Button).DataContext as Materials;

            cmbMaterialType.ItemsSource = ConnectOdb.conObj.MaterialType.ToList();

            tboxName.Text = _materials.Name;
            cmbMaterialType.SelectedIndex = _materials.MaterialTypeID - 1;
            tboxWarehouseCount.Text = _materials.WarehouseCount.ToString();
            tboxUnit.Text = _materials.Unit;
            tboxBoxCount.Text = _materials.BoxCount.ToString();
            tboxMinimumCount.Text = _materials.MinimumCount.ToString();
            tboxPrice.Text = _materials.Price.ToString();
            tboxImage.Text = _materials.Image;
            tboxDescription.Text = _materials.Description;
        }
       

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            _materials.Name = tboxName.Text;
            _materials.MaterialTypeID = cmbMaterialType.SelectedIndex + 1;
            _materials.WarehouseCount = Convert.ToInt32(tboxWarehouseCount.Text);
            _materials.Unit = tboxUnit.Text;
            _materials.BoxCount = Convert.ToInt32(tboxBoxCount.Text);
            _materials.MinimumCount = Convert.ToInt32(tboxMinimumCount.Text);
            _materials.Price = Convert.ToInt32(tboxPrice.Text);
            _materials.Image = tboxImage.Text;
            _materials.Description = tboxDescription.Text;

            ConnectOdb.conObj.SaveChanges();
            _page.RefreshTable();
            MessageBox.Show("Данные успешно обновлены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            FrameObj.frameMain.GoBack();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы действительно хотите удалить редактируемый материал?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ConnectOdb.conObj.Materials.Remove(_materials);
                ConnectOdb.conObj.SaveChanges();

                _page.RefreshTable();
                MessageBox.Show("Материал успешно удален", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                FrameObj.frameMain.GoBack();
            }
        }
    }
}
