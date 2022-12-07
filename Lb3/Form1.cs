using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lb3
{
    public partial class Form1 : Form
    {
        private List<Factory> _list = new List<Factory>(); //контейнер для зберігання заводів
        public Form1()
        {
            InitializeComponent();
        }
        private void Clear_Elements() //чистка полів
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox16.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox26.Clear();
            textBox13.Clear();
            checkBox2.Checked = false;
            textBox25.Clear();
            textBox17.Clear();
            textBox18.Clear();
            textBox19.Clear();
            textBox20.Clear();
            textBox21.Clear();
            textBox22.Clear();
            textBox23.Clear();
            textBox24.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox3.Text = "Цехи..";
            comboBox4.Text = "Працівники..";
        }  
        private void create_factory_Click(object sender, EventArgs e) 
        {
            string FactoryName = textBox1.Text;
            int MasterSalary;
            int EmployeeSalary;
            int MonthBenefitFactoryFromEmployee;
            int MonthBenefitFactoryFromMaster;
            try
            {
                if (FactoryName == "")
                    throw new Exception();
                foreach (Factory item in _list)
                {
                    if(item.FactoryName == FactoryName)
                    {
                        MessageBox.Show("Завод із такою назвою вже існує!", "Error", MessageBoxButtons.OK);
                        return;
                    }
                }
                MasterSalary = Convert.ToInt32(textBox3.Text);
                EmployeeSalary = Convert.ToInt32(textBox4.Text);
                MonthBenefitFactoryFromEmployee = Convert.ToInt32(textBox5.Text);
                MonthBenefitFactoryFromMaster = Convert.ToInt32(textBox6.Text);
                _list.Add(new Factory(FactoryName, MasterSalary, EmployeeSalary, MonthBenefitFactoryFromEmployee, MonthBenefitFactoryFromMaster));
                comboBox1.Items.Add(_list[_list.Count - 1]);
                MessageBox.Show("Завод успішно зареєстровано!\nОберіть його з переліку заводів для подальших дій", "Повідомлення", MessageBoxButtons.OK);
            }
            catch
            {
                MessageBox.Show("Невірно введені дані!");
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1) { return; }
            int index = comboBox1.SelectedIndex;
            textBox1.Text = _list[index].FactoryName;
            textBox3.Text = _list[index].MasterSalary.ToString();
            textBox4.Text = _list[index].EmployeeSalary.ToString();
            textBox5.Text = _list[index].MonthBenefitFromEmployee.ToString();
            textBox6.Text = _list[index].MonthBenefitFromMaster.ToString();
            textBox7.Text = _list[index].CountOfEmployees.ToString();
            textBox8.Text = _list[index].CountOfMasters.ToString();
            textBox16.Text = _list[index].CountOfDepartment.ToString();
            textBox2.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox26.Clear();
            textBox25.Clear();
            textBox17.Clear();
            textBox18.Clear();
            textBox19.Clear();
            textBox20.Clear();
            textBox21.Clear();
            textBox22.Clear();
            textBox23.Clear();
            textBox24.Clear();
            textBox25.Clear();
            comboBox3.Items.Clear();
            comboBox3.Text = "Цехи..";
            comboBox4.Text = "Працівники..";
            Factory item = _list[index];
            foreach (var it in item.dictionary)
            {
                comboBox3.Items.Add(it.Key.ID);
            }
            comboBox4.Items.Clear();
            foreach (var it in item.dictionary)
            {
                foreach (Person ps in it.Value)
                {
                    comboBox4.Items.Add(ps);
                }
            }
        }

        private void hire_employee_Click(object sender, EventArgs e) //найм робітників
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Створіть або оберіть завод з переліку заводів для дії", "Повідомлення", MessageBoxButtons.OK);
                return;
            }
            int index = comboBox1.SelectedIndex;
            string FirstName = textBox2.Text;
            string LastName = textBox11.Text;
            try
            {
                if (FirstName == "" | LastName == "")
                    throw new Exception();
                _list[index].HireEmployee(new Worker(FirstName, LastName, checkBox2.Checked));
            }
            catch
            {
                MessageBox.Show("Невірно введено ім'я або прізвище робітника!");
            }
            comboBox4.Items.Clear();
            foreach (var item in _list[index].dictionary)
            {
                foreach (Person ps in item.Value)
                {
                    comboBox4.Items.Add(ps);
                }
            }
            textBox7.Text = _list[index].CountOfEmployees.ToString();
            if (comboBox3.SelectedIndex != -1)
                comboBox3_SelectedIndexChanged(sender, e);
        }
        private void hire_master_Click(object sender, EventArgs e) //найм майстрів
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Створіть або оберіть завод з переліку заводів для дії", "Повідомлення", MessageBoxButtons.OK);
                return;
            }
            int index = comboBox1.SelectedIndex;
            string FirstName = textBox2.Text;
            string LastName = textBox11.Text;
            ushort CountOfCertificates;
            try
            {
                if (FirstName == "" | LastName == "")
                    throw new Exception();
                CountOfCertificates = Convert.ToUInt16(textBox14.Text);
                _list[index].HireMaster(new Master(FirstName, LastName, CountOfCertificates));
            }
            catch
            {
                MessageBox.Show("Невірно введено дані майстра!");
            }
            comboBox4.Items.Clear();
            foreach (var item in _list[index].dictionary)
            {
                foreach (Person ps in item.Value)
                {
                    comboBox4.Items.Add(ps);
                }
            }
            textBox8.Text = _list[index].CountOfMasters.ToString();
            textBox16.Text = _list[index].CountOfDepartment.ToString();
            if (_list[index].dictionary.Count > comboBox3.Items.Count)
            {
                comboBox3.Items.Clear();
                comboBox3.Text = "Інформацію оновлено";
                Factory item = _list[comboBox1.SelectedIndex];
                foreach (var it in item.dictionary)
                {
                    comboBox3.Items.Add(it.Key.ID);
                }
                textBox17.Clear();
                textBox18.Clear();
                textBox19.Clear();
                textBox20.Clear();
                textBox21.Clear();
                textBox22.Clear();
                textBox23.Clear();
                textBox24.Clear();
                return;
            }
            if (comboBox3.SelectedIndex != -1)
                comboBox3_SelectedIndexChanged(sender, e);
        }
        private void dismiss_employee_Click(object sender, EventArgs e) //звільнення робітників
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Створіть або оберіть завод з переліку заводів для дії", "Повідомлення", MessageBoxButtons.OK);
                return;
            }
            int index = comboBox1.SelectedIndex;
            _list[index].DismissEmployee(textBox25.Text);
            comboBox4.Items.Clear();
            foreach (var item in _list[index].dictionary)
            {
                foreach (Person ps in item.Value)
                {
                    comboBox4.Items.Add(ps);
                }
            }
            textBox7.Text = _list[index].CountOfEmployees.ToString();
            textBox8.Text = _list[index].CountOfMasters.ToString();
            textBox16.Text = _list[index].CountOfDepartment.ToString();
            textBox2.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox26.Clear();
            textBox25.Clear();
            comboBox4.Text = "Працівники..";
            if (_list[index].dictionary.Count < comboBox3.Items.Count)
            {
                comboBox3.Items.Clear();
                comboBox3.Text = "Інформацію оновлено";
                Factory item = _list[comboBox1.SelectedIndex];
                foreach (var it in item.dictionary)
                {
                    comboBox3.Items.Add(it.Key.ID);
                }
                textBox17.Clear();
                textBox18.Clear();
                textBox19.Clear();
                textBox20.Clear();
                textBox21.Clear();
                textBox22.Clear();
                textBox23.Clear();
                textBox24.Clear();
                return;
            }
            if (comboBox3.SelectedIndex != -1)
                comboBox3_SelectedIndexChanged(sender, e);
        }

        private void factory_copy_Click(object sender, EventArgs e) //копіювання заводу
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Створіть або оберіть завод з переліку заводів для дії", "Повідомлення", MessageBoxButtons.OK);
                return;
            }
            int index = comboBox1.SelectedIndex;
            _list.Add(new Factory(_list[index]));
            comboBox1.Items.Add(_list[index]);
        }
        
        private void factory_comparison_Click(object sender, EventArgs e) //сортування заводів
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Створіть або оберіть завод з переліку заводів для дії", "Повідомлення", MessageBoxButtons.OK);
                return;
            }
            int index1 = comboBox1.SelectedIndex;
            string index2 = "";
            if (comboBox2.SelectedIndex != -1)
                index2 = (string)comboBox2.SelectedItem;
            switch (index2)
            {
                case "За кількістю майстрів":
                    {
                        Factory.SortFactoryByMasters ms = new Factory.SortFactoryByMasters();
                        _list.Sort(ms);
                        MessageBox.Show("Список заводів було відсортовано за кількістю майстрів");
                        comboBox1.SelectedIndex = -1;
                        Clear_Elements();
                        break;
                    }
                case "За кількістю робітників":
                    {
                        Factory.SortFactoryByEmployees ps = new Factory.SortFactoryByEmployees();
                        _list.Sort(ps);
                        MessageBox.Show("Список заводів було відсортовано за кількістю робітників");
                        comboBox1.SelectedIndex = -1;
                        Clear_Elements();
                        break;
                    }
                case "За кількістю цехів":
                    {
                        Factory.SortFactoryByDepartments dp = new Factory.SortFactoryByDepartments();
                        _list.Sort(dp);
                        MessageBox.Show("Список заводів було відсортовано за кількістю цехів");
                        comboBox1.SelectedIndex = -1;
                        Clear_Elements();
                        break;
                    }
                default:
                    {
                        _list.Sort();
                        MessageBox.Show("Список заводів було відсортовано за назвою,\nбо ви не обрали жодного зі способів сортування!", "Відбулося сортування за замовчуванням", MessageBoxButtons.OK);
                        comboBox1.SelectedIndex = -1;
                        Clear_Elements();
                        break;
                    }
            }
        }

        private void Form1_Load(object sender, EventArgs e) //встановка курсора в textbox1
        {
            textBox1.Select();
            textBox1.ScrollToCaret();
            string[] arr = { "За кількістю майстрів", "За кількістю робітників", "За кількістю цехів" };
            comboBox2.Items.AddRange(arr);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            if (comboBox4.SelectedItem is Worker wk)
            {
                textBox14.Clear();
                textBox2.Text = wk.FirstName;
                textBox11.Text = wk.LastName;
                textBox12.Text = wk.IndividualTaxNumber;
                textBox13.Text = wk.ID_Workshop;
                textBox15.Text = Worker.Salary.ToString();
                if (wk.HavingHigherTechnikalEducation)
                    textBox26.Text = "Так";
                else
                    textBox26.Text = "Ні";
            }
            if (comboBox4.SelectedItem is Master ms)
            {
                textBox26.Clear();
                checkBox2.Checked = false;
                textBox2.Text = ms.FirstName;
                textBox11.Text = ms.LastName;
                textBox12.Text = ms.IndividualTaxNumber;
                textBox13.Text = ms.ID_Workshop;
                textBox15.Text = Master.Salary.ToString();
                textBox14.Text = ms.CountOfCertificates.ToString();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            foreach (var wrk in _list[index].dictionary)
            {
                if (wrk.Key.ID == (Guid)comboBox3.SelectedItem)
                {
                    textBox17.Text = wrk.Key.ID.ToString();
                    textBox18.Text = wrk.Key.CountOfWorkPlaces.ToString();
                    textBox19.Text = Workshop.MaxCapacity.ToString();
                    textBox20.Text = Workshop.MaxCapacityOfMasters.ToString();
                    textBox21.Text = Workshop.MaxCapacityOfEmployees.ToString();
                    textBox22.Text = wrk.Key.CountOfMasters.ToString();
                    textBox23.Text = wrk.Key.CountOfEmployees.ToString();
                    textBox24.Text = wrk.Key.PriceOnDetail.ToString();
                    break;
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if(_list.Count == 0)
            {
                MessageBox.Show("Жодного заводу ще не зареєстровано,\nтому нічого не було збережено!", "Error", MessageBoxButtons.OK);
                return;
            }
            StringBuilder sb = new StringBuilder();
            foreach (Factory factory in _list)
            {
                string dir = $"C:\\FactoryData\\{factory.FactoryName}";
                string dir2 = $"C:\\FactoryData\\{factory.FactoryName}\\data";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                    Directory.CreateDirectory(dir2);
                }
                if (!Directory.Exists(dir2))
                    Directory.CreateDirectory(dir2);
                string path = $"C:\\FactoryData\\{factory.FactoryName}\\{factory.FactoryName}.txt";
                string pathMaster = $"C:\\FactoryData\\{factory.FactoryName}\\data\\Masters.txt";
                string pathWorker = $"C:\\FactoryData\\{factory.FactoryName}\\data\\Workers.txt";
                using (FileStream file = new FileStream(path,FileMode.Create))
                {
                    using (StreamWriter stream = new StreamWriter(file))
                    {
                        sb.Clear();
                        sb.AppendLine(factory.CountOfCreatedGetails.ToString());
                        sb.AppendLine(factory.CountOfDepartment.ToString());
                        sb.AppendLine(factory.CountOfEmployees.ToString());
                        sb.AppendLine(factory.CountOfMasters.ToString());
                        sb.AppendLine(factory.EmployeeSalary.ToString());
                        sb.AppendLine(factory.MasterSalary.ToString());
                        sb.AppendLine(factory.FactoryName);
                        sb.AppendLine(factory.MonthBenefitFromEmployee.ToString());
                        sb.AppendLine(factory.MonthBenefitFromMaster.ToString());
                        foreach (var item in factory.dictionary)
                        {
                            sb.AppendLine(item.Key.ID.ToString());
                            sb.AppendLine(item.Key.CountOfEmployees.ToString());
                            sb.AppendLine(item.Key.CountOfMasters.ToString());
                            sb.AppendLine(item.Key.CountOfWorkPlaces.ToString());
                        }
                        stream.WriteLine(sb.ToString());
                    }
                }
                using (FileStream file = new FileStream(pathMaster, FileMode.Create))
                {
                    using (StreamWriter stream = new StreamWriter(file))
                    {
                        sb.Clear();
                        foreach (var item in factory.dictionary)
                        {
                            foreach (var m in item.Value)
                            {
                                if (m is Master master)
                                {
                                    sb.AppendLine(master.FirstName);
                                    sb.AppendLine(master.LastName);
                                    sb.AppendLine(master.IndividualTaxNumber);
                                    sb.AppendLine(master.ID_Workshop);
                                    sb.AppendLine(master.CountOfCertificates.ToString());
                                }
                            }
                        }
                        stream.WriteLine(sb.ToString());
                    }
                }
                using (FileStream file = new FileStream(pathWorker, FileMode.Create))
                {
                    using (StreamWriter stream = new StreamWriter(file))
                    {
                        sb.Clear();
                        foreach (var item in factory.dictionary)
                        {
                            foreach (var w in item.Value)
                            {
                                if (w is Worker worker)
                                {
                                    sb.AppendLine(worker.FirstName);
                                    sb.AppendLine(worker.LastName);
                                    sb.AppendLine(worker.IndividualTaxNumber);
                                    sb.AppendLine(worker.ID_Workshop);
                                    sb.AppendLine(worker.HavingHigherTechnikalEducation.ToString());
                                }
                            }
                        }
                        stream.WriteLine(sb.ToString());
                    }
                }
            }
            MessageBox.Show("Усі заводи збережено", "Повідомлення", MessageBoxButtons.OK);
        }
        private void Open_File_Click(object sender, EventArgs e)
        {
            List<Person> ps = new List<Person>();
            List<Workshop> work = new List<Workshop>();
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            if ((Path.GetFileName(filename) == "Workers.txt") | (Path.GetFileName(filename) == "Masters.txt"))
            {
                MessageBox.Show("Вибрано некоректний файл", "Error", MessageBoxButtons.OK);
                return;
            }
            string path = Path.GetDirectoryName(filename);
            string pathMaster = Path.Combine(path, "data", "Masters.txt");
            string pathWorker = Path.Combine(path, "data", "Workers.txt");
            if (!File.Exists(pathMaster) | !File.Exists(pathWorker))
            {
                MessageBox.Show("Вибрано некоректний файл", "Error", MessageBoxButtons.OK);
                return;
            }
            using (StreamReader stream = new StreamReader(pathMaster, Encoding.UTF8))
            {
                string FirstName;
                while((FirstName = stream.ReadLine()) != "")
                {
                    try
                    {
                        string LastName = stream.ReadLine();
                        string TaxNumber = stream.ReadLine();
                        string id = stream.ReadLine();
                        ushort certificates = Convert.ToUInt16(stream.ReadLine());
                        Master m = new Master(FirstName, LastName, certificates);
                        m.IndividualTaxNumber = TaxNumber;
                        m.ID_Workshop = id;
                        ps.Add(m);
                    }
                    catch
                    {
                        MessageBox.Show("Файл не містить коректні дані!", "Error", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
            using (StreamReader stream = new StreamReader(pathWorker, Encoding.UTF8))
            {
                string FirstName;
                while ((FirstName = stream.ReadLine()) != "")
                {
                    try
                    {
                        string LastName = stream.ReadLine();
                        string TaxNumber = stream.ReadLine();
                        string id = stream.ReadLine();
                        bool education = Convert.ToBoolean(stream.ReadLine());
                        Worker w = new Worker(FirstName, LastName, education);
                        w.IndividualTaxNumber = TaxNumber;
                        w.ID_Workshop = id;
                        ps.Add(w);
                    }
                    catch
                    {
                        MessageBox.Show("Файл не містить коректні дані!", "Error", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
            using (StreamReader stream = new StreamReader(filename, Encoding.UTF8))
            {
                string temp;
                if ((temp = stream.ReadLine()) != "")
                {
                    try
                    {
                        int CountOfCreatedDetails = Convert.ToInt32(temp);
                        int CountOfDepartments = Convert.ToInt32(stream.ReadLine());
                        int CountOfEmployee = Convert.ToInt32(stream.ReadLine());
                        int CountOfMasters = Convert.ToInt32(stream.ReadLine());
                        int EmployeeSalary = Convert.ToInt32(stream.ReadLine());
                        int MasterSalary = Convert.ToInt32(stream.ReadLine());
                        string FactoryName = stream.ReadLine();
                        foreach (Factory item in _list)
                        {
                            if(FactoryName == item.FactoryName)
                            {
                                MessageBox.Show("Завод із такою назвою відкритий у програмі!", "Error", MessageBoxButtons.OK);
                                return;
                            }
                        }
                        int MonthBenefitFromEmployee = Convert.ToInt32(stream.ReadLine());
                        int MonthBenefitFromMaster = Convert.ToInt32(stream.ReadLine());
                        string id;
                        while ((id = stream.ReadLine()) != "")
                        {
                            Workshop w = new Workshop(id);
                            w.CountOfEmployees = Convert.ToInt32(stream.ReadLine());
                            w.CountOfMasters = Convert.ToInt32(stream.ReadLine());
                            w.CountOfWorkPlaces = Convert.ToInt32(stream.ReadLine());
                            work.Add(w);
                        }
                        Factory f = new Factory(FactoryName, work, ps, MasterSalary, EmployeeSalary, MonthBenefitFromEmployee, MonthBenefitFromMaster, CountOfCreatedDetails, CountOfMasters, CountOfEmployee, CountOfDepartments);
                        _list.Add(f);
                        comboBox1.Items.Add(f);
                        MessageBox.Show("Вибраний завод успішно загружено!\nДля роботи з ним оберіть його з переліку заводів", "Повідомлення", MessageBoxButtons.OK);
                    }
                    catch
                    {
                        MessageBox.Show("Файл не містить коректні дані!", "Error",MessageBoxButtons.OK);
                        return;
                    }
                }
            }
        }
        private void clear_field_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            Clear_Elements();
        }
    }
}
