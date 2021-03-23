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
using System.Windows.Shapes;
using ClothersForHands_FILSOV.EF;
using ClothersForHands_FILSOV.Windows;
using static ClothersForHands_FILSOV.EF.BDAdditional;

namespace ClothersForHands_FILSOV.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditMinimumQuantityWindow.xaml
    /// </summary>
    public partial class EditMinimumQuantityWindow : Window
    {
        public EditMinimumQuantityWindow()
        {
            InitializeComponent();
            txtMinCount.Text = HelperClass.EditMinMaterial.getMinCount.ToString();


        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var resultMsg = MessageBox.Show("Нажмите Да для подтверждения", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultMsg == MessageBoxResult.Yes)
            {
                HelperClass.EditMinMaterial.getMinCount = Int32.Parse(txtMinCount.Text);
                HelperClass.EditMinMaterial.goEdit = true;
                Close();
            } else
            {
                HelperClass.EditMinMaterial.goEdit = false;
            }
        }
    }
}
