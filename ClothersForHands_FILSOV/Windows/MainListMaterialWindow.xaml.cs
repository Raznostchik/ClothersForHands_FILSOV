using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using static ClothersForHands_FILSOV.EF.BDAdditional;
using ClothersForHands_FILSOV.EF;
using ClothersForHands_FILSOV.Windows;
using ClothersForHands_FILSOV.HelperClass;

namespace ClothersForHands_FILSOV
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainListMaterialWindow : Window
    {
        List<string> listOrderBy = new List<string>()
        {
            "По-умолчанию",
            "Наименование (по-возрастанию)",
            "Наименование (по-убыванию)",
            "Остаток на складе (по-возрастанию)",
            "Остаток на складе (по-убыванию)",
            "Стоимость (по-возрастанию)",
            "Стоимость (по-убыванию)"
        };
        List<Materials> materials = new List<Materials>();
        List<string> listFilter = new List<string>();
       


        int numberPage = 0;
        
        private Materials selectedMaterial;

        private void Filter()
        {
            materials = BDContent.Materials.ToList();





            var selectSort = cmbSort.SelectedIndex;
            switch (selectSort)
            {
                case 1:
                    materials = materials.OrderBy(i => i.Name).ToList();
                    break;

                case 2:
                    materials = materials.OrderByDescending(i => i.Name).ToList();
                    break;

                case 3:
                    materials = materials.OrderBy(i => i.QuantityOnStorage).ToList();
                    break;

                case 4:
                    materials = materials.OrderByDescending(i => i.QuantityOnStorage).ToList();
                    break;

                case 5:
                    materials = materials.OrderBy(i => i.Cost).ToList();
                    break;

                case 6:
                    materials = materials.OrderByDescending(i => i.Cost).ToList();
                    break;

                default:
                    materials = materials.OrderBy(i => i.idMaterials).ToList();
                    break;
            }

            var selectFilter = cmbFiltr.SelectedIndex;
            if (selectFilter != 0)
            {
                materials = materials.Where(i => i.idTypeMaterials == selectFilter).ToList();
            }

            materials = materials.Where(i => i.Name
    .ToLower()
    .Contains(txtSearch.Text.ToLower()))
    .ToList();
            int countNotesOnFilter = materials.Count();
            materials = materials.OrderBy(i => i.idMaterials).Skip(numberPage * 15).Take(15).ToList();
            MaterialLV.ItemsSource = materials;



            TXTcountNotes.Text = (materials.Count()).ToString() + "  из  " + (countNotesOnFilter).ToString() + " записей";
        }
        public MainListMaterialWindow()
        {   
            InitializeComponent();
            btnEditMinCount.Visibility = Visibility.Hidden;
            btnEditMaterial.Visibility = Visibility.Hidden;
            cmbSort.ItemsSource = listOrderBy;
            cmbSort.SelectedIndex = 0;

            var filterList = BDContent.TypeMaterials.ToList();
            foreach ( var i in filterList)
            {
                listFilter.Add(i.TypeMaterial);
            }

            listFilter.Insert(0, "Все типы");
            cmbFiltr.ItemsSource = listFilter;
            cmbFiltr.SelectedIndex = 0;

            Filter();
        }

       

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (numberPage > 0)
            {
                numberPage--;
            }
            btnGo1.Content = (numberPage + 1).ToString();
            btnGo2.Content = (numberPage + 2).ToString();
            btnGo3.Content = (numberPage + 3).ToString();
            Filter();

                int countPage = (BDContent.Materials.Count() / 15) + 1;
                if (Convert.ToInt32(btnGo2.Content) <= countPage)
                {
                    btnGo2.Visibility = Visibility.Visible;
                }

                if (Convert.ToInt32(btnGo3.Content) <= countPage)
                {
                    btnGo3.Visibility = Visibility.Visible;
                }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (materials.Count >= 15)
            {
                numberPage++;

                btnGo1.Content = (numberPage + 1).ToString();
                btnGo2.Content = (numberPage + 2).ToString();
                btnGo3.Content = (numberPage + 3).ToString();

                int countPage = (BDContent.Materials.Count() / 15) + 1;
                if (Convert.ToInt32(btnGo2.Content) > countPage)
                {
                    btnGo2.Visibility = Visibility.Hidden;
                }

                if (Convert.ToInt32(btnGo3.Content) > countPage)
                {
                    btnGo3.Visibility = Visibility.Hidden;
                }

            }

            Filter();
        }

        private void btnGo2_Click(object sender, RoutedEventArgs e)
        {
            numberPage = Convert.ToInt32(btnGo2.Content) - 1;

            btnGo1.Content = (numberPage + 1).ToString();
            btnGo2.Content = (numberPage + 2).ToString();
            btnGo3.Content = (numberPage + 3).ToString();

            int countPage = (BDContent.Materials.Count() / 15) + 1;
            if (Convert.ToInt32(btnGo2.Content) > countPage)
            {
                btnGo2.Visibility = Visibility.Collapsed;
            }

            if (Convert.ToInt32(btnGo3.Content) > countPage)
            {
                btnGo3.Visibility = Visibility.Collapsed;
            }
            Filter();
        }

        private void btnGo3_Click(object sender, RoutedEventArgs e)
        {
            numberPage = Convert.ToInt32(btnGo3.Content) - 1;

            btnGo1.Content = (numberPage + 1).ToString();
            btnGo2.Content = (numberPage + 2).ToString();
            btnGo3.Content = (numberPage + 3).ToString();

            int countPage = (BDContent.Materials.Count() / 15) + 1;
            if (Convert.ToInt32(btnGo2.Content) > countPage)
            {
                btnGo2.Visibility = Visibility.Collapsed;
            }

            if (Convert.ToInt32(btnGo3.Content) > countPage)
            {
                btnGo3.Visibility = Visibility.Collapsed;
            }
            Filter();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();

        }

        private void cmbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void MaterialLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int biggestMinimum = 0;
            btnEditMinCount.Visibility = Visibility.Visible;
            btnEditMaterial.Visibility = Visibility.Visible;
            foreach (var item in MaterialLV.SelectedItems)
            {
                if (item is Materials materials)
                {
                    if (biggestMinimum < materials.MinimumQuantity)
                    {
                        biggestMinimum = materials.MinimumQuantity;
                    }
                    HelperClass.EditMinMaterial.getMinCount = biggestMinimum;
                    HelperClass.EditMinMaterial.editNote = materials;
                }
            }

        }
        private void btnEditMinCount_Click(object sender, RoutedEventArgs e)
        {
            HelperClass.EditMinMaterial.goEdit = false;
            EditMinimumQuantityWindow editMinWindow = new EditMinimumQuantityWindow();
            editMinWindow.ShowDialog();
            if (HelperClass.EditMinMaterial.goEdit == true)
            {
                foreach (var item in MaterialLV.SelectedItems)
                {
                    if (item is Materials materials)
                    {
                        selectedMaterial = materials;
                        selectedMaterial.MinimumQuantity = HelperClass.EditMinMaterial.getMinCount;
                    }
                }
                BDContent.SaveChanges();
                Filter();
            } else
            {
                Filter();
            }



            
        }

        private void btnAddMaterial_Click(object sender, RoutedEventArgs e)
        {
            AddMaterialWindow addMaterialWindow = new AddMaterialWindow();
            addMaterialWindow.ShowDialog();
            addMaterialWindow.edit = false;
            Filter();
        }

        private void btnEditMaterial_Click(object sender, RoutedEventArgs e)
        {
            AddMaterialWindow editMaterialWindow = new AddMaterialWindow();
            editMaterialWindow.btnSave.Content = "Изменить";
            editMaterialWindow.btnDelete.Visibility = Visibility.Visible;
            editMaterialWindow.edit = true;
            editMaterialWindow.txtPrice.Text = HelperClass.EditMinMaterial.editNote.Cost.ToString();
            editMaterialWindow.txtName.Text = HelperClass.EditMinMaterial.editNote.Name;
            editMaterialWindow.txtMinCount.Text = HelperClass.EditMinMaterial.editNote.MinimumQuantity.ToString();
            editMaterialWindow.txtCountInBox.Text = HelperClass.EditMinMaterial.editNote.QuntityInPack.ToString();
            editMaterialWindow.txtCount.Text = HelperClass.EditMinMaterial.editNote.QuantityOnStorage.ToString();
            editMaterialWindow.cmbTypeMAterial.SelectedIndex = HelperClass.EditMinMaterial.editNote.idTypeMaterials - 1;
            editMaterialWindow.cmbUnitMaterial.SelectedIndex = HelperClass.EditMinMaterial.editNote.idMaterials - 1;
            //editMaterialWindow.imgMaterial.Source = new BitmapImage(new Uri (HelperClass.EditMinMaterial.editNote.Image));
            editMaterialWindow.ShowDialog();
            selectedMaterial = HelperClass.EditMinMaterial.editNote;
            BDContent.SaveChanges();
            Filter();
            
        }
    }
}
