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
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        public Database Database
        {
            get;
            set;
        }

        public Product SelectedProduct
        {
            get;
            set;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                return;
            }

            var product = new Product(SelectedProduct?.Id ?? 0)
            {
                Name = txtName.Text,
                UnitPrice = decimal.Parse(txtPrice.Text),
                Discontinued = chkDiscontinued.Checked
            };

            try
            {
                if (product.Id == 0)
                {
                    Database.Products.Add(product);
                }
                else
                {
                    Database.Products.Update(product);
                }

                
                SelectedProduct = product;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch(Exception ex)
            {
               
                MessageBox.Show(owner: this, text: ex.Message, caption: "Save Failed", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }


        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            var txt = sender as TextBox;
            if (string.IsNullOrEmpty(txt.Text))
            {
                errors.SetError(txt, "Name is required.");
                e.Cancel = true;
            }
            else
            {
                errors.SetError(txt, "");
                e.Cancel = false;
            }
        }

        private void txtPrice_Validating(object sender, CancelEventArgs e)
        {
            var txt = sender as TextBox;
            decimal price;
            if (!Decimal.TryParse(txt.Text, out price) || price <= 0)
            {
                e.Cancel = true;
                errors.SetError(txt, "Price must be greater than zero.");
            }
            else
            {
                e.Cancel = false;
                errors.SetError(txt, "");
            }



        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (SelectedProduct != null)
            {
                txtID.Text = SelectedProduct.Id + "";
                txtName.Text = SelectedProduct.Name;
                txtPrice.Text = SelectedProduct.UnitPrice + "";
                chkDiscontinued.Checked = SelectedProduct.Discontinued;
            }


            ValidateChildren();
        }
    }
}
