using AutoLotModel;
using System;
using System.Data;
using System.Data.Entity;
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

namespace Zakarias_Ovidiu_Lab7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        AutoLotEntitiesModel ctx = new AutoLotEntitiesModel();
        CollectionViewSource customerViewSource;
        CollectionViewSource inventoryViewSource;
        CollectionViewSource customerOrdersViewSource;

        Binding txtFirstNameBinding = new Binding();
        Binding txtLastNameBinding = new Binding();

        Binding txtColorBinding = new Binding();
        Binding txtMakeBinding = new Binding();

        Binding cmbCustomerBinding = new Binding();
        Binding cmbInventoryBinding = new Binding();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            txtFirstNameBinding.Path = new PropertyPath("FirstName");
            txtLastNameBinding.Path = new PropertyPath("LastName");
            firstNameTextBox.SetBinding(TextBox.TextProperty, txtFirstNameBinding);
            lastNameTextBox.SetBinding(TextBox.TextProperty, txtLastNameBinding);

            txtColorBinding.Path = new PropertyPath("Color");
            txtMakeBinding.Path = new PropertyPath("Make");
            colorTextBox.SetBinding(TextBox.TextProperty, txtColorBinding);
            makeTextBox.SetBinding(TextBox.TextProperty, txtMakeBinding);

            cmbCustomerBinding.Path = new PropertyPath("CustId");
            cmbInventoryBinding.Path = new PropertyPath("CarId");
            cmbCustomers.SetBinding(ComboBox.ItemsSourceProperty, cmbCustomerBinding);
            cmbInventory.SetBinding(ComboBox.ItemsSourceProperty, cmbInventoryBinding);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            customerViewSource = (CollectionViewSource)this.FindResource("customerViewSource");
            customerViewSource.Source = ctx.Customers.Local;
           
            inventoryViewSource = (CollectionViewSource)this.FindResource("inventoryViewSource");
            inventoryViewSource.Source = ctx.Inventories.Local;

            customerOrdersViewSource = (CollectionViewSource)this.FindResource("customerOrdersViewSource");
            customerOrdersViewSource.Source = ctx.Orders.Local;

            ctx.Customers.Load();
            ctx.Inventories.Load();
            ctx.Orders.Load();

            cmbCustomers.ItemsSource = ctx.Customers.Local;
            cmbCustomers.DisplayMemberPath = "FirstName";
            cmbCustomers.SelectedValuePath = "CustId";

            cmbInventory.ItemsSource = ctx.Inventories.Local;
            cmbInventory.DisplayMemberPath = "Make";
            cmbInventory.SelectedValuePath = "CarId";
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;

            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = false;
            customerDataGrid.IsEnabled = false;

            firstNameTextBox.IsEnabled = true;
            lastNameTextBox.IsEnabled = true;

            BindingOperations.ClearBinding(firstNameTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(lastNameTextBox, TextBox.TextProperty);
            firstNameTextBox.Text = "";
            lastNameTextBox.Text = "";
            Keyboard.Focus(firstNameTextBox);
        }

        private void btnNewCar_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNewCar.IsEnabled = false;
            btnEditCar.IsEnabled = false;
            btnDeleteCar.IsEnabled = false;

            btnSaveCar.IsEnabled = true;
            btnCancelCar.IsEnabled = true;
            btnPrevCar.IsEnabled = false;
            btnNextCar.IsEnabled = false;
            inventoryDataGrid.IsEnabled = false;

            colorTextBox.IsEnabled = true;
            makeTextBox.IsEnabled = true;

            BindingOperations.ClearBinding(colorTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(makeTextBox, TextBox.TextProperty);
            colorTextBox.Text = "";
            makeTextBox.Text = "";
            Keyboard.Focus(colorTextBox);
        }

        private void btnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNewOrder.IsEnabled = false;
            btnEditOrder.IsEnabled = false;
            btnDeleteOrder.IsEnabled = false;

            btnSaveOrder.IsEnabled = true;
            btnCancelOrder.IsEnabled = true;
            btnPrevOrder.IsEnabled = false;
            btnNextOrder.IsEnabled = false;
            ordersDataGrid.IsEnabled = false;

            cmbCustomers.IsEnabled = true;
            cmbInventory.IsEnabled = true;

            BindingOperations.ClearBinding(cmbCustomers, ComboBox.ItemsSourceProperty);
            BindingOperations.ClearBinding(cmbInventory, ComboBox.ItemsSourceProperty);
            cmbCustomers.SelectedIndex = -1;
            cmbInventory.SelectedIndex = -1;
            Keyboard.Focus(cmbCustomers);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            string tempFirstName = firstNameTextBox.Text.ToString();
            string tempLastName = lastNameTextBox.Text.ToString();

            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;

            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = false;
            customerDataGrid.IsEnabled = false;

            firstNameTextBox.IsEnabled = true;
            lastNameTextBox.IsEnabled = true;

            BindingOperations.ClearBinding(firstNameTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(lastNameTextBox, TextBox.TextProperty);
            firstNameTextBox.Text = tempFirstName;
            lastNameTextBox.Text = tempLastName;
            Keyboard.Focus(firstNameTextBox);
        }

        private void btnEditCar_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            string tempColor = colorTextBox.Text.ToString();
            string tempMake = makeTextBox.Text.ToString();

            btnNewCar.IsEnabled = false;
            btnEditCar.IsEnabled = false;
            btnDeleteCar.IsEnabled = false;

            btnSaveCar.IsEnabled = true;
            btnCancelCar.IsEnabled = true;
            btnPrevCar.IsEnabled = false;
            btnNextCar.IsEnabled = false;
            inventoryDataGrid.IsEnabled = false;

            colorTextBox.IsEnabled = true;
            makeTextBox.IsEnabled = true;

            BindingOperations.ClearBinding(colorTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(makeTextBox, TextBox.TextProperty);
            colorTextBox.Text = tempColor;
            makeTextBox.Text = tempMake;
            Keyboard.Focus(colorTextBox);
        }

        private void btnEditOrder_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            Customer tempCustomer = (Customer)cmbCustomers.SelectedItem;
            Inventory tempInventory = (Inventory)cmbInventory.SelectedItem;

            btnNewOrder.IsEnabled = false;
            btnEditOrder.IsEnabled = false;
            btnDeleteOrder.IsEnabled = false;

            btnSaveOrder.IsEnabled = true;
            btnCancelOrder.IsEnabled = true;
            btnPrevOrder.IsEnabled = false;
            btnNextOrder.IsEnabled = false;
            ordersDataGrid.IsEnabled = false;

            cmbCustomers.IsEnabled = true;
            cmbInventory.IsEnabled = true;

            BindingOperations.ClearBinding(cmbCustomers, ComboBox.ItemsSourceProperty);
            BindingOperations.ClearBinding(cmbInventory, ComboBox.ItemsSourceProperty);
            cmbCustomers.SelectedItem = tempCustomer;
            cmbInventory.SelectedItem = tempInventory;
            Keyboard.Focus(cmbCustomers);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
            string tempFirstName = firstNameTextBox.Text.ToString();
            string tempLastName = lastNameTextBox.Text.ToString();

            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;

            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = false;
            customerDataGrid.IsEnabled = true;

            BindingOperations.ClearBinding(firstNameTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(lastNameTextBox, TextBox.TextProperty);
            firstNameTextBox.Text = tempFirstName;
            lastNameTextBox.Text = tempLastName;
        }

        private void btnDeleteCar_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
            string tempColor = colorTextBox.Text.ToString();
            string tempMake = makeTextBox.Text.ToString();

            btnNewCar.IsEnabled = false;
            btnEditCar.IsEnabled = false;
            btnDeleteCar.IsEnabled = false;

            btnSaveCar.IsEnabled = true;
            btnCancelCar.IsEnabled = true;
            btnPrevCar.IsEnabled = false;
            btnNextCar.IsEnabled = false;
            inventoryDataGrid.IsEnabled = true;

            BindingOperations.ClearBinding(colorTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(makeTextBox, TextBox.TextProperty);
            colorTextBox.Text = tempColor;
            makeTextBox.Text = tempMake;
        }

        private void btnDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
            Customer tempCustomer = (Customer)cmbCustomers.SelectedItem;
            Inventory tempInventory = (Inventory)cmbInventory.SelectedItem;

            btnNewOrder.IsEnabled = false;
            btnEditOrder.IsEnabled = false;
            btnDeleteOrder.IsEnabled = false;

            btnSaveOrder.IsEnabled = true;
            btnCancelOrder.IsEnabled = true;
            btnPrevOrder.IsEnabled = false;
            btnNextOrder.IsEnabled = false;
            ordersDataGrid.IsEnabled = true;

            BindingOperations.ClearBinding(cmbCustomers, ComboBox.ItemsSourceProperty);
            BindingOperations.ClearBinding(cmbInventory, ComboBox.ItemsSourceProperty);
            cmbCustomers.SelectedItem = tempCustomer;
            cmbInventory.SelectedItem = tempInventory;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = null;
            if (action == ActionState.New)
            {
                try
                {
                    customer = new Customer()
                    {
                        FirstName = firstNameTextBox.Text.Trim(),
                        LastName = lastNameTextBox.Text.Trim()
                    };
                    ctx.Customers.Add(customer);
                    customerViewSource.View.Refresh();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;

                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                btnPrev.IsEnabled = true;
                btnNext.IsEnabled = true;
                customerDataGrid.IsEnabled = true;

                firstNameTextBox.IsEnabled = false;
                lastNameTextBox.IsEnabled = false;
            }
            else if (action == ActionState.Edit)
            {
                try
                {
                    customer = (Customer)customerDataGrid.SelectedItem;
                    customer.FirstName = firstNameTextBox.Text.Trim();
                    customer.LastName = lastNameTextBox.Text.Trim();

                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                customerViewSource.View.Refresh();
                customerViewSource.View.MoveCurrentTo(customer);

                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;

                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                customerDataGrid.IsEnabled = true;
                btnPrev.IsEnabled = true;
                btnNext.IsEnabled = true;

                firstNameTextBox.IsEnabled = false;
                lastNameTextBox.IsEnabled = false;

                firstNameTextBox.SetBinding(TextBox.TextProperty, txtFirstNameBinding);
                lastNameTextBox.SetBinding(TextBox.TextProperty, txtLastNameBinding);
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    customer = (Customer)customerDataGrid.SelectedItem;
                    ctx.Customers.Remove(customer);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                customerViewSource.View.Refresh();

                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;

                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                customerDataGrid.IsEnabled = true;
                btnPrev.IsEnabled = true;
                btnNext.IsEnabled = true;

                firstNameTextBox.IsEnabled = false;
                lastNameTextBox.IsEnabled = false;

                firstNameTextBox.SetBinding(TextBox.TextProperty, txtFirstNameBinding);
                lastNameTextBox.SetBinding(TextBox.TextProperty, txtLastNameBinding);
            }
        }

        private void btnSaveCar_Click(object sender, RoutedEventArgs e)
        {
            Inventory inventory = null;
            if (action == ActionState.New)
            {
                try
                {
                    inventory = new Inventory()
                    {
                        Color = colorTextBox.Text.Trim(),
                        Make = makeTextBox.Text.Trim()
                    };
                    ctx.Inventories.Add(inventory);
                    inventoryViewSource.View.Refresh();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                btnNewCar.IsEnabled = true;
                btnEditCar.IsEnabled = true;
                btnDeleteCar.IsEnabled = true;

                btnSaveCar.IsEnabled = false;
                btnCancelCar.IsEnabled = false;
                btnPrevCar.IsEnabled = true;
                btnNextCar.IsEnabled = true;
                inventoryDataGrid.IsEnabled = true;

                colorTextBox.IsEnabled = false;
                makeTextBox.IsEnabled = false;
            }
            else if (action == ActionState.Edit)
            {
                try
                {
                    inventory = (Inventory)inventoryDataGrid.SelectedItem;
                    inventory.Color = colorTextBox.Text.Trim();
                    inventory.Make = makeTextBox.Text.Trim();

                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                inventoryViewSource.View.Refresh();
                inventoryViewSource.View.MoveCurrentTo(inventory);

                btnNewCar.IsEnabled = true;
                btnEditCar.IsEnabled = true;
                btnDeleteCar.IsEnabled = true;

                btnSaveCar.IsEnabled = false;
                btnCancelCar.IsEnabled = false;
                inventoryDataGrid.IsEnabled = true;
                btnPrevCar.IsEnabled = true;
                btnNextCar.IsEnabled = true;

                colorTextBox.IsEnabled = false;
                makeTextBox.IsEnabled = false;

                colorTextBox.SetBinding(TextBox.TextProperty, txtColorBinding);
                makeTextBox.SetBinding(TextBox.TextProperty, txtMakeBinding);
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    inventory = (Inventory)inventoryDataGrid.SelectedItem;
                    ctx.Inventories.Remove(inventory);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                inventoryViewSource.View.Refresh();

                btnNewCar.IsEnabled = true;
                btnEditCar.IsEnabled = true;
                btnDeleteCar.IsEnabled = true;

                btnSaveCar.IsEnabled = false;
                btnCancelCar.IsEnabled = false;
                inventoryDataGrid.IsEnabled = true;
                btnPrevCar.IsEnabled = true;
                btnNextCar.IsEnabled = true;

                colorTextBox.IsEnabled = false;
                makeTextBox.IsEnabled = false;

                colorTextBox.SetBinding(TextBox.TextProperty, txtColorBinding);
                makeTextBox.SetBinding(TextBox.TextProperty, txtMakeBinding);
            }
        }

        private void btnSaveOrder_Click(object sender, RoutedEventArgs e)
        {
            Order order = null;
            if (action == ActionState.New)
            {
                try
                {
                    Customer customer = (Customer)cmbCustomers.SelectedItem;
                    Inventory inventory = (Inventory)cmbInventory.SelectedItem;

                    order = new Order()
                    {
                        CustId = customer.CustId,
                        CarId = inventory.CarId
                    };
                    ctx.Orders.Add(order);
                    customerOrdersViewSource.View.Refresh();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                btnNewOrder.IsEnabled = true;
                btnEditOrder.IsEnabled = true;
                btnDeleteOrder.IsEnabled = true;

                btnSaveOrder.IsEnabled = false;
                btnCancelOrder.IsEnabled = false;
                btnPrevOrder.IsEnabled = true;
                btnNextOrder.IsEnabled = true;
                ordersDataGrid.IsEnabled = true;

                cmbCustomers.IsEnabled = false;
                cmbInventory.IsEnabled = false;
            }
            else if (action == ActionState.Edit)
            {
                try
                {
                    order = (Order)ordersDataGrid.SelectedItem;
                    Customer customer = (Customer)cmbCustomers.SelectedItem;
                    Inventory inventory = (Inventory)cmbInventory.SelectedItem;

                    order.CustId = customer.CustId;
                    order.CarId = inventory.CarId;

                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                customerOrdersViewSource.View.Refresh();
                customerOrdersViewSource.View.MoveCurrentTo(order);

                btnNewOrder.IsEnabled = true;
                btnEditOrder.IsEnabled = true;
                btnDeleteOrder.IsEnabled = true;

                btnSaveOrder.IsEnabled = false;
                btnCancelOrder.IsEnabled = false;
                ordersDataGrid.IsEnabled = true;
                btnPrevOrder.IsEnabled = true;
                btnNextOrder.IsEnabled = true;

                cmbCustomers.IsEnabled = false;
                cmbInventory.IsEnabled = false;

                cmbCustomers.SetBinding(ComboBox.ItemsSourceProperty, cmbCustomerBinding);
                cmbInventory.SetBinding(ComboBox.ItemsSourceProperty, cmbInventoryBinding);
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    order = (Order)ordersDataGrid.SelectedItem;
                    ctx.Orders.Remove(order);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                customerOrdersViewSource.View.Refresh();

                btnNewOrder.IsEnabled = true;
                btnEditOrder.IsEnabled = true;
                btnDeleteOrder.IsEnabled = true;

                btnSaveOrder.IsEnabled = false;
                btnCancelOrder.IsEnabled = false;
                ordersDataGrid.IsEnabled = true;
                btnPrevOrder.IsEnabled = true;
                btnNextOrder.IsEnabled = true;

                cmbCustomers.IsEnabled = false;
                cmbInventory.IsEnabled = false;

                cmbCustomers.SetBinding(ComboBox.ItemsSourceProperty, cmbCustomerBinding);
                cmbInventory.SetBinding(ComboBox.ItemsSourceProperty, cmbInventoryBinding);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNew.IsEnabled = true;
            btnEdit.IsEnabled = true;

            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            btnPrev.IsEnabled = true;
            btnNext.IsEnabled = true;
            customerDataGrid.IsEnabled = true;

            firstNameTextBox.IsEnabled = false;
            lastNameTextBox.IsEnabled = false;

            firstNameTextBox.SetBinding(TextBox.TextProperty, txtFirstNameBinding);
            lastNameTextBox.SetBinding(TextBox.TextProperty, txtLastNameBinding);
        }

        private void btnCancelCar_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNewCar.IsEnabled = true;
            btnEditCar.IsEnabled = true;

            btnSaveCar.IsEnabled = false;
            btnCancelCar.IsEnabled = false;
            btnPrevCar.IsEnabled = true;
            btnNextCar.IsEnabled = true;
            inventoryDataGrid.IsEnabled = true;

            colorTextBox.IsEnabled = false;
            makeTextBox.IsEnabled = false;

            colorTextBox.SetBinding(TextBox.TextProperty, txtColorBinding);
            makeTextBox.SetBinding(TextBox.TextProperty, txtMakeBinding);
        }

        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNewOrder.IsEnabled = true;
            btnEditOrder.IsEnabled = true;

            btnSaveOrder.IsEnabled = false;
            btnCancelOrder.IsEnabled = false;
            btnPrevOrder.IsEnabled = true;
            btnNextOrder.IsEnabled = true;
            ordersDataGrid.IsEnabled = true;

            cmbCustomers.IsEnabled = false;
            cmbInventory.IsEnabled = false;

            cmbCustomers.SetBinding(ComboBox.ItemsSourceProperty, cmbCustomerBinding);
            cmbInventory.SetBinding(ComboBox.ItemsSourceProperty, cmbInventoryBinding);
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            customerViewSource.View.MoveCurrentToPrevious();
        }

        private void btnPrevCar_Click(object sender, RoutedEventArgs e)
        {
            inventoryViewSource.View.MoveCurrentToPrevious();
        }

        private void btnPrevOrder_Click(object sender, RoutedEventArgs e)
        {
            customerOrdersViewSource.View.MoveCurrentToPrevious();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            customerViewSource.View.MoveCurrentToNext();
        }

        private void btnNextCar_Click(object sender, RoutedEventArgs e)
        {
            inventoryViewSource.View.MoveCurrentToNext();
        }

        private void btnNextOrder_Click(object sender, RoutedEventArgs e)
        {
            customerOrdersViewSource.View.MoveCurrentToNext();
        }
    }

    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
}
