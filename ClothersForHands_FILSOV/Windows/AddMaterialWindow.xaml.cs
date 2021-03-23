using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using ClothersForHands_FILSOV.EF;
using Microsoft.Win32;
using static ClothersForHands_FILSOV.EF.BDAdditional;
using static ClothersForHands_FILSOV.HelperClass.EditMinMaterial;

namespace ClothersForHands_FILSOV.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddMaterialWindow.xaml
    /// </summary>
    public partial class AddMaterialWindow : Window
    {
        string pathPhoto;
        public bool edit;
        public AddMaterialWindow()
        {
            InitializeComponent();
            btnDelete.Visibility = Visibility.Hidden;
            cmbTypeMAterial.ItemsSource = BDContent.TypeMaterials.ToList();
            cmbTypeMAterial.DisplayMemberPath = "TypeMaterial";
            cmbTypeMAterial.SelectedIndex = 0;

            cmbUnitMaterial.ItemsSource = BDContent.Unit.ToList();
            cmbUnitMaterial.DisplayMemberPath = "NameUnit";
            cmbUnitMaterial.SelectedIndex = 0;
        }

        private void btnChooseImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();
            pathPhoto = openFile.FileName;
            imgMaterial.Source = new BitmapImage(new Uri(pathPhoto));
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(txtMinCount.Text) < 0 || Convert.ToInt32(txtPrice.Text) < 0)
            {
                MessageBox.Show("Не должно быть отрицательных чисел");
                txtPrice.Text = HelperClass.EditMinMaterial.editNote.Cost.ToString();
                txtMinCount.Text = HelperClass.EditMinMaterial.editNote.MinimumQuantity.ToString();
                
            }
            else if (edit == true)

            {
                HelperClass.EditMinMaterial.editNote.Name = txtName.Text;
                HelperClass.EditMinMaterial.editNote.QuntityInPack = Convert.ToInt32(txtCountInBox.Text);
                HelperClass.EditMinMaterial.editNote.QuantityOnStorage = Convert.ToInt32(txtCount.Text);
                HelperClass.EditMinMaterial.editNote.idTypeMaterials = cmbTypeMAterial.SelectedIndex + 1;
                HelperClass.EditMinMaterial.editNote.idMaterials = cmbUnitMaterial.SelectedIndex + 1;
                HelperClass.EditMinMaterial.editNote.MinimumQuantity = Convert.ToInt32(txtMinCount.Text);
                Close();
            }
            else
            {
                Materials materials = new Materials();
                materials.Cost = Convert.ToInt32(txtPrice.Text);
                materials.Image = pathPhoto;
                materials.idUnit = cmbUnitMaterial.SelectedIndex + 1;
                materials.MinimumQuantity = Convert.ToInt32(txtMinCount.Text);
                materials.Name = txtName.Text;
                materials.QuantityOnStorage = Convert.ToInt32(txtCount.Text);
                materials.QuntityInPack = Convert.ToInt32(txtCountInBox.Text);
                materials.idTypeMaterials = cmbTypeMAterial.SelectedIndex + 1;
                materials.Image = pathPhoto;
                BDContent.Materials.Add(materials);
                try
                {
                    BDContent.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Console.WriteLine("Объект: {0} Состав ошибки: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }

               
                Close();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            BDContent.Materials.Remove(HelperClass.EditMinMaterial.editNote);
            Close();
        }
    }
}
