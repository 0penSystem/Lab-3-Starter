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

        /// <summary>
        /// A Reference to the Nile Database.
        /// </summary>
        public Database Database
        {
            get;
            set;
        }

        /// <summary>
        /// The form's selected customer. Set before the form loads to edit a specific customer.
        /// </summary>
        public Customer SelectedCustomer
        {
            get; set;
        }

        /// <summary>
        /// Form for creating and editing customers.
        /// </summary>
        public CustomerForm()
        {
            InitializeComponent();
        }

        #region Event Handlers

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
        #endregion


    }
}
