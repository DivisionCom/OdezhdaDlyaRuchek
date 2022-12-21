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
    /// Interaction logic for PageMaterials.xaml
    /// </summary>
    public partial class PageMaterials : Page
    {
        private List<Materials> _materials = new List<Materials>();
        private string _selectedType;
        private string _findedName;
        private int _currentPage = 1;
        private int _maxPage = 1;
        private int _currentCount;
        private int _maxCount;
        public PageMaterials()
        {
            InitializeComponent();
            listMaterials.ItemsSource = ConnectOdb.conObj.Materials.ToList();

            List<Materials> sorting = new List<Materials>();
            sorting.Add(new Materials() { Name = "Наименование" });
            sorting.Add(new Materials() { Name = "Остаток на складе" });
            sorting.Add(new Materials() { Name = "Стоимость" });
            cmbSotring.ItemsSource = sorting;

            List<MaterialType> type = new List<MaterialType>();
            type.Add(new MaterialType() { Name = "Все типы" });
            type.AddRange(ConnectOdb.conObj.MaterialType.ToList());
            cmbFilering.ItemsSource = type;

            _materials = ConnectOdb.conObj.Materials.ToList();
            RefreshMaterials();
        }

        public void UpdateMaterials()
        {
            if (cmbFilering.SelectedItem != null)
            {
                if ((cmbFilering.SelectedItem as MaterialType).Name != "Все типы")
                {
                    _materials = _materials.Where(m => m.MaterialTypeID == cmbFilering.SelectedIndex).ToList();
                }
                else if ((cmbFilering.SelectedItem as MaterialType).Name == "Все типы")
                {
                    _materials = ConnectOdb.conObj.Materials.ToList();
                }
            }

            if (cmbSotring.SelectedIndex >= 0)
            {
                if ((cmbSotring.SelectedItem as Materials).Name == "Наименование")
                {
                    _materials = _materials.OrderBy(m => m.Name).ToList();
                }
                else if ((cmbSotring.SelectedItem as Materials).Name == "Остаток на складе")
                {
                    _materials = _materials.OrderBy(m => m.WarehouseCount).ToList();
                }
                else if ((cmbSotring.SelectedItem as Materials).Name == "Стоимость")
                {
                    _materials = _materials.OrderBy(m => m.Price).ToList();
                }
            }

            if (tboxSearch.Text != "")
            {
                _materials = _materials.Where(m => m.Name.ToLower().Contains(tboxSearch.Text.ToLower()) || m.Description.ToLower().Contains(tboxSearch.Text.ToLower())).ToList();
            }
            else
            {
                _materials = _materials.ToList();
            }

            listMaterials.ItemsSource = _materials;
        }

        public void RefreshMaterials()
        {
            listMaterials.ItemsSource = ConnectOdb.conObj.Materials.ToList();
            _maxPage = (int)Math.Ceiling(_materials.ToList().Count * 1.0 / 15);
            _maxCount = ConnectOdb.conObj.Materials.ToList().Count();
            var pushListMaterials = _materials.ToList().Skip((_currentPage - 1) * 15).Take(15).ToList();

            _currentCount = _materials.ToList().Count();

            tblockPaginationPages.Text = _currentPage.ToString() + " из " + _maxPage.ToString();
            tblockDataCount.Text = _currentCount.ToString() + " из " + _maxCount.ToString();
            

            listMaterials.ItemsSource = pushListMaterials;

        }

        private void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            RefreshMaterials();
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
                _currentPage--;
            else
                return;
            RefreshMaterials();
        }

        private void btnForwardPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < _maxPage)
                _currentPage++;
            else
                return;
            RefreshMaterials();
        }

        private void btnLastPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = _maxPage;
            RefreshMaterials();
        }

        private void cmbFilering_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _materials = ConnectOdb.conObj.Materials.ToList();
            UpdateMaterials();
            RefreshMaterials();
        }

        private void tboxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            _materials = ConnectOdb.conObj.Materials.ToList();
            UpdateMaterials();
            RefreshMaterials();
        }

        private void cmbSotring_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _materials = ConnectOdb.conObj.Materials.ToList();
            UpdateMaterials();
            RefreshMaterials();
        }
    }
}
