using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace laby
{
    public class RoleWyswietl:DataTemplateSelector
    {
        public DataTemplate Inne { get; set; }
        public DataTemplate Ceow { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Employee pracownik=(Employee)item;
            if (pracownik.CompanyRole == Role.CEO) return Ceow;
            return Inne;
        }
    }
}
