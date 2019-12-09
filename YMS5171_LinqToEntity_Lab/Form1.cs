using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YMS5171_LinqToEntity_Lab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        NorthwindEntities db = new NorthwindEntities();

        private void btnSorgu1_Click(object sender, EventArgs e)
        {
            //Ürün fiyatları 50'ye eşit ve 50'den büyük olan ürünlerin Ürün Adı, Stock Mikatını, Pirim Fiyatını getirin ayrıca ürün fiyatlarına göre çoktan aza sıralayın

            dataGridView1.DataSource = db.Products.Where(x => x.UnitPrice >= 50).OrderByDescending(x => x.UnitPrice).Select(x => new
            {
                x.ProductID,
                x.ProductName,
                x.UnitsInStock,
                x.UnitPrice
            }).ToList();
        }

        private void btnSorgu2_Click(object sender, EventArgs e)
        {
            //CampanyName içeriisnde restaurant  kelimesi geçen müşterilerimi listele
            dataGridView1.DataSource = db.Customers.Where(x => x.CompanyName.Contains("restaurant")).ToList();
        }

        private void btnSorgu3_Click(object sender, EventArgs e)
        {
            //Siparişler tablosundan MUsteriSirketAdı, , CalışanAdiSoyadi, SiparişID, SİparişTarihi, KargoŞirketAdi getiren sorguyu yazınız
            dataGridView1.DataSource = db.Orders.Select(x => new
            {
                MusteriSirketAdi = x.Customer.CompanyName,
                CalisanAdiSoyadi = x.Employee.FirstName+ " "+x.Employee.LastName,
                SiparisID=x.OrderID,
                SiparisTarihi = x.OrderDate,
                KargoSirketAdi= x.Shipper.CompanyName
            }).ToList();
        }

        private void btnSorgu4_Click(object sender, EventArgs e)
        {
            //Çalışanların ID'si 2 ile 8 arasında olan çalışanları A-Z'ye olacak çekilde isimlerine göre sıralayınız

            dataGridView1.DataSource = db.Employees.Where(x => x.EmployeeID > 2 && x.EmployeeID < 8).OrderBy(x => x.FirstName).ToList();
        }

        private void btnSorgu5_Click(object sender, EventArgs e)
        {
            //Çalışanların yaşı 60 büyük olanlatın Adı, Soyadını,Ünvanını, DoğumTarihini getirin
            dataGridView1.DataSource = db.Employees.Where(x => SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now) > 60).Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.Title,
                x.BirthDate
            }).ToList();
        }

        private void btnSorgu6_Click(object sender, EventArgs e)
        {
            //1950 ve 1961 aralığında doğmuş çalışnaların ismi ve soyismini ünvanını ve doğum tarihini listeleyin
            dataGridView1.DataSource = db.Employees.Where(x => SqlFunctions.DatePart("Year", x.BirthDate) >= 1950 && SqlFunctions.DatePart("Year", x.BirthDate) <= 1961).Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.Title,
                x.BirthDate
            }).ToList();
        }

        private void btnSorgu7_Click(object sender, EventArgs e)
        {
            //Ünvanı Mr. olan ve yaşı 60'tan büyük olan çalışanları listeleyin
            dataGridView1.DataSource = db.Employees.Where(x => x.TitleOfCourtesy == "Mr." && SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now)> 60).ToList();
        }

        private void btnSorgu8_Click(object sender, EventArgs e)
        {
            //Ünvanı mr veya dr olanları listeleyin
            dataGridView1.DataSource = db.Employees.Where(x => x.TitleOfCourtesy == "Mr." || x.TitleOfCourtesy == "Dr.").ToList();
        }

        private void btnSorgu9_Click(object sender, EventArgs e)
        {
            //Doğum TArihi 1930 ile 1960 arasında lup USA'de çalışanları listeleyin
            dataGridView1.DataSource = db.Employees.Where(x => SqlFunctions.DatePart("Year", x.BirthDate) >= 1930 && SqlFunctions.DatePart("Year", x.BirthDate) <= 1960 && x.Country == "USA").ToList();
        }

        private void btnSorgu10_Click(object sender, EventArgs e)
        {
            //Çalışanalrın firstname,lastname,titleofcourtesy ve age ekrana getirilsin, yaşa göre azalan şekilde sırlanasın
            dataGridView1.DataSource = db.Employees.OrderByDescending(x=> SqlFunctions.DateDiff("Year",x.BirthDate,DateTime.Now)).Select(x=> new { 
                FirstName = x.FirstName,
                LastName = x.LastName,
                Title= x.Title,
                Age = SqlFunctions.DateDiff("Year",x.BirthDate,DateTime.Now)
            }).ToList();
        }
    }
}
