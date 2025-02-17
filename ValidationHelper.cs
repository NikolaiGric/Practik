using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Practik
{
    public static class ValidationHelper
    {
        public static bool ValidateWarehouseInput(TextBox productName, TextBox quantity,
                                                  ComboBox comboBoxStatus, ComboBox comboBoxSection,
                                                  ComboBox comboShelf, ComboBox comboRack)
        {
            if (string.IsNullOrWhiteSpace(productName.Text) ||
                string.IsNullOrWhiteSpace(quantity.Text) ||
                comboBoxStatus.SelectedItem == null ||
                comboBoxSection.SelectedItem == null ||
                comboShelf.SelectedItem == null ||
                comboRack.SelectedItem == null)
            {
                MessageBox.Show("Заполните все обязательные поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            int qty;
            if (!int.TryParse(quantity.Text, out qty) || qty < 0)
            {
                MessageBox.Show("Некорректный формат количества!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
