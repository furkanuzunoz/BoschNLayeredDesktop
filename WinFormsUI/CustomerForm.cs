using AutoMapper;
using Business.Abstracts;
using Business.Adapters;
using Business.Adapters.Abstracts;
using Business.Adapters.Concretes;
using Business.BusinessRules;
using Business.Concretes;
using Business.Profiles;
using Business.Request.Customer;
using Business.Response.Customer;
using DataAccess.Abstracts;
using DataAccess.Concretes.Adonet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsUI
{
    public partial class CustomerForm : Form
    {
        private ICustomerService _customerService;
        public string fordelete;
        public CustomerForm()
        {
            ICustomerDal _customerDal = new AdoCustomerDal();
            AutoMapperProfiles autoMapperProfiles = new AutoMapperProfiles();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(autoMapperProfiles);
            });
            IMapper mapper = new Mapper(mapperConfig);
            IIdentityAdapter identityAdapter = new KPSIdentityAdapter();
            _customerService = new 
                CustomerManager(_customerDal, 
                new CustomerBusinessRules(_customerDal,identityAdapter),
                mapper);
            InitializeComponent();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            //SqlConnection sqlConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDb;Initial Catalog=Northwind;");
            //SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from Customers", sqlConnection);
            //DataSet dataSet = new DataSet();
            //sqlConnection.Open();
            //dataAdapter.Fill(dataSet, "Customers");
            //sqlConnection.Close();
            //customersDataGridView.DataSource = dataSet;
            //customersDataGridView.DataMember = "Customers";


            // Abstraction
            // Adapters
            getAllCustomers();
        }


        private void getAllCustomers()
        {
            customersDataGridView.DataSource = _customerService.GetAll();
        }

        //ListCustomerResponse için, tablodaki tüm alanları dahil et.
        //Ekleme, güncelleme için validatorler ekle.

        //Silmek için buton, eklemek ve update etmek için ilgili grupları ekle.

        private void customersDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (customersDataGridView.SelectedRows.Count <= 0) return;
            var firstlySelectedRow = customersDataGridView.SelectedRows[0];
            Console.WriteLine(firstlySelectedRow.Cells["CustomerID"].Value);
            Console.WriteLine("Selection Changed");
            updateCompanyNameTb.Text = firstlySelectedRow.Cells["CompanyName"].Value?.ToString();
            updateCustomerIdTb.Text = firstlySelectedRow.Cells["CustomerId"].Value?.ToString();
            fordelete= firstlySelectedRow.Cells["CustomerId"].Value?.ToString();
        }

        private void addCustomerBtn_Click(object sender, EventArgs e)
        {
            CreateCustomerRequest createCustomerRequest = new CreateCustomerRequest()
            {
                Name = nameTb.Text,
                Surname = surnameTb.Text,
                BirthDate = birthDateDp.Value,
                IdentityNumber = identityNumberTb.Text,
                CompanyName = companyNameTb.Text
            };
            _customerService.Add(createCustomerRequest);
            getAllCustomers();
        }

        private void updateCustomerBtn_Click(object sender, EventArgs e)
        {
            UpdateCustomerRequest updateCustomerRequest = new UpdateCustomerRequest()
            {
                CustomerID= updateCustomerIdTb.Text,
                CompanyName = updateCompanyNameTb.Text
            };
            _customerService.Update(updateCustomerRequest);
            getAllCustomers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _customerService.Delete(fordelete);
            getAllCustomers();
        }
    }
}
