using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace CarInventory
{
    public partial class Form1 : Form
    {
        List<Car> carsList = new List<Car>();

        public Form1()
        {
            InitializeComponent();
            loadDB();
        }
       
        private void addButton_Click(object sender, EventArgs e)
        {
            string year, make, colour, mileage;

            year = yearInput.Text;
            make = makeInput.Text;
            colour = colourInput.Text;
            mileage = mileageInput.Text;

            Car car = new Car(year, make, colour, mileage);

            carsList.Add(car);
             
            outputLabel.Text = "";


            for (int i = 0; i < carsList.Count; i++)
            {
                outputLabel.Text += carsList[i].year + " " + carsList[i].make + " " + carsList[i].colour + " " + carsList[i].mileage + "\n";
            }

            foreach(Car c in carsList)
            {
                outputLabel.Text += c.year + " "
                    + c.make + " "
                    + c.colour + " "
                    + c.mileage + "\n";
            }

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveDB();
            Application.Exit();
        }
         public void saveDB()
        {
            XmlWriter writer = XmlWriter.Create("Resources/CarXML.xml");

            writer.WriteStartElement("cars");

            foreach (Car c in carsList)
            {
                writer.WriteStartElement("car");

                // writer.WriteElementString("id", Convert.ToString(e.id));
                writer.WriteElementString("year", c.year);
                writer.WriteElementString("make", c.make);
                writer.WriteElementString("colour", c.colour);
                writer.WriteElementString("mileage", c.mileage);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.Close();
        }
        public void loadDB()
        {
            //int newID;
            string newYr, newMk, newCl, newMl;

            XmlReader reader = XmlReader.Create("Resources/CarXML.xml");

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                    // newID = Convert.ToInt16(reader.ReadString());

                    reader.ReadToNextSibling("year");
                    newYr = reader.ReadString();

                    reader.ReadToNextSibling("make");
                    newMk = reader.ReadString();

                    reader.ReadToNextSibling("colour");
                    newCl = reader.ReadString();

                    reader.ReadToNextSibling("mileage");
                    newMl = reader.ReadString();

                    Car c = new Car(newYr, newMk, newCl, newMl);
                    carsList.Add(c);
                }
            }

            reader.Close();
        }
    }
}
