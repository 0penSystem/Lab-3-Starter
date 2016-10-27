using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nile.Data;

namespace Nile.Windows
{
    public partial class MainForm : Form
    {

        public Database Database
        {
            get; set;
        } = new Database();

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            bindingCustomers.DataSource = Database.Customers.GetAll();
        }

        private Customer getSelectedCustomer(DataGridView grid, int rowIndex)
        {
            if (rowIndex < 0 || rowIndex > grid.RowCount)
            {
                return null;
            }

            return grid.Rows[rowIndex].DataBoundItem as Customer;
        }

        private bool isLinkClicked(DataGridView grid, int columnIndex, DataGridViewLinkColumn column)
        {

            if (columnIndex < 0 || columnIndex > grid.ColumnCount)
            {
                return false;
            }

            return grid.Columns[columnIndex] == column;

        }

        private void miFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void miHelpAbout_Click(object sender, EventArgs e)
        {
            var dlg = new AboutForm();

            dlg.ShowDialog(this);
        }

        private void miCustomersAdd_Click(object sender, EventArgs e)
        {
            var form = new CustomerForm() { Database = Database };

            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                RefreshGrid();
            }

        }

        private void miProductsAdd_Click(object sender, EventArgs e)
        {
            var form = new ProductForm() { Database = Database };

            form.ShowDialog(this);
        }

        private void gridCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = sender as DataGridView;
            var customer = getSelectedCustomer(grid, e.RowIndex);
            if (customer == null)
            {
                return;
            }
            if (!isLinkClicked(grid, e.ColumnIndex, grid.Columns["colEdit"] as DataGridViewLinkColumn ))
            {
                return;
            }

            var form = new CustomerForm()
            {
                SelectedCustomer = customer
                , Database = Database
            };

            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                RefreshGrid();
            }


        }

        private void RefreshGrid()
        {
            gridCustomers.DataSource = null;
            gridCustomers.DataSource = Database.Customers.GetAll();
            gridCustomers.Refresh();
        }

        private void miProductsManage_Click(object sender, EventArgs e)
        {
            var form = new ManageProductsForm() { Database = Database };

            form.ShowDialog(this);
        }
    }
}
