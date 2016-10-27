using Nile.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nile.Windows
{
    public partial class CustomerForm : Form
    {

        public Database Database
        {
            get;
            set;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (SelectedCustomer != null)
            {
                txtFirstName.Text = SelectedCustomer.FirstName;
                txtID.Text = "" + SelectedCustomer.Id;
                txtLastName.Text = SelectedCustomer.LastName;
            }

            ValidateChildren();
        }

        public Customer SelectedCustomer
        {
            get; set;
        }
        public CustomerForm()
        {
            InitializeComponent();
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


            Customer customer = new Customer(SelectedCustomer?.Id ?? 0)
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
            };



            try
            {
                if (customer.Id == 0)
                {
                    Database.Customers.Add(customer);
                }
                else
                {
                    Database.Customers.Update(customer);
                }

                
                DialogResult = DialogResult.OK;
                SelectedCustomer = customer;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(owner: this, text: ex.Message, caption: "Save Failed", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }




        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            var txt = sender as TextBox;
            if (string.IsNullOrEmpty(txt.Text))
            {
                errors.SetError(txt, "First Name is required.");
                e.Cancel = true;
            }
            else
            {
                errors.SetError(txt, "");
                e.Cancel = false;
            }
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            var txt = sender as TextBox;
            if (string.IsNullOrEmpty(txt.Text))
            {
                errors.SetError(txt, "Last Name is required.");
                e.Cancel = true;
            }
            else
            {
                errors.SetError(txt, "");
                e.Cancel = false;
            }
        }
    }
}
