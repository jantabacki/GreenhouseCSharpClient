using GreenhouseUIClient.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GreenhouseUIClient.Controls
{
    /// <summary>
    /// Interaction logic for EntityUserControl.xaml
    /// </summary>
    public partial class EntityUserControl : UserControl
    {
        public static readonly DependencyProperty EntityProperty = 
            DependencyProperty.Register("Entity", 
                typeof(Entity), 
                typeof(EntityUserControl), 
                new PropertyMetadata(new Entity(0)));

        public Entity Entity
        {
            get
            {
                return (Entity)GetValue(EntityProperty);
            }
            set
            {
                SetValue(EntityProperty, value);
            }
        }

        public EntityUserControl()
        {
            InitializeComponent();
        }
    }
}
