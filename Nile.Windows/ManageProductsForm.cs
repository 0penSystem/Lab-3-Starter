using Nile.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nile.Windows
{
    public partial class ManageProductsForm : Form
    {
        public Database Database
        {
            get;
            set;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            bindingProducts.DataSource = Database.Products.GetAll();
        }

        public ManageProductsForm()
        {
            InitializeComponent();
        }


        private void RefreshGrid()
        {
            gridProducts.DataSource = null;
            gridProducts.DataSource = bindingProducts;
            gridProducts.Refresh();
        }

        private Product getSelectedProduct(DataGridView grid, int rowIndex)
        {
            if (rowIndex < 0 || rowIndex > grid.RowCount)
            {
                return null;
            }

            return grid.Rows[rowIndex].DataBoundItem as Product;
        }

        private bool isLinkClicked(DataGridView grid, int columnIndex, DataGridViewLinkColumn column)
        {

            if (columnIndex < 0 || columnIndex > grid.ColumnCount)
            {
                return false;
            }

            return grid.Columns[columnIndex] == column;

        }

        private void gridProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = sender as DataGridView;
            var product = getSelectedProduct(grid, e.RowIndex);
            if (product == null)
            {
                return;
            }
            if (!isLinkClicked(grid, e.ColumnIndex, grid.Columns["colEdit"] as DataGridViewLinkColumn))
            {
                return;
            }

            var form = new ProductForm()
            {
                Database = Database,
                SelectedProduct = product
            };

            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                RefreshGrid();
            }

        }
    }
}
